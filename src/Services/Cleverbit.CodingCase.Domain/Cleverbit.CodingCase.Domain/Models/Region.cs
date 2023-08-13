namespace Cleverbit.CodingCase.Domain.Models;

public class Region : BaseEntity
{
    public string? Name { get; set; }
    public int? ParentId { get; set; }
    public ICollection<Employee> Employees { get; set; }

    // Navigation property for self-referencing relationship
    public Region? Parent { get; set; }
}
