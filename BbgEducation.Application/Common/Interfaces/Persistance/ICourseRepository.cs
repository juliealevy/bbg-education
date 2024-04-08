using BbgEducation.Domain.CourseDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BbgEducation.Application.Common.Interfaces.Persistance;
public interface ICourseRepository
{
    int AddCourse(string name, string description, bool isPublic);
    Task<bool> CheckCourseNameExistsAsync(string name, CancellationToken cancellationToken);
    Task<CourseEntity> GetCourseByIdAsync(int newCourseId, CancellationToken cancellationToken);
}
