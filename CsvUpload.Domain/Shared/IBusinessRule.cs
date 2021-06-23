namespace CsvUpload.Domain.Shared
{
    public interface IBusinessRule
    {
        bool IsBroken();
        string Message { get; }
    }
}
