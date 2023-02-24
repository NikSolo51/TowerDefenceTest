using CodeBase.Infrastructure.Services;
using CodeBase.Spawner.Monster;
using CodeBase.Spawner.Tower;

namespace CodeBase.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void Initialize();
        MonsterStaticData ForMonster(MonsterType monsterType);
        LevelStaticData ForLevel(string sceneKey);
        TowerStaticData ForTower(TowerType towerType);
    }
}