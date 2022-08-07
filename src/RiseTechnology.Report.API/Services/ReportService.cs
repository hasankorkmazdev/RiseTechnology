using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RiseTechnology.Common.DependencyInjectionsLifeCycles;
using RiseTechnology.Common.GenericRepository;
using RiseTechnology.Common.Models.Request;
using RiseTechnology.Common.Models.Response;
using RiseTechnology.Common.QueueMenager;
using RiseTechnology.Report.API.Context;
using RiseTechnology.Report.API.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace RiseTechnology.Report.API.Services
{
    public class ReportService : IReportService, ITransientLifetime
    {
        public readonly IHttpClientFactory clientFactory;
        public readonly IMessageBrokerService messageBrokerService;
        public readonly IRepositoryWrapper repositoryWrapper;
        public readonly IUnitOfWork unitOfWork;
        public readonly IMapper mapper;
        public ReportService(
            IHttpClientFactory clientFactory,
            IMessageBrokerService messageBrokerService,
            IRepositoryWrapper repositoryWrapper,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.clientFactory = clientFactory;
            this.messageBrokerService = messageBrokerService;
            this.unitOfWork = unitOfWork;
            this.repositoryWrapper = repositoryWrapper;
            this.mapper = mapper;
        }
        public async Task CreateReport()
        {
            var client = clientFactory.CreateClient(Common.Constant.ReportApiConstants.ContactAPIClient);
            var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, "GetList"));
            if (response.IsSuccessStatusCode)
            {
                var contactItems = JsonSerializer.Deserialize<List<PersonContactInformationResponseModel>>(await response.Content.ReadAsStringAsync());

                var reportUuid = Guid.NewGuid();
                messageBrokerService.Publish("ReportQueue", new CreateReportRabbitMqRequest() { ReportUUID = reportUuid, ContactItems = contactItems });
                repositoryWrapper.ReportRepository.Add(new Context.DbEntities.Report()
                {
                    UUID = reportUuid,
                    RequestDate = DateTime.Now,
                    Status = Common.Enums.ReportStatus.Pending
                });
                await unitOfWork.SaveChangesAsync();

            }
        }

        public async Task<List<ReportResponseModel>> GetReports()
        {
            var reportEntity = await repositoryWrapper.ReportRepository.GetQuery().ToListAsync();
            var mappedData = mapper.Map<List<ReportResponseModel>>(reportEntity);
            return mappedData;

        }
    }
}
