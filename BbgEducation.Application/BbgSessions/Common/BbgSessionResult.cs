using BbgEducation.Application.BbgPrograms.Common;

namespace BbgEducation.Application.BbgSessions.Common;
public class BbgSessionResult
{

    public BbgProgramResult Program { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }


    public BbgSessionResult(
        int programId,
        string programName,
        string programDescription,
        int id,
        string name,
        string description,
        DateOnly startDate,
        DateOnly endDate) :
            this(
                new BbgProgramResult(programId, programName, programDescription),
                id,
                name,
                description,
                startDate,
                endDate) { }

       

    public BbgSessionResult(
        BbgProgramResult program,     
      int id,
      string name,
      string description,
      DateOnly startDate,
      DateOnly endDate) {

        Program = program;
        Id = id;
        Name = name;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
    }

    public BbgSessionResult() {

    }
}
