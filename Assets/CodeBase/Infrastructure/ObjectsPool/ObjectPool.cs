using System;
using System.Collections.Generic;

namespace CodeBase.Infrastructure.ObjectsPool
{
    public class ObjectPool<T> : IDisposable where T : class
    {
        private readonly Stack<T> m_Stack;
        private readonly Func<T> m_CreateFunc;
        private readonly Action<T> m_ActionOnGet;
        private readonly Action<T> m_ActionOnRealise;
        private readonly Action<T> m_ActionOnDestroy;
        private readonly int m_MaxSize;
        private bool m_CollectionCheck;

        public int CountAll { get; private set; }
        public int CounInactive => this.m_Stack.Count;
        public int CountActive => this.CountAll - CounInactive;

        public ObjectPool(Func<T> createFunc, Action<T> actionOnGet = null, Action<T> actionOnRealise = null,
            Action<T> actionOnDestroy = null, bool collectionCheck = true, int defaultCapacity = 10, int maxSize = 100)
        {
            if (createFunc == null)
                throw new ArgumentNullException(nameof(createFunc));
            if (maxSize <= 0)
                throw new ArgumentException("Max Size must be greater than 0", nameof(maxSize));

            m_Stack = new Stack<T>(defaultCapacity);
            m_CreateFunc = createFunc;
            m_ActionOnGet = actionOnGet;
            m_ActionOnRealise = actionOnRealise;
            m_ActionOnDestroy = actionOnDestroy;
            m_MaxSize = maxSize;
            m_CollectionCheck = collectionCheck;
        }

        public T Get()
        {
            T obj;
            if (m_Stack.Count == 0)
            {
                obj = m_CreateFunc();
                ++CountAll;
            }
            else
                obj = m_Stack.Pop();

            Action<T> actionOnGet = m_ActionOnGet;
            if (actionOnGet != null)
                actionOnGet(obj);
            return obj;
        }

        public void Release(T element)
        {
            if (m_CollectionCheck && m_Stack.Count > 0 && m_Stack.Contains(element))
                throw new InvalidOperationException(
                    "Trying to release an object than has already been released to the pool.");
            Action<T> actionOnRelease = m_ActionOnRealise;

            if (actionOnRelease != null)
                actionOnRelease(element);

            if (CounInactive < m_MaxSize)
                m_Stack.Push(element);
            else
            {
                Action<T> actionOnDestroy = m_ActionOnDestroy;
                if (actionOnDestroy != null)
                    actionOnDestroy(element);
            }
        }

        public void Clear()
        {
            if (m_ActionOnDestroy != null)
            {
                foreach (T obj in m_Stack)
                    m_ActionOnDestroy(obj);
            }

            m_Stack.Clear();
            CountAll = 0;
        }

        public void Dispose()
        {
            Clear();
        }
    }
}