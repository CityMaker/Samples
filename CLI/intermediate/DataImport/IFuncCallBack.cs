using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Gvitech.CityMaker.FdeCore;

namespace DataImport
{
    public delegate bool ReplicatingHandler(IFeatureProgress Progress);

    [ComVisibleAttribute(true)]
    public class IFuncCallBack
    {
        private ReplicatingHandler _RepHandler;
        public event ReplicatingHandler Replicating
        {
            add { _RepHandler += new ReplicatingHandler(value); }
            remove { _RepHandler -= new ReplicatingHandler(value); }
        }

        public bool OnReplicating(IFeatureProgress Progress)
        {
            if (_RepHandler != null)
                return _RepHandler(Progress);
            else
                return false;
        }
        public bool OnProcessing(IFeatureProgress Progress)
        {
            if (_RepHandler != null)
                return _RepHandler(Progress);
            else
                return false;
        }
    }
}
