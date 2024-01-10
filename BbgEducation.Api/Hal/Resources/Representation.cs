using BbgEducation.Api.Hal.Links;
using BbgEducation.Application.Common.Validation;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text.Json.Serialization;

namespace BbgEducation.Api.Hal.Resources;

public class Representation
{
    private IDictionary<string, List<Link>> __links = new Dictionary<string, List<Link>>();
    private IDictionary<string, object?>? _properties = null;
    private IDictionary<string, List<Representation>>? _resources = null;   //list of representations
    public IDictionary<string, List<Link>> _links => __links;

    [JsonExtensionData]  //can only use on one IDictionary, has to contains objects, will prevent "Properties" from being included in JSON
    public IDictionary<string, object?>? Properties => _properties;
    public IDictionary<string, List<Representation>>? _embedded => _resources;


    public Representation(Link selfLink)
    {

        AddLink(selfLink);
    }

    public Representation WithLink(Link link)
    {
        AddLink(link);
        return this;
    }

    public Representation WithLink(string rel, string href, string method)
    {
        AddLink(new Link(rel, href, method));
        return this;
    }

    public Representation WithLink(string rel, string href, string method, object? requestBody)
    {
        AddLink(new Link(rel, href, method, requestBody));
        return this;
    }

    public Representation WithProperty(string name, object value)
    {
        AddProperty(name, value);
        return this;
    }

    public Representation WithObject(object value)
    {
        AddObject(value);
        return this;
    }

    public Representation WithRepresentation(string name, Representation representation)
    {
        AddRepresentation(name, representation);

        return this;
    }

    private void AddLink(Link? link)
    {
        if (link is null)
            return;

        if (__links is null)
        {
            __links = new Dictionary<string, List<Link>>();
        }
        if (!_links.ContainsKey(link.Rel))
        {
            __links.Add(link.Rel, new List<Link>());
        }
        __links[link.Rel].Add(link);
    }

    private void AddProperty(string name, object? value)
    {
        if (name.IsNullOrEmpty())
        {
            throw new HalRepresentationException("Representation property name must have a value");
        }
        if (_properties is null)
        {
            _properties = new Dictionary<string, object?>();
        }
        if (_properties.ContainsKey(name))
        {
            throw new HalRepresentationException($"Duplicate property {name} found for representation");
        }
        _properties.Add(name, value);

    }

    private void AddObject(object obj)
    {
        if (obj is null)
        {
            throw new HalRepresentationException("Representation object must have a value");
        }

        if (_properties is null)
        {
            _properties = new Dictionary<string, object?>();
        }
        Type objType = obj.GetType();

        List<PropertyInfo> props = new List<PropertyInfo>(objType.GetProperties());

        foreach (PropertyInfo prop in props)
        {
            object? propValue = prop.GetValue(obj, null);
            var propName = prop.Name.ToString().ToLower();
            AddProperty(propName, propValue);


        }
    }

    private void AddRepresentation(string name, Representation representation)
    {
        if (representation is null)
        {
            throw new HalRepresentationException("Representation must have a value");
        }
        if (name.IsNullOrEmpty())
        {
            throw new HalRepresentationException("Representation name must have a value");
        }

        if (_resources is null)
        {
            _resources = new Dictionary<string, List<Representation>>();
        }
        if (!_resources.ContainsKey(name))
        {
            _resources.Add(name, new List<Representation>());
        }
        _resources[name].Add(representation);
    }
}
