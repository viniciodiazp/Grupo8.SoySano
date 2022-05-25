using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grupo8.SoySano.Services;
using Grupo8.SoySano.Droid;
using Grupo8.SoySano.Droid.Services;

[assembly: Xamarin.Forms.Dependency(typeof(GoogleAuthService))]

namespace Grupo8.SoySano.Droid.Services
{
    public class GoogleAuthService : IGoogleAuthService
    {
        internal static MainActivity MainActivity { get; set; }

        public GoogleAuthService()
        {

        }

        public void Autheticate(IGoogleAuthenticationDelegate googleAuthenticationDelegate)
        {
            /*GoogleAuthenticatorHelper.Auth = new GoogleAuthenticator(
               "964681194779-8219jilqvq65g6b9pafmqsohfcnj7m0k.apps.googleusercontent.com",
               "email",
               "com.jdc.OAuth:/oauth2redirect",
               googleAuthenticationDelegate);*/

            GoogleAuthenticatorHelper.Auth = new GoogleAuthenticator(
               "15715590093-db3labbii5asggdc41tevo31pd38okgb.apps.googleusercontent.com",
               "email",
               "com.uisrael.grupo8.soysano:/oauth2redirect",
               googleAuthenticationDelegate);

            // Display the activity handling the authentication
            var authenticator = GoogleAuthenticatorHelper.Auth.GetAuthenticator();
            var intent = authenticator.GetUI(MainActivity);
            MainActivity.StartActivity(intent);
        }
    }
}