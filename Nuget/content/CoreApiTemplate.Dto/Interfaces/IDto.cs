using System;

namespace CoreApiTemplate.Dto.Interfaces
{
    public interface IDto<T>
    {
        T Id { get; set; }
    }

    public interface IDto : IDto<Guid> { }
}
