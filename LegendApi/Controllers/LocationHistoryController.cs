using LegendApi.Context;
using LegendApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace LegendApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class LocationHistoryController : ControllerBase
{
    private LegendDbContext _context = new LegendDbContext();
    [HttpGet(Name = "SearchHistoryLocationId")]
    public IActionResult SearchHistoryLocationId([FromQuery] int id)
    {
        if (id == null || id <= 0)
        {
            return Ok(Results.StatusCode(400));
        }

        var result = _context.Historyvisitlocations.Find(id);

        if (result == null)
        {
            return Ok(Results.StatusCode(404));
        }
        
        return Ok(_context.Historyvisitlocations.Find(id));
    }
    
    
    [HttpPost(Name = "AddHistoryLocation")]
    public IResult AddHistoryLocation(ConstHistoryLocation historylocation)
    {
        
        if (historylocation.Locationpointid == null || historylocation.Locationpointid <= 0 || historylocation.Idanimal == null || historylocation.Idanimal <= 0)
        {
            return Results.StatusCode(400);
        }

        Location locationCheck = new Location();
        Animal animalCheck = new Animal();
        try
        {
            locationCheck = _context.Locations.Where(x => x.Id == historylocation.Locationpointid).First();
            animalCheck = _context.Animals.Where(x => x.Id == historylocation.Idanimal).First();
        }
        catch (Exception e)
        {
           
        }
        
        if (locationCheck.Id != historylocation.Locationpointid || animalCheck.Id != historylocation.Idanimal)
        {
            return Results.StatusCode(404);
        }

        Historyvisitlocation historyvisitlocation = new Historyvisitlocation();
        historyvisitlocation.Locationpointid = historylocation.Locationpointid;
        historyvisitlocation.Idanimal = historylocation.Idanimal;
        historyvisitlocation.Datetimeofvisitlocationpoint = DateTime.Now;

      
        
        
        _context.Historyvisitlocations.Add(historyvisitlocation);
        _context.SaveChanges();
        return Results.StatusCode(201);
    }
    
    
    [HttpPut(Name = "RedactionHistoryLocation")]
    public IResult RedactionHistoryLocation([FromQuery] int id, ConstLocationHistoryRedaction locationHistory )
    {
        if (id == null || id <= 0)
        {
            return Results.StatusCode(400);
        }
        var redaction = _context.Historyvisitlocations.Find(id);

        if (redaction == null)
        {
            return Results.StatusCode(404);
        }

        if (locationHistory.Locationpointid == null || locationHistory.Locationpointid <= 0)
        {
            return Results.StatusCode(400);
        }
        redaction.Locationpointid = locationHistory.Locationpointid;
        _context.SaveChanges();
        return Results.StatusCode(200);
    }
    
    
    [HttpDelete(Name = "DeleteHistoryLocation")]
    public IResult DeleteHistoryLocation([FromQuery] int id)
    {
        if (id == null || id <= 0)
        {
            return Results.StatusCode(400);
        }
        var delete = _context.Historyvisitlocations.Find(id);
        if (delete == null)
        {
            return Results.StatusCode(404);
        } 
        _context.Historyvisitlocations.Remove(delete);
        _context.SaveChanges();
        return Results.StatusCode(200);
    }
}