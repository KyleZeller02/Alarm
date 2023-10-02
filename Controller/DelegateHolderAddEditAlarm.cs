using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alarm501
{
    /// <summary>
    /// A container class that holds delegate instances specific to the AddEditAlarm Form.
    /// These delegates allow for greater separation of concerns by decoupling the form from
    /// the underlying logic.
    /// </summary>
    public class DelegateHolderAddEditAlarm
    {
        /// <summary>
        /// Gets or sets the delegate used to update an existing alarm at a specific index.
        /// </summary>
        public UpdateAlarmDelegate UpdateAlarm { get; set; }

        /// <summary>
        /// Gets or sets the delegate used to add a new alarm.
        /// </summary>
        public AddAlarmDelegate AddAlarm { get; set; }

        /// <summary>
        /// Gets or sets the delegate used to close the AddEditAlarm form.
        /// </summary>
        public CloseFormDelegate CloseForm { get; set; }

        /// <summary>
        /// Gets or sets the delegate used to fetch the selected time from the AddEditAlarm form.
        /// </summary>
        public GetSelectedTimeDelegate GetSelectedTime { get; set; }

        /// <summary>
        /// Gets or sets the delegate used to check the status of the checkbox in the AddEditAlarm form.
        /// </summary>
        public GetCheckBoxStatus IsChecked { get; set; }

        /// <summary>
        /// Gets the selected alarm sound from the view.
        /// </summary>
        public GetAlarmSound AlarmSound { get; set; }
    }
}
