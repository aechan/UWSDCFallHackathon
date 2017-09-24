using Geocoding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TwilioSDCProject
{
    public class SaveLoadMessages
    {
        public static void SaveMessages(Dictionary<Address, Message> d)
        {
            Dictionary<string, Message> s = new Dictionary<string, Message>();

            foreach(KeyValuePair<Address, Message> kvp in d)
            {
                s.Add(kvp.Key.FormattedAddress, kvp.Value);
            }

            if (!File.Exists("SavedMessages.hm"))
            {
                File.Create("SavedMessages.hm");
            }
            File.WriteAllBytes("SavedMessages.hm", s.Serialize());
        }

        public static Dictionary<Address, Message> LoadMessagesFromFile()
        {
            if (!File.Exists("SavedMessages.hm"))
            {
                File.Create("SavedMessages.hm");
                return null;
            }
            if (new FileInfo("SavedMessages.hm").Length == 0)
            {
                return null;
            }
            
            var d = (Dictionary<string, Message>)File.ReadAllBytes("SavedMessages.hm").DeSerialize();

            Dictionary<Address, Message> dict = new Dictionary<Address, Message>();

            foreach(KeyValuePair<string, Message> t in d)
            {
                dict.Add(GeocodingManager.ParseAddress(t.Key), t.Value);
            }

            return dict;
        }
    }
}
