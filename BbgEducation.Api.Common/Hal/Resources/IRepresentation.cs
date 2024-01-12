using BbgEducation.Api.Common.Hal.Links;
using System.Text.Json.Serialization;

namespace BbgEducation.Api.Common.Hal.Resources;
public interface IRepresentation
{
    IDictionary<string, List<IRepresentation>>? _embedded { get; }
    IDictionary<string, List<Link>> _links { get; }

    [JsonExtensionData]  //excludes "Properties" from json output... can only use on one IDictionary in a class    
    IDictionary<string, object?>? Properties { get; }

    IRepresentation WithLink(Link link);
    IRepresentation WithLink(string rel, string href, string method);
    IRepresentation WithLink(string rel, string href, string method, object? requestBody);
    IRepresentation WithObject(object value);
    IRepresentation WithProperty(string name, object value);
    IRepresentation WithRepresentation(string name, IRepresentation representation);
}