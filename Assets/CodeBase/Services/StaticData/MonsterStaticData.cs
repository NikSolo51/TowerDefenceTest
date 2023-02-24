using CodeBase.Spawner.Monster;
using UnityEngine;

namespace CodeBase.Services.StaticData
{
    [CreateAssetMenu(fileName = "MonsterData", menuName = "StaticData/Monster", order = 0)]
    public class MonsterStaticData : ScriptableObject
    {
        public MonsterType MonsterType;
        public float Hp = 1;
        public float MoveSpeed;
        public GameObject Prefab;
    }
}