using BbgEducation.Domain.BbgSessionDomain;

namespace BbgEducation.Application.Common.Interfaces.Persistance;
public interface ISessionRepository
{
    Task AddSession(Session sessionToAdd);
    Task<IEnumerable<Session>> GetAllFullSessions(bool includeInactive = false);
    Task<IEnumerable<Session>> GetAllSessions(bool includeInactive = false);
    Task<Session> GetSession(string sessionID);
    Task InactivateSession(int sessionID);
    Task UpdateSession(Session sessionToUpdate);
}