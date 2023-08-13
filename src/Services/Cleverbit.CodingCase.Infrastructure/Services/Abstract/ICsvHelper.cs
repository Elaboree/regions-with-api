namespace Cleverbit.CodingCase.Infrastructure.Services.Abstract;

public interface ICsvService
{
    IEnumerable<T> GetData<T>(string filePath) where T : class;
}