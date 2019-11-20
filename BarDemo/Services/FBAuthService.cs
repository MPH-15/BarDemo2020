using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Auth;
using BarDemo.Views;
using Xamarin.Forms;
using BarDemo.Models;
using System.Diagnostics;


namespace BarDemo.Services
{
    public class FBAuthService : IAuthService
    {
        public bool authenticated = false;

        Account account;

        // Overloaded SignInAsync method with hardcoded input parameters for client id, scope, authorize url, and callback url
        public void SignInAsync()
        //public Task SignInAsync(string clientId, Uri authUrl, Uri callbackUrl, Action<string> tokenCallback, Action<string> errorCallback)
        {
            //string scope = "email, user_gender, user_age_range";

            string clientId = null;
            string redirectUri = null;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    clientId = Constants.FBiOSClientId;
                    redirectUri = Constants.FBiOSRedirectUrl;
                    break;

                case Device.Android:
                    clientId = Constants.FBAndroidClientId;
                    redirectUri = Constants.FBAndroidRedirectUrl;
                    break;
            }


            var FBauthenticator = new OAuth2Authenticator(
             clientId: clientId,
             scope: "email, user_gender",
             authorizeUrl: new Uri(Constants.FBAuthorizeUrl),
             redirectUrl: new Uri(Constants.FBiOSRedirectUrl),
             isUsingNativeUI: false
             );

            FBauthenticator.Completed += OnFBAuthCompleted;
            FBauthenticator.Error += OnAuthError;

            AuthenticationState.Authenticator = FBauthenticator;

            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(FBauthenticator);

        }



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




        public async void Auth_Completed(object sender, AuthenticatorCompletedEventArgs e)
        {

            if (e.Account != null && e.IsAuthenticated)
            {
                //tokenCallback?.Invoke(
                //    e.Account.Properties["access_token"]);
                authenticated = true;

            }
            else
            {
                //errorCallback?.Invoke("Not authenticated");
                authenticated = false;
                throw new NotImplementedException();
            }
        }


        public async void OnFBAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= OnFBAuthCompleted;
                authenticator.Error -= OnAuthError;
            }

            FBUser user = null;
            if (e.IsAuthenticated)
            {
                authenticated = true;
                Debug.WriteLine("Facebook Authenticated! ");

                var request = new OAuth2Request(
                    "GET",
                    new Uri("https://graph.facebook.com/me?fields=name,gender"),
                    null,
                    e.Account);

                //var response = await request.GetResponseAsync();
                //if (response != null)
                //{
                //    string userJson = await response.GetResponseTextAsync();
                //    user = JsonConvert.DeserializeObject<FBUser>(userJson);

                //    Debug.WriteLine("User Name: " + user.Name);
                //    Debug.WriteLine("User Gender: " + user.Gender);

                //}


                //if (account != null)
                //{
                //    //store.Delete(account, Constants.AppName);
                //    Debug.WriteLine("Account isn't null");
                //}

            }
        }


        public void OnAuthError(object sender, AuthenticatorErrorEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= OnFBAuthCompleted;
                authenticator.Error -= OnAuthError;
            }

            Debug.WriteLine("Authentication error: " + e.Message);
        }


    }
}
