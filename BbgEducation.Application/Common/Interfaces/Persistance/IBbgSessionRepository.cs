using BbgEducation.Domain.BbgSessionDomain;

namespace BbgEducation.Application.Common.Interfaces.Persistance;
public interface IBbgSessionRepository
{
    Task<BbgSession> AddSession(int programId, string sessionName, string description,
        DateOnly startDate, DateOnly endDate);
    Task<BbgSession> UpdateSession(BbgSession sessionToUpdate);

    Task<IEnumerable<BbgSession>> GetSessionsByProgramId(int programId, bool includeInactive = false);
    Task<IEnumerable<BbgSession>> GetAllSessions(bool includeInactive = false);
    Task<BbgSession> GetSessionById(int sessionID);

    Task<bool> CheckSessionNameExistsAsync(string name);
    Task InactivateSession(int sessionID);
   
}