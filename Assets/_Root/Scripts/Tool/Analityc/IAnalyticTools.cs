using System.Collections.Generic;

namespace Profile.Analytic
{
    internal interface IAnalyticTools
    {
        void SendMessage(string message);
        void SendMessage(string message, IDictionary<string, object> eventData);
    }
}