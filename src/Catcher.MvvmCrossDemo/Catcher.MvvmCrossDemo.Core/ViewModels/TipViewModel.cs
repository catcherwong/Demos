using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross;
using MvvmCross.Core.ViewModels;
using Catcher.MvvmCrossDemo.Core.Services;

namespace Catcher.MvvmCrossDemo.Core.ViewModels
{
    public class TipViewModel : MvxViewModel
    {
        private ICalculation _calculation;
        public TipViewModel(ICalculation calculation)
        {
            this._calculation = calculation;
        }

        private int _generosity;
        public int Generosity
        {
            get
            {
                return _generosity;
            }

            set
            {
                _generosity = value;
                RaisePropertyChanged(()=>Generosity);
                Recalcuate();
            }
        }

        private double _subTotal;
        public double SubTotal
        {
            get
            {
                return _subTotal;
            }

            set
            {
                _subTotal = value;
                RaisePropertyChanged(()=>SubTotal);
                Recalcuate();
            }
        }

        private double _tip;
        public double Tip
        {
            get { return _tip; }
            set
            {
                _tip = value;
                RaisePropertyChanged(() => Tip);
            }
        }

        public override void Start()
        {
            this._subTotal = 100;
            this._generosity = 10;
            Recalcuate();
            base.Start();
        }

        private void Recalcuate()
        {
            Tip = _calculation.TipAmount(SubTotal, Generosity);
        }

    }
}
