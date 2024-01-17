using BbgEducation.Domain.BbgProgramDomain;

namespace BbgEducation.Application.Common.Interfaces.Persistance;
public interface IBbgProgramRepository
{
    int AddProgram(string name, string description);
    void UpdateProgram(BbgProgram entity);
    void DeleteProgram(BbgProgram entity);
    void DeleteAllPrograms();
    Task<BbgProgram> GetProgramByIdAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<BbgProgram>> GetProgramsAsync(CancellationToken cancellationToken);
    Task<bool> CheckProgramNameExistsAsync(string name, CancellationToken cancellationToken);
    

}