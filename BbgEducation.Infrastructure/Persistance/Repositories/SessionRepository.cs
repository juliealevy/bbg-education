using BbgEducation.Application.Common.Interfaces.Persistance;
using BbgEducation.Domain.BbgProgramDomain;
using BbgEducation.Domain.BbgSessionDomain;
using BbgEducation.Domain.UserDomain;
using BbgEducation.Infrastructure.Persistance.Common;
using BbgEducation.Infrastructure.Persistance.Connections;
using Dapper;

namespace BbgEducation.Infrastructure.Persistance.Repositories;
public class SessionRepository : GenericRepository<Session>, ISessionRepository
{
    protected override string GetAllStoredProc => DbConstants.StoredProcedures.Session.GET_ALL;

    protected override string GetByIDStoredProc => DbConstants.StoredProcedures.Session.GET_BY_ID;

    protected override string AddUpdateStoredProc => DbConstants.StoredProcedures.Session.ADD_UPDATE;

    protected override string GetNameExistsStoredProc => throw new NotImplementedException();

    public SessionRepository(ISQLConnectionFactory connectionFactory) : base(connectionFactory) {

    }

    public Task<IEnumerable<Session>> GetAllSessions(bool includeInactive = false) {
        return GetAllAsync(includeInactive);

    }

    public Task<IEnumerable<Session>> GetAllFullSessions(bool includeInactive = false) {
        return GetAllAsync<Session, BbgProgram, User, Session, dynamic>("program_id", "user_id",
            (s, p, u) =>
            {
                s.session_program = p;
                s.inactivated_user = u;
                return s;
            }, new { IncludeInactive = includeInactive });


    }

    public Task<Session> GetSession(int sessionID) {
        return GetByIdAsync(sessionID);
    }

    public Task AddSession(Session sessionToAdd) {
        return Add(sessionToAdd);
    }

    public Task UpdateSession(Session sessionToUpdate) {
        return Update(sessionToUpdate);
    }

    public Task InactivateSession(int sessionID) {
        //later when i figure out users/authentication
        throw new NotImplementedException();
    }

    private DynamicParameters BuildSessionParams(Session session) {
        var inputParams = new DynamicParameters();
        if (session == null) {
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

    protected override DynamicParameters BuildAddUpdateParams(Session entity) {
        var inputParams = new DynamicParameters();
        if (entity == null) {
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
        throw new NotImplementedException();
    }
}
