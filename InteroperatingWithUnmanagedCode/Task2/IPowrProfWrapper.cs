using System.Runtime.InteropServices;

namespace Task2
{
    [ComVisible(true)]
    [Guid("7D0BC2EF-2542-4C0C-A84E-20715B73C157")]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IPowrProfWrapper
    {
        /// <summary>
        /// Reserves or removes system hibernation file.
        /// </summary>
        /// <param name="reserve">
        /// The reserver. If parameter is set to TRUE then the hibernation file is reserved,
        /// if the parameter is set to FALSE then the hibernation file is removed.
        /// </param>
        /// <returns>
        /// Returns the numner which stands for one of Nt statuses.
        /// </returns>
        uint ReserveHibernationFile(bool reserve);

        /// <summary>
        /// Turns on sleep mode.
        /// </summary>
        /// <returns>
        /// Returns true if a sleep mode was successfully activated.
        /// </returns>
        bool TurnOnSleepMode();
    }
}