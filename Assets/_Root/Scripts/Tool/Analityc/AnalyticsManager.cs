using UnityEngine;

namespace Profile.Analytic
{
    public class AnalyticsManager : MonoBehaviour
    {
        private IAnalyticTools[] _analyticTools;

        private void Awake() => _analyticTools = new IAnalyticTools[]
        {
            new UnityAnalyticTools()
        };

        public void SendStartGame() => SendMessage("Start game");

        private void SendAnalitycMessage(string message) 
        {
            for (int i = 0; i < _analyticTools.Length; i++) 
            {
                _analyticTools[i].SendMessage(message);
            }
        }
    }
}