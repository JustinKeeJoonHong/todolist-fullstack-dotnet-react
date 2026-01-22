namespace Server.Dtos;

public class UpdateTodoRequest
{
    public string Title {get; set;} = "";
    public bool IsDone {get; set;}
}