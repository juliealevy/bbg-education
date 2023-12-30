namespace BbgEducation.Api.Api;

public static class ApiRoutes
{
    public const string Root = "/api/";

    public static class Authentication
    {
        public const string AuthResource = "auth";
        public const string AuthRoot = Root + AuthResource;
        public const string Register = AuthRoot + "/register";
        public const string Login = AuthRoot + "/login";
    }

    public static class Programs
    {
        public const string ProgramsResource = "programs";
        public const string ProgramsRoot = Root + ProgramsResource;
        public const string GetAll = ProgramsRoot;
        public const string Create = ProgramsRoot;
        public const string Update = ProgramsRoot;
        public const string GetById = ProgramsRoot + "/{programId}";

    }
}
