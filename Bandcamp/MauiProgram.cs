using Microsoft.Extensions.Logging;
using Bandcamp.Stores;
using Bandcamp.Services;
using Bandcamp.Utils;
using Bandcamp.ProcessCache;

namespace Bandcamp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>().ConfigureFonts(fonts =>{
                 fonts.AddFont("OpenSans-Regular.ttf","OpenSansRegular");
            });

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddSingleton<GlobalStore>();
            builder.Services.AddSingleton<StreamStore>();
            builder.Services.AddSingleton<ApplicationService>();
            builder.Services.AddSingleton<Utilities>();
            builder.Services.AddSingleton<ApplicationProcess>();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		    builder.Logging.AddDebug();
            #endif

            return builder.Build();
        }
    }
}
