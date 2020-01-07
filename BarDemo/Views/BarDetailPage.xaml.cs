using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarDemo.ViewModels;
using BarDemo.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BarDemo.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BarDetailPage : ContentPage
	{
        BarDetailViewModel _vm
        {
            get { return BindingContext as BarDetailViewModel; }
        }

        public BarDetailPage ()
		{
			InitializeComponent();

            BindingContext = new BarDetailViewModel(DependencyService.Get<INavService>());

        }

        private void ContentPage_Appearing(object sender, EventArgs e)
        {


        }

        private void DetailListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }
}