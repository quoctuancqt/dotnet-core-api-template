using System;

namespace Demo.Dto.Interfaces
{
    public interface IDto<T>
    {
        T Id { get; set; }
    }

    public interface IDto : IDto<Guid> { }
}
