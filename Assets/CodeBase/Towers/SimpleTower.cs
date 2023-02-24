using System.Collections.Generic;
using CodeBase.Infrastructure.ObjectsPool;
using CodeBase.Projectile;
using CodeBase.Spawner.Monster;
using UnityEngine;

namespace CodeBase.Towers
{
    public class SimpleTower : TowerAbstract
    {
        private float m_lastShotTime = -0.5f;

        private void Awake()
        {
            m_projectilePool =
                new ObjectPool<ProjectileAbstract>(CreateProjectile, OnTakeProjectileFromPool,
                    OnReturnProjectileToPool);
        }

        private void OnReturnProjectileToPool(ProjectileAbstract obj)
        {
            obj.gameObject.SetActive(false);
        }

        private void OnTakeProjectileFromPool(ProjectileAbstract obj)
        {
            obj.gameObject.SetActive(true);
        }

        void Update()
        {
            if (m_projectilePrefab == null)
                return;

            for (int i = 0; i < m_monsterSpawners.Count; i++)
            {
                Queue<MonsterData> monsterQueue = m_monsterSpawners[i].GetActivatedMonsters();

                if (monsterQueue.Count <= 0)
                    continue;
                GameObject monster = monsterQueue.Peek().m_monsterGO;

                if (Vector3.Distance(transform.position, monster.transform.position) > m_range)
                    continue;

                if (m_lastShotTime + m_shootInterval > Time.time)
                    continue;

                ProjectileAbstract projectile = m_projectilePool.Get();
                projectile.transform.position = m_shootPoint.position;
                projectile.transform.rotation = m_shootPoint.rotation;
                GuidedProjectile guidedProjectile = projectile as GuidedProjectile;
                guidedProjectile.Construct(monster.gameObject);

                m_lastShotTime = Time.time;
            }
        }

        private ProjectileAbstract CreateProjectile()
        {
            GameObject projectileGO =
                m_gameFactory.CreateProjectile(m_projectilePrefab, m_shootPoint.position, m_shootPoint.rotation);
            ProjectileAbstract projectileAbstract = projectileGO.GetComponent<ProjectileAbstract>();
            projectileAbstract.SetPool(m_projectilePool);
            return projectileAbstract;
        }
    }
}