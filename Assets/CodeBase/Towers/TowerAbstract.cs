using System.Collections.Generic;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.ObjectsPool;
using CodeBase.Projectile;
using CodeBase.Spawner.Monster;
using UnityEngine;

namespace CodeBase.Towers
{
    public abstract class TowerAbstract : MonoBehaviour
    {
        [SerializeField] protected Transform m_shootPoint;
        [SerializeField] protected float m_rotationSpeed = 1;
        protected GameObject m_projectilePrefab;
        protected float m_shootInterval = 0.5f;
        protected float m_range = 4f;
        protected List<MonsterSpawner> m_monsterSpawners;
        protected ObjectPool<ProjectileAbstract> m_projectilePool;
        protected IGameFactory m_gameFactory;


        public void Construct(IGameFactory gameFactory, GameObject projectilePrefab,
            List<MonsterSpawner> monsterSpawners,
            float shootInterval, float range, float rotationSpeed)
        {
            m_gameFactory = gameFactory;
            m_projectilePrefab = projectilePrefab;
            m_monsterSpawners = monsterSpawners;
            m_shootInterval = shootInterval;
            m_range = range;
            m_rotationSpeed = rotationSpeed;
        }
    }
}