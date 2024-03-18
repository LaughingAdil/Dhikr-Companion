using System;
namespace DhikkrCompanion
{
    public class DictionaryDefinition
    {
        // Singleton instance to ensure a single dictionary throughout the application
        private static DictionaryDefinition _instance;

        // Private constructor to prevent external instantiation
        private DictionaryDefinition()
        {
            // Initialize your dictionary here if needed
            dateCounterDictionary = new Dictionary<DateTime, int>();
        }

        // Public property to access the dictionary
        public Dictionary<DateTime, int> dateCounterDictionary { get; set; }

        // Public method to get the singleton instance
        public static DictionaryDefinition Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DictionaryDefinition();
                }
                return _instance;
            }
        }
    }
}


