using MvvmCross.Core.ViewModels;

namespace Catcher.MvvmCrossDemo.Core.ViewModels
{
    public class SecondViewModel : MvxViewModel
    {
        public SecondViewModel()
        {
        }

        //private string _userName;
        //public string UserName
        //{
        //    get
        //    {
        //        return _userName;
        //    }
        //    set
        //    {
        //        _userName = value;
        //        RaisePropertyChanged(() => UserName);
        //    }
        //}

        //private string _userPassword;

        //public string UserPassword
        //{
        //    get
        //    {
        //        return _userPassword;
        //    }

        //    set
        //    {
        //        _userPassword = value;
        //        RaisePropertyChanged(()=>UserPassword);
        //    }
        //}

        public MvxCommand LoginCommand
        {
            get { return new MvxCommand(()=>ShowViewModel<MainViewModel>()); }
        }

        //private IMvxCommand _loginCommand;
        //public virtual IMvxCommand LoginCommand
        //{
        //    get
        //    {
                
        //        return _loginCommand;
        //    }
        //}

    }
}
