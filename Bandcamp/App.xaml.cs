using System.Diagnostics;
using Bandcamp.ProcessCache;

namespace Bandcamp
{
    public partial class App : Application
    {
        private ApplicationProcess _ApplicationProcess;
        public App(ApplicationProcess applicationProcess)
        {
            InitializeComponent();
            MainPage = new MainPage();
            _ApplicationProcess = applicationProcess;
            AppDomain.CurrentDomain.ProcessExit += (s, e) => OnStop();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = base.CreateWindow(activationState);
            const int newWidth = 1220;
            const int newHight = 720;
            window.Width = newWidth;
            window.Height = newHight;
            window.MinimumWidth = newWidth;
            window.MaximumWidth = newWidth;
            window.MinimumHeight = newHight;
            window.MaximumHeight = newHight;
            window.Title = "Bandcamp Player";

            window.Destroying += (s, e) =>
            {
                OnStop();
            };
            return window;
        }

        protected override void OnStart()
        {
            base.OnStart();
            Debug.WriteLine("#######________________Aplicación Iniciada_______________");
        }

        protected override void OnResume()
        {
            base.OnResume();
            Debug.WriteLine("#######________________Aplicación Activa________________");
        }

        protected override void OnSleep()
        {
            base.OnSleep();
            Debug.WriteLine("#######________________Aplicación Inactiva_______________");
        }

        protected void OnStop() {
            Debug.WriteLine($"Deteniendo procesos de {nameof(MainPage)}");
            _ApplicationProcess.DestroyedProcessCache();
        }
    }

}
