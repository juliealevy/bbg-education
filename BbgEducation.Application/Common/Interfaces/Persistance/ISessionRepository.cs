using BbgEducation.Domain.BbgSessionDomain;

namespace BbgEducation.Application.Common.Interfaces.Persistance;
public interface ISessionRepository
{
    Task<BbgSession> AddSession(int programId, string sessionName, string description,
        DateOnly startDate, DateOnly endDate);

    Task<BbgSession> UpdateSession(BbgSession sessionToUpdate);
    Task<IEnumerable<BbgSession>> GetAllSessions(bool includeInactive = false);
    Task<BbgSession> GetSessionById(int sessionID);

    Task<bool> CheckSessionNameExistsAsync(string name);
    Task InactivateSession(int sessionID);
   
}