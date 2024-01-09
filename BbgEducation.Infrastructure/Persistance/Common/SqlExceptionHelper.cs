using BbgEducation.Application.Common.Errors;
using System.Data.SqlClient;
using System.Text;

namespace BbgEducation.Infrastructure.Persistance.Common;
public static class SqlExceptionHelper
{
    public static DBException ToDBException(SqlException sqlEx) {

        StringBuilder errorMessages = new StringBuilder();
        for (int i = 0; i < sqlEx.Errors.Count; i++) {
            StringBuilder message = new StringBuilder();
            message.Append("Message: " + sqlEx.Errors[i].Message)
                .Append(" LineNumber: " + sqlEx.Errors[i].LineNumber)
                .Append(" Source: " + sqlEx.Errors[i].Source)
                .Append(" Procedure: " + sqlEx.Errors[i].Procedure)
                .Append(" Server: " + sqlEx.Errors[i].Server);
            errorMessages.AppendLine(message.ToString());
        }

        return new DBException(sqlEx.Message, errorMessages.ToString(), sqlEx);
        

    }
}
