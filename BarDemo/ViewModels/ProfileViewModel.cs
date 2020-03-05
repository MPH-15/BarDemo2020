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
using Xamarin.Essentials;


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


        int _age;
        public int Age
        {
            //get { return _age; }
            get => Preferences.Get("Age", 21);

            set
            {
                //_age = value;
                Preferences.Set("Age", value);

                OnPropertyChanged();
                Debug.WriteLine($"Age Property is set to: {_age}");
                // ToDo: Persist Age 
            }
        }

        int _radius;
        public int Radius
        {
            get { return _radius; }
            set
            {
                _radius = value;
                MessagingCenter.Send<ProfileViewModel, int>(this, "RadiusSliderValue", Radius);
                OnPropertyChanged();
            }
        }




        public ProfileViewModel(INavService navService) : base(navService)
        {



            MessagingCenter.Subscribe<TabViewModel, FBUser>(this, "UserData", async (sender, arg) =>
            {
                Debug.WriteLine("ProfileViewModel: In Messaging Center");
                Debug.WriteLine("Name: " + arg.Name);


                //if (arg.DoB == null)
                //{
                //    Age = 21;
                //}
                //else
                //{
                //    Age = DateTime.Today.Subtract(Convert.ToDateTime(arg.DoB)).Days / 365;
                //}


                //Debug.WriteLine("Gender" + arg.Gender);
                //Debug.WriteLine("Age Range Min: " + arg.age_range.min);
                //Debug.WriteLine("Age Range Max: " + arg.age_range.max);

                FB_User = arg;


                if (String.IsNullOrEmpty(FB_User.DoB))
                {
                    _age = 21;
                }
                else
                {
                    _age = DateTime.Today.Subtract(Convert.ToDateTime(FB_User.DoB)).Days / 365;
                }


            });

        }

        public override async Task Init()
        {

        }

    }
}