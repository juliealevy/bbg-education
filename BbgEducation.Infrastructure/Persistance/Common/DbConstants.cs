namespace BbgEducation.Infrastructure.Persistance.Common;
public static class DbConstants
{
    private const string SCHEMA_PREFIX = "dbo.";

    public static class StoredProcedures
    {

        public const string PROGRAM_GET = SCHEMA_PREFIX + "spProgramGet";
        public const string PROGRAM_GET_ALL = SCHEMA_PREFIX + "spProgramGetAll";
        public const string PROGRAM_ADD_UPDATE = SCHEMA_PREFIX + "spProgramAddUpdate";
        public const string PROGRAM_INACTIVATE = SCHEMA_PREFIX + "spProgramInactivate";

        public const string SESSION_GET = SCHEMA_PREFIX + "spSessionGet";
        public const string SESSION_GET_ALL = SCHEMA_PREFIX + "spSessionGetAll";
        public const string SESSION_ADD_UPDATE = SCHEMA_PREFIX + "spSessionAddUpdate";
        public const string SESSION_INACTIVATE = SCHEMA_PREFIX + "spSessionInactivate";


    }



}
