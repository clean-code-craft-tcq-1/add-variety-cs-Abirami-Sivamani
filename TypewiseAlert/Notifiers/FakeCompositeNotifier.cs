using System;
using System.Collections.Generic;
using System.Text;
using TypewiseAlert.Interfaces;
using static TypewiseAlert.TypewiseAlert;

namespace TypewiseAlert.Notifiers
{
    public class FakeCompositeNotifier : INotification
    {
        List<INotification> _notifierList = new List<INotification>();
        public bool IsCompositeTriggerNotificationCalledOnce = false;
        public void TriggerNotification(BreachType breachType)
        {
            IsCompositeTriggerNotificationCalledOnce = true;
        }

        public void AddNotifierToList(INotification _notifier)
        {
            _notifierList.Add(_notifier);
        }
    }
}
