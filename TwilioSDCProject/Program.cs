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

            using (var host = new NancyHost(new Uri("http://localhost:80"), new DefaultNancyBootstrapper(), hostConfigs))
            {
                host.Start();
                Console.WriteLine("Running on http://localhost:80");
                Console.ReadLine();
                host.Stop();
            }
        }
    }
}
