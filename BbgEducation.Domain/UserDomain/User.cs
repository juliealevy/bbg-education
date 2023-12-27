using BbgEducation.Domain.Common;

namespace BbgEducation.Domain.UserDomain;
public class User : Entity {
    public string Id { get; private set; }
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Password { get; private set; } = null!;

    //public bool IsAdmin{ get; set; } = false;

    private User(
        string id,
        string firstName,
        string lastName,
        string email,
        string password) {

        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
    }

    public static User Create(
        string firstName,
        string lastName,
        string email,
        string password) {

        return new User(
            Guid.NewGuid().ToString(),
            firstName,
            lastName,
            email,
            password);

    }

    public override bool isNew() {
        return String.IsNullOrEmpty(Id);
    }

    private User() {

    }
}
