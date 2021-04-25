using System;
using System.Collections.Generic;
using System.Text;
using TypewiseAlert.Interfaces;
using static TypewiseAlert.TypewiseAlert;

namespace TypewiseAlert.Notifiers
{
    public class CompositeNotifier : INotification
    {
        List<INotification> _notifierList= new List<INotification>();
        public void TriggerNotification(BreachType breachType)
        {
            foreach(INotification _notifier in _notifierList)
            {
                _notifier.TriggerNotification(breachType);
            }
        }

        public void AddNotifierToList(INotification _notifier)
        {
            _notifierList.Add(_notifier);
        }
    }
}
