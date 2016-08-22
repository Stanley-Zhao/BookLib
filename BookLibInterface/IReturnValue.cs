namespace BookLib.Interface
{
    public interface IReturnValue<T>
    {
        string DetailMessage { get; set; }
        bool HasError { get; set; }
        bool SingleEntity { get; set; }
        string Message { get; set; }
        int StatusCode { get; set; }
        T[] ReturnEntities { get; set; }
        T ReturnEntity { get; set; }
    }
}
