using Core.Constants;

namespace Core.Request
{
    public class FilterAccountModel
    {
        public string? Number { get; set; }
        public AccountType Type { get; set; }
        public string? Currency { get; set; }
    }
}
