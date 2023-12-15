namespace BbgEducation.Domain.UserDomain;
public class User
{
    public int user_id { get; set; }
    public string user_name { get; set; } = string.Empty;
    public string email { get; set; } = string.Empty;
    public bool is_admin { get; set; }
}
