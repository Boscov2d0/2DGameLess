using Profile;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;
using Profile.Ads.UnityAds;
using Profile.IAP;

namespace Ui
{
    internal class MainMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/MainMenu");
        private readonly ProfilePlayer _profilePlayer;
        private readonly MainMenuView _view;
        private UnityAdsService _adsService;
        private IAPService _iapService;

        public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);
            _view.Init(StartGame, GameSettings, StartAds, Shop);
            _adsService = _view.AdsService;
        }

        private MainMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<MainMenuView>();
        }

        private void StartGame() =>
            _profilePlayer.CurrentState.Value = GameState.Game;

        private void GameSettings() =>
            _profilePlayer.CurrentState.Value = GameState.Settings;

        private void StartAds()
        {
            if (_adsService.IsInitialized) OnAdsInitialized();
            else _adsService.Initialized.AddListener(OnAdsInitialized);
        }
        private void Shop() 
        {
            if (_iapService.IsInitialized) OnIapInitialized();
            else _iapService.Initialized.AddListener(OnIapInitialized);
        }
        public void OnDestroy()
        {
            _adsService.Initialized.RemoveListener(OnAdsInitialized);
            _iapService.Initialized.RemoveListener(OnIapInitialized);
        }

        private void OnAdsInitialized() => _adsService.RewardedPlayer.Play();
        private void OnIapInitialized() => _iapService.Buy("1");
    }
}
