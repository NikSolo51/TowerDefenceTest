using CodeBase.Infrastructure.ObjectsPool;
using CodeBase.Monster;
using UnityEngine;

namespace CodeBase.Projectile
{
    public class GuidedProjectile : ProjectileAbstract
    {
        [SerializeField] private float m_speed = 0.2f;
        [SerializeField] private int m_damage = 10;
        private GameObject m_target;
        private ObjectPool<ProjectileAbstract> m_objectPool;

        public void Construct(GameObject target)
        {
            m_target = target;
        }

        void Update()
        {
            if (m_target == null)
            {
                m_objectPool.Release(this);
                return;
            }

            Vector3 translation = m_target.transform.position - transform.position;
            if (translation.magnitude > m_speed)
            {
                translation = translation.normalized * m_speed;
            }

            transform.Translate(translation);
        }


        void OnTriggerEnter(Collider other)
        {
            MonsterHP monsterHP = other.gameObject.GetComponent<MonsterHP>();
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