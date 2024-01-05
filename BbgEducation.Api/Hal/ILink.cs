namespace BbgEducation.Api.Hal;

public interface ILink
{
    string Href { get; set; }
    string Method { get; set; }
    string Rel { get; set; }
    bool Templated { get; set; }
    object? Body { get; set; }
}