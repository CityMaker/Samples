using System;
using CityMakerExplorer.AddIn.Core;
using CityMakerExplorer.WorkSpace;
using System.Windows.Forms;

namespace CityMakerExplorer.AddIn.Example
{
    class TestCommand : AbstractCommand
    {
        public override void RestoreEnv()
        {
                
        }
        public override void Run(object sender, EventArgs e)
        {
            CommandManager.Push(this);
            MessageBox.Show("test");
        }
    }
}
