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
    public class SearchViewModel : BaseViewModel
    {

        public SearchViewModel(INavService navService) : base(navService)
        {
            //ExecuteBarListCommand();
        }


        public override async Task Init()
        {

        }

        async Task ExecuteBarListCommand()
        {
            await NavService.NavigateTo<BarListViewModel>();
            await NavService.RemoveLastView();
        }

    }
}

