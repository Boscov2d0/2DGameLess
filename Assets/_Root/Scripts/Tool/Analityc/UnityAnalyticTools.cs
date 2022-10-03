using System.Collections.Generic;
using UnityEngine.Analytics;

namespace Profile.Analytic
{
    internal class UnityAnalyticTools : IAnalyticTools
    {
        public void SendMessage(string message) => Analytics.CustomEvent(message);
        public void SendMessage(string message, IDictionary<string, object> eventData) => Analytics.CustomEvent(message, eventData);
    }
}