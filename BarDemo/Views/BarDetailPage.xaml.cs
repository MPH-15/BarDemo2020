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
		public BarDetailPage ()
		{
			InitializeComponent ();

            BindingContext = new BarDetailViewModel(DependencyService.Get<INavService>());
		}
	}
}