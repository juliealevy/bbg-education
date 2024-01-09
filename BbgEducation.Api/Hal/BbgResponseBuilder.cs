using BbgEducation.Api.BbgPrograms.Response;
using BbgEducation.Api.BbgSessions;
using BbgEducation.Application.BbgPrograms.Common;
using MapsterMapper;

namespace BbgEducation.Api.Hal;

public abstract class BbgResponseBuilder<TResult, TResponse>: IBbgResponseBuilder<TResult, TResponse>
    where TResponse :   HalResponse, new()
{
    protected readonly IMapper _mapper;
    protected readonly IBbgLinkGenerator _linkGenerator;

    public BbgResponseBuilder(IMapper mapper, IBbgLinkGenerator linkGenerator)
    {
        _mapper = mapper;
        _linkGenerator = linkGenerator;
    }

    public abstract TResponse? Build(TResult resultData, HttpContext context, bool selfFromPath,
        bool addGetLinks, bool addCreateUpdateLinks); 

    protected abstract void AddGetLinks(TResponse response, HttpContext context);


    protected abstract void AddCreateUpdateLinks(TResponse response, HttpContext context);

    protected abstract void AddGetByIdSelfLink(TResponse response, HttpContext context);
}
