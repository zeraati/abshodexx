namespace App.Feature.Domain;

public partial class User
{
    public User(string name,string username,string? mobile)
    {
        Name = name;
        Username=username;
        Mobile = mobile;
    }

    public void Update(string name, string username, string? mobile)
    {
        Name = name;
        Username = username;
        Mobile = mobile;
    }
}