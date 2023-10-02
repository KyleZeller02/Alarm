
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alarm501
{
    #region Delegates

    // ====================
    // Delegates for AddEditAlarmForm
    // ====================

    /// <summary>
    /// Updates an existing alarm at the specified index.
    /// </summary>
    /// <param name="index">The index of the alarm to update.</param>
    public delegate void UpdateAlarmDelegate(int index, bool isChecked, DateTime? trigger, AlarmSound? sound);

    /// <summary>
    /// Adds a new alarm to the list of alarms.
    /// </summary>
    public delegate void AddAlarmDelegate(DateTime triggerTime, bool check, AlarmSound? sound);

    /// <summary>
    /// Closes the AddEditAlarmForm.
    /// </summary>
    public delegate void CloseFormDelegate();

    /// <summary>
    /// Retrieves the selected time from the AddEditAlarmForm UI.
    /// </summary>
    /// <returns>The DateTime value of the selected time.</returns>
    public delegate DateTime GetSelectedTimeDelegate();

    /// <summary>
    /// Checks the status of the checkbox in the AddEditAlarmForm UI.
    /// </summary>
    /// <returns>True if the checkbox is checked, otherwise false.</returns>
    public delegate bool GetCheckBoxStatus();

    /// <summary>
    /// Gets the selected alarm noise from the AddEditAlarmForm UI.
    /// </summary>
    /// <returns>the alarm sound as enum</returns>
    public delegate AlarmSound? GetAlarmSound();

    // ====================
    // Delegates for Alarm501
    // ====================

    /// <summary>
    /// Updates the Edit, Snooze, and/or Stop buttons in the Alarm501 form
    /// </summary>
    public delegate (bool EditEnabled, bool SnoozeEnabled, bool StopEnabled) EnableDisableButtonsDelegate(Alarm selectedAlarm);

    /// <summary>
    /// Handles the opening of the Alarm501 form.
    /// </summary>
    public delegate void HandleOnOpenEventDelegate();

    /// <summary>
    /// Handles the editing of an alarm.
    /// </summary>
    /// <param name="index">Index of the alarm to edit.</param>
    public delegate void HandleEditDelegate(int index);

    /// <summary>
    /// Handles updating the alarm list in the main view
    /// </summary>
    /// <returns>the updated list</returns>
    public delegate List<Alarm> GetAlarmsDelegate();


    /// <summary>
    /// Stops an active alarm.
    /// </summary>
    public delegate void HandleStopAlarmDelegate(int index);

    /// <summary>
    /// Handles the closed event of the Alarm501 form.
    /// </summary>
    public delegate void HandleFormClosedDelegate();

    
    /// <summary>
    /// Delegate to handle the snooze logic
    ///</summary>
    public delegate void HandleSnoozeDelegate(TimeSpan? snoozeDuration, int index);

    /// <summary>
    /// Delegate to begin the process of adding an alarm
    /// </summary>
    public delegate void StartAddAlarm();

    /// <summary>
    /// Delegate to begin the process of snoozing an alarm.
    /// </summary>
   public delegate void StartSnooze(int index);

    // ====================
    // SnoozeForm Delegates
    // ====================
    /// <summary>
    /// Delegate to handle Adding snooze time when ok is pressed on the form
    /// </summary>
    public delegate void FinishSnoozeDel( TimeSpan? time, int index);

    /// <summary>
    /// Delegate to get the snooze time from the snooze form.
    /// </summary>
    /// <returns></returns>
    public delegate TimeSpan? GetSnoozeDurationDelegate();

    #endregion


    //----------------------------------------------------------------------------------------
    // <summary>
    //   Represents the main entry point class for the Alarm501 application.
    // </summary>
    // <remarks>
    //   This class sets up the main application form, initializes the controller, and
    //   establishes necessary event handlers and delegates to ensure correct behavior
    //   and communication between the view and controller components.
    // </remarks>
    //----------------------------------------------------------------------------------------

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Create an instance of the Controller class which will manage application logic.
            Controller controller = new Controller();

            // Set up basic visual styles and compatibility settings for the Windows form application.
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Alarm501 mainForm = new Alarm501();

            // To enhance modularity and reduce coupling, delegate references are used to allow the form 
            // to indirectly call controller functions without having a direct reference to the controller.
            DelegateHolderAlarm501 delegateholderAlarm = new DelegateHolderAlarm501
            {
                HandleOpen = controller.HandleOnOpenEvent,
                HandleEdit = controller.HandleEdit,
                HandleStopAlarm = controller.StopAlarm,
                HandleFormClosed = controller.HandleOnCloseEvent,
                EnableDisableButtons = controller.DecideButtonStates,
                HandleSnooze = controller.HandleSnooze,
                StartAddAlarm = controller.startAddAlarm,
                StartSnoozeAlarm = controller.startSnooze,
                GetAlarms = controller.FetchAllAlarms
            };

            // Pass the delegate references to the main form for it to use.
            mainForm.SetUpDelegates(delegateholderAlarm);

            // Subscribe the main form methods to controller events for real-time updates.
            controller.MessageEvent += mainForm.UpdateTextBoxAndAlarmListMethod;
            controller.UpdateAlarmEvent += mainForm.UpdateAlarmListMethod;
            controller.UpdateButtonsEvent += mainForm.EnableDisableButtons;

            // Start and run the main application form.
            Application.Run(mainForm);
        }
    }

}
