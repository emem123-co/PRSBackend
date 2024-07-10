using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PRSBackend.Models;

[Index(nameof(PartNbr), IsUnique = true)]
public class Product
{
    public int Id { get; set; }


    [StringLength(30)]
    public string PartNbr { get; set; } = string.Empty;


    [StringLength(30)]
    public string Name { get; set; } = string.Empty;


    [Column(TypeName = "decimal(11,2)")]
    public decimal Price { get; set; }


    [StringLength(30)]
    public string Unit { get; set; } = string.Empty;


    [StringLength(255)]
    public string? PhotoPath { get; set; } = string.Empty;


    public int VendorId { get; set; }
    public virtual Vendor? Vendor { get; set; }
    //place vendorID once user selects it in POST or PUT.
    //display list of vendor names when user is creating (POST)
    //display current vendor when user is editing (PUT) a product. 


    //public virtual ICollection<Vendor>? Vendors { get; set; } = new List<Vendor>();
    //return list of products if in this is in the vendor class


}
