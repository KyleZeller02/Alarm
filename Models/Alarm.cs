using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alarm501
{
    /// <summary>
    /// Represents an Alarm entity with a trigger time, nullable snooze time, and active status.
    /// </summary>
    public class Alarm
    {
        #region Properties

        /// <summary>
        /// Gets or sets the time when the alarm should be triggered.
        /// </summary>
        public DateTime OriginalTriggerTime { get; set; }

        /// <summary>
        /// Gets or sets the time when the snoozed alarm should be triggered.
        /// This is marked as nullable, might not exist, be sure to check when it matters
        /// This is used so that we do not over ride the original trigger time
        /// </summary>
        public DateTime? SnoozeTime { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the alarm is active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// gets or sets the alarm sound for the alarm.
        /// </summary>
        public AlarmSound AlarmSound { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Alarm"/> class with specified trigger time and active status.
        /// </summary>
        /// <param name="triggerTime">The time when the alarm should be triggered.</param>
        /// <param name="isActive">A value indicating whether the alarm is active.</param>
        public Alarm(DateTime triggerTime, bool isActive, AlarmSound sound)
        {
            this.OriginalTriggerTime = triggerTime;
            this.IsActive = isActive;
            this.AlarmSound = sound;
            this.SnoozeTime = null; // No snooze time by default
        }

        #endregion

        #region Methods


        // <summary>
        /// Formats the alarm time and status as a string. 
        /// this says it does not have references, but is needed for the listbox 
        /// </summary>
        /// <returns>A string representing the alarm time and status.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            // Use SnoozeTime if it's set, otherwise use OriginalTriggerTime
            DateTime displayTime = SnoozeTime ?? OriginalTriggerTime;

            // Append the display time in the format "h:mm tt"
            sb.Append(displayTime.ToString("h:mm tt"));

            // Separate the time and the status
            sb.Append("\t");

            // Append the status of the alarm
            sb.Append(IsActive ? " ON" : " OFF");

            return sb.ToString();
        }


        /// <summary>
        /// Compares this Alarm object with another object for equality.
        /// </summary>
        /// <param name="obj">The object to compare with.</param>
        /// <returns>True if the objects are equal, false otherwise.</returns>
        public override bool Equals(object obj)
        {
            // Check for null
            if (obj == null)
            {
                return false;
            }

            // Check for the same reference
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            // Check for the same type
            if (GetType() != obj.GetType())
            {
                return false;
            }

            // Cast to Alarm and compare properties
            Alarm other = (Alarm)obj;
            return OriginalTriggerTime == other.OriginalTriggerTime &&
                   SnoozeTime == other.SnoozeTime &&
                   IsActive == other.IsActive;
        }
        #endregion
    }

}
