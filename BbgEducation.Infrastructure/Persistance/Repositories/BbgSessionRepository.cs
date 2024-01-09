using BbgEducation.Application.Common.Interfaces.Persistance;
using BbgEducation.Domain.BbgProgramDomain;
using BbgEducation.Domain.BbgSessionDomain;
using BbgEducation.Domain.UserDomain;
using BbgEducation.Infrastructure.Persistance.Common;
using BbgEducation.Infrastructure.Persistance.Connections;
using Dapper;
using MediatR;

namespace BbgEducation.Infrastructure.Persistance.Repositories;
public class BbgSessionRepository : GenericRepository<BbgSession>, IBbgSessionRepository
{
    protected override string GetAllStoredProc => DbConstants.StoredProcedures.Session.GET_ALL;

    protected override string GetByIDStoredProc => DbConstants.StoredProcedures.Session.GET_BY_ID;

    protected override string AddUpdateStoredProc => DbConstants.StoredProcedures.Session.ADD_UPDATE;

    protected override string GetNameExistsStoredProc => DbConstants.StoredProcedures.Session.NAME_EXISTS;

    public BbgSessionRepository(ISQLConnectionFactory connectionFactory) : base(connectionFactory) {

    }


    public async Task<IEnumerable<BbgSession>> GetSessionsByProgramId(int programId, bool includeInactive = false) {
        return await GetAllAsync<BbgSession, BbgProgram, User, BbgSession, dynamic>("program_id", "user_id",
            (s, p, u) =>
            {
                s.session_program = p;
                s.inactivated_user = u;
                return s;
            }, new {
                IncludeInactive = includeInactive,
                ProgramId = programId
            }
        );
    }

    public async Task<IEnumerable<BbgSession>> GetAllSessions(bool includeInactive = false) {
        return await GetAllAsync<BbgSession, BbgProgram, User, BbgSession, dynamic>("program_id", "user_id",
            (s, p, u) =>
            {
                s.session_program = p;
                s.inactivated_user = u;
                return s;
            }, new { IncludeInactive = includeInactive });

    }

    public async Task<BbgSession> GetSessionById(int sessionID) {
        return await GetByIdAsync<BbgSession, BbgProgram, User, BbgSession>(sessionID, "program_id", "user_id",
            (s, p, u) =>
            {
                s.session_program = p;
                s.inactivated_user = u;
                return s;
            });
    } 

    public async Task<BbgSession> AddSession(int programId, string sessionName, string description, 
        DateOnly startDate, DateOnly endDate) {

        var sessionToAdd = BbgSession.Create(programId, sessionName, description,
            startDate.ToDateTime(TimeOnly.Parse("12:00 AM")), endDate.ToDateTime(TimeOnly.Parse("12:00 AM")));
        var newId = Add(sessionToAdd);
        return await GetSessionById(newId);
    }

    public async Task<BbgSession> UpdateSession(BbgSession sessionToUpdate) {
        Update(sessionToUpdate);
        return await GetSessionById((int)sessionToUpdate.session_id!);

    }

    public async Task<bool> CheckSessionNameExistsAsync(string name) {
        return await CheckNameExistsAsync(name);
    }

    public Task InactivateSession(int sessionID) {
        //later when i figure out users/authentication
        throw new NotImplementedException();
    }

    private DynamicParameters BuildSessionParams(BbgSession session) {
        var inputParams = new DynamicParameters();
        if (session is null) {
            throw new Exception("Session input cannot be null");
        }

        if (!session.isNew()) {
            inputParams.Add("@id", session.session_id);
        }
        inputParams.Add("@program_id", session.session_program.program_id);
        inputParams.Add("@name", session.session_name);
        inputParams.Add("@description", session.description);
        inputParams.Add("@start", session.start_date);
        inputParams.Add("@end", session.end_date);

        return inputParams;
    }

    protected override DynamicParameters BuildAddUpdateParams(BbgSession entity) {
        var inputParams = new DynamicParameters();
        if (entity is null) {
            throw new Exception("Session input cannot be null");
        }

        if (!entity.isNew()) {
            inputParams.Add("@id", entity.session_id);
        }
        inputParams.Add("@program_id", entity.session_program.program_id);
        inputParams.Add("@name", entity.session_name);
        inputParams.Add("@description", entity.description);
        inputParams.Add("@start", entity.start_date);
        inputParams.Add("@end", entity.end_date);

        return inputParams;
    }

    protected override DynamicParameters BuildGetByIdParam(int id) {
        var inputParams = new DynamicParameters();
        inputParams.Add("@id", id);
        return inputParams;
    }

    protected override DynamicParameters BuildCheckNameExistsParam(string name) {
        var inputParams = new DynamicParameters();
        inputParams.Add("@session_name", name);
        return inputParams;
    }   

    private DynamicParameters BuildByProgramIdParams(int programId, bool includeInactive) {
        var inputParams = new DynamicParameters();
        inputParams.Add("@includeInactive", includeInactive);
        inputParams.Add("@program_id", programId);
        
        return inputParams;
    }
}
