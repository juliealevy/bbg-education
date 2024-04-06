using BbgEducation.Application.Common.Interfaces.Persistance;
using BbgEducation.Domain.CourseDomain;
using BbgEducation.Infrastructure.Persistance.Connections;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BbgEducation.Infrastructure.Persistance.Repositories;
public class CourseRepository : GenericRepository<CourseEntity>, ICourseRepository
{

    public CourseRepository(ISQLConnectionFactory connectionFactory) : base(connectionFactory) 
    {
        
    }
    protected override string GetAllStoredProc => throw new NotImplementedException();

    protected override string GetByIDStoredProc => throw new NotImplementedException();

    protected override string GetNameExistsStoredProc => throw new NotImplementedException();

    protected override string AddUpdateStoredProc => throw new NotImplementedException();

    public int AddCourse(string name, string description) {
        throw new NotImplementedException();
    }

    public Task<bool> CheckCourseNameExistsAsync(string name, CancellationToken value) {
        throw new NotImplementedException();
    }

    protected override DynamicParameters BuildAddUpdateParams(CourseEntity entity) {
        throw new NotImplementedException();
    }

    protected override DynamicParameters BuildCheckNameExistsParam(string name) {
        throw new NotImplementedException();
    }

    protected override DynamicParameters BuildGetByIdParam(int id) {
        throw new NotImplementedException();
    }
}
