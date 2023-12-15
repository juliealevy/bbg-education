using BbgEducation.Domain.BbgProgramDomain;
using BbgEducation.Domain.UserDomain;

namespace BbgEducation.Domain.BbgSessionDomain;

public sealed class Session
{
    public int? session_id { get; set; }
    public string session_name { get; set; } = string.Empty;
    public string description { get; set; } = string.Empty;
    public DateTime start_date { get; set; }
    public DateTime end_date { get; set; }
    public DateTime? inactivated_datetime { get; set; }
    public User? inactivated_user { get; set; }
    public BbgProgram session_program { get; set; } = new BbgProgram();

    public bool isNew() {
        return session_id == null || session_id <= 0;
    }

}
