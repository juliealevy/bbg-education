using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BbgEducation.Infrastructure.Persistance.Connections;
public interface ISQLConnectionFactory
{
    IDbConnection Create();
}
