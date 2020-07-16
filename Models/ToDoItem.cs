namespace ToDoApp.Models
{
  public class ToDoItem
  {
    public long Id { get; set; }
    public string Text { get; set; }
    public bool IsComplete { get; set; }
    public string Secret { get; set; }
  }
}