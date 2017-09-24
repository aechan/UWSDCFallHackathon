using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geocoding.Google;
using Geocoding;

namespace TwilioSDCProject
{
    public class MessageCollector
    {
        private const int MessageExpiresAfterMinutes = 1440;
        public static Dictionary<Address, Message> MessageCollection { get; set; }

        static MessageCollector()
        {
            LoadMessages();
        }

        public static void SaveMessages()
        {
            SaveLoadMessages.SaveMessages(MessageCollection);
            Console.WriteLine("Saved {0} messages to file: SavedMessages.hm", MessageCollection.Count);
        }

        public static void LoadMessages()
        {
            MessageCollection = SaveLoadMessages.LoadMessagesFromFile();
            if (MessageCollection != null)
            {
                Console.WriteLine("Loaded {0} messages from file: SavedMessages.hm", MessageCollection.Count);
            }
            else
            {
                MessageCollection = new Dictionary<Address, Message>();
                Console.WriteLine("Initialized MessageCollection");
            }
        }
        public static void AddMessageAtLocation(Address location, Message message)
        {
            MessageCollection.Add(location, message);
        }

        public static void CleanupMessages()
        {
            foreach(KeyValuePair<Address, Message> kvp in MessageCollection)
            {
                var terminalDate = kvp.Value.Date.AddMinutes(MessageExpiresAfterMinutes);
                var difference = terminalDate.Subtract(kvp.Value.Date);
                if (difference.TotalMinutes == 0)
                {
                    MessageCollection.Remove(kvp.Key);
                }
            }
        }

        public static List<Message> GetMessagesFromAddress(Address ad)
        {
            List<Message> m = new List<Message>();
            foreach(KeyValuePair<Address, Message> kvp in MessageCollection)
            {
                if(kvp.Key.Coordinates.Latitude == ad.Coordinates.Latitude && kvp.Key.Coordinates.Longitude == ad.Coordinates.Longitude)
                {
                    m.Add(kvp.Value);
                }
            }
            return m;
        }


    }
}
