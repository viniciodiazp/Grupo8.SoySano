using Android.App;
using Android.Content;
using Android.OS;
using Grupo8.SoySano.Services;
using System;

namespace Grupo8.SoySano.Droid.Services
{
    [Activity(Label = "GoogleAuthInterceptor")]
    [
        IntentFilter
        (
            actions: new[] { Intent.ActionView },
            Categories = new[]
            {
                    Intent.CategoryDefault,
                    Intent.CategoryBrowsable
            },
            DataSchemes = new[]
            {
                // First part of the redirect url (Package name)
                "com.uisrael.grupo8.soysano"
            },
            DataPaths = new[]
            {
                // Second part of the redirect url (Path)
                "/oauth2redirect",
            },
            AutoVerify = true
        )
    ]
    public class GoogleAuthInterceptor : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            global::Android.Net.Uri uri_android = Intent.Data;
            //Android.Net.Uri uri_android = Intent.Data;

            // Convert iOS NSUrl to C#/netxf/BCL System.Uri - common API
            Uri uri_netfx = new Uri(uri_android.ToString());

            // Send the URI to the Authenticator for continuation
            GoogleAuthenticatorHelper.Auth?.OnPageLoading(uri_netfx);

            Finish();
        }
    }
}