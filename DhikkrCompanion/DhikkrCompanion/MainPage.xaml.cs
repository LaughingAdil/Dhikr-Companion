using Microsoft.Maui.Controls;
using Mopups.Services;
using System.Collections.ObjectModel;
namespace DhikkrCompanion;

public partial class MainPage : ContentPage
{
    
    int counter = 0;
    int countertwo = 0;
    public MainPage()
	{
		InitializeComponent();
        RadialControl.AnchorX = 0.5;
        RadialControl.AnchorY = 0.5;

        if(DeviceDisplay.MainDisplayInfo.Orientation == DisplayOrientation.Landscape)
        {
            var scrollView = new ScrollView();
            scrollView.Content = mainpagelayout;
            Content = scrollView;
        }else
        {
            Content = mainpagelayout;
            
        }

    }

	

	
    void dailyProgressButton_Clicked(System.Object sender, System.EventArgs e)
    {
        Vibration.Vibrate(1);
        Navigation.PushAsync(new ProgressCharts());
        
    }

    void Dhikrpopupbutton_Clicked(System.Object sender, System.EventArgs e)
    {
        Vibration.Vibrate(1);
        MopupService.Instance.PushAsync(new DhikrPopUp());
        
    }

   async void DhikrNamesPopUp_Clicked(System.Object sender, System.EventArgs e)
    {
        Vibration.Vibrate(1);

        await MopupService.Instance.PushAsync(new DhikrNamesPopUp());
    }

   async void MyNotes_Clicked(System.Object sender, System.EventArgs e)
    {
        Vibration.Vibrate(1);
       await MopupService.Instance.PushAsync(new MyNotes());
    }

    void SupportMe_Clicked(System.Object sender, System.EventArgs e)
    {
        Vibration.Vibrate(1);
        string url = "https://buymeacoffee.com/laughingadil";
        Launcher.OpenAsync(url);
    }

    [Obsolete]
     async  void DhikkrButton_Clicked(System.Object sender, System.EventArgs e)
    {
        Vibration.Vibrate(1);
        counter++;

        

            //Global Daily Counter
            ProgressCharts progressCharts = new ProgressCharts();
            progressCharts.HandleIncrementFromMainPage();
            DailyProgress dailyProgress = new DailyProgress();
            dailyProgress.HandleIncrementFromMainPage2();
        
        if (counter >= 33)
        {
            
            countertwo++;
            CounterButtontwo.Text = countertwo.ToString();
            Vibration.Default.Vibrate(TimeSpan.FromSeconds(0.1));
            
            counter = 0;
        }
        DhikkrButton.Text = counter.ToString();
        RadialControl.Value = counter;

        RadialControl.AnchorX = RadialControl.AnchorY = 0.501;
        await RadialControl.RotateTo(360, 220);
        RadialControl.Rotation = 0;

    }

    async void ResetCounters_Clicked(System.Object sender, System.EventArgs e)
    {
        Vibration.Vibrate(1);
       await CounterButtontwo.RotateXTo(360, 500, Easing.BounceIn);
       CounterButtontwo.RotationX = 0;
 


        ResetCounterTwo();



        void ResetCounterTwo()
        {
            Vibration.Vibrate(1);
            countertwo = 0;
            counter = 0;
            CounterButtontwo.Text = countertwo.ToString();
            DhikkrButton.Text = counter.ToString();
            RadialControl.Value = counter;
        }

    }

   async void MorningAdhkar_Clicked(System.Object sender, System.EventArgs e)
    {
        Vibration.Vibrate(1);

        await MopupService.Instance.PushAsync(new MorningAdhkar());
    }

   async void EveningAdhkar_Clicked(System.Object sender, System.EventArgs e)
    {
        Vibration.Vibrate(1);

        await MopupService.Instance.PushAsync(new EveningAdhkar());
    }
}

