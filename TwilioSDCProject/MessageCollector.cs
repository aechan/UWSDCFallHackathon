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
            MessageCollection = new Dictionary<Address, Message>();
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
