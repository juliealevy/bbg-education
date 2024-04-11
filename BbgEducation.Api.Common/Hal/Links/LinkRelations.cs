namespace BbgEducation.Api.Common.Hal.Links;

public static class LinkRelations
{
    public const string SELF = "self";
    public static class Authentication
    {
        private const string _namespace = "auth";
        public const string REGISTER = $"{_namespace}:register";
        public const string LOGIN = $"{_namespace}:login";
        public const string LOGOUT = $"{_namespace}:logout";
    }
    public static class Program
    {
        private const string _namespace = "program";
        public const string GET_ALL = $"{_namespace}:get-all";
        public const string GET_BY_ID = $"{_namespace}:get-by-id";
        public const string CREATE = $"{_namespace}:create";
        public const string UPDATE = $"{_namespace}:update";
    }

    public static class Session
    {
        private const string _namespace = "session";
        public const string GET_ALL = $"{_namespace}:get-all";
        public const string GET_BY_PROGRAM_ID = $"{_namespace}:get-by-program-id";
        public const string GET_BY_ID = $"{_namespace}:get-by-id";
        public const string CREATE = $"{_namespace}:create";
        public const string UPDATE = $"{_namespace}:update";
    }

    public static class Course
    {
        private const string _namespace = "course";
        public const string GET_ALL = $"{_namespace}:get-all";
        public const string GET_BY_ID = $"{_namespace}:get-by-id";
        public const string CREATE = $"{_namespace}:create";
        public const string UPDATE = $"{_namespace}:update";
    }
}
