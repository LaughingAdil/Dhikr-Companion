using System.Text.Json;
using Microcharts;
using Microcharts.Maui;
using SkiaSharp;

namespace DhikkrCompanion;

public partial class ProgressCharts180days : ContentPage
{
    //private Dictionary<DateTime, int> dateCounterDictionary;

    private ChartEntry[] entries;

    public ProgressCharts180days()
    {
        InitializeComponent();
        Dictionary<DateTime, int> dateCounterDictionary = DictionaryDefinition.Instance.dateCounterDictionary;

        // Initialize the dictionary and labels with default values
        InitializeDateCounterData();

        // Check if the dictionary is not empty before finding the latest date
        DateTime latestDate = dateCounterDictionary.Any() ? dateCounterDictionary.Keys.Max() : DateTime.MinValue;

        // If the dictionary is empty, assign a default static value
        if (latestDate == DateTime.MinValue)
        {
            // Assign your default static value here
            latestDate = DateTime.UtcNow; // Replace this with your default value
        }

        // Generate entries for the date range starting from the latest date
        entries = Enumerable.Range(0, 180)
            .Select(index =>
            {
                DateTime currentDate = latestDate.AddDays(-index); // Subtract index to go back in time

                // Check if the date exists in the dictionary
                if (DictionaryDefinition.Instance.dateCounterDictionary.TryGetValue(currentDate, out int counter))
                {
                    // If the date exists in the dictionary, use the counter value
                    return new ChartEntry(counter)
                    {
                        Label = currentDate.ToString("MM/dd/yyyy"), // Reverse the labels to represent oldest to newest
                        ValueLabel = counter.ToString(), // Incremental values from left to right
                        Color = SKColor.Parse("#000000")
                    };
                }
                else
                {
                    // If the date does not exist, default to zero
                    return new ChartEntry(0)
                    {
                        Label = currentDate.ToString("MM/dd/yyyy"), // Reverse the labels to represent oldest to newest
                        ValueLabel = "0", // Incremental values from left to right
                        Color = SKColor.Parse("#000000")
                    };
                }
            })
            .ToArray();


        // Set the entries directly to the ChartView

        chartView1.Chart = new BarChart
        {
            Entries = entries.Reverse(),

        };



        chartView4.Chart = new LineChart
        {
            Entries = entries.Reverse(),
            LineMode = LineMode.Straight,
            PointMode = PointMode.Circle,

        };

        chartView6.Chart = new HalfRadialGaugeChart
        {
            Entries = entries
        };






        // Set the BindingContext to enable data binding if needed for other controls
        BindingContext = this;
    }

    public void HandleIncrementFromMainPage()
    {
        DateTime currentDate = DateTime.Now.Date; // Use only the date part

        // Check if the date exists in the dictionary
        if (DictionaryDefinition.Instance.dateCounterDictionary.TryGetValue(currentDate, out int counter))
        {
            // If the date exists in the dictionary, increment the counter
            DictionaryDefinition.Instance.dateCounterDictionary[currentDate] = counter + 1;
        }
        else
        {
            // If the date does not exist, add it to the dictionary with counter 1
            DictionaryDefinition.Instance.dateCounterDictionary[currentDate] = 1;
        }

        // Update the labels with the new counter values


        // Save the dictionary to Preferences for persistence
        SaveDateCounterData();
    }
    void InitializeDateCounterData()
    {
        // Load the dictionary from Preferences or initialize with default values
        string serializedData = Preferences.Get("DateCounterDictionary", string.Empty);

        if (!string.IsNullOrEmpty(serializedData))
        {
            DictionaryDefinition.Instance.dateCounterDictionary = JsonSerializer.Deserialize<Dictionary<DateTime, int>>(serializedData);
        }

        else
        {
            DictionaryDefinition.Instance.dateCounterDictionary = new Dictionary<DateTime, int>();
        }

        // Update the labels with the initial data

    }



    void SaveDateCounterData()
    {
        // Save the dictionary to Preferences for persistence
        string serializedData = JsonSerializer.Serialize(DictionaryDefinition.Instance.dateCounterDictionary);
        Preferences.Set("DateCounterDictionary", serializedData);
    }

  
}


