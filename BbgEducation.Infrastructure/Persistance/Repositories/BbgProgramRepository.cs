﻿using BbgEducation.Application.Common.Interfaces.Persistance;
using BbgEducation.Domain.BbgProgramDomain;
using BbgEducation.Infrastructure.Persistance.Common;
using BbgEducation.Infrastructure.Persistance.Connections;
using Dapper;

namespace BbgEducation.Infrastructure.Persistance.Repositories;
public class BbgProgramRepository : GenericRepository<BbgProgram>, IBbgProgramRepository
{

    protected override string GetAllStoredProc => DbConstants.StoredProcedures.PROGRAM_GET_ALL;

    protected override string GetByIDStoredProc => DbConstants.StoredProcedures.PROGRAM_GET;

    protected override string AddUpdateStoredProc => DbConstants.StoredProcedures.PROGRAM_ADD_UPDATE;

    public BbgProgramRepository(IConnectionProvider connectionProvider) : base(connectionProvider) {

    }

    public Task<IEnumerable<BbgProgram>> GetPrograms() {
        return GetAll();
    }

    public Task<BbgProgram> GetProgramById(int id) {
        return GetById(id);
    }

    public Task AddProgram(BbgProgram entity) {
        return Add(entity);
    }

    public Task UpdateProgram(BbgProgram entity) {
        return Update(entity);
    }

    public Task DeleteProgram(BbgProgram entity) {
        throw new NotImplementedException();
    }

    public Task DeleteAllPrograms() {
        throw new NotImplementedException();
    }



    protected override DynamicParameters BuildAddUpdateParams(BbgProgram entity) {
        var inputParams = new DynamicParameters();
        if (entity == null) {
            throw new Exception("program input cannot be null");
        }

        if (!entity.isNew()) {
            inputParams.Add("@id", entity.program_id);
        }
        inputParams.Add("@program_name", entity.program_name);
        inputParams.Add("@description", entity.description);

        return inputParams;
    }

    protected override DynamicParameters BuildGetByIdParam(int id) {
        var inputParams = new DynamicParameters();
        inputParams.Add("@id", id);
        return inputParams;
    }
}