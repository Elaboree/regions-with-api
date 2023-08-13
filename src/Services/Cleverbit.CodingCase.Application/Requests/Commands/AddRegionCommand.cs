using System.Text.Json.Serialization;

namespace Cleverbit.CodingCase.Application.Requests.Commands;
public class AddRegionCommand : BaseRequest
{
    [JsonIgnore]
    public int Id { get; set; }
    public string Name { get; set; }
    public int? ParentId { get; set; }
}