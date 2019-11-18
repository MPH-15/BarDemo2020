using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Auth;
using BarDemo.Views;
using Xamarin.Forms;


namespace BarDemo.Services
{
    public class FBAuthService : IAuthService
    {
        public void SignInAsync(string clientId, Uri authUrl, Uri callbackUrl)
        //public Task SignInAsync(string clientId, Uri authUrl, Uri callbackUrl, Action<string> tokenCallback, Action<string> errorCallback)
        {
            //string scope = "email, user_gender, user_age_range";
            var auth = new OAuth2Authenticator(clientId, string.Empty, authUrl, callbackUrl);
            auth.AllowCancel = true;
            auth.Completed += Auth_Completed;
            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(auth);
        }

        private async void Auth_Completed(object sender, AuthenticatorCompletedEventArgs e)
        {

            if (e.Account != null && e.IsAuthenticated)
            {
                //tokenCallback?.Invoke(
                //    e.Account.Properties["access_token"]);
                await Application.Current.MainPage.Navigation.PushAsync(new TabPage());
            }
            else
            {
                //errorCallback?.Invoke("Not authenticated");
                throw new NotImplementedException();
            }
        }
    }
}
