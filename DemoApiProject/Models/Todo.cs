namespace DemoApiProject.Models;

public class Todo
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool Completed { get; set; }
}

public class TodoHistory
{
    public int Id { get; set; }
    public Todo Todo { get; set; } = new ();
    public int TodoId { get; set; }
    public DateTime Date { get; set; }
    public string Action { get; set; } = string.Empty;
}