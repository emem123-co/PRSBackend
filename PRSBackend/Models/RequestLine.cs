namespace PRSBackend.Models;

public class RequestLine
{
    public int Id { get; set; }


    public int RequestID { get; set; }
    /*[JsonIgnore]*/public virtual Request? Request { get; set;}


    public int ProductID { get; set; }
    public virtual Product? Product { get; set;}


    public int Quantity { get; set; }
}
