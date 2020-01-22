using System;
using System.Collections.Generic;
using BarDemo.Services;
using BarDemo.ViewModels;
using System.Diagnostics;
using Xamarin.Forms;
using System.ComponentModel;
using BarDemo.Models;
using BarDemo.Views;

namespace BarDemo.Views
{
    public partial class ProfilePage : ContentPage
    {

        ProfileViewModel _vm
        {
            get { return BindingContext as ProfileViewModel; }
        }

        TabViewModel _tvm
        {
            get { return BindingContext as TabViewModel; }
        }

        public ProfilePage()
        {
            InitializeComponent();
            BindingContext = new ProfileViewModel(DependencyService.Get<INavService>());

        }

        public void printUser()
        {
            string name = _tvm.FB_User.Name;
            Debug.WriteLine("Profile Page : UserName: " + name);

        }

        void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs args)
        {

            if (args.PropertyName == nameof(TabViewModel.FB_User))
            {
                printUser();
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            printUser();

            if (_vm != null)
            {
                _vm.PropertyChanged += OnViewModelPropertyChanged;
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            if (_vm != null)
            {
                _vm.PropertyChanged -= OnViewModelPropertyChanged;
            }
        }


        
    }
}
