using BbgEducation.Api.Common.Hal.Links;

namespace BbgEducation.Api.Common.Hal.Resources;
public interface IRepresentationFactory
{
    IRepresentation NewRepresentation(HttpContext context);
    IRepresentation NewRepresentation(Link selfLink);
}