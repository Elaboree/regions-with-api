
namespace Cleverbit.CodingCase.Application.Requests.Commands;
public class AddEmployeeCommand : BaseRequest
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public int RegionId { get; set; }
}