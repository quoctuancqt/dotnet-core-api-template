namespace Dto
{
    public interface IDto<T>
    {
        T Id { get; set; }
    }

    public interface IDto : IDto<int> { }
}
