namespace Application.Models.Entity;

public class Team
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Company Company { get; set; }
    public int CompanyId { get; set; }
    private readonly List<User> _users = new();
    public IEnumerable<User> Users => _users;
    public IEnumerable<Task> Tasks { get; set; }

    public void AddMember(User user)
    {
        if (!_users.Contains(user))
        {
            _users.Add(user);
        }
    }
}