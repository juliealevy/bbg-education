using BbgEducation.Api.Common.Hal.Links;
using BbgEducation.Api.Common.Hal.Resources;
using System.Text.Json.Serialization;

namespace Api.FunctionalTests.Common;
internal class TestRepresentation : BbgRepresentation
{

    //TODO:  add public getter/setters for other contents when/if necessary

    [JsonConstructor]
    public TestRepresentation() 
        : base(null) {

    }

    //creating public getter/setter so can be deserialized in tests
    public IDictionary<string, List<Link>> _links {
        get {
            return __links;
        }
        set {
            __links = value;
        }
    }
}
