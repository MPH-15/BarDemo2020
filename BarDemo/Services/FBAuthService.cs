using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Auth;
using BarDemo.Views;
using Xamarin.Forms;
using System.Json;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Net.Http;

namespace BarDemo.Services
{
    public class FBAuthService : IAuthService
        
    {
        // private Xamarin.Auth.Account fbaccount = new Xamarin.Auth.Account();

        Xamarin.Auth.Account Fbaccount;
        string fbtoken;

        public async Task SignInAsync(string clientId, Uri authUrl, Uri callbackUrl)
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
                //Fbaccount.Properties = Xamarin.Auth.Account.Deserialize
                string fbuser = JsonConvert.SerializeObject(e.Account.Properties);
                Debug.WriteLine(fbuser);
                fbtoken = JsonConvert.DeserializeObject<FacebookToken>(fbuser).AccessToken;
                //tokenCallback?.Invoke(
                //    e.Account.Properties["access_token"]);
                //var request = new OAuth2Request(
                //    "GET",
                //    new Uri("https://graph.facebook.com/me?fields=name"),
                //    null,
                //    e.Account);

                //var fbResponse = await request.GetResponseAsync();
                //var fbuser = JsonValue.Parse(fbResponse.GetResponseText());
                //var name = fbuser["name"];
                //Debug.WriteLine(name + "*****************************");

                await Application.Current.MainPage.Navigation.PushAsync(new TabPage());
            }
            else
            {
                //errorCallback?.Invoke("Not authenticated");
                throw new NotImplementedException();
            }
        }

        public async void FBDataRequest(string fbfields)
        {
            //var request = new OAuth2Request(
            //    "GET",
            //    new Uri("https://graph.facebook.com/me?fields=" + fbfields),
            //    null,
            //    Fbaccount);

            HttpClient httpClient = new HttpClient();
            HttpResponseMessage httpResponse = await httpClient.GetAsync("https://graph.facebook.com/me?fields=name&access_token=" + fbtoken);

            string fbresponse = await httpResponse.Content.ReadAsStringAsync();
            //var fbResponse = await request.GetResponseAsync();
           // var fbuser = JsonValue.Parse(fbResponse.GetResponseText());
            //var name = fbuser["name"];
            Debug.WriteLine(fbresponse + "*****************************");
        }
    }

    public class FacebookToken
    {
        [JsonProperty("state")]
        public string State
        {
            get;
            set;
        }
        [JsonProperty("access_token")]
        public string AccessToken
        {
            get;
            set;
        }

        [JsonProperty("expires_in")]
        public string ExpiresIn
        {
            get;
            set;
        }

        [JsonProperty("reauthorize_required_in")]
        public string ReauthorizeRequiredIn
        {
            get;
            set;
        }

        [JsonProperty("data_access_expiration_time")]
        public string DataAccessExpirationTime
        {
            get;
            set;
        }
    }
}
