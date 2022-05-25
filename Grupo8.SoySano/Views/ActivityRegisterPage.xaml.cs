using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Grupo8.SoySano.Models;
using Xamarin.Essentials;
using System.IO;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.Runtime;

namespace Grupo8.SoySano.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActivityRegisterPage : ContentPage
    {
        private DateTime startDate;
        private DateTime finishDate;
        private TimeSpan elapsedTime;
        private bool continueTimer;
        private String photoPath;
        public ActivityRegisterPage()
        {
            InitializeComponent();
            this.btnStart.IsVisible = true;
            this.btnFinish.IsVisible = false;
            this.btnPhoto.IsVisible = false;
            this.btnSave.IsVisible = false;
            this.btnDiscard.IsVisible = false;
        }

        private void btnStart_Clicked(object sender, EventArgs e)
        {
            // Set start date
            startDate = DateTime.Now;
            continueTimer = true;
            btnFinish.IsVisible = true;
            btnStart.IsVisible = false;
            // Start timer
            Device.StartTimer(TimeSpan.FromSeconds(1), () => {
                lbDuration.Text = DateTime.Now.Subtract(startDate).ToString(@"hh\:mm\:ss");
                return continueTimer;
            });
        }

        private void btnFinish_Clicked(object sender, EventArgs e)
        {
            // set finish date and elapsed time
            this.finishDate = DateTime.Now;
            this.elapsedTime = this.finishDate.Subtract(this.startDate);
            // stop timer
            this.continueTimer = false;
            // enable, disable buttons
            this.btnStart.IsVisible = false;
            this.btnFinish.IsVisible = false;
            this.btnSave.IsVisible = false;
            this.btnPhoto.IsVisible = true;
            this.btnDiscard.IsVisible = true;
        }

        private async void btnSave_Clicked(object sender, EventArgs e)
        {
            Activity activity = BuildActivity();

            

        }

        private void btnDiscard_Clicked(object sender, EventArgs e)
        {
            // stop timer
            this.continueTimer = false;
            // enable, disable buttons
            this.btnStart.IsVisible = true;
            this.btnFinish.IsVisible = false;
            this.btnPhoto.IsVisible = false;
            this.btnSave.IsVisible = false;
            this.btnDiscard.IsVisible = false;
            this.lbDuration.Text = "00:00:00";
            this.imgPhoto.Source = null;
        }

       

        private Activity BuildActivity()
        {
            Activity activity = new Activity();
            activity.Id = 0;
            activity.User = AppHelpers.CurrentUser;
            activity.StartDate = this.startDate;
            activity.FinishDate = this.finishDate;

            return activity;
        }

        private async void btnPhoto_Clicked(object sender, EventArgs e)
        {
            try
            {
                await CrossMedia.Current.Initialize();
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await DisplayAlert("SoySano", "Cámara no disponible", "Aceptar");
                    return;
                }

                StoreCameraMediaOptions options = new StoreCameraMediaOptions()
                {
                    Directory = "Sample",
                    Name = "test.jpg",
                    SaveToAlbum = true,
                    CompressionQuality = 75,
                    CustomPhotoSize = 50,
                    PhotoSize = PhotoSize.MaxWidthHeight,
                    MaxWidthHeight = 2000,
                    DefaultCamera = CameraDevice.Front
                };

                var file = await CrossMedia.Current.TakePhotoAsync(options);

                if (file == null)
                {
                    await DisplayAlert("SoySano", "Foto no guardada", "Aceptar");
                    return;
                }

                //await DisplayAlert("SoySano", "Foto guardada: " + file.Path, "Aceptar");
                string filePath = file.Path;
                imgPhoto.Source = ImageSource.FromStream(() => {
                    var stream = file.GetStream();
                    file.Dispose();
                    return stream;
                });

                BasicAWSCredentials credentials = new BasicAWSCredentials("AKIA3LXSABKFDXQC26KY", "c57bCptJUocPd9Mox7Nb235Cx+qT+80PIMZcmxWT");

                // Create a client
                AmazonS3Client client = new AmazonS3Client(credentials, Amazon.RegionEndpoint.USEast1);

                // Create a PutObject request
                PutObjectRequest request = new PutObjectRequest
                {
                    BucketName = string.Format("uisrael.grupo8/{0}", AppHelpers.CurrentUser.Id),
                    Key = string.Format("{0}.jpg", Guid.NewGuid().ToString()),
                    FilePath = filePath
                };

                // Put object
                PutObjectResponse response = await client.PutObjectAsync(request);

                Console.WriteLine("HTTPCODEStatus: " + response.HttpStatusCode);

                this.btnStart.IsVisible = false;
                this.btnFinish.IsVisible = false;
                this.btnPhoto.IsVisible = false;
                this.btnSave.IsVisible = true;
                this.btnDiscard.IsVisible = true;

                Console.WriteLine($"CapturePhotoAsync COMPLETED: {photoPath}");
            }
            catch (FeatureNotSupportedException exception)
            {
                Console.WriteLine(exception.Message);
                DisplayAlert("SoySano", "No soportado", "Aceptar");
            }
            catch (PermissionException exception)
            {
                Console.WriteLine(exception.Message);
                DisplayAlert("SoySano", "Sin permiso para acceder a la cámara", "Aceptar");
            }
            catch (Exception exception)
            {
                Console.WriteLine("ERROR: " + exception.Message);
                DisplayAlert("SoySano", "Error al capturar foto", "Aceptar");
            }
        }
    }
}