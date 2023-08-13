using AutoMapper;
using System.Net;
using Microsoft.Extensions.Logging;
using Cleverbit.CodingCase.Application.Constants;
using Cleverbit.CodingCase.Application.Requests;
using Cleverbit.CodingCase.Application.Responses;
using Cleverbit.CodingCase.Application.Handlers.Base;
using Cleverbit.CodingCase.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using Cleverbit.CodingCase.Application.Models;

namespace Cleverbit.CodingCase.Application.Handlers;

public class GetEmployeeQueryHandler : BaseHandler<BaseRequest, GetEmployeeQueryHandler>
{
    private readonly IEmployeeRepository repository;
    private readonly IMapper mapper;
    private readonly ILogger<GetEmployeeQueryHandler> logger;

    public GetEmployeeQueryHandler(ILogger<GetEmployeeQueryHandler> logger, IEmployeeRepository repository, IMapper mapper) : base(logger)
    {
        this.logger = logger;
        this.repository = repository;
        this.mapper = mapper;
    }

    public async override Task<ServiceResponse> Handle(BaseRequest request, CancellationToken cancellationToken)
    {
        var query = repository.AsQueryable().Include(i => i.Region);

        var employees = await query.ProjectTo<EmployeeViewModel>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        if (employees is null || !employees.Any())
        {
            LogError(request.RequestId, ResponseMessage.EmployeeNotFound);
            return ServiceResponse.CreateError(ResponseMessage.EmployeeNotFound, (int)HttpStatusCode.NoContent);
        }

        Logger.LogInformation($"GetEmployeeQueryHandler.Handle - {ResponseMessage.GetEmployees}");
        return ServiceResponse.CreateSuccess(ResponseMessage.OK, employees);
    }
}