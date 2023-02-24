using System.Collections.Generic;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Logic;
using CodeBase.Services.StaticData;
using CodeBase.Services.Update;
using CodeBase.Spawner.Monster;
using CodeBase.Spawner.Tower;
using CodeBase.Towers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayLoadedState<string>
    {
        private readonly LoadingCurtain _curtain;
        private readonly SceneLoader _sceneLoader;
        private readonly GameStateMachine _stateMachine;
        private readonly IGameFactory _gameFactory;
        private readonly IStaticDataService _staticData;

        public LoadLevelState(GameStateMachine stateMachine,
            SceneLoader sceneLoader,
            LoadingCurtain curtain,
            IGameFactory gameFactory,
            IStaticDataService staticData)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _staticData = staticData;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            _curtain.Hide();
        }

        private async void OnLoaded()
        {
            InitGameWorld();
            _stateMachine.Enter<GameLoopState>();
        }


        private void InitGameWorld()
        {
            LevelStaticData levelData = LevelStaticData();
            IUpdateService updateService = AllServices.Container.Single<IUpdateService>();
            InitUpdateManger(updateService);
            List<MonsterSpawner> monsterSpawners = InitSpawners(levelData);
            InitTowers(levelData, monsterSpawners);
        }

        private void InitTowers(LevelStaticData levelData, List<MonsterSpawner> monsterSpawners)
        {
            for (int i = 0; i < levelData.TowersData.Count; i++)
            {
                TowerSpawnerData towerSpawnerData = levelData.TowersData[i];
                TowerStaticData towerStaticData = _staticData.ForTower(towerSpawnerData.m_towerType);
                GameObject towerGO = _gameFactory.CreateTower(towerStaticData.Prefab, towerSpawnerData.m_position);

                TowerAbstract tower = towerGO.GetComponent<TowerAbstract>();
                tower.Construct(_gameFactory, towerStaticData.ProjectilePrefab, monsterSpawners,
                    towerStaticData.ShootInterval, towerStaticData.Range, towerStaticData.RotationSpeed);
            }
        }

        private List<MonsterSpawner> InitSpawners(LevelStaticData levelData)
        {
            List<MonsterSpawner> monsterSpawners = new List<MonsterSpawner>();
            for (int i = 0; i < levelData.MonsterSpawnerDatas.Count; i++)
            {
                MonsterSpawnerData spawnerData = levelData.MonsterSpawnerDatas[i];
                GameObject monsterSpawnerGO = _gameFactory.CreateMonsterSpawner();

                MonsterSpawner monsterSpawner = monsterSpawnerGO.GetComponent<MonsterSpawner>();

                monsterSpawnerGO.transform.position = spawnerData.m_position;
                monsterSpawner.Construct(_gameFactory, _staticData, spawnerData.m_targetPosition,
                    spawnerData.m_monsterType, spawnerData.m_spawnInterval);
                monsterSpawners.Add(monsterSpawner);
            }

            return monsterSpawners;
        }

        private void InitUpdateManger(IUpdateService updateService)
        {
            GameObject updateManager = _gameFactory.CreateUpdateManager();
            UpdateManagerProvider updateProvider = updateManager.GetComponent<UpdateManagerProvider>();
            updateProvider.Construct(updateService);
        }

        private LevelStaticData LevelStaticData() => _staticData.ForLevel(SceneManager.GetActiveScene().name);
    }
}