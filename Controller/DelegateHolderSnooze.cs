using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alarm501
{
    /// <summary>
    /// Provides a container for delegate references associated with the snooze functionality.
    /// By consolidating these delegates, this class promotes modular design and facilitates separation 
    /// between user interface interactions and business logic.
    /// </summary>
    public class DelegateHolderSnooze
    {
        /// <summary>
        /// Delegate responsible for retrieving the user-specified snooze duration.
        /// Expected to return a duration (such as a TimeSpan) indicating how long an alarm should be snoozed.
        /// </summary>
        public GetSnoozeDurationDelegate GetSnoozeDurationMethod { get; set; }

        /// <summary>
        /// Delegate responsible for executing the final snooze logic once the snooze duration has been determined.
        /// It encompasses actions like updating alarm states, refreshing UI elements, or reconfiguring associated timers.
        /// </summary>
        public FinishSnoozeDel FinishSnooze { get; set; }
    }

}
