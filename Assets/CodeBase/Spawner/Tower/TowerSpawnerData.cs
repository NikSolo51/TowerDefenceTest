using System;
using UnityEngine;

namespace CodeBase.Spawner.Tower
{
    [Serializable]
    public class TowerSpawnerData
    {
        public TowerType m_towerType;
        public Vector3 m_position;

        public TowerSpawnerData(TowerType towerType, Vector3 position)
        {
            m_towerType = towerType;
            m_position = position;
        }
    }
}