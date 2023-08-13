namespace Cleverbit.CodingCase.UI.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public T Data { get; set; }
    }

    public class EmployeeItem
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string RegionName { get; set; }
    }

}
