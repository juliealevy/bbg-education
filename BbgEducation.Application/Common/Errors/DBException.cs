using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BbgEducation.Application.Common.Errors;
public class DBException: Exception
{
    public string Title { get; set; }
    
    public DBException (string title, string message, Exception innerException):base(message, innerException) {
        Title = title;
    }
}
