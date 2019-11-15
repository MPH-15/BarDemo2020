using System;

using Xamarin.Forms;

namespace BarDemo.ViewModels
{
    public class SearchViewModel : ContentPage
    {
        public SearchViewModel()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello ContentPage" }
                }
            };
        }
    }
}

