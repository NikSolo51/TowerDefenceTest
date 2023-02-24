using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateUpdateManager();
        GameObject CreateMonster(GameObject monsterPrefab);
        GameObject CreateMonsterSpawner();
        GameObject CreateTower(GameObject towerPrefab, Vector3 towerPosition);
        GameObject CreateProjectile(GameObject projectilePrefab, Vector3 position);
        GameObject CreateProjectile(GameObject projectilePrefab, Vector3 position, Quaternion rotation);
    }
}