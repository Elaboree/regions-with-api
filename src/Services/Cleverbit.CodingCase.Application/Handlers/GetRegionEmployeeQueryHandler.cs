using AutoMapper;
using AutoMapper.QueryableExtensions;
using Cleverbit.CodingCase.Application.Constants;
using Cleverbit.CodingCase.Application.Handlers.Base;
using Cleverbit.CodingCase.Application.Models;
using Cleverbit.CodingCase.Application.Requests.Queries;
using Cleverbit.CodingCase.Application.Responses;
using Cleverbit.CodingCase.Application.Services.Abstract;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Cleverbit.CodingCase.Application.Handlers;
public class GetRegionEmployeeQueryHandler : BaseHandler<GetRegionEmployeeQuery, GetRegionEmployeeQueryHandler>
{
    private readonly IRegionEmployeeService regionEmployeeService;
    private readonly IMapper mapper;
    private readonly ILogger<GetRegionEmployeeQueryHandler> logger;

    public GetRegionEmployeeQueryHandler(ILogger<GetRegionEmployeeQueryHandler> logger, IRegionEmployeeService regionEmployeeService, IMapper mapper) : base(logger)
    {
        this.logger = logger;
        this.mapper = mapper;
        this.regionEmployeeService = regionEmployeeService;
    }
    public async override Task<ServiceResponse> Handle(GetRegionEmployeeQuery request, CancellationToken cancellationToken)
    {
        var employees = await regionEmployeeService.GetEmployeesInRegionAndDescendants(request.RegionId);

        if (employees is null) return ServiceResponse.CreateError(ResponseMessage.RegionEmployeeNotFound, (int)HttpStatusCode.NotFound);

        var employeesInRegion = employees.AsQueryable()
                                         .ProjectTo<RegionEmployeeViewModel>(mapper.ConfigurationProvider)
                                         .ToList();

        Logger.LogInformation($"GetRegionEmployeeQuery.Handle - {ResponseMessage.AllRegionEmployeesFetched}");
        return ServiceResponse.CreateSuccess(ResponseMessage.OK, employeesInRegion);
    }
}