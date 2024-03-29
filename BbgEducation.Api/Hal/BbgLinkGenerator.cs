﻿using BbgEducation.Api.Common.Hal.Links;
using BbgEducation.Api.Common.Routes;

namespace BbgEducation.Api.Hal
{
    public class BbgLinkGenerator : IBbgLinkGenerator
    {
        private readonly IApiRouteService _routeService;
        private readonly LinkGenerator _linkGenerator;
        public BbgLinkGenerator(IApiRouteService routeService, LinkGenerator linkGenerator) {
            _routeService = routeService;
            _linkGenerator = linkGenerator;
        }

        public Link? GetActionLink(HttpContext context, string linkRelation, Type module, string actionName, object? values) {
            var routeData = _routeService.GetRouteData(module, actionName);

            if (routeData is null) return null;

            var link = _linkGenerator.GetPathByAction(context, action: actionName,
               controller: routeData.ModuleName, values: values);

            if (link is null) return null;

            return new Link(linkRelation, link, routeData.HttpMethod);
        }

        public Link? GetApiLink(string linkRelation, Type module, string actionName, object? body) {
            var routeData = _routeService.GetRouteData(module, actionName);
            if (routeData is null) return null;

            return new Link(linkRelation, routeData.RouteTemplate, routeData.HttpMethod, body);
        }

        public ApiRouteData? GetSelfRouteData(HttpContext context) {
            return _routeService.GetSelfRouteData(context.Request.RouteValues);
        }

        public Link? GetSelfLink(HttpContext context) {
            var routeData = _routeService.GetSelfRouteData(context.Request.RouteValues);
            if (routeData is null) return null;

            return new Link(LinkRelations.SELF, context.Request.Path.Value!, routeData.HttpMethod);
        }
    }
}