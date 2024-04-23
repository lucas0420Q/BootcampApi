namespace Infrastructure.Repositories
{
    internal class CreateCurrentAccountDTO
    {
        public int AccountId { get; set; }
        public object OperationalLimit { get; set; }
        public object MonthAverage { get; set; }
        public object Interest { get; set; }
    }
}