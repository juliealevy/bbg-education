using BbgEducation.Api.Authentication;

namespace BbgEducation.Api.Links;

public record Link (string Rel, string Href, string Method, object? Body = null);

