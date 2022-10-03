using UnityEngine.Advertisements;

namespace Profile.Ads.UnityAds
{
    internal sealed class InterstitialPlayer : UnityAdsPlayer
    {
        public InterstitialPlayer(string id) : base(id) { }

        protected override void OnPlaying() => Advertisement.Show(Id);
        protected override void Load() => Advertisement.Load(Id);
    }
}