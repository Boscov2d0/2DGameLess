using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Profile.Ads.UnityAds;
using Profile.IAP;

namespace Ui
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _buttonStart;
        [SerializeField] private Button _buttonSettings;
        [SerializeField] private Button _buttonPlayRewardAds;
        [SerializeField] private Button _buttonShop;
        [SerializeField] private UnityAdsService _adsService;
        [SerializeField] private IAPService _iapService;

        internal UnityAdsService AdsService { get => _adsService; set => _adsService = value; }

        public void Init(UnityAction startGame, UnityAction gameSettings, UnityAction playAds, UnityAction shop)
        {
            _buttonStart.onClick.AddListener(startGame);
            _buttonSettings.onClick.AddListener(gameSettings);
            _buttonPlayRewardAds.onClick.AddListener(playAds);
            _buttonShop.onClick.AddListener(shop);
        }
        public void OnDestroy()
        {
            _buttonStart.onClick.RemoveAllListeners();
            _buttonSettings.onClick.RemoveAllListeners();
            _buttonPlayRewardAds.onClick.RemoveAllListeners();
            _buttonShop.onClick.RemoveAllListeners();
        }
    }
}