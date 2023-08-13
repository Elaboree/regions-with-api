using AutoMapper;
using Cleverbit.CodingCase.Application.Constants;
using Cleverbit.CodingCase.Application.Handlers.Base;
using Cleverbit.CodingCase.Application.Interfaces.Repositories;
using Cleverbit.CodingCase.Application.Requests.Commands;
using Cleverbit.CodingCase.Application.Responses;
using Cleverbit.CodingCase.Domain.Models;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Cleverbit.CodingCase.Application.Handlers;
public class AddEmployeeCommandHandler : BaseHandler<AddEmployeeCommand, AddEmployeeCommandHandler>
{
    private readonly IMapper mapper;
    private readonly IEmployeeRepository employeeRepository;
    private readonly ILogger<AddEmployeeCommandHandler> logger;
    private readonly IRegionRepository regionRepository;
    public AddEmployeeCommandHandler(
        IMapper mapper,
        ILogger<AddEmployeeCommandHandler> logger,
        IEmployeeRepository employeeRepository,
        IRegionRepository regionRepository) : base(logger)
    {
        this.mapper = mapper;
        this.logger = logger;
        this.employeeRepository = employeeRepository;
        this.regionRepository = regionRepository;
    }
    public async override Task<ServiceResponse> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = mapper.Map<Employee>(request);

        var isRegionExists = await regionRepository.GetByIdAsync(employee.RegionId);
        if (isRegionExists is null) return ServiceResponse.CreateError(ResponseMessage.RegionNotFound, (int)HttpStatusCode.NotFound);

        await employeeRepository.AddAsync(employee);

        Logger.LogInformation($"AddEmployeeCommandHandler.Handle - {ResponseMessage.EmployeeAdded}");
        return ServiceResponse.CreateSuccess(ResponseMessage.OK, null);
    }
}

