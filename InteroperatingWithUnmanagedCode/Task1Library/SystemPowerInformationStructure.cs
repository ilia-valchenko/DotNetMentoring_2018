namespace Task1Library
{
    public struct SYSTEM_POWER_INFORMATION
    {
        public ulong MaxIdlenessAllowed;
        public ulong Idleness;
        public ulong TimeRemaining;
        public char CoolingMode;

        public override string ToString()
        {
            return $"SYSTEM_POWER_INFORMATION\nMaxIdlenessAllowed: {MaxIdlenessAllowed}\nIdleness: {Idleness}\nTimeRemaining: {TimeRemaining}\nCoolingMode: {CoolingMode}\n";
        }
    }
}
