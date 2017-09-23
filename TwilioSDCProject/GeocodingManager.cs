using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geocoding.Google;
using Geocoding;

namespace TwilioSDCProject
{
    public class GeocodingManager
    {
        static GoogleGeocoder geocoder;

        static GeocodingManager()
        {
            geocoder = new GoogleGeocoder() { ApiKey = "AIzaSyCmsW-KsXCLqOZifa0Hwz7jFH3IqfG5CtI" };
        }

        public static Address ParseAddress(string address)
        {
            IEnumerable<Address> addresses = geocoder.Geocode(address);

            return addresses.First();
        }

        public static List<Message> GetMessagesFromAddress(string address)
        {
            var ad = ParseAddress(address);
            return MessageCollector.GetMessagesFromAddress(ad);
        }

        public static void AddMessageAtAddress(string address, Message m)
        {
            var ad = ParseAddress(address);

            MessageCollector.AddMessageAtLocation(ad, m);
        }
    }
}
