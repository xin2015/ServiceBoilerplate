using System.Configuration;
using Topshelf;

namespace ServiceBoilerplate.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<ServiceBoilerplateService>();
                x.RunAsLocalSystem();
                x.SetDescription(ConfigurationManager.AppSettings["Description"]);
                x.SetDisplayName(ConfigurationManager.AppSettings["DisplayName"]);
                x.SetServiceName(ConfigurationManager.AppSettings["ServiceName"]);
                x.StartAutomatically();
            });
        }
    }
}
