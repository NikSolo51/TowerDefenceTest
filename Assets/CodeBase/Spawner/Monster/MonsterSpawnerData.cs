using System;
using UnityEngine;

namespace CodeBase.Spawner.Monster
{
    [Serializable]
    public class MonsterSpawnerData
    {
        public float m_spawnInterval = 3;
        public MonsterType m_monsterType;
        public Vector3 m_position;
        public Vector3 m_targetPosition;

        public MonsterSpawnerData(float spawnInterval, MonsterType monsterType, Vector3 position,
            Vector3 targetPosition)
        {
            m_spawnInterval = spawnInterval;
            m_monsterType = monsterType;
            m_position = position;
            m_targetPosition = targetPosition;
        }
    }
}