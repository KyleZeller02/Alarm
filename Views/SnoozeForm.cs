using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alarm501
{
    /// <summary>
    /// Represents the "SnoozeForm" view, which is a part of the Alarm501 application.
    /// This form provides the user with the ability to set the snooze duration for an alarm.
    /// </summary>
    public partial class SnoozeForm : Form
    {
        // Container for delegates specific to snooze-related actions.
        private DelegateHolderSnooze delegateHolderSnooze;

        // Represents the selected alarm's index.
        private int index;

        /// <summary>
        /// Initializes a new instance of the <see cref="SnoozeForm"/> class.
        /// </summary>
        /// <param name="index">The index of the selected alarm to snooze.</param>
        public SnoozeForm(int index)
        {
            InitializeComponent();
            this.index = index;
        }

        /// <summary>
        /// Sets up the delegate instances for the form.
        /// </summary>
        /// <param name="bucket">Container holding the delegates.</param>
        public void SetUpDelegates(DelegateHolderSnooze bucket)
        {
            this.delegateHolderSnooze = bucket;
        }

        /// <summary>
        /// Handles the event when the OK button is clicked.
        /// </summary>
        private void uxSnoozeOk_Click(object sender, EventArgs e)
        {
            // Handles the snooze operation by using the provided delegate.
            delegateHolderSnooze.FinishSnooze(parseDateTime(), index);

            // Close the snooze form.
            this.Close();
        }

        /// <summary>
        /// Attempts to parse the user input from the TextBoxes and generate a TimeSpan representation of the snooze duration.
        /// </summary>
        /// <returns>A TimeSpan representing the snooze duration or null if any input is invalid.</returns>
        public TimeSpan? parseDateTime()
        {
            int? seconds = ParseTextBoxContent(secondsTxtBox);
            int? minutes = ParseTextBoxContent(MinutesTxtBox);
            int? hours = ParseTextBoxContent(HoursTxtBox);

            if (seconds == null || minutes == null || hours == null)
            {
                return null;
            }
            else
            {
                return new TimeSpan(hours.Value, minutes.Value, seconds.Value);
            }
        }

        /// <summary>
        /// This event is triggered when the form is loaded. It currently contains no custom logic.
        /// </summary>
        private void SnoozeForm_Load(object sender, EventArgs e)
        {
           
        }

        /// <summary>
        /// Attempts to parse an integer value from the content of the given TextBox.
        /// </summary>
        /// <param name="textBox">The TextBox whose content needs to be parsed.</param>
        /// <returns>An integer parsed from the TextBox content or null if parsing fails.</returns>
        private int? ParseTextBoxContent(TextBox textBox)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                return 0;
            }

            if (int.TryParse(textBox.Text, out int result))
            {
                return result;
            }

            return null;
        }

        /// <summary>
        /// Handles the event when the Cancel button is clicked.
        /// </summary>
        private void uxSnoozeCancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }


}
