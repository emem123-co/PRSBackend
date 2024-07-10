namespace PRSBackend.Models;

public class RequestLine
{
    public int Id { get; set; }


    public int RequestID { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public virtual Request? Request { get; set;}


    public int ProductID { get; set; }
    public virtual Product? Product { get; set;}


    public int Quantity { get; set; }
}
