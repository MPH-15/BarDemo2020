using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using BarDemo.Views;
using BarDemo.Services;
using BarDemo.Models;
using System.Diagnostics;
using Xamarin.Forms;

namespace BarDemo.ViewModels
{
    class ProfileViewModel : BaseViewModel
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
            Debug.WriteLine("ProfileViewModel: Initialize");
            MessagingCenter.Subscribe<TabViewModel, FBUser>(this, "UserData", async (sender, arg) =>
            {
                Debug.WriteLine("ProfileViewModel: In Messaging Center");
                Debug.WriteLine("Name: " + arg.Name);
                FB_User = arg;
            });

        }

        public override async Task Init()
        {
            //Debug.WriteLine("ProfileViewModel: Initialize");
            //MessagingCenter.Subscribe<FBUser>(this, "UserData", (user_data) =>
            //{
            //    FB_User = user_data;
            //    Debug.WriteLine("ProfileViewModel: " + user_data.Name);
            //});

        }

        //public override async Task Init(FBUser fbUser)
        //{
        //    //FB_User = fbUser;
        //    //Debug.WriteLine("ProfileViewModel: UserName: " + FB_User.Name);

        //}

    }
}