using System;

namespace CodeBase.Infrastructure.ObjectsPool
{
    public interface IResetObject
    {
        event Action OnResetObject;

        void Reset();
    }
}