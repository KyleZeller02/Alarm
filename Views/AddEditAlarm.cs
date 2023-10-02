
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Alarm501.Controller;
using static Alarm501.DelegateHolderAddEditAlarm;

namespace Alarm501
{
    /// <summary>
    /// Represents the Add/Edit Alarm Form in the Alarm501 application, facilitating the creation and modification of alarm entities.
    /// </summary>
    public partial class AddEditAlarm : Form
    {
        /// <summary>
        /// Holds delegate references that encapsulate methods specific to the AddEditAlarm Form.
        /// This structure promotes separation between user interface interactions and business logic.
        /// </summary>
        private DelegateHolderAddEditAlarm delegateholderAddEdit;

        /// <summary>
        /// Contains the alarm's initial state or settings. If editing an existing alarm, it captures the current properties of the alarm.
        /// </summary>
        private Alarm initialAlarm;

        /// <summary>
        /// Captures the index of the selected alarm in the main application list. 
        /// The value is -1 when adding a new alarm, otherwise, it indicates the position of the alarm being edited.
        /// </summary>
        private int selectedIndex;

        /// <summary>
        /// Initializes the AddEditAlarm Form, setting up the required elements for either adding a new alarm or editing an existing one.
        /// </summary>
        /// <param name="inject">Existing alarm to be edited; null if adding a new alarm.</param>
        /// <param name="selectedIndex">Index of the alarm to edit, or -1 for new alarms.</param>
        public AddEditAlarm(Alarm inject, int selectedIndex)
        {
            // Basic form initialization logic.
            InitializeComponent();

            // Initial setup and configurations.
            this.selectedIndex = selectedIndex;
            this.initialAlarm = inject;
            dateTimePicker1.Format = DateTimePickerFormat.Time;
            dateTimePicker1.ShowUpDown = true;

            // Set initial alarm details if in 'edit' mode.
            if (inject != null)
            {
                dateTimePicker1.Value = initialAlarm.OriginalTriggerTime;
            }
            else
            {
                dateTimePicker1.Value = DateTime.Now;
            }
        }

        /// <summary>
        /// Handles the Form's Load event. Prepares the form's UI elements, including populating fields if editing an existing alarm.
        /// </summary>
        private void AddEditAlarm_Load(object sender, EventArgs e)
        {
            // Populate fields if editing an alarm.
            if (this.initialAlarm != null)
            {
                dateTimePicker1.Value = initialAlarm.OriginalTriggerTime;
                checkBox1.Checked = initialAlarm.IsActive;
                uxComboBoxSounds.SelectedText = initialAlarm.AlarmSound.ToString();
            }

            // Populate the combo box with available alarm sounds.
            foreach (AlarmSound sound in Enum.GetValues(typeof(AlarmSound)))
            {
                uxComboBoxSounds.Items.Add(sound);
            }
        }

        /// <summary>
        /// Event handler for the 'Cancel' button. Closes the form without applying any changes.
        /// </summary>
        private void UxCancelEditAlarmBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Event handler for the 'Set' button. Determines whether the form is in 'add' or 'edit' mode and applies changes accordingly.
        /// </summary>
        private void UXSetAlarmBtn_Click(object sender, EventArgs e)
        {
            if (initialAlarm != null)
            {
                delegateholderAddEdit.UpdateAlarm(selectedIndex, IsCheckBoxChecked(), GetSelectedTime(), GetSound());
            }
            else
            {
                delegateholderAddEdit.AddAlarm(GetSelectedTime(), IsCheckBoxChecked(), GetSound());
            }
            this.Close();
        }

        /// <summary>
        /// Retrieves the user-selected time from the form.
        /// </summary>
        public DateTime GetSelectedTime()
        {
            return DateTime.Today.Add(dateTimePicker1.Value.TimeOfDay);
        }

        /// <summary>
        /// Determines whether the alarm should be active based on the state of the checkbox in the form.
        /// </summary>
        public bool IsCheckBoxChecked()
        {
            return checkBox1.Checked;
        }

        /// <summary>
        /// Fetches the alarm sound chosen by the user from the dropdown list.
        /// </summary>
        public AlarmSound? GetSound()
        {
            if (uxComboBoxSounds.SelectedItem is AlarmSound selectedSound)
            {
                return selectedSound;
            }
            if (Enum.TryParse<AlarmSound>(uxComboBoxSounds.Text, true, out AlarmSound parsedSound))
            {
                return parsedSound;
            }
            return null;
        }

        /// <summary>
        /// Sets the delegate holder, which encapsulates methods for the AddEditAlarm Form, enabling separation of concerns.
        /// </summary>
        public void SetUpDelegate(DelegateHolderAddEditAlarm bucket)
        {
            this.delegateholderAddEdit = bucket;
        }
    }

}
