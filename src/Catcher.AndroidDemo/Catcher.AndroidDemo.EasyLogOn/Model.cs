using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Catcher.AndroidDemo.EasyLogOn
{
    public class Model
    {        
        public string CategoryName { get; set; }
        public string MoneyAbout { get; set; }
        public string MoneyType { get; set; }
        public string MoneyDate { get; set; }
        public string MoneyValue { get; set; }        
    }
}