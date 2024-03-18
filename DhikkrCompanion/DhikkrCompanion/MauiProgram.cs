using Mopups.Hosting;
using Microcharts;
using SkiaSharp;
using Microcharts.Maui;
using epj.RadialDial.Maui;
using Microsoft.Extensions.Logging;

namespace DhikkrCompanion;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMicrocharts()
			.ConfigureMopups()
			.UseRadialDial()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");

                fonts.AddFont("Brands-Regular-400.otf", "BrandsRegular");
                fonts.AddFont("Free-Regular-400.otf", "FreeRegular400");
                fonts.AddFont("Free-Solid-900.otf", "FreeSolid900");
            });

#if DEBUG
		builder.Logging.AddDebug();
#endif
		//
		return builder.Build();
	}
}
