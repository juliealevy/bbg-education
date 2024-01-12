using BbgEducation.Api.Common.Hal.Links;

namespace BbgEducation.Api.Hal;
public interface IRepresentationFactory
{
    IRepresentation NewRepresentation(HttpContext context);
    IRepresentation NewRepresentation(Link selfLink);
}