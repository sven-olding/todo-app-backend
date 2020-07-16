namespace ToDoApp.Models
{
  public class ToDoItemDTO
  {
    public long Id { get; set; }
    public string Text { get; set; }
    public bool IsComplete { get; set; }
  }
}