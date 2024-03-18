namespace DhikkrCompanion;

public partial class DhikrPopUp 
{
	public DhikrPopUp()
	{
		InitializeComponent();

        if (DeviceInfo.Platform == DevicePlatform.Android)
        {
            TitleLabel.Margin = new Thickness(0, 30, 0, 0);

            DhikrFinishLabel.Margin = new Thickness(25, 0, 25, 0);
            Border.HeightRequest = 350;
            StackLayoutInsideBorder.HeightRequest = 350;
        }
        else if (DeviceInfo.Platform == DevicePlatform.iOS)
        {
            TitleLabel.Margin = new Thickness(0, 10, 0, 0);
            Border.HeightRequest = 320;
            StackLayoutInsideBorder.HeightRequest = 320;

        }
    }
}
