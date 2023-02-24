using CodeBase.Infrastructure.ObjectsPool;
using CodeBase.Spawner.Monster;
using UnityEngine;

namespace CodeBase.Monster
{
    public class DeactivateMonsterByDistance : MonoBehaviour
    {
        [SerializeField] private float m_reachDistance = 0.3f;
        private Vector3 m_moveTarget;
        private ObjectPool<MonsterData> m_objectPool;
        private MonsterData m_monsterData;

        public void Construct(Vector3 moveTarget)
        {
            m_moveTarget = moveTarget;
        }

        void Update()
        {
            if (Vector3.SqrMagnitude(transform.position - m_moveTarget) <= m_reachDistance)
            {
                m_objectPool.Release(m_monsterData);
            }
        }

        public void SetPool(ObjectPool<MonsterData> objectPool)
        {
            m_objectPool = objectPool;
        }

        public void SetMonsterData(MonsterData monsterData)
        {
            m_monsterData = monsterData;
        }
    }
}