using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using DirectumTestProject.Downloader;
using static Android.Views.View;

namespace DirectumTestProject
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            var downloaderButton = FindViewById<Button>(Resource.Id.downloaderButton);
            downloaderButton.Click += DownloaderButton_Click;
        }

        private void DownloaderButton_Click(object sender, System.EventArgs e)
        {
            if (sender is Button button)
            {
                if (button.Id == Resource.Id.downloaderButton)
                {
                    Intent intent = new Intent(this, typeof(DownloaderActivity));
                    StartActivity(intent);
                }
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }
}