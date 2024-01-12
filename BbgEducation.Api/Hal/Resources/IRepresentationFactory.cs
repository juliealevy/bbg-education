using BbgEducation.Api.Hal.Links;

namespace BbgEducation.Api.Hal.Resources;
public interface IRepresentationFactory
{
    IRepresentation NewRepresentation(HttpContext context);
    IRepresentation NewRepresentation(Link selfLink);
}