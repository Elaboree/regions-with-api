namespace Cleverbit.CodingCase.Domain.Models;

public class Employee : BaseEntity
{

    public string? Name { get; set; } 
    public string? Surname { get; set; }
    public int RegionId { get; set; }
    public virtual Region Region { get; set; }
}
