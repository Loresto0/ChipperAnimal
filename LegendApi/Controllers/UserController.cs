using LegendApi.Context;
using LegendApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace LegendApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class UserController : ControllerBase
{
    private LegendDbContext _context = new LegendDbContext();
    
    [HttpPost(Name = "registration")]
    public IResult registration(ConstUser user)
    {
        
        User use = new User();
        use.Email = user.Email;
        use.Password = user.Password;
        use.Firstname = user.Firstname;
        use.Lastname = user.Lastname;

        User userCheck = new User();

        try
        {

            userCheck = _context.Users.Where(x => x.Email.Contains(user.Email)).First();
        }
        catch (Exception e)
        {
            
        }
         
       
       

            if (userCheck.Email == user.Email)
            {
                return Results.StatusCode(409);
            }
            else if(use.Firstname == null || use.Lastname == null || use.Email == null || use.Password == null || use.Firstname == "" || use.Lastname == "" || use.Email == "" || use.Password == "")
            {
                return Results.StatusCode(400);
            }
            else
            {
                _context.Users.Add(use);
                _context.SaveChanges();
                return Results.StatusCode(201);
            }
            
            
          
    
       
    }
    
    [HttpGet(Name = "FindUser")]
    public IActionResult FindUser([FromQuery] int id)
    {
        var a = _context.Users.Find(id);
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
            return Ok(_context.Users.Find(id));
        }
        
    }
    
    [HttpGet(Name = "search")]
    public IActionResult search([FromQuery] string ParamSearch)
    {
        var a =_context.Users.Where(x => x.Email.Contains(ParamSearch) || x.Firstname.Contains(ParamSearch) || x.Lastname.Contains(ParamSearch));
            return Ok(a);
    }
    
    
    [HttpPut(Name = "RedactionUser")]
    public IResult RedactionUser([FromQuery] int id, ConstUser user)
    {
        
        if (id == null || id <= 0 )
        {
            return Results.StatusCode(400);
        }
        
        var redaction = _context.Users.Find(id);
         if(redaction == null)
        {
            return Results.StatusCode(403);
        }
        User userK = new User();
        try
        {

            userK = _context.Users.Where(x => x.Email.Contains(user.Email)).First();
        }
        catch (Exception e)
        {
           
        }
       
        
        redaction.Email = user.Email;
        redaction.Firstname = user.Firstname;
        redaction.Lastname = user.Lastname;
        redaction.Password = user.Password;

        if (redaction.Firstname == null || redaction.Firstname == "" ||
            redaction.Lastname == null || redaction.Lastname == "" || redaction.Email == null ||
            redaction.Email == "" || redaction.Password == null || redaction.Password == "")
        {
            return Results.StatusCode(400);
        }
        else if(userK.Email == user.Email && userK.Email != null && userK.Email != "")
        {
            return Results.StatusCode(409);
        }
     
        else
        {
            _context.SaveChanges();
            return Results.StatusCode(200);
        }
       
    }
    
    [HttpDelete(Name = "DeleteUser")]
    public IResult DeleteUser([FromQuery] int id)
    {
       var a =  _context.Users.Find(id);

       if (id == null || id <= 0)
       {
           return Results.StatusCode(400); 
       }
       else if(a == null)
       {
           return Results.StatusCode(403);  
       }
       else
       {
           _context.Users.Remove(a);
           _context.SaveChanges();
           return Results.StatusCode(200); 
       }


    }

}