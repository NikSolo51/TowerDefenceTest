using CodeBase.Spawner.Tower;
using UnityEngine;

namespace CodeBase.Services.StaticData
{
    [CreateAssetMenu(fileName = "TowerData", menuName = "StaticData/Towers", order = 0)]
    public class TowerStaticData : ScriptableObject
    {
        public float ShootInterval;
        public float Range;
        public float RotationSpeed = 1;
        public TowerType TowerType;
        public GameObject Prefab;
        public GameObject ProjectilePrefab;
    }
}