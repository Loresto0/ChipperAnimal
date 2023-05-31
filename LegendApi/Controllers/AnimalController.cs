using LegendApi.Context;
using LegendApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace LegendApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AnimalController : ControllerBase
{
    private LegendDbContext _context = new LegendDbContext();
 
    [HttpGet(Name = "FindAnimalId")]
    public IActionResult FindAnimalId([FromQuery]  int id)
    {
        if (id == null || id <= 0)
        {
            return Ok(Results.StatusCode(400));
        }

        var animal = _context.Animals.Find(id);
        
        if (animal == null)
        {
            return Ok(Results.StatusCode(404));
        }

        return Ok(animal);
    }
    
    [HttpGet(Name = "FindAnimal")]
    public IActionResult FindAnimal([FromQuery]  string SearchInfo)
    {
        if (SearchInfo == null)
        {
            return Ok(Results.StatusCode(400));
        }
        
        var animal =  Ok(_context.Animals.Where(x=> x.Chipperid == int.Parse(SearchInfo) || x.Animaltype == int.Parse(SearchInfo) || x.Gender.Contains(SearchInfo) || x.Height == int.Parse(SearchInfo) || x.Weight == int.Parse(SearchInfo) || x.Lenght == int.Parse(SearchInfo) || x.Lifestatus.Contains(SearchInfo) || x.Chippinglocationid == int.Parse(SearchInfo)));
        
        if (animal == null)
        {
            return Ok(Results.StatusCode(404));
        }

        return animal;
    }
    
    
    [HttpPost(Name = "AddAnimal")]
    public IResult AddAnimal([FromBody] ConstAnimal animal)
    {
        Animal anim = new Animal();

        if (animal.Weight == null || animal.Weight <= 0 || animal.Lenght == null || animal.Lenght <= 0 ||
            animal.Height == null || animal.Height <= 0 || animal.Gender == null || animal.Chipperid == null ||
            animal.Chipperid <= 0 || animal.Chippinglocationid == null || animal.Chippinglocationid <= 0 || animal.Animaltype == null || animal.Animaltype <= 0)
        {
            return Results.StatusCode(400);
        }

        User user = new User();
        Location location = new Location();
        Typeanimal typeanimal = new Typeanimal();

        try
        {
            user = _context.Users.Find(animal.Chipperid);
            location = _context.Locations.Find(animal.Chippinglocationid);
            typeanimal = _context.Typeanimals.Find(animal.Animaltype);
        }
        catch (Exception e)
        {
           
        }

        if (user == null || location == null || typeanimal == null)
        {
            return Results.StatusCode(404);
        }
        anim.Deathdatetime = null;
        anim.Chippingdatetime = DateTime.Now;
        anim.Lifestatus = "ALIVE";
        anim.Gender = animal.Gender;
        anim.Chipperid = animal.Chipperid;
        anim.Chippinglocationid = animal.Chippinglocationid;
        anim.Visitedlocations = animal.Visitedlocations;
        anim.Weight = animal.Weight;
        anim.Height = animal.Height;
        anim.Lenght = animal.Lenght;
        anim.Animaltype = animal.Animaltype;    
        
        
        _context.Animals.Add(anim);
        _context.SaveChanges();
        return Results.StatusCode(201);
    }
    
    [HttpPut(Name = "RedactionAnimal")]
    public IResult RedactionAnimal([FromQuery] int id,  [FromBody] ConstAnimal animal)
    {
        if (id == null || id <= 0)
        {
            return Results.StatusCode(400);
        }
        if (animal.Weight == null || animal.Weight <= 0 || animal.Lenght == null || animal.Lenght <= 0 ||
            animal.Height == null || animal.Height <= 0 || animal.Gender == null || animal.Chipperid == null ||
            animal.Chipperid <= 0 || animal.Chippinglocationid == null || animal.Chippinglocationid <= 0 || animal.Animaltype == null || animal.Animaltype <= 0)
        {
            return Results.StatusCode(400);
        }
    
        User user = new User();
        Location location = new Location();
        

        try
        {
            user = _context.Users.Find(animal.Chipperid);
            location = _context.Locations.Find(animal.Chippinglocationid);
            
        }
        catch (Exception e)
        {
           
        }

        if (user == null || location == null)
        {
            return Results.StatusCode(404);
        }
        var redaction = _context.Animals.Find(id);

        if (redaction == null)
        {
            return Results.StatusCode(404);
        }
        redaction.Weight = animal.Weight;
        redaction.Lenght = animal.Lenght;
        redaction.Height = animal.Height;
        redaction.Gender = animal.Gender;
        redaction.Lifestatus = animal.Lifestatus;
        redaction.Chipperid = animal.Chipperid;
        redaction.Chippinglocationid = animal.Chippinglocationid;

        /*Historyvisitlocation VisitLocation = new Historyvisitlocation();
        VisitLocation.Idanimal = id;
        VisitLocation.Datetimeofvisitlocationpoint = DateTime.Now;
        VisitLocation.Locationpointid = animal.Visitedlocations;
        _context.Historyvisitlocations.Add(VisitLocation);*/
        
        
        _context.SaveChanges();
        return Results.StatusCode(200);
    }
    
    
  
    
    
    [HttpPut(Name = "AddAnimalType")]
    public IResult AddAnimalType([FromQuery] int id, [FromQuery] int typeid)
    {
        if (id == null || id <= 0 || typeid == null || typeid <= 0)
        {
            return Results.StatusCode(400);
        }
        
        var redaction = _context.Animals.Find(id);
        
        if (redaction == null)
        {
            return Results.StatusCode(404);
        }
        redaction.Animaltype = typeid;
        _context.SaveChanges();
        return Results.StatusCode(201);
    }
    
    
       
    [HttpPut(Name = "RedactionAnimalType")]
    public IResult RedactionAnimalType([FromQuery] int id, [FromBody] ConstTypeOld type)
    {
        
        if (id == null || id <= 0 || type.newTypeid == null || type.newTypeid <= 0)
        {
            return Results.StatusCode(400);
        }

        var redaction = _context.Animals.Find(id);

        if (redaction == null)
        {
            return Results.StatusCode(404);
        }
        redaction.Animaltype = type.newTypeid;
        _context.SaveChanges();
        
        return Results.StatusCode(200);
    }
    
    [HttpDelete(Name = "DeleteTypeAnimal")]
    public IResult DeleteTypeAnimal([FromQuery] int id)
    {
        if (id == null || id <= 0)
        {
            return Results.StatusCode(400);
        }
        var deletetype =  _context.Animals.Find(id);
        
        if (deletetype == null)
        {
            return Results.StatusCode(404);
        }
        
        deletetype.Animaltype = null;
        _context.SaveChanges();
        return Results.StatusCode(200);

    }
    
    [HttpDelete(Name = "DeleteAnimal")]
    public IResult DeleteAnimal([FromQuery] int id)
    {
        if (id == null || id <= 0)
        {
            return Results.StatusCode(400);
        }
        var a =  _context.Animals.Find(id);
        
        if (a == null)
        {
            return Results.StatusCode(404);
        }
        _context.Animals.Remove(a);
        _context.SaveChanges();
        return Results.StatusCode(200);
    }
   
}