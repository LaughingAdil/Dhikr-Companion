using System.Text.Json;


namespace DhikkrCompanion;

public partial class DailyProgress : ContentPage
{
    
    private Dictionary<DateTime, int> dateCounterDictionary;
    private List<Label> dateCounterLabels;

    [Obsolete]
    public DailyProgress()
    {

        InitializeComponent();



        dateCounterDictionary = new Dictionary<DateTime, int>();
        dateCounterLabels = new List<Label>();
        
        // Initialize the dictionary and labels with default values
        InitializeDateCounterData();
        
        

      
        

      
        StackLayout stackLayout = new StackLayout();

        // Add labels to the stack layout
        foreach (var kvp in dateCounterDictionary)
        {
            Label label = new Label
            {
                Text = $"{kvp.Key.ToShortDateString()} :   {kvp.Value}",
               // HorizontalOptions = LayoutOptions.CenterAndExpand,
               // VerticalOptions = LayoutOptions.CenterAndExpand
            };
            dateCounterLabels.Add(label);
            stackLayout.Children.Add(label);
            label.TextColor = Colors.White;
            label.FontSize = 18;
            stackLayout.Padding = new Thickness(0, 20, 0, 0);
            label.HorizontalOptions = LayoutOptions.CenterAndExpand;
            stackLayout.Spacing = 5;
        }

        Content = new StackLayout
        {
            Children = { stackLayout}
        };

        
    }



    public void HandleIncrementFromMainPage2()
    {
        DateTime currentDate = DateTime.Now.Date; // Use only the date part

        // Check if the date exists in the dictionary
        if (dateCounterDictionary.TryGetValue(currentDate, out int counter))
        {
            // If the date exists in the dictionary, increment the counter
           // dateCounterDictionary[currentDate] = counter + 1;
        }
        else
        {
            // If the date does not exist, add it to the dictionary with counter 1
           // dateCounterDictionary[currentDate] = 1;
        }

        // Update the labels with the new counter values
        UpdateLabels();

        // Save the dictionary to Preferences for persistence
        SaveDateCounterData();
    }



    private void InitializeDateCounterData()
    {
        // Load the dictionary from Preferences or initialize with default values
        string serializedData = Preferences.Get("DateCounterDictionary", string.Empty);

        if (!string.IsNullOrEmpty(serializedData))
        {
            dateCounterDictionary = JsonSerializer.Deserialize<Dictionary<DateTime, int>>(serializedData);
        }

        else
        {
            dateCounterDictionary = new Dictionary<DateTime, int>();
        }

        // Update the labels with the initial data
        UpdateLabels();
    }

    private void UpdateLabels()
    {
        // Update labels based on the current dictionary values
        for (int i = 0; i < dateCounterLabels.Count; i++)
        {
            var kvp = dateCounterDictionary.ElementAt(i);
            dateCounterLabels[i].Text = $"{kvp.Key.ToShortDateString()}: {kvp.Value}";
        }
    }

    private void SaveDateCounterData()
    {
        // Save the dictionary to Preferences for persistence
        string serializedData = JsonSerializer.Serialize(dateCounterDictionary);
        Preferences.Set("DateCounterDictionary", serializedData);
    }

    
    
}