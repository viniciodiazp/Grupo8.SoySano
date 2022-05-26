using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Grupo8.SoySano.Api;
using Grupo8.SoySano.Models;
using Grupo8.SoySano.Services;
using Grupo8.SoySano.Utils;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
        private ActivityService service;
        public ActivityRegisterPage()
        {
            InitializeComponent();
            this.service = new ActivityServiceRestImpl();
            this.btnStart.IsVisible = true;
            this.btnFinish.IsVisible = false;
            this.btnPhoto.IsVisible = false;
            this.btnSave.IsVisible = false;
            this.btnDiscard.IsVisible = false;
            //this.txtName.IsVisible = false;
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
                    PhotoSize = PhotoSize.Medium,
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
                this.photoPath = file.Path;
                imgPhoto.Source = ImageSource.FromStream(() => {
                    var stream = file.GetStream();
                    file.Dispose();
                    return stream;
                });

                Console.WriteLine($"CapturePhotoAsync COMPLETED: {photoPath}");

                this.btnStart.IsVisible = false;
                this.btnFinish.IsVisible = false;
                this.btnPhoto.IsVisible = false;
                this.btnSave.IsVisible = true;
                this.btnDiscard.IsVisible = true;
                this.txtName.IsVisible = true;
            }
            catch (FeatureNotSupportedException exception)
            {
                Console.WriteLine(exception.Message);
                DisplayAlert(Constant.Messages.DISPLAY_TITLE, "No soportado", "Aceptar");
            }
            catch (PermissionException exception)
            {
                Console.WriteLine(exception.Message);
                DisplayAlert(Constant.Messages.DISPLAY_TITLE, "Sin permiso para acceder a la cámara", "Aceptar");
            }
            catch (Exception exception)
            {
                Console.WriteLine("ERROR: " + exception.Message);
                DisplayAlert(Constant.Messages.DISPLAY_TITLE, "Error al capturar foto", "Aceptar");
            }
        }

        private async void btnSave_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                DisplayAlert(Constant.Messages.DISPLAY_TITLE, "Digite un nombre para la actividad", "Aceptar");
                txtName.Focus();
                return;
            }
            // Upload to S3
            BasicAWSCredentials credentials = new BasicAWSCredentials("AKIA3LXSABKFDXQC26KY", "c57bCptJUocPd9Mox7Nb235Cx+qT+80PIMZcmxWT");

            // Create a client
            AmazonS3Client client = new AmazonS3Client(credentials, Amazon.RegionEndpoint.USEast1);

            // Create a PutObject request
            PutObjectRequest request = new PutObjectRequest
            {
                BucketName = string.Format("uisrael.grupo8/{0}", AppHelpers.CurrentUser.Id),
                Key = string.Format("{0}.jpg", Guid.NewGuid().ToString()),
                FilePath = this.photoPath
            };

            // Put object
            PutObjectResponse response = await client.PutObjectAsync(request);

            Console.WriteLine("HttpCodeStatus: " + response.HttpStatusCode);

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                Activity activity = BuildActivity(request.BucketName, request.Key);
                Activity responseEntity = this.service.Create(activity);
                if (!responseEntity.Id.Equals(0))
                {
                    AppHelpers.CurrentActivity = responseEntity;
                    Discard();
                    await Shell.Current.GoToAsync(nameof(ActivityDetailPage));
                } else
                {
                    DisplayAlert(Constant.Messages.DISPLAY_TITLE, "No se guardó la actividad", "Aceptar");
                }
            } 
            else
            {
                DisplayAlert(Constant.Messages.DISPLAY_TITLE, "No se cargó la foto al almacén", "Aceptar");
            }

        }

        private void btnDiscard_Clicked(object sender, EventArgs e)
        {
            Discard();
        }

        private void Discard()
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
            this.txtName.IsVisible = false;
        }

        private Activity BuildActivity(String bucketName, String key)
        {
            String s3PhotoPath = string.Format("{0}/{1}/{2}",
                Constant.Services.SERVICE_S3_BASE_URL, AppHelpers.CurrentUser.Id, key);
            Activity activity = new Activity();
            activity.Id = 0;
            activity.Name = txtName.Text;
            activity.User = AppHelpers.CurrentUser;
            activity.StartDate = this.startDate;
            activity.FinishDate = this.finishDate;
            activity.Photo = s3PhotoPath;
            return activity;
        }
    }
}