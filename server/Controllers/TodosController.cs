using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Dtos;

namespace Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodosController : ControllerBase
{
    private static readonly List<TodoItem> _todos = new();
    private static int _nextId = 1;

    [HttpGet]
    public ActionResult<List<TodoItem>> GetAll()
    {
        var result = _todos.OrderBy( t=> t.Id).ToList();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public ActionResult<TodoItem> GetById(int id)
    {
        var todo = _todos.FirstOrDefault( t => t.Id == id);
        if (todo == null) return NotFound();

        return Ok(todo);
    }

    [HttpPost]
    public ActionResult<TodoItem> Create([FromBody] CreateTodoRequest req)
    {
        if (string.IsNullOrWhiteSpace(req.Title))
        {
            return BadRequest("Title is empty");
        };
        
        var todo = new TodoItem
        {
            Id = _nextId++,
            Title = req.Title,
            IsDone = false
        };

        _todos.Add(todo);
        return CreatedAtAction(nameof(GetById), new { id = todo.Id}, todo);
    }

    [HttpPut("{id:int}")]
    public ActionResult<TodoItem> Update(int id, [FromBody] UpdateTodoRequest req)
    {
        var todo = _todos.FirstOrDefault( t => t.Id == id);
        if (todo == null) return NotFound();

        if (string.IsNullOrWhiteSpace(req.Title))
        {
            return BadRequest("Title is empty");
        }

        todo.Title = req.Title;
        todo.IsDone = req.IsDone;

        return Ok(todo);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var todo = _todos.FirstOrDefault( t => t.Id == id);
        if (todo == null) return NotFound();

        _todos.Remove(todo);
        return NoContent();
    }

}