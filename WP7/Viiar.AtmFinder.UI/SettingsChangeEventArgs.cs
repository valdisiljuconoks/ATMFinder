using System;

namespace Viiar.AtmFinder.UI
{
    public class SettingsChangeEventArgs : EventArgs
    {
        public SettingsChangeEventArgs(string keyName, object value, bool triggerReload)
        {
            KeyName = keyName;
            Value = value;
            TriggerReload = triggerReload;
        }

        public string KeyName { get; set; }
        public object Value { get; set; }
        public bool TriggerReload { get; set; }
    }
}
