using BbgEducation.Application.Common.Interfaces.Persistance;
using BbgEducation.Domain.CourseDomain;
using BbgEducation.Infrastructure.Persistance.Common;
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
    protected override string GetAllStoredProc => DbConstants.StoredProcedures.Course.GET_ALL;

    protected override string GetByIDStoredProc => DbConstants.StoredProcedures.Course.GET_BY_ID;

    protected override string GetNameExistsStoredProc => DbConstants.StoredProcedures.Course.NAME_EXISTS;

    protected override string AddUpdateStoredProc => DbConstants.StoredProcedures.Course.ADD_UPDATE;

    public int AddCourse(string name, string description, bool isPublic) {
        int newId = Add(CourseEntity.CreateNew(name, description, isPublic));
        return newId;
    }

    public async Task<bool> CheckCourseNameExistsAsync(string name, CancellationToken cancellationToken) {
       return await CheckNameExistsAsync(name, cancellationToken);
    }

    public async Task<CourseEntity> GetCourseByIdAsync(int newCourseId, CancellationToken cancellationToken) {
        return await GetByIdAsync(newCourseId, cancellationToken);
    }
    protected override DynamicParameters BuildAddUpdateParams(CourseEntity entity) {
        var inputParams = new DynamicParameters();
        if (entity is null) {
            throw new Exception("course input cannot be null");
        }

        if (!entity.isNew()) {
            inputParams.Add("@id", entity.course_id);
        }
        inputParams.Add("@course_name", entity.course_name);
        inputParams.Add("@description", entity.description);
        inputParams.Add("@is_public", entity.is_public);

        return inputParams;
    }

    protected override DynamicParameters BuildCheckNameExistsParam(string name) {
        var inputParams = new DynamicParameters();
        inputParams.Add("@course_name", name);
        return inputParams;
    }

    protected override DynamicParameters BuildGetByIdParam(int id) {
        var inputParams = new DynamicParameters();
        inputParams.Add("@id", id);
        return inputParams;
    }
}
