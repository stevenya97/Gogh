using Microsoft.UI.Xaml;
using System;
namespace Gogh
{
    public class Settings
    {
        static Settings _instance = null;

        //public static Settings Instance => _instance ??= Settings.FromJson();
        // We default this to false to prevent saves firing when loading from json.
        bool _autoSave = false;
        bool _EnableOneDrive = false;
        public bool EnableOneDrive
        {
            get { return _EnableOneDrive; }
            set
            {
                if (_EnableOneDrive != value)
                {
                    _EnableOneDrive = value;
                    if (_autoSave)
                    {
                        SaveJson();
                    }
                }
            }
        }
        void SaveJson()
        {
            //AsyncHelper.RunSync(() => Storage.SaveSettingsJsonAsync(this));
        }

        static Settings FromJson()
        {
            Settings settings = null;

            //var settingsFromJson;//= AsyncHelper.RunSync(() => Storage.LoadSettingsJsonAsync());
            //// If we couldn't load settings then save the defaults.
            //if (settingsFromJson == null)
            //{
            //    settings = new Settings();
            //    settings.SaveJson();
            //}
            //else
            //{
            //    settings = settingsFromJson;
            //}

            // Re-enable auto save.
            settings._autoSave = true;
            return settings;
        }
    }
}