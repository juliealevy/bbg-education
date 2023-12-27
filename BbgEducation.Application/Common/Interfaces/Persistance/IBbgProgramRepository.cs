using BbgEducation.Domain.BbgProgramDomain;

namespace BbgEducation.Application.Common.Interfaces.Persistance;
public interface IBbgProgramRepository
{
    Task<BbgProgram> AddProgram(BbgProgram entity);
    Task DeleteProgram(BbgProgram entity);
    Task DeleteAllPrograms();
    Task<BbgProgram> GetProgramByIdAsync(string id);
    Task<IEnumerable<BbgProgram>> GetProgramsAsync();
    Task<BbgProgram> UpdateProgram(BbgProgram entity);
}