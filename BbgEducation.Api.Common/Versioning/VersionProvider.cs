namespace BbgEducation.Api.Common.Versioning;

public class VersionProvider : IVersionProvider
{
    //todo, read from db??
    private readonly string _currentVersion = "0.0.1";
    public string Get() => _currentVersion;
}
