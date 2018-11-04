using System;

namespace Task1Library
{
    public struct SYSTEM_BATTERY_STATE
    {
        public byte AcOnLine;
        public byte BatteryPresent;
        public byte Charging;
        public byte Discharging;
        public byte spare1;
        public byte spare2;
        public byte spare3;
        public byte spare4;
        public UInt32 MaxCapacity;
        public UInt32 RemainingCapacity;
        public Int32 Rate;
        public UInt32 EstimatedTime;
        public UInt32 DefaultAlert1;
        public UInt32 DefaultAlert2;

        public override string ToString()
        {
            return $"SYSTEM_BATTERY_STATE\nAcOnLine: {AcOnLine}\nBatteryPresent: {BatteryPresent}\nCharging: {Charging}\nDischarging: {Discharging}\nspare1: {spare1}\nspare2: {spare2}\nspare3: {spare3}\nspare4: {spare4}\nMaxCapacity: {MaxCapacity}\nRemainingCapacity: {RemainingCapacity}\nRate: {Rate}\nEstimatedTime: {EstimatedTime}\nDefaultAlert1: {DefaultAlert1}\nDefaultAlert2: {DefaultAlert2}";
        }
    }
}
