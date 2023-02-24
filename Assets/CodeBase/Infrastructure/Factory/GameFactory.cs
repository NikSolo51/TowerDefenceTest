using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Services.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticData;


        public GameFactory(IAssetProvider assets,
            IStaticDataService staticData)
        {
            _assets = assets;
            _staticData = staticData;
        }

        public GameObject CreateUpdateManager()
        {
            GameObject updateManager = Instantiate(AssetsAdress.UpdateManager);
            return updateManager;
        }

        public GameObject CreateMonster(GameObject monsterPrefab)
        {
            GameObject monster = Instantiate(monsterPrefab);
            return monster;
        }

        public GameObject CreateMonsterSpawner()
        {
            GameObject monsterSpawnerGO = Instantiate(AssetsAdress.MonsterSpawner);
            return monsterSpawnerGO;
        }

        public GameObject CreateTower(GameObject towerPrefab, Vector3 towerosition)
        {
            GameObject towerGO = Instantiate(towerPrefab, towerosition);
            return towerGO;
        }

        public GameObject CreateProjectile(GameObject projectilePrefab, Vector3 position)
        {
            GameObject projectileGO = Instantiate(projectilePrefab, position);
            return projectileGO;
        }

        public GameObject CreateProjectile(GameObject projectilePrefab, Vector3 position, Quaternion rotation)
        {
            GameObject projectileGO = Instantiate(projectilePrefab, position, rotation);
            return projectileGO;
        }

        private GameObject Instantiate(GameObject prefab, Vector3 at, Quaternion rotation)
        {
            GameObject gameObject = _assets.Instantiate(prefab, at: at, rotation);

            return gameObject;
        }

        private GameObject Instantiate(string prefabPath, Vector3 at)
        {
            GameObject gameObject = _assets.Instantiate(path: prefabPath, at: at);

            return gameObject;
        }

        private GameObject Instantiate(string prefabPath)
        {
            GameObject gameObject = _assets.Instantiate(path: prefabPath);

            return gameObject;
        }

        private GameObject Instantiate(GameObject prefab)
        {
            GameObject gameObject = _assets.Instantiate(prefab);

            return gameObject;
        }

        private GameObject Instantiate(GameObject prefab, Vector3 at)
        {
            GameObject gameObject = _assets.Instantiate(prefab, at);

            return gameObject;
        }
    }
}