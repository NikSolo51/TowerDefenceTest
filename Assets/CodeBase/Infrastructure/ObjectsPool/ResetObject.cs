using System;
using UnityEngine;

namespace CodeBase.Infrastructure.ObjectsPool
{
    public class ResetObject : MonoBehaviour, IResetObject
    {
        public event Action OnResetObject;

        public void Reset()
        {
            OnResetObject?.Invoke();
        }
    }
}