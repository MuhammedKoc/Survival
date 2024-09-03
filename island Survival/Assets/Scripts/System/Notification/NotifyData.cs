using UnityEngine;
using UnityEngine.Localization;

namespace Notification
{
    [CreateAssetMenu(fileName = "New Notify Data", menuName = "Notification/NotifyData", order = 0)]
    public class NotifyData : ScriptableObject
    {
        public NotifyTypes type;
        
        [Space(10)]
        
        public Sprite icon;
        public LocalizedString localizedText;
    }
}