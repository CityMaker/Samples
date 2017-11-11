using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace Authorization
{
    class RestartService
    {
        public void Restart(string appName)
        {
            Thread restart = new Thread(new ParameterizedThreadStart(Run));
            object obj = appName;
            Thread.Sleep(2000);
            restart.Start(obj);
        }
        private void Run(Object obj)
        {
            Process ps = new Process();
            ps.StartInfo.FileName = obj.ToString();
            ps.Start();
        }
    }
}
