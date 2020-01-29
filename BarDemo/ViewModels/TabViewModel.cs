using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Xamarin.Auth;
using Xamarin.Essentials;
using Newtonsoft.Json;
using System.Net.Http;
using System.Json;
using Xamarin.Forms;
using BarDemo.Views;
using BarDemo.Services;
using BarDemo.Models;
using System.Threading.Tasks;
using Xamarin.Auth;

namespace BarDemo.ViewModels
{
    public class TabViewModel : BaseViewModel<FBUser>
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

        public TabViewModel(INavService navService) : base(navService)
        {

        }

        public override async Task Init()
        {

        }

        public override async Task Init(FBUser fb_user)
        {
            FB_User = fb_user;
            MessagingCenter.Send<TabViewModel, FBUser>(this, "UserData", FB_User);

        }


    }
}
