using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BarDemo.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BarListTemplate : ContentPage
	{
		public BarListTemplate ()
		{
			InitializeComponent ();
		}
	}
}