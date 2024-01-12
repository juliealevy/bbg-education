using BbgEducation.Api.Common.Hal.Links;

namespace BbgEducation.Api.Hal;

public class RepresentationFactory : IRepresentationFactory
{
    public const string HAL_JSON = "application/hal+json";
    public IRepresentation NewRepresentation(HttpContext context)
    {

        var selfLink = new Link(LinkRelations.SELF, context.Request.Path.Value!, context.Request.Method);
        var representation = new BbgRepresentation(selfLink);

        return representation;
    }


    public IRepresentation NewRepresentation(Link selfLink)
    {

        selfLink.Rel = LinkRelations.SELF;
        var representation = new BbgRepresentation(selfLink);

        return representation;
    }
}
