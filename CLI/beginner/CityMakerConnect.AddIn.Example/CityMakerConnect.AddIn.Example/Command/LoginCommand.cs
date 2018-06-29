using System;
using CityMakerExplorer.AddIn.Core;
using CityMakerExplorer.WorkSpace;
using System.Windows.Forms;

namespace CityMakerExplorer.AddIn.Example
{
    class LoginCommand : AbstractCommand
    {
        public override void RestoreEnv()
        {
                
        }
        public override void Run(object sender, EventArgs e)
        {
            CommandManager.Push(this);
            LoginForm form = new LoginForm();
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                MessageBox.Show("");
            }
        }
    }
}
