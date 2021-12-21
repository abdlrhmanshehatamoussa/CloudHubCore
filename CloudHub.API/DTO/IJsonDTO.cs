namespace CloudHub.API.DTO
{
    public interface IJson<T>
    {
        T ToObject();
    }
}
