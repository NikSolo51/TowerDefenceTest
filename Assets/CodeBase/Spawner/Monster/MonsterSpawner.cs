using System.Collections.Generic;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.ObjectsPool;
using CodeBase.Monster;
using CodeBase.Services.StaticData;
using UnityEngine;

namespace CodeBase.Spawner.Monster
{
    public class MonsterSpawner : MonoBehaviour
    {
        private Vector3 m_target;
        private MonsterType m_monsterType;
        private float m_spawnInterval = 3;
        private float m_lastSpawnTime = -1;
        private Queue<MonsterData> m_activatedMonsters = new Queue<MonsterData>();
        private ObjectPool<MonsterData> m_monsterPool;
        private IGameFactory m_gameFactory;
        private IStaticDataService m_staticDataService;

        public void Construct(IGameFactory gameFactory, IStaticDataService staticDataService,
            Vector3 target, MonsterType monsterType, float spawnInterval)
        {
            m_gameFactory = gameFactory;
            m_staticDataService = staticDataService;
            m_target = target;
            m_monsterType = monsterType;
            m_spawnInterval = spawnInterval;
        }

        private void Awake()
        {
            m_monsterPool = new ObjectPool<MonsterData>(CreateMonster, OnTakeMonsterFromPool, OnReturnBallToPool);
        }

        private void OnReturnBallToPool(MonsterData monster)
        {
            monster.m_monsterGO.SetActive(false);
            m_activatedMonsters.Dequeue();
        }

        private void OnTakeMonsterFromPool(MonsterData monster)
        {
            monster.m_monsterGO.SetActive(true);
            monster.m_resetObject.Reset();
            monster.m_monsterGO.transform.position = transform.position;
            m_activatedMonsters.Enqueue(monster);
        }


        void Update()
        {
            if (Time.time > m_lastSpawnTime + m_spawnInterval)
            {
                m_monsterPool.Get();
                m_lastSpawnTime = Time.time;
            }
        }

        public Queue<MonsterData> GetActivatedMonsters()
        {
            return m_activatedMonsters;
        }

        private MonsterData CreateMonster()
        {
            GameObject monsterGO;
            MonsterStaticData monsterStaticData = m_staticDataService.ForMonster(m_monsterType);
            monsterGO = m_gameFactory.CreateMonster(monsterStaticData.Prefab);

            IResetObject resetObject = monsterGO.GetComponent<IResetObject>();
            MonsterMovement monsterMovement = monsterGO.GetComponent<MonsterMovement>();
            monsterMovement.Construct(m_target, monsterStaticData.MoveSpeed);
            MonsterHP monsterHp = monsterGO.GetComponent<MonsterHP>();
            monsterHp.Construct(monsterStaticData.Hp);
            DeactivateMonsterByDistance deactivateMonsterByDistance =
                monsterGO.GetComponent<DeactivateMonsterByDistance>();
            deactivateMonsterByDistance.Construct(m_target);
            MonsterData monsterData = new MonsterData(monsterGO, resetObject, monsterMovement, monsterHp,
                deactivateMonsterByDistance);

            monsterData.m_monsterHP.SetPool(m_monsterPool);
            monsterData.m_monsterHP.SetMonsterData(monsterData);
            monsterData.m_deactivateMonsterByDistance.SetPool(m_monsterPool);
            monsterData.m_deactivateMonsterByDistance.SetMonsterData(monsterData);

            monsterData.m_monsterMovement.transform.position = transform.position;

            return monsterData;
        }
    }
}