using System;
using System.Collections.Generic;
using TypewiseAlert.Interfaces;
using TypewiseAlert.Notifiers;
using static TypewiseAlert.TypewiseAlert;

namespace TypewiseAlert
{
    public class TypewiseAlert
    {
        #region enums
        public enum BreachType {
            NORMAL,
            TOO_LOW,
            TOO_HIGH
        };
        
        public enum CoolingType
        {
            PASSIVE_COOLING,
            HI_ACTIVE_COOLING,
            MED_ACTIVE_COOLING
        };

        public enum AlertTarget
        {
            TO_CONTROLLER,
            TO_EMAIL,
            TO_CONSOLE,
        };
        #endregion

        #region struct
        public struct BatteryCharacter
        {
            public CoolingType coolingType;
            public string brand;
        }
        #endregion

        public static BreachType InferBreach(double value, double lowerLimit, double upperLimit) {
            return IsValueLowerThanLimit(value, lowerLimit, upperLimit);
        }

        public static BreachType IsValueLowerThanLimit(double value, double lowerLimit, double upperLimit)
        {
            return (value < lowerLimit) ? BreachType.TOO_LOW : IsValueHigherThanLimit(value, upperLimit);

        }

        public static BreachType IsValueHigherThanLimit(double value, double upperLimit)
        {
            return (value > upperLimit) ? BreachType.TOO_HIGH : BreachType.NORMAL;
        }

        public static BreachType ClassifyTemperatureBreach(CoolingType coolingType, double temperatureInC) 
        {
          ExtremeLimit _extremeLimit = new CoolingLimitDictionaryInitializer()._CoolingLimitType[coolingType]().SetExtremeLimit(coolingType);
          return InferBreach(temperatureInC, _extremeLimit.lowerLimit, _extremeLimit.upperLimit);
        }

        public static void CheckAndAlert(INotification _notifier, BatteryCharacter batteryChar, double temperatureInC) 
        {
          BreachType breachType = ClassifyTemperatureBreach(batteryChar.coolingType, temperatureInC);
          _notifier.TriggerNotification(breachType);
        }
              
    }
}
