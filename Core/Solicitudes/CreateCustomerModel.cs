using Core.Constants;

namespace Core.Solicitudes
{
    public class CreateCustomerModel
    {
        public static object? CustomerStatus;

        public string Name { get; set; } = string.Empty;
        public string? Lastname { get; set; }
        public string DocumentNumber { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? Mail { get; set; }
        public string? Phone { get; set; }
        public int BankId { get; set; }
        public string? CustonmerStatus { get; set; }
        public object? Birth { get; set; }
    }
}
