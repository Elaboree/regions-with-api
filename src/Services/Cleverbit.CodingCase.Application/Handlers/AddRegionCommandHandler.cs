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
public class AddRegionCommandHandler : BaseHandler<AddRegionCommand, AddRegionCommandHandler>
{
    private readonly IMapper mapper;
    private readonly IRegionRepository regionRepository;
    private readonly ILogger<AddRegionCommandHandler> logger;
    public AddRegionCommandHandler(
        IMapper mapper,
        ILogger<AddRegionCommandHandler> logger,
        IRegionRepository regionRepository) : base(logger)
    {
        this.mapper = mapper;
        this.logger = logger;
        this.regionRepository = regionRepository;
    }
    public async override Task<ServiceResponse> Handle(AddRegionCommand request, CancellationToken cancellationToken)
    {
        var region = mapper.Map<Region>(request);

        var isParentRegionExists = regionRepository.Get(x => x.Id == region.ParentId.Value).FirstOrDefault();
        if (isParentRegionExists is null) return ServiceResponse.CreateError(ResponseMessage.RegionParentNotFound, (int)HttpStatusCode.NotFound);

        await regionRepository.AddAsync(region);

        Logger.LogInformation($"AddRegionCommandHandler.Handle - {ResponseMessage.RegionAdded}");
        return ServiceResponse.CreateSuccess(ResponseMessage.OK, null);
    }
}

