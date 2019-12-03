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
using Xamarin.Forms.Maps;



namespace BarDemo.ViewModels
{
    public class MapViewModel : BaseViewModel<Business>
    {
        Business _bar;
        public Business Bar
        {
            get { return _bar; }
            set
            {
                _bar = value;
                OnPropertyChanged();
            }
        }


        public MapViewModel(INavService navService) : base(navService)
        {

        }

        public override async Task Init()
        {

        }

        public override async Task Init(Business biz)
        {
            Bar = biz;
        }


    }
}
