using BbgEducation.Domain.BbgProgramDomain;
using BbgEducation.Domain.Common;
using BbgEducation.Domain.UserDomain;

namespace BbgEducation.Domain.BbgSessionDomain;

public sealed class Session: Entity
{
    public int? session_id { get; private set; }
    public string session_name { get; private set; } = string.Empty;
    public string description { get; private set; } = string.Empty;
    public DateTime start_date { get; private set; }
    public DateTime end_date { get; private set; }
    public BbgProgram session_program { get; set; } 

    public override bool isNew() {
        return session_id == null || session_id <= 0;
    }

    

}
