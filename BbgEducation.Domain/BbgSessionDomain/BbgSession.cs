using BbgEducation.Domain.BbgProgramDomain;
using BbgEducation.Domain.Common;
using BbgEducation.Domain.UserDomain;

namespace BbgEducation.Domain.BbgSessionDomain;

public sealed class BbgSession: Entity
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

    private BbgSession(
        int? sessionId,
        string sessionName,
        string description,
        DateTime startDate,
        DateTime endDate,
        BbgProgram program,
        DateTime? createdDateTime,
        DateTime? updatedDateTime) {

        this.session_id = session_id;
        this.session_name = sessionName.Trim();
        this.description = description.Trim();
        this.start_date = startDate;
        this.end_date = endDate;
        this.session_program = program;
        
    }

    public static BbgSession Build(
       int sessionId,
       string sessionName,
       string description,
       DateTime startDate,
       DateTime endDate,
       BbgProgram program,
       DateTime createdDate,
       DateTime updatedDate) {

        return new BbgSession(
            sessionId,
            sessionName,
            description,
            startDate,
            endDate,
            program,
            createdDate,
            updatedDate);  
    }

    public static BbgSession Create(
        int programId,
        string sessionName,
        string description,
        DateTime startDate,
        DateTime endDate) {

        return new BbgSession(
            null,
            sessionName,
            description,
            startDate,
            endDate,
            BbgProgram.Create(programId,"", ""),
            null, 
            null);  
    }
    private BbgSession() {

    }

    public override bool Equals(object? obj) {
        return obj is BbgSession entity && session_id.Equals(entity.session_id);
    }

    public static bool operator ==(BbgSession left, BbgSession right) {
        return (left is null && right is null) ||
            left!.Equals(right);
    }

    public static bool operator !=(BbgSession left, BbgSession right) {
        return
            (left is null && right is not null) ||
            (right is null && left is not null) ||
            !left!.Equals(right);
    }

    public override int GetHashCode() {
        return HashCode.Combine(session_id, session_name);
    }

    public bool Equals(BbgSession? other) {
        return Equals((object?)other);
    }

}
