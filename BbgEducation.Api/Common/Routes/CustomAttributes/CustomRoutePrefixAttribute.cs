namespace BbgEducation.Api.Common.Routes.CustomAttributes;

[AttributeUsage(AttributeTargets.Class)]
public class CustomRoutePrefixAttribute : Attribute
{
    public CustomRoutePrefixAttribute(string prefix)
    {
        Prefix = prefix;
    }

    public string Prefix { get; }
}
