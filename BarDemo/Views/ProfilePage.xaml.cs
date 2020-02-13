﻿using System;
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




        public ProfilePage()
        {
            InitializeComponent();
            BindingContext = new ProfileViewModel(DependencyService.Get<INavService>());

        }



        void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs args)
        {


        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

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


        void OnSliderValueChanged(object sender, ValueChangedEventArgs args)
        {
            double value = args.NewValue;
            int radius = Convert.ToInt32(value);
            Debug.WriteLine("ProfilePage: The radius value is {0}", radius);

            // _blvm.Radius = radius;
            //_blvm.Radius = radius;
        }
    


    }
}
