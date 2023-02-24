using System.Collections;
using System.Collections.Generic;
using CodeBase.Infrastructure.ObjectsPool;
using CodeBase.Projectile;
using CodeBase.Spawner.Monster;
using UnityEngine;

namespace CodeBase.Towers
{
    public class CannonTower : TowerAbstract
    {
        [SerializeField] private GameObject m_barrelGO;
        private Vector3 m_predictedPosition;
        private float m_attackTime = 0.5f;
        private float m_groundDistance;
        private const float m_gravity = 9.8f;
        private float m_height;

        private float m_lastShotTime = -0.5f;
        private float m_coolDown = 0.3f;
        private bool m_cantRotate;

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
            if (m_projectilePrefab == null || m_shootPoint == null)
                return;

            for (int i = 0; i < m_monsterSpawners.Count; i++)
            {
                Queue<MonsterData> monsterQueue = m_monsterSpawners[i].GetActivatedMonsters();

                if (monsterQueue.Count <= 0)
                    continue;
                MonsterData monsterData = monsterQueue.Peek();
                GameObject monster = monsterData.m_monsterGO;

                m_predictedPosition = new Vector3(
                    monster.transform.position.x + (-m_attackTime * monsterData.m_monsterMovement.GetSpeed()),
                    monster.transform.position.y,
                    monster.transform.position.z);

                if (!m_cantRotate)
                    RotateTowardMonster();

                if (Vector3.Distance(transform.position, monster.transform.position) > m_range)
                    continue;


                if (m_lastShotTime + m_shootInterval > Time.time)
                    continue;


                ProjectileAbstract projectile = m_projectilePool.Get();
                projectile.transform.position = m_shootPoint.position;
                projectile.transform.rotation = m_shootPoint.rotation;
                Rigidbody projectileRB = projectile.GetComponent<Rigidbody>();
                projectileRB.velocity = (m_predictedPosition - m_shootPoint.transform.position) /
                                        m_attackTime;
                projectileRB.velocity = new Vector3(projectileRB.velocity.x,
                    ((m_attackTime * m_gravity) / 2) + projectileRB.velocity.y, projectileRB.velocity.z);

                m_lastShotTime = Time.time;

                StartCoroutine(CoolDown());
            }
        }


        private IEnumerator CoolDown()
        {
            m_cantRotate = true;
            yield return new WaitForSeconds(m_coolDown);
            m_cantRotate = false;
        }

        private void RotateTowardMonster()
        {
            if (m_predictedPosition == Vector3.zero)
                return;
            Quaternion lookRotation =
                Quaternion.LookRotation((m_predictedPosition - m_barrelGO.transform.position).normalized);
            lookRotation.z = 0;
            m_barrelGO.transform.rotation = Quaternion.Slerp(m_barrelGO.transform.rotation, lookRotation,
                m_rotationSpeed * Time.deltaTime);
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