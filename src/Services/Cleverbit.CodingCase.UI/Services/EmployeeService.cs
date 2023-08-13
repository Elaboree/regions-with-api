using Cleverbit.CodingCase.UI.Models;
using Cleverbit.CodingCase.UI.Services.Abstract;
using System.Text.Json;

namespace Cleverbit.CodingCase.UI.Services;

public class EmployeeService : IEmployeeService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    public EmployeeService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<IEnumerable<EmployeeItem>> GetEmployeesAsync()
    {
        HttpResponseMessage response = await _httpClient.GetAsync(_configuration["ApiUrl"]);

        if (response.IsSuccessStatusCode)
        {
            using var responseStream = await response.Content.ReadAsStreamAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var responseData = await JsonSerializer.DeserializeAsync<ApiResponse<IEnumerable<EmployeeItem>>>(responseStream, options);
            return responseData.Data;
        }
        else
        {
            // Handle the error case or return an empty collection
            return new List<EmployeeItem>();
        }
    }
}