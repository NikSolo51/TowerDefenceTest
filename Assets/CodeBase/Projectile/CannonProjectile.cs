using CodeBase.Infrastructure.ObjectsPool;
using CodeBase.Monster;
using UnityEngine;

namespace CodeBase.Projectile
{
    public class CannonProjectile : ProjectileAbstract
    {
        public int m_damage = 10;
        private ObjectPool<ProjectileAbstract> m_objectPool;

        void OnTriggerEnter(Collider other)
        {
            var monsterHP = other.gameObject.GetComponent<MonsterHP>();
            if (monsterHP != null)
            {
                monsterHP.TakeDamage(m_damage);
            }

            m_objectPool.Release(this);
        }

        public override void SetPool(ObjectPool<ProjectileAbstract> pool)
        {
            m_objectPool = pool;
        }
    }
}