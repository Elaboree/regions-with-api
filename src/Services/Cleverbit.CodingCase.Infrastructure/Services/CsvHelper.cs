using Cleverbit.CodingCase.Infrastructure.Services.Abstract;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace Cleverbit.CodingCase.Infrastructure.Services;

public class CsvService : ICsvService
{
    private readonly IFactory _csvFactory;
    private readonly CsvConfiguration _csvConfig;
    private List<string> _badRows;
    private bool _isBadData;

    public CsvService(IFactory factory)
    {
        _csvFactory = factory;
        _csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture)
        {
            Delimiter = ",",
            IgnoreBlankLines = true,
            HasHeaderRecord = false,
            BadDataFound = context =>
            {
                _isBadData = true;
                _badRows?.Add(context.RawRecord);

            }
        };
    }

    public IEnumerable<T> GetData<T>(string filePath) where T : class
    {
        {
            using var textReader = new StreamReader(filePath);
            using var csv = _csvFactory.CreateReader(textReader, _csvConfig);
            {
                var result = csv.GetRecords<T>().ToArray();

                return result;
            }
        }
    }
}