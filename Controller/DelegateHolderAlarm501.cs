
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alarm501
{
    /// <summary>
    /// A container class that holds delegate instances related to operations on the Alarm501 Form.
    /// These delegate references facilitate the separation of UI and business logic, 
    /// promoting modular design and easier maintainability.
    /// </summary>
    public class DelegateHolderAlarm501
    {
        /// <summary>
        /// Delegate to handle the open event of the Alarm501 Form.
        /// </summary>
        public HandleOnOpenEventDelegate HandleOpen { get; set; }

        /// <summary>
        /// Delegate to handle the edit action for an alarm within the Alarm501 Form.
        /// </summary>
        public HandleEditDelegate HandleEdit { get; set; }

        /// <summary>
        /// Delegate to stop a ringing alarm within the Alarm501 Form.
        /// </summary>
        public HandleStopAlarmDelegate HandleStopAlarm { get; set; }

        /// <summary>
        /// Delegate to handle the form's close event within the Alarm501 Form.
        /// </summary>
        public HandleFormClosedDelegate HandleFormClosed { get; set; }

        /// <summary>
        /// Delegate to manage the enablement or disablement of UI buttons within the Alarm501 Form.
        /// </summary>
        public EnableDisableButtonsDelegate EnableDisableButtons { get; set; }

        /// <summary>
        /// Delegate to process the snooze functionality for an alarm in the Alarm501 Form.
        /// </summary>
        public HandleSnoozeDelegate HandleSnooze { get; set; }

        /// <summary>
        /// Delegate to initiate the procedure of adding a new alarm in the Alarm501 Form.
        /// </summary>
        public StartAddAlarm StartAddAlarm { get; set; }

        /// <summary>
        /// Delegate to begin the procedure of snoozing an active alarm in the Alarm501 Form.
        /// </summary>
        public StartSnooze StartSnoozeAlarm { get; set; }

       

        /// <summary>
        /// Delegate to fetch the current set of alarms from the Alarm501 Form.
        /// </summary>
        public GetAlarmsDelegate GetAlarms { get; set; }

    }


}

