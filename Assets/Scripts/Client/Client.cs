using Systems;
using GameStates;
using Handlers;
using Managers;
using UnityEngine;

namespace Client
{
    public class Client : IClient
    {
        #region --- Properties ---

        public static IClient Instance { get; private set; }
        public PopupsManager Popups { get; private set; }
        public StateMachine StateMachine { get; }
        public UpdateManager Update { get; private set; }
        public AssetBundleSystem AssetBundle { get; private set; }
        public BroadcastSystem Broadcaster { get; private set; }
        public CameraManager Camera { get; private set; }
        public ISaverManager GameSaver { get; private set; }
        public SocialGameConfigurationSO Configuration { get; private set; }
        public UIController UI { get; private set; }
        public DataManager Data { get; private set; }
        public LevelManager Level { get; private set; }

        #endregion


        #region --- Construction ---

        public Client(UpdateManager updateManager)
        {
            SetupSingleton(this);
            LoadConfiguration();
            SetupSystems();
            SetupManagers(updateManager);
            SetupControllers();
        }

        private void LoadConfiguration()
        {
            Configuration = Resources.Load<SocialGameConfigurationSO>("Configuration/Main/Main");
        }

        private void SetupControllers()
        {
            UI = new UIController();
        }

        #endregion


        #region --- Private Methods ---

        private static void SetupSingleton(IClient client)
        {
            Instance = client;
        }

        private void SetupSystems()
        {
            Camera = new CameraManager();
            GameSaver = new GameSaverManager();
            Broadcaster = new BroadcastSystem();
            AssetBundle = new AssetBundleSystem();
        }

        private void SetupManagers(UpdateManager updateManager)
        {
            Data = new DataManager();
            Popups = new PopupsManager(this);
            Update = updateManager;
            Level = new LevelManager();
        }

        #endregion
    }

    public interface IClient
    {
        #region --- Properties ---

        AssetBundleSystem AssetBundle { get; }
        BroadcastSystem Broadcaster { get; }
        CameraManager Camera { get; }
        ISaverManager GameSaver { get; }
        PopupsManager Popups { get; }
        StateMachine StateMachine { get; }
        UpdateManager Update { get; }
        SocialGameConfigurationSO Configuration { get; }
        UIController UI { get; }
        DataManager Data { get; }
        LevelManager Level { get; }

        #endregion
    }
}