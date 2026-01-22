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

    
}