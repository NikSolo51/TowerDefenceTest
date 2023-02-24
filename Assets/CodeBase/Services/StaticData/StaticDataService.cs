using System.Collections.Generic;
using System.Linq;
using CodeBase.Spawner.Monster;
using CodeBase.Spawner.Tower;
using UnityEngine;

namespace CodeBase.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<string, LevelStaticData> m_levels;
        private Dictionary<MonsterType, MonsterStaticData> m_monstersData;
        private Dictionary<TowerType, TowerStaticData> m_towersData;

        public void Initialize()
        {
            m_levels = Resources.LoadAll<LevelStaticData>("StaticData/Levels").ToDictionary(x => x.LevelKey, x => x);

            m_monstersData = Resources.LoadAll<MonsterStaticData>("StaticData/Monsters")
                .ToDictionary(x => x.MonsterType, x => x);

            m_towersData = Resources.LoadAll<TowerStaticData>("StaticData/Towers")
                .ToDictionary(x => x.TowerType, x => x);
        }

        public MonsterStaticData ForMonster(MonsterType monsterType)
        {
            return m_monstersData.TryGetValue(monsterType, out MonsterStaticData staticData)
                ? staticData
                : null;
        }

        public LevelStaticData ForLevel(string sceneKey)
        {
            return m_levels.TryGetValue(sceneKey, out LevelStaticData staticData)
                ? staticData
                : null;
        }

        public TowerStaticData ForTower(TowerType towerType)
        {
            return m_towersData.TryGetValue(towerType, out TowerStaticData staticData)
                ? staticData
                : null;
        }
    }
}