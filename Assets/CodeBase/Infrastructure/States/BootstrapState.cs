using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Services.StaticData;
using CodeBase.Services.Update;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string LevelName = "Level1";
        private readonly GameStateMachine _stateMachine;
        private SceneLoader _sceneLoader;
        private AllServices _services;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;

            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(LevelName, onLoaded: EnterLoadLevel);
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel()
        {
            _stateMachine.Enter<LoadLevelState, string>(LevelName);
        }

        private void RegisterServices()
        {
            RegisterAssetProvider();
            RegisterStaticData();

            AllServices.Container.RegisterSingle<IUpdateService>(new UpdateManager());

            _services.RegisterSingle<IGameFactory>(new GameFactory(
                _services.Single<IAssetProvider>(), _services.Single<IStaticDataService>()));
        }

        private void RegisterAssetProvider()
        {
            AssetProvider assetProvider = new AssetProvider();
            _services.RegisterSingle<IAssetProvider>(assetProvider);
        }

        private void RegisterStaticData()
        {
            IStaticDataService StaticData = new StaticDataService();
            StaticData.Initialize();
            _services.RegisterSingle<IStaticDataService>(StaticData);
        }
    }
}