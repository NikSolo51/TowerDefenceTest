using System.Collections.Generic;
using CodeBase.Spawner.Monster;
using CodeBase.Spawner.Tower;
using UnityEngine;

namespace CodeBase.Services.StaticData
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "StaticData/Level")]
    public class LevelStaticData : ScriptableObject
    {
        public string LevelKey;
        public List<MonsterSpawnerData> MonsterSpawnerDatas;
        public List<TowerSpawnerData> TowersData;
    }
}