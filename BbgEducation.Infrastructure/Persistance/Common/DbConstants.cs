namespace BbgEducation.Infrastructure.Persistance.Common;
public static class DbConstants
{
    private const string SCHEMA_PREFIX = "dbo.";

    public static class StoredProcedures
    {

        public static class Program
        {
            public const string GET_BY_ID = SCHEMA_PREFIX + "spProgramGetById";
            public const string GET_ALL = SCHEMA_PREFIX + "spProgramGetAll";
            public const string ADD_UPDATE = SCHEMA_PREFIX + "spProgramAddUpdate";
            public const string INACTIVATE = SCHEMA_PREFIX + "spProgramInactivate";
            public const string NAME_EXISTS = SCHEMA_PREFIX + "spProgramNameExists";
        }

        public static class Session
        {
            public const string GET_BY_ID = SCHEMA_PREFIX + "spSessionGet";
            public const string GET_ALL = SCHEMA_PREFIX + "spSessionGetAll";
            public const string ADD_UPDATE = SCHEMA_PREFIX + "spSessionAddUpdate";
            public const string INACTIVATE = SCHEMA_PREFIX + "spSessionInactivate";
        }
    }



}
