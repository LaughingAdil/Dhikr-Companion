namespace DhikkrCompanion;

public partial class MyNotes
{
    public MyNotes()
    {
        InitializeComponent();

        if (DeviceInfo.Platform == DevicePlatform.iOS)
        {
            border.StrokeThickness = 0;
            verticalstacklayout.HeightRequest = 550;

            entry1.WidthRequest = 310; entry1.HeightRequest = 420;
        }
        else
        {
            border.HeightRequest = 550;
            verticalstacklayout.HeightRequest = 550;

            entry1.WidthRequest = 310; entry1.HeightRequest = 440;
        }

        LoadSavedData(); // Load saved data when the page is created
    }

    private void LoadSavedData()
    {
        // Load text from preferences and set it to respective entry fields
        entry1.Text = Preferences.Get("entry1", string.Empty);
        


        // Repeat the pattern for other entries (entry2 to entry10)
    }

    private void SaveData()
    {
        // Save text from entry fields to preferences

        Preferences.Set("entry1", entry1.Text);
       


        // Repeat the pattern for other entries (entry2 to entry10)
    }

    private void ClearNotesButton_Clicked(object sender, EventArgs e)
    {
        // Clear text in all entry fields
        entry1.Text = string.Empty;




    }

    void entry1_TextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
        SaveData();
    }



}