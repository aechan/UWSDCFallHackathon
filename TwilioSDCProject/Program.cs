using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;
using Nancy.Hosting.Self;
namespace TwilioSDCProject
{
    class Program
    {
        static void Main(string[] args)
        {
            HostConfiguration hostConfigs = new HostConfiguration()
            {
                UrlReservations = new UrlReservations() { CreateAutomatically = true }
            };

            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromMinutes(1);

            var timer = new System.Threading.Timer((e) =>
            {
                MessageCollector.SaveMessages();
                MessageCollector.LoadMessages();
            }, null, startTimeSpan, periodTimeSpan);

            using (var host = new NancyHost(new Uri("http://localhost:8080"), new DefaultNancyBootstrapper(), hostConfigs))
            {
                host.Start();
                Console.WriteLine("Running on http://localhost:8080");
                Console.ReadLine();
                host.Stop();
            }

            
           
        }
    }
}
