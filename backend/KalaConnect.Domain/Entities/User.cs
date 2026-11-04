namespace KalaConnect.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }

    public string FullName { get; private set; } = string.Empty;

    public string Email { get; private set; } = string.Empty;

    public DateTime CreatedAt { get; private set; }

    public bool IsActive { get; private set; }

    private User()
    {
    }

    public User(string fullName, string email)
    {
        Id = Guid.NewGuid();

        FullName = fullName;
        Email = email;

        CreatedAt = DateTime.UtcNow;

        IsActive = true;
    }

    public void UpdateName(string fullName)
    {
        FullName = fullName;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}