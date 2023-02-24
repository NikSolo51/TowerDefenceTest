using CodeBase.Infrastructure.ObjectsPool;
using CodeBase.Spawner.Monster;
using UnityEngine;

namespace CodeBase.Monster
{
    public class MonsterHP : MonoBehaviour
    {
        private float m_maxHP = 30;
        private float m_hp;
        private ObjectPool<MonsterData> m_objectPool;
        private MonsterData m_monsterData;
        private IResetObject m_resetObject;

        public void Construct(float maxHP)
        {
            m_maxHP = maxHP;
        }

        private void Awake()
        {
            m_resetObject = GetComponent<IResetObject>();
            if (m_resetObject != null) m_resetObject.OnResetObject += ResetHP;
        }

        private void OnDestroy()
        {
            if (m_resetObject != null) m_resetObject.OnResetObject -= ResetHP;
        }

        void Start()
        {
            ResetHP();
        }

        public void ResetHP()
        {
            m_hp = m_maxHP;
        }

        public void SetPool(ObjectPool<MonsterData> objectPool)
        {
            m_objectPool = objectPool;
        }

        public void SetMonsterData(MonsterData monsterData)
        {
            m_monsterData = monsterData;
        }

        public void TakeDamage(float damage)
        {
            m_hp -= damage;
            if (m_hp <= 0)
            {
                m_objectPool.Release(m_monsterData);
            }
        }
    }
}