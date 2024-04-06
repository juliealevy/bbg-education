using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BbgEducation.Application.Common.Interfaces.Persistance;
public interface ICourseRepository
{
    int AddCourse(string name, string description);
    Task<bool> CheckCourseNameExistsAsync(string name, CancellationToken value);
}
