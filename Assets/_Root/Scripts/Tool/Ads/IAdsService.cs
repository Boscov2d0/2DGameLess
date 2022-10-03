using UnityEngine.Events;

namespace Profile.Ads
{
    internal interface IAdsService
    {
        IAdsPlayer InterstitialPlayer { get; }
        IAdsPlayer RewardedPlayer { get; }
        IAdsPlayer BannerPlayer { get; }
        UnityEvent Initialized { get; }
        bool IsInitialized { get; }
    }
}