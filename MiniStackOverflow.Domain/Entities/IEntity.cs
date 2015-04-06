using System;

namespace MiniStackOverflow.Domain.Entities
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}