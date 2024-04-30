namespace NSIProject.Domain.Entities;

public class Post
{
    public string Content { get; set; }
    public string Title { get; set; }
    public ApplicationUser User { get; set; }
    public Guid Id { get; set; }
}