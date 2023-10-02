
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alarm501
{
    /// <summary>
    /// Represents the main form of the Alarm501 application, allowing users to manage alarms.
    /// </summary>
    public partial class Alarm501 : Form
    {
        /// <summary>
        /// Holds delegate references specific to the Alarm501 Form, providing a bridge between the UI interactions and underlying business logic.
        /// </summary>
        private DelegateHolderAlarm501 delegateHolder501;

        /// <summary>
        /// Initializes a new instance of the <see cref="Alarm501"/> class.
        /// </summary>
        public Alarm501()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets up the delegate holder with associated actions and functionalities for the Alarm501 Form.
        /// </summary>
        /// <param name="bucket">The delegate holder containing methods to be used by the form.</param>
        public void SetUpDelegates(DelegateHolderAlarm501 bucket)
        {
            this.delegateHolder501 = bucket;
        }

        /// <summary>
        /// Event handler for the "Add" button click. Initiates the process to add a new alarm.
        /// </summary>
        private void UxAddBtn_Click(object sender, EventArgs e)
        {
            delegateHolder501.StartAddAlarm();
        }

        /// <summary>
        /// Event handler for the form's Load event. Handles necessary setups upon form loading.
        /// </summary>
        private void Alarm501_Load(object sender, EventArgs e)
        {
            delegateHolder501.HandleOpen();
            UxAlarmList.SelectedIndex = -1; // Ensures no alarm is initially selected.
        }

        /// <summary>
        /// Updates the list of alarms displayed in the form.
        /// </summary>
        /// <param name="alarms">List of alarms to display.</param>
        public void UpdateAlarmListMethod(List<Alarm> alarms)
        {
            UxAlarmList.DataSource = null;
            UxAlarmList.DataSource = alarms;
        }

        /// <summary>
        /// Event handler for the form's Closed event. Executes any necessary teardown actions upon form closure.
        /// </summary>
        private void Alarm501_FormClosed(object sender, FormClosedEventArgs e)
        {
            delegateHolder501.HandleFormClosed();
        }

        /// <summary>
        /// Event handler for the "Edit" button click. Initiates the process to edit the selected alarm.
        /// </summary>
        private void UxEditBtn_Click(object sender, EventArgs e)
        {
            delegateHolder501.HandleEdit(UxAlarmList.SelectedIndex);
        }

        /// <summary>
        /// Event handler for the "Stop" button click. Initiates the process to stop the selected alarm.
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            delegateHolder501.HandleStopAlarm(UxAlarmList.SelectedIndex);
        }

        /// <summary>
        /// Updates the status text box with the provided message.
        /// </summary>
        /// <param name="status">Status message to display.</param>
        public void UpdateTextBoxAndAlarmListMethod(string status)
        {
            if (uxAlarmAlert.InvokeRequired)
            {
                uxAlarmAlert.Invoke(new Action(() => uxAlarmAlert.Text = status)); // make sure we update the view on the main thread
            }
            else
            {
                uxAlarmAlert.Text = status;
            }
        }

        /// <summary>
        /// Event handler for the "Snooze" button click. Initiates the process to snooze the selected alarm.
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            delegateHolder501.StartSnoozeAlarm(UxAlarmList.SelectedIndex);
        }

        /// <summary>
        /// Updates the enablement states of the UI buttons based on the current state of the alarms.
        /// </summary>
        private void UpdateUIButtons(bool editEnabled, bool snoozeEnabled, bool stopEnabled)
        {
            Action action = () => //set button status to the bool passed in the method
            {
                UxEditBtn.Enabled = editEnabled;
                UxSnoozeButton.Enabled = snoozeEnabled;
                UxStopButton.Enabled = stopEnabled;
            };

            if (this.InvokeRequired) // get on the main thread
            {
                this.Invoke(action);
            }
            else
            {
                action();
            }
        }

        /// <summary>
        /// Determines the enablement states of the UI buttons and updates them. this is the method the controller uses
        /// </summary>
        public void EnableDisableButtons()
        {
            if (this.InvokeRequired) //get on the main thread
            {
                this.Invoke(new Action(() => DetermineAndUpdateButtonStates()));
            }
            else
            {
                DetermineAndUpdateButtonStates();
            }
        }

        /// <summary>
        /// Determines the button enablement states based on the selected alarm's state and updates them.
        /// </summary>
        private void DetermineAndUpdateButtonStates()
        {
            // Retrieve the alarms from the data source.
            List<Alarm> alarms = UxAlarmList.DataSource as List<Alarm>;

            // Check if there are any alarms in the list.
            bool hasItems = alarms != null && alarms.Count > 0;

            // Get the currently selected index from the alarm list.
            int selectedIndex = UxAlarmList.SelectedIndex;

            // Validate that the selected index is within the bounds of the alarms list.
            bool isIndexValid = hasItems && selectedIndex >= 0 && selectedIndex < alarms.Count;

            // Get the alarm corresponding to the selected index if it's valid, otherwise, null.
            Alarm selectedAlarm = isIndexValid ? alarms[selectedIndex] : null;

            // Determine the enablement states of the buttons based on the selected alarm.
            (bool editEnabled, bool snoozeEnabled, bool stopEnabled) = delegateHolder501.EnableDisableButtons(selectedAlarm);

            // Update the UI buttons based on the determined states.
            UpdateUIButtons(editEnabled, snoozeEnabled, stopEnabled);
        }


        /// <summary>
        /// Event handler for changes in the alarm list selection. Updates the button enablement states accordingly.
        /// </summary>
        private void UxAlarmList_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableDisableButtons(); //whenever a new alarm is selected, we need to change button states
        }
    }

}
