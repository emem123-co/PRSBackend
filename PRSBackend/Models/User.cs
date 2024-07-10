using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PRSBackend.Models;

[Index(nameof(Username), IsUnique = true)]

public class User

{
    public int Id {get; set; }


    [StringLength(30)]
    public string Username {get; set; } = string.Empty;

    
    [StringLength(30)]
    public string Password {get; set; } = string.Empty;


    [StringLength(30)]
    public string FirstName {get; set; } = string.Empty;


    [StringLength(30)]
    public string LastName {get; set; } = string.Empty;


    [StringLength(12)]
    public string? PhoneNumber {get; set; } = string.Empty;


    [StringLength(255)]
    public string? Email {get; set; } = string.Empty;

    
    [Column(TypeName = "bit")]
    public bool IsReviewer {get; set; }


    [Column(TypeName = "bit")]
    public bool IsAdmin {get; set; }

    //constructor for Login(username, password)
    //{
    //return User.Id
    //};
}
  
   
