using LegendApi.Context;
using LegendApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace LegendApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AnimalTypeController : ControllerBase
{
    
    private LegendDbContext _context = new LegendDbContext();
 
    [HttpGet(Name = "FindTypeAnimal")]
    public IActionResult FindTypeAnimal([FromQuery]  int id)
    {
        if (id == null || id <= 0)
        {
            return Ok(Results.StatusCode(400));
        }
        
        var type =  Ok(_context.Typeanimals.Find(id));
        
        if (type == null)
        {
            return Ok(Results.StatusCode(404)); 
        }

        return type;
    }
    
    
    [HttpPost(Name = "AddTypeAnimal")]
    public IResult AddTypeAnimal(Typeanimal type)
    {
        if (type.Type == null || type.Type == "")
        {
            return Results.StatusCode(404);
        }
        
        _context.Typeanimals.Add(type);
        _context.SaveChanges();
        return Results.StatusCode(201);
    }
    
    [HttpPut(Name = "RedactionTypeAnimal")]
    public IResult RedactionTypeAnimal([FromQuery] int id,  Typeanimal type)
    {
        if (id == null || id <= 0 || type.Type == null || type.Type == "")
        {
            return Results.StatusCode(404);
        }
        
        var redaction = _context.Typeanimals.Find(id);

        if (redaction == null)
        {
            return Results.StatusCode(400);
        }
        redaction.Type = type.Type;
        _context.SaveChanges();
        return Results.StatusCode(200);
    }
    
    [HttpDelete(Name = "DeleteType")]
    public IResult DeleteType([FromQuery] int id)
    {
        if (id == null || id <= 0)
        {
            return Results.StatusCode(404);
        }
        
        var delete =  _context.Typeanimals.Find(id);
        
        if (delete == null)
        {
            return Results.StatusCode(400);
        }
        _context.Typeanimals.Remove(delete);
        _context.SaveChanges();
        return Results.StatusCode(200);

    }
}