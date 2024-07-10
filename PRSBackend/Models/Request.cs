using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRSBackend.Models;

public class Request
{
    public int Id { get; set; }
    
    
    [StringLength(80)]
    public string Description { get; set; } = string.Empty;
    
    
    [StringLength(80)]
    public string Justification { get; set; } = string.Empty;
    
    
    [StringLength(80)] 
    public string? RejectionReasoning { get; set; } = string.Empty; //cannot be null only when status is "REJECTED"
    
    
    [StringLength(20)]
    public string DeliveryMode { get; set; } = "PICKUP";
    
    
    [StringLength(10)] 
    public string Status { get; set; } = string.Empty; //user not able to change this directly. Application will update via Requests controller. NEW / APPROVED / REJECTED / REVIEW
    
    
    [Column(TypeName = "decimal(11,2)")] 
    public decimal Total { get; set; } = 0; //user not able to change this directly. Application will update via RequestLines controller.
    
    public int UserId { get; set; } //automatically will be the ID of the logged in user
    public virtual User? User { get; set; } 
    
    
    public virtual ICollection<RequestLine>? Requestlines { get; set; } = new List<RequestLine>();
    
}
