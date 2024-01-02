using BbgEducation.Application.Common.Interfaces.Persistance;
using BbgEducation.Domain.BbgProgramDomain;
using BbgEducation.Infrastructure.Persistance.Common;
using BbgEducation.Infrastructure.Persistance.Connections;
using Dapper;

namespace BbgEducation.Infrastructure.Persistance.Repositories;
public class BbgProgramRepository : GenericRepository<BbgProgram>, IBbgProgramRepository
{

    protected override string GetAllStoredProc => DbConstants.StoredProcedures.Program.GET_ALL;

    protected override string GetByIDStoredProc => DbConstants.StoredProcedures.Program.GET_BY_ID;

    protected override string AddUpdateStoredProc => DbConstants.StoredProcedures.Program.ADD_UPDATE;

    protected override string GetNameExistsStoredProc => DbConstants.StoredProcedures.Program.NAME_EXISTS;

    public BbgProgramRepository(ISQLConnectionFactory sqlConnectionFactory) : base(sqlConnectionFactory) {

    }

    public async Task<IEnumerable<BbgProgram>> GetProgramsAsync() {
        return await GetAllAsync();
    }

    public async Task<BbgProgram> GetProgramByIdAsync(int id) {
        return await GetByIdAsync(id);
    }

    public async Task<bool> CheckProgramNameExistsAsync(string name) {
        return await CheckNameExistsAsync(name);
    }

    public Task<BbgProgram> AddProgram(string name, string description) {
        return Add(BbgProgram.CreateNew(name, description));
    }

    public Task<BbgProgram> UpdateProgram(BbgProgram entity) {
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

    protected override DynamicParameters BuildCheckNameExistsParam(string name) {
        var inputParams = new DynamicParameters();
        inputParams.Add("@program_name", name);
        return inputParams;
    }


}
