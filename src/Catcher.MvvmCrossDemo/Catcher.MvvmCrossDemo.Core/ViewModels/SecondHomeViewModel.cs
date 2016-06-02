using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;

namespace Catcher.MvvmCrossDemo.Core.ViewModels
{
    public class SecondHomeViewModel : MvxViewModel
    {
        public SecondHomeViewModel() { }

        private string _hello;
        public string Hello
        {
            get { return _hello; }
            set { _hello = value; RaisePropertyChanged(()=>Hello); }
        }
    }
}
