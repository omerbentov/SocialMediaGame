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
        public UIManager UI { get; private set; }
        public LevelManager Level { get; private set; }

        #endregion


        #region --- Construction ---

        public Client(UpdateManager updateManager)
        {
            SetupSingleton(this);
            LoadConfiguration();
            SetupSystems();
            SetupManagers(updateManager);
        }

        private void LoadConfiguration()
        {
            Configuration = Resources.Load<SocialGameConfigurationSO>("Configuration/Main/Main");
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
            Popups = new PopupsManager(this);
            Update = updateManager;
            Level = new LevelManager();
            UI = new UIManager();
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
        UIManager UI { get; }
        LevelManager Level { get; }

        #endregion
    }
}