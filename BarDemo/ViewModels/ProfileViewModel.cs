using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using BarDemo.Views;
using BarDemo.Services;
using BarDemo.Models;
using System.Diagnostics;

namespace BarDemo.ViewModels
{
    class ProfileViewModel : BaseViewModel<FBUser>
    {
        FBUser _fbUser;
        public FBUser FB_User
        {
            get { return _fbUser; }
            set
            {
                _fbUser = value;
                OnPropertyChanged();
            }
        }



        public ProfileViewModel(INavService navService) : base(navService)
        {

        }

        public override async Task Init()
        {



        }

        public override async Task Init(FBUser fbUser)
        {
            FB_User = fbUser;
            Debug.WriteLine("ProfileViewModel: UserName: " + FB_User.Name);
        }

    }
}