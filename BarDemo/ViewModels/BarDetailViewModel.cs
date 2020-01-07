using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BarDemo.Services;
using BarDemo.Models;
using System.Threading;


namespace BarDemo.ViewModels
{
    class BarDetailViewModel : BaseViewModel<Business>
    {

        Business _bar;
        public Business bar
        {
            get { return _bar; }
            set { _bar = value; }
        }

        public BarDetailViewModel(INavService navService) : base(navService)
        {


        }


        public override Task Init()
        {
            throw new NotImplementedException();
        }

        public override async Task Init(Business biz)
        {
            _bar = biz;
            Console.WriteLine(_bar.name);
            Console.WriteLine(_bar.id);
            //await Task.CompletedTask;

        }


    }
}
