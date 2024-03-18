using System.Text.Json;
using Microcharts;
using Microcharts.Maui;
using SkiaSharp;

namespace DhikkrCompanion;

public partial class ProgressCharts : ContentPage
{
    private ChartEntry[] entries;

    public ProgressCharts()
    {
        InitializeComponent();
        InitializeDateCounterData();

       
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        InitializeChartDataAsync();
    }

   

    private async void InitializeChartDataAsync()
    {
        await Task.Run(() =>
        {
            DateTime latestDate = DictionaryDefinition.Instance.dateCounterDictionary.Any() ? DictionaryDefinition.Instance.dateCounterDictionary.Keys.Max() : DateTime.MinValue;

            if (latestDate == DateTime.MinValue)
            {
                latestDate = DateTime.UtcNow;
            }

            entries = Enumerable.Range(0, 5)
                .Select(index =>
                {
                    DateTime currentDate = latestDate.AddDays(-index);

                    if (DictionaryDefinition.Instance.dateCounterDictionary.TryGetValue(currentDate, out int counter))
                    {
                        return new ChartEntry(counter)
                        {
                            Label = currentDate.ToString("MM/dd/yyyy"),
                            ValueLabel = counter.ToString(),
                            Color = SKColor.Parse("#000000")
                        };
                    }
                    else
                    {
                        return new ChartEntry(0)
                        {
                            Label = currentDate.ToString("MM/dd/yyyy"),
                            ValueLabel = "0",
                            Color = SKColor.Parse("#000000")
                        };
                    }
                })
                .ToArray();
        });

        MainThread.BeginInvokeOnMainThread(() =>
        {
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
            }; BindingContext = this;
        });
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

    // Set the BindingContext to enable data binding if needed for other controls
    void tendays_Clicked(System.Object sender, System.EventArgs e)
    {
        Vibration.Vibrate(1);
        Navigation.PushAsync(new ProgressCharts10days());
    }

    void thirtydays_Clicked(System.Object sender, System.EventArgs e)
    {
        Vibration.Vibrate(1);
        Navigation.PushAsync(new ProgressCharts30days());
    }

    void oneeightydays_Clicked(System.Object sender, System.EventArgs e)
    {
        Vibration.Vibrate(1);
        Navigation.PushAsync(new ProgressCharts180days());
    }

    [Obsolete]
    void ViewData_Clicked(System.Object sender, System.EventArgs e)
    {
        Vibration.Vibrate(1);
        Navigation.PushAsync(new DailyProgress());
    }

}

  

    



