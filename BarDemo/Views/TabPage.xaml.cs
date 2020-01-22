using System;
using System.Collections.Generic;
using BarDemo.Services;
using BarDemo.ViewModels;
using System.Diagnostics;
using Xamarin.Forms;
using System.ComponentModel;
using BarDemo.Models;

using Xamarin.Forms;

namespace BarDemo.Views
{
    public partial class TabPage : TabbedPage
    {

        TabViewModel _vm
        {
            get { return BindingContext as TabViewModel; }
        }

        public TabPage()
        {
            InitializeComponent();
            BindingContext = new TabViewModel(DependencyService.Get<INavService>());
        
        }

        public void printUser()
        {
            string name = _vm.FB_User.Name;
            Debug.WriteLine("Tab Page : UserName: " + name);

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
