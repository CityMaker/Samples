using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CityMakerExplorer.AddIn.Example
{
    public class ExampleProcess
    {
        private static ExampleProcess _process;
        public static ExampleProcess Instance()
        {
            if (_process == null)
                _process = new ExampleProcess();

            return _process;
        }

        private bool bHasLogin = false;

        public bool HasLogin
        {
            get { return bHasLogin; }
            set { bHasLogin = value; }
        }

    }
}
