using BbgEducation.Domain.BbgProgramDomain;

namespace BbgEducation.Application.Common.Interfaces.Persistance;
public interface IBbgProgramRepository
{
    Task<BbgProgram> AddProgram(BbgProgram entity);
    Task<BbgProgram> UpdateProgram(BbgProgram entity);
    Task DeleteProgram(BbgProgram entity);
    Task DeleteAllPrograms();
    Task<BbgProgram> GetProgramByIdAsync(int id);
    Task<IEnumerable<BbgProgram>> GetProgramsAsync();
    Task<bool> CheckProgramNameExistsAsync(string name);
    
}