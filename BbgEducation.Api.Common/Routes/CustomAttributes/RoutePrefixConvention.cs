﻿
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc;

namespace BbgEducation.Api.Common.Routes.CustomAttributes;

public class RoutePrefixConvention : IControllerModelConvention
{
    public void Apply(ControllerModel controller)
    {
        foreach (var selector in controller.Selectors)
        {
            var prefixes = GetPrefixes(controller.ControllerType);  // [prefix, parentPrefix, grandpaPrefix,...]
            if (prefixes.Count == 0) continue;
            // combine these prefixes one by one
            var prefixRouteModels = prefixes.Select(p => new AttributeRouteModel(new RouteAttribute(p.Prefix)))
                .Aggregate((acc, prefix) => AttributeRouteModel.CombineAttributeRouteModel(prefix, acc));
            selector.AttributeRouteModel = selector.AttributeRouteModel != null ?
                AttributeRouteModel.CombineAttributeRouteModel(prefixRouteModels, selector.AttributeRouteModel) :
                selector.AttributeRouteModel = prefixRouteModels;
        }
    }

    private IList<CustomRoutePrefixAttribute> GetPrefixes(Type controlerType)
    {
        var list = new List<CustomRoutePrefixAttribute>();
        FindPrefixesRec(controlerType, ref list);
        list = list.Where(r => r != null).ToList();
        return list;

        // find [MyRoutePrefixAttribute('...')] recursively 
        void FindPrefixesRec(Type type, ref List<CustomRoutePrefixAttribute> results)
        {
            var prefix = type.GetCustomAttributes(false).OfType<CustomRoutePrefixAttribute>().FirstOrDefault();
            results.Add(prefix);   // null is valid because it will seek prefix from parent recursively
            var parentType = type.BaseType;
            if (parentType == null) return;
            FindPrefixesRec(parentType, ref results);
        }
    }
}
