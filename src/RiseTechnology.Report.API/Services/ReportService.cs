using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiseTechnology.Common;
using RiseTechnology.Common.DependencyInjectionsLifeCycles;
using RiseTechnology.Common.GenericRepository;
using RiseTechnology.Common.Models.Base;
using RiseTechnology.Common.Models.Request;
using RiseTechnology.Common.Models.Response;
using RiseTechnology.Common.QueueMenager;
using RiseTechnology.Common.Tools.Exceptions;
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

        public async Task<ServiceResultModel> ChangeStatus(Guid reportUuid, Enums.ReportStatus status)
        {
            if (!await repositoryWrapper.ReportRepository.GetQuery().AnyAsync(x => x.UUID == reportUuid))
                throw new BadRequestException($"reportUuid:{reportUuid} not found please check reportUuid");

            var entity = await repositoryWrapper.ReportRepository.GetQuery().Where(x => x.UUID == reportUuid).FirstOrDefaultAsync();
            entity.Status = status;
            repositoryWrapper.ReportRepository.Update(entity);
            await unitOfWork.SaveChangesAsync();
            return new ServiceResultModel();
        }

        public async Task<ServiceResultModel> CreateReport()
        {
            var entity = new Context.DbEntities.Report()
            {
                UUID = Guid.NewGuid(),
                RequestDate = DateTime.Now,
                Status = Enums.ReportStatus.Pending,

            };
            messageBrokerService.Publish("ReportQueue", entity.UUID);
            repositoryWrapper.ReportRepository.Add(entity);
            await unitOfWork.SaveChangesAsync();
            return new ServiceResultModel(entity);
        }

        public async Task<ServiceResultModel> GetReports()
        {
            var reportEntity = await repositoryWrapper.ReportRepository.GetQuery().ToListAsync();
            var mappedData = mapper.Map<List<ReportResponseModel>>(reportEntity);
            return new ServiceResultModel(mappedData);
        }

        public async Task<ServiceResultModel> UpdateReportPath(Guid reportUuid, UpdateReportPathRequestModel model)
        {
            if (!await repositoryWrapper.ReportRepository.GetQuery().AnyAsync(x => x.UUID == reportUuid))
                throw new BadRequestException($"reportUuid:{reportUuid} not found please check reportUuid");

            var entity = await repositoryWrapper.ReportRepository.GetQuery().Where(x => x.UUID == reportUuid).FirstOrDefaultAsync();
            entity.XLSXPath = model.Path;
            repositoryWrapper.ReportRepository.Update(entity);
            await unitOfWork.SaveChangesAsync();
            return new ServiceResultModel(entity);

        }
    }
}
