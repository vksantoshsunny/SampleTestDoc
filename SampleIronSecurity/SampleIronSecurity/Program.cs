using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace SampleIronSecurity
{
    class Program
    {
        public static IConfigurationRoot configuration;
        static void Main(string[] args)
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            Console.WriteLine("Hello World!");
            IronPdf.License.LicenseKey = "IRONPDF-1074125E7B-505437-994C4A-D829DFF424-4ED57CA5-UExA3EA6298E88F7D8-COMMUNITY.TRIAL.EXPIRES.15.MAY.2020";
            var documentBody = configuration["document:documentBody"];
            var bytes = Convert.FromBase64String(documentBody);
            var pdf = new IronPdf.PdfDocument(bytes, "12345", "123");
            pdf.SecuritySettings.AllowUserAnnotations = false;
            pdf.SecuritySettings.AllowUserCopyPasteContent = false;
            pdf.SecuritySettings.AllowUserPrinting = IronPdf.PdfDocument.PdfSecuritySettings.PdfPrintSecrity.NoPrint;


            pdf.OwnerPassword = "123";

            pdf.SaveAs("sample.pdf");
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {

            // Build configuration
            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();

            // Add access to generic IConfigurationRoot
            serviceCollection.AddSingleton<IConfigurationRoot>(configuration);


        }
    }
}
