using LegendApi.Context;
using LegendApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace LegendApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class LocationController : ControllerBase
{
    private LegendDbContext _context = new LegendDbContext();
    
    [HttpGet(Name = "FindLocation")]
    public IActionResult FindLocation([FromQuery]  int id)
    {
        var a = Ok(_context.Locations.Find(id));
        if (id == null || id <= 0)
        {
            return Ok(Results.StatusCode(400));
        }
        else if(a == null)
        {
            return Ok(Results.StatusCode(404));
        }
        else
        {
            return a;
        }
    }
    
    
    [HttpPost(Name = "AddLocation")]
    public IResult AddLocation([FromBody] ConstLocation location)
    {
        Location loc = new Location();
        
            loc.Longitude = location.Longitude;
            loc.Latitude = location.Latitude;


            Location locationCheck = new Location();
            try
            {

                locationCheck = _context.Locations.Where(x=> x.Longitude == location.Longitude && x.Latitude == location.Latitude).First();
            }
            catch (Exception e)
            {
               
            }


            if (locationCheck.Longitude == location.Longitude && locationCheck.Latitude == location.Latitude)
            {
                return Results.StatusCode(409);
            }
           else if (loc.Latitude == null || loc.Latitude > 90 || loc.Latitude < -90 || loc.Longitude == null ||
                loc.Longitude < -180 || loc.Longitude > 180)
            {
                return Results.StatusCode(400);
            }
            else
            {
                _context.Locations.Add(loc);
                _context.SaveChanges();
                return Results.StatusCode(201);
            }
     
   
       
    }
    
    [HttpPut(Name = "RedactionLocation")]
    public IResult RedactionLocation([FromQuery] int id, ConstLocation location)
    {
        if (id == null || id <= 0)
        {
            return Results.StatusCode(400);
        }
        
        var redaction = _context.Locations.Find(id);
        if (redaction == null)
        {
            return Results.StatusCode(404);
        }
        redaction.Latitude = location.Latitude;
        redaction.Longitude = location.Longitude;
        
        Location locationCheck = new Location();
        try
        {
 
            locationCheck =  _context.Locations.Where(x => x.Longitude == location.Longitude || x.Latitude == location.Latitude).First();
        }
        catch (Exception e)
        {
            
        }
      





       
         if (locationCheck.Longitude == location.Longitude)
        {
            return Results.StatusCode(409);
        }
        else if (location.Latitude == null || location.Latitude > 90 || location.Latitude < -90 || location.Longitude == null ||
            location.Longitude < -180 || location.Longitude > 180)
        {
            return Results.StatusCode(400);
        }
        
        else
        {
           
            _context.SaveChanges();
            return Results.StatusCode(201);
        }
    }
    
    [HttpDelete(Name = "DeleteLocation")]
    public IResult DeleteLocation([FromQuery] int id)
    {
        var a =  _context.Locations.Find(id);

        if (id == null || id <= 0)
        {
            return Results.StatusCode(400);
        }
        else if(a == null)
        {
            return Results.StatusCode(404);
        }
        else
        {
            _context.Locations.Remove(a);
            _context.SaveChanges();
            return Results.StatusCode(200);
        }
       

    }
}