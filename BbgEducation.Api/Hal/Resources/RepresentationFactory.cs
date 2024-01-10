using BbgEducation.Api.Hal.Links;

namespace BbgEducation.Api.Hal.Resources;

public static class RepresentationFactory
{
    public const string HAL_JSON = "application/hal+json";
    public static Representation NewRepresentation(HttpContext context)
    {

        var selfLink = new Link(LinkRelations.SELF, context.Request.Path.Value!, context.Request.Method);
        var representation = new Representation(selfLink);

        return representation;
    }


    public static Representation NewRepresentation(Link selfLink)
    {

        selfLink.Rel = LinkRelations.SELF;
        var representation = new Representation(selfLink);

        return representation;
    }
}
