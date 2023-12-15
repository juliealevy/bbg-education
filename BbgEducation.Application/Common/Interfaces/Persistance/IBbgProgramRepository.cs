using BbgEducation.Domain.BbgProgramDomain;

namespace BbgEducation.Application.Common.Interfaces.Persistance;
public interface IBbgProgramRepository
{
    Task AddProgram(BbgProgram entity);
    Task DeleteProgram(BbgProgram entity);
    Task DeleteAllPrograms();
    Task<BbgProgram> GetProgramById(int id);
    Task<IEnumerable<BbgProgram>> GetPrograms();
    Task UpdateProgram(BbgProgram entity);
}