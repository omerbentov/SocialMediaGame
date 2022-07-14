using Systems;
using GameStates;
using Handlers;
using Managers;

namespace Client
{
    public class Client : IClient
    {
        #region --- Properties ---

        public static IClient Instance { get; private set; }
        public PopupsManager PopupsManager { get; }
        
        public StateMachine StateMachine { get; }
        public UpdateManager UpdateManager { get; }
        public AssetBundleSystem AssetBundleSystem { get; private set; }
        public BroadcastSystem Broadcaster { get; private set; }
        public CameraManager CameraManager { get; private set; }
        public ISaverManager GameSaverManager { get; private set; }

        #endregion


        #region --- Construction ---

        public Client(UpdateManager updateManager)
        {
            SetupSingleton(this);
            UpdateManager = updateManager;

            SetupSystems();

            PopupsManager = new PopupsManager(this);
        }

        #endregion


        #region --- Private Methods ---

        private static void SetupSingleton(IClient client)
        {
            Instance = client;
        }

        private void SetupSystems()
        {
            CameraManager = new CameraManager();
            GameSaverManager = new GameSaverManager();

            Broadcaster = new BroadcastSystem();
            AssetBundleSystem = new AssetBundleSystem();
        }

        #endregion
    }

    public interface IClient
    {
        #region --- Properties ---

        AssetBundleSystem AssetBundleSystem { get; }
        BroadcastSystem Broadcaster { get; }
        CameraManager CameraManager { get; }
        ISaverManager GameSaverManager { get; }
        PopupsManager PopupsManager { get; }
        StateMachine StateMachine { get; }
        UpdateManager UpdateManager { get; }

        #endregion
    }
}