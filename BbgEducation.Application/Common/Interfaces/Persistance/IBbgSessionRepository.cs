using BbgEducation.Domain.BbgSessionDomain;

namespace BbgEducation.Application.Common.Interfaces.Persistance;
public interface IBbgSessionRepository
{
    int AddSession(int programId, string sessionName, string description,DateOnly startDate, DateOnly endDate);
    void UpdateSession(BbgSession sessionToUpdate);
    void InactivateSession(int sessionID);
    Task<IEnumerable<BbgSession>> GetSessionsByProgramIdAsync(int programId, CancellationToken cancellationToken,bool includeInactive = false);
    Task<IEnumerable<BbgSession>> GetAllSessionsAsync(CancellationToken cancellationToken,bool includeInactive = false);
    Task<BbgSession> GetSessionByIdAsync(int sessionID, CancellationToken cancellationToken);
    Task<bool> CheckSessionNameExistsAsync(string name, CancellationToken cancellationToken);
   
   
}