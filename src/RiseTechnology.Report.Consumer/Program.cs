using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RiseTechnology.Common;
using RiseTechnology.Common.Constant;
using RiseTechnology.Common.Models.Request;
using RiseTechnology.Common.Models.Response;
using RiseTechnology.Common.QueueMenager;
using RiseTechnology.Common.Tools.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Linq;
using OfficeOpenXml;
using System.Reflection;
using System.Net.Http;
using System.Text;

namespace RiseTechnology.Report.Consumer
{
    public class Program
    {
        static IConfiguration configuration;
        static IHttpClientFactory clientFactory;
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            var messageBroker = (RabbitMqMessageBroker)host.Services.GetService<IMessageBrokerService>();
            clientFactory = host.Services.GetService<IHttpClientFactory>();
            messageBroker.Consume("ReportQueue", CreateReport);
            Console.ReadKey();
        }
        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            var hostBuilder = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, builder) =>
                {
                    builder.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
                    configuration = builder.Build();
                })
                .ConfigureServices((context, services) =>
                {

                    services.AddHttpClient(EnpointNames.ReportAPIClient, httpClient =>
                    {
                        httpClient.BaseAddress = new Uri(configuration.GetValue<string>("Services:ReportServiceEndPoint"));
                        httpClient.Timeout = TimeSpan.FromSeconds(30);
                    });
                    services.AddHttpClient(EnpointNames.ContactAPIClient, httpClient =>
                    {
                        httpClient.BaseAddress = new Uri(configuration.GetValue<string>("Services:ContactServiceEndPoint"));
                        httpClient.Timeout = TimeSpan.FromSeconds(30);
                    });
                    services.AddSingleton<IMessageBrokerService, RabbitMqMessageBroker>();
                    services.BuildServiceProvider();
                });

            return hostBuilder;
        }
        public static void CreateReport(object incomingData)
        {
            var reportUuid = Guid.Parse(incomingData.ToString());
            Debug.WriteLine($"RabbitMq-> ${reportUuid}");
            Thread.Sleep(15 * 1000);
            List<PersonContactInformationResponseModel> contactItems = new List<PersonContactInformationResponseModel>();
            var client = clientFactory.CreateClient(EnpointNames.ContactAPIClient);
            var response = client.Send(new HttpRequestMessage(HttpMethod.Get, "/Contact"));
            if (!response.IsSuccessStatusCode)
                throw new RemoteServiceException();
            contactItems = JsonSerializer.Deserialize<List<PersonContactInformationResponseModel>>(response.Content.ReadAsStringAsync().Result);




            #region Preparing
            var reportPreparingClient = clientFactory.CreateClient(EnpointNames.ReportAPIClient);
            var reportPreparingClientResponse = reportPreparingClient.Send(new HttpRequestMessage(HttpMethod.Post, $"/Report/{reportUuid}/ChangeStatus/{Enums.ReportStatus.Preparing}"));
            if (!reportPreparingClientResponse.IsSuccessStatusCode)
                throw new RemoteServiceException();

            var locationPersonCount = contactItems.GroupBy(x => x.ContactInformations.Where(y => y.ContactType == Enums.ContactType.Location)).GroupBy(y => y.Key.FirstOrDefault().ContactContent)
               .Select(group => new { Location = group.Key, Count = group.Count(), TelephoneCount = contactItems.Where(x => x.ContactInformations.Any(y => y.ContactContent == group.Key) && x.ContactInformations.Any(y => y.ContactType == Enums.ContactType.Telephone)).Count() })
               .ToList();
            var filePath = "";
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                //create a new Worksheet
                ExcelWorksheet sheet = excelPackage.Workbook.Worksheets.Add("Contact Report");
                sheet.Cells["A1"].Value = "LOCATION";
                sheet.Cells["B1"].Value = "COUNT OF PERSON AT THIS LOCATION";
                sheet.Cells["C1"].Value = "COUNT OF PERSON PHONE NUMER REGISTERED AT THIS LOCATION";
                int index = 2;
                foreach (var item in locationPersonCount)
                {
                    sheet.Cells[$"A{index}"].Value = item.Location;
                    sheet.Cells[$"B{index}"].Value = item.Count;
                    sheet.Cells[$"C{index}"].Value = item.TelephoneCount;
                    index++;
                }

                var reportDirectory = $"{Environment.CurrentDirectory}\\Report\\";
                if (!Directory.Exists(reportDirectory))
                    Directory.CreateDirectory(reportDirectory);

                filePath = $"{reportDirectory}{reportUuid}.xlsx";
                //Write the file to the disk
                FileInfo fi = new FileInfo(filePath);
                excelPackage.SaveAs(fi);
            }
            #endregion

            #region Preapared

            var reportPreparedClient = clientFactory.CreateClient(EnpointNames.ReportAPIClient);

            var requestData = JsonSerializer.Serialize(new UpdateReportPathRequestModel()
            {
                Path = filePath,
            });
            var content = new StringContent(requestData, Encoding.UTF8, "application/json");
            var reportPreparedResponse = reportPreparedClient.Send(new HttpRequestMessage(HttpMethod.Post, $"/Report/{reportUuid}/UpdateReportPath") { Content = content }); ;
            if (!reportPreparedResponse.IsSuccessStatusCode)
                throw new RemoteServiceException();

            var completeNotificationClient = clientFactory.CreateClient(EnpointNames.ReportAPIClient);
            var completeNotificationResponse = completeNotificationClient.Send(new HttpRequestMessage(HttpMethod.Post, $"/Report/{reportUuid}/ChangeStatus/{Enums.ReportStatus.Prepared}"));
            if (!completeNotificationResponse.IsSuccessStatusCode)
                throw new RemoteServiceException();
            #endregion


        }
    }
}
