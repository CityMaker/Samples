using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace Authorization
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool runOne = false;
            using (Mutex mutex = new Mutex(true, System.Reflection.Assembly.GetExecutingAssembly().FullName, out runOne))
            {
                try
                {
                    if (runOne)
                    {
                        //正常代码
                        MainForm frm = new MainForm();
                        int iRet = frm.CheckNetworkLicense();
                        if (iRet > 0)
                        {
                            frm.OnInitialize();
                            Application.Run(frm);
                        }
                        else if(iRet == 0)
                        {
                            Application.ExitThread();
                            RestartService rs = new RestartService();
                            rs.Restart(Application.ExecutablePath);
                        }
                        else if(iRet < 0)
                        {
                            System.Threading.Thread.Sleep(1000);
                            System.Environment.Exit(1);
                        }
                    }
                    else
                    {
                        MessageBox.Show("应用程序已经在运行中。");
                        return;
                    }
                }
                catch (System.Runtime.InteropServices.COMException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
