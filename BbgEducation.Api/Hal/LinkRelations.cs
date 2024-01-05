namespace BbgEducation.Api.Hal;

public static class LinkRelations
{
    public static partial class Authentication
    {
        private const string _namespace = "auth";
        public const string REGISTER = $"{_namespace}:register";
        public const string LOGIN = $"{_namespace}:login";
        public const string LOGOUT = $"{_namespace}:logout";
    }
    public static partial class Program
    {
        private const string _namespace = "program";
        public const string GET_ALL = $"{_namespace}:get-all";
        public const string GET_BY_ID = $"{_namespace}:get-by-id";
        public const string CREATE = $"{_namespace}:create";
        public const string UPDATE = $"{_namespace}:update";
    }
}
