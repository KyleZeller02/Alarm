
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json; // Required for JSON deserialization
using System.Threading;
using System.Windows.Forms;
using ThreadingTimer = System.Threading.Timer;
using System.Data;
using System.Security.Claims;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using System.Reflection;


namespace Alarm501
{
    
    public class Controller
    {

        
       

        #region Properties
        // Properties for the controller
        /// <summary>
        /// Dictionary to keep track of each Alarm object along with its associated timer.
        /// </summary>
        private Dictionary<Alarm, ThreadingTimer> alarmTimers = new Dictionary<Alarm, ThreadingTimer>();
        #endregion

        #region Events
        /// <summary>
        /// This event is used to update the status label in the main form
        /// </summary>
        public event Action<string> MessageEvent;

        /// <summary>
        /// this event is used to update the alarms list with the current alarms
        /// found in alarmTimers.Keys
        /// </summary>
        public event Action<List<Alarm>> UpdateAlarmEvent;

        /// <summary>
        /// this event is used to determine if buttons in the view are enabled/disabled
        /// For Example:
        ///     -Edit Button should only be enabled when the selected index for the alarm list box is withing
        ///     the range of alarmTimers.Keys.Count
        ///     -Stop button should only be active when an alarm is going off
        ///     -Snooze button should only be active when an alarm is going off
        /// </summary>
        public event Action UpdateButtonsEvent;
        #endregion


        #region Constructor

        /// <summary>
        /// the controller constructor has no logic done. All delegates are set upon form instantions.
        /// </summary>
        public Controller()
        {
            
        }

        #endregion

        #region Methods



        /// <summary>
        /// Handles the event triggered upon closing the application.
        /// </summary>
        /// <remarks>
        /// This method performs the following tasks:
        /// 1. Retrieves all alarms from the `alarmTimers` dictionary.
        /// 2. Skips any null alarms.
        /// 3. Serializes each alarm to a JSON representation using the `JsonConvert` class.
        /// 4. Appends each serialized JSON string to a StringBuilder.
        /// 5. Writes the combined JSON strings to the file "Alarms.txt".
        /// 6. Stops all alarms using the `StopAllAlarms` method.
        /// Note: This method assumes the delegate setup is correct. If it's not, and no alarms are present, the method will return early.
        /// </remarks>
        public void HandleOnCloseEvent()
        {
            List<Alarm> alarms = alarmTimers.Keys.ToList(); //get the alarms in the alarmTimers Dict as a List

            // Return early if alarms is null (bad delegate setup) or there are no alarms.
            if (alarms == null || alarms.Count == 0)
            {
                return;
            }

            const string filePath = "Alarms.txt"; // the name of the file we are writing to in the bin file

            StringBuilder sb = new StringBuilder(); // store the string of json alarms

            foreach (Alarm alarm in alarms) //loop over all the alarms
            {
                // If object is null, skip it.
                if (alarm == null)
                {
                    continue;
                }

                string json = JsonConvert.SerializeObject(alarm); // convert the Alarm object to a json string

                // If the string isn't null, the object was converted properly.
                if (json != null)
                {
                    sb.Append(json + "\n"); //append it to the string builder
                }
            }

            File.WriteAllText(filePath, sb.ToString()); //write the contents of the string builder to the file
            StopAllAlarms(); //stop all threading timers associated with the alarm objects
        }



        /// <summary>
        /// Handles the event triggered upon opening the application.
        /// </summary>
        /// <remarks>
        /// This method executes the following steps:
        /// 1. Verifies the existence of the file "Alarms.txt".
        /// 2. In case the file is found, it reads its contents, splitting each line which is expected to be a serialized JSON representation of an Alarm object.
        /// 3. Deserializes each JSON string into an Alarm object.
        /// 4. Appends the deserialized Alarm objects to the `alarms` list.
        /// 5. For each alarm that's marked as active, the corresponding timer is initialized. Inactive alarms are added to the `alarmTimers` dictionary with a null value.
        /// 6. Notifies the user if alarms were loaded successfully or if there were no saved alarms.
        /// 7. Invokes the `UpdateAlarmEvent` with the loaded alarms.
        /// 8. Checks if any alarms are currently ringing.
        /// 9. Determines the appropriate state for the buttons using `DecideButtonStates` method.
        /// 
        /// Error Handling:
        /// - Exceptions that might occur during the reading and deserialization process are caught and the user is informed about them.
        ///
        /// Note: It is expected that the "Alarms.txt" file contains valid JSON representations of Alarm objects. An improper format might cause exceptions.
        /// </remarks>
        public void HandleOnOpenEvent()
        {
            const string filePath = "Alarms.txt"; // The file we are reading from in the bin folder
            List<Alarm> alarms = new List<Alarm>(); //create a new list to hold the alarms we are reading in

           
            if (File.Exists(filePath)) // check to see if the file exists. if it doesn't, there are no objects to read in
            {
                try
                {
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        string content = reader.ReadToEnd(); // a string containing the content of the file
                        string[] alarmJsons = content.Split('\n');// an array that has each json string in it

                        foreach (string alarmJson in alarmJsons)// loop over each json string
                        {
                            if (!string.IsNullOrWhiteSpace(alarmJson))// this will handle the extra white space at the end of the file
                            {
                                Alarm alarm = JsonConvert.DeserializeObject<Alarm>(alarmJson); // Deserialize the json string into an Alarm
                                if (alarm != null) // if converted properly,
                                {
                                    alarms.Add(alarm); //add the alarm object tot thed dictionary that holds all the alarms for the progrma

                                    if (alarm.IsActive)// if the alarm is active, set up an alarm. Could be a late alarm that went off since last use
                                    {
                                        SetUpTimer(alarm); //sets up an alarm for the associated timer in the alarms dictionary
                                    }
                                    else
                                    {
                                        alarmTimers[alarm] = null;// the alarm is not active, no need to set up a timer for it
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    //catch any exception and write to the main form
                    MessageEvent.Invoke($"Exception caught in Controller.HandleOnOpenEvent(): {e.Message}"); //relay error to user
                }
            }
            if (alarmTimers.Keys.Count == 0)
            {
                //if there were no alarms read, the file probably did not have any alarms, alert the user that 
                // no issues were encountered, but there are no alarms
                MessageEvent.Invoke("No saved alarms found. Application started successfully.");
            }
            else
            {
                // alert the user that the alarms were loaded properly
                MessageEvent.Invoke("Alarms have been loaded properly");
            }
           
            //raise the event to update the UxAlarmList.DataSource with the AlarmTimers.Keys()
            UpdateAlarmEvent.Invoke(FetchAllAlarms());
            //this is an async func that will alert the user that more alarms are going off 
            // after the user has been shown that the program read in the alarms
            AlertAlarmGoingOff();
            // decide button states for edit, stop alarm, and snooze alarm
            //we pass null to disable all those buttons
            //selected index of the alarmList is -1, so no alarm selected
            DecideButtonStates(null);
        }


        /// <summary>
        /// Alerts the user if any alarms are currently ringing, specifically the one that has been ringing the longest.
        /// </summary>
        /// <remarks>
        /// This method undertakes the following steps:
        /// 1. Iterates through the `alarmTimers` dictionary to discern alarms that are active and whose trigger time (either `SnoozeTime` or `OriginalTriggerTime`) has transpired.
        /// 2. From the active alarms, it identifies the one that started ringing the earliest based on its trigger time.
        /// 3. Pauses for 3 seconds, ensuring users have sufficient time to perceive any preceding notifications, like messages confirming the successful initialization of the application.
        /// 4. Subsequently, if there are alarms actively ringing, the user is informed about the alarm that's been ringing for the most extended period.
        /// 
        /// Use Case: This method is instrumental when there's a need to inform the user of alarms currently ringing, especially shortly after notifying them of other operational events, such as the successful retrieval of saved alarms.
        /// </remarks>
        private async void AlertAlarmGoingOff()
        {
            // Create a list to store alarms that are currently ringing.
            List<Alarm> alarmsGoingOff = new List<Alarm>();

            // Iterate through the alarms in the alarmTimers dictionary.
            foreach (var alarm in alarmTimers.Keys)
            {
                // Check if the alarm is active and if its trigger time has already passed.
                if (alarm.IsActive && (alarm.SnoozeTime ?? alarm.OriginalTriggerTime) <= DateTime.Now)
                {
                    // Add the active alarm to the list.
                    alarmsGoingOff.Add(alarm);
                }
            }

            // If there are any alarms currently ringing.
            if (alarmsGoingOff.Any())
            {
                // Wait for 3 seconds to allow the user to read any previous messages.
                await Task.Delay(TimeSpan.FromSeconds(3));

                // Find the alarm that has been ringing the longest, based on the earliest OriginalTriggerTime or SnoozeTime.
                var longestRingingAlarm = alarmsGoingOff.OrderBy(a => a.SnoozeTime ?? a.OriginalTriggerTime).First();

                // Alert the user about the alarm that has been ringing the longest.
                MessageEvent.Invoke(longestRingingAlarm + " is going off. You can not edit alarms that are going off");
            }
        }


        /// <summary>
        /// Sets up the timer for a given alarm or snooze event.
        /// </summary>
        /// <param name="alarm">The Alarm object representing the alarm or snooze event. Cannot be null.</param>
        /// <param name="isSnooze">Optional flag indicating whether this is a snooze event. Default is false.</param>
        /// <remarks>
        /// This method performs the following actions:
        /// 1. Checks if the provided alarm is null, and if so, exits the method.
        /// 2. Checks if the alarm is set to snooze but has no snooze time, and if so, exits the method.
        /// 3. Determines the target trigger time for the alarm, considering snooze time if applicable.
        /// 4. Calculates the time duration until the alarm should go off.
        /// 5. If the calculated time has already passed, the alarm is immediately handled and stored as null in the alarmTimers dictionary.
        /// 6. Otherwise, creates and configures a ThreadingTimer to trigger the alarm at the calculated time and stores it in the alarmTimers dictionary.
        /// Note: The alarm parameter is expected to be non-null and properly initialized. 
        /// If 'isSnooze' is set to true, the SnoozeTime property on the alarm object should also be set.
        /// </remarks>
        private void SetUpTimer(Alarm alarm, bool isSnooze = false)
        {
            // Check if the provided alarm object is null. If it is, exit the method.
            if (alarm == null)
            {
                return;
            }

            // If the alarm is set to snooze but has no snooze time, exit the method.
            if (isSnooze && !alarm.SnoozeTime.HasValue)
            {
                return;
            }

            // Determine the target time for the alarm. Use snooze time if isSnooze is true; otherwise, use the original trigger time.
            DateTime targetTime = isSnooze ? alarm.SnoozeTime.Value : alarm.OriginalTriggerTime;

            // Calculate the time duration until the alarm should trigger.
            TimeSpan timeToGo = targetTime - DateTime.Now;

            // If the alarm time has already passed, handle the alarm immediately and exit the method.
            if (timeToGo <= TimeSpan.Zero)
            {
                HandleAlarm(alarm); //handle the alarm going off, alert the user and handle the call back
                alarmTimers[alarm] = null; // dispose of the associated threading timer
                return;
            }

            // Create a new timer that will trigger the alarm when the time is reached.
            ThreadingTimer timer = new ThreadingTimer(x => HandleAlarm(alarm), null, timeToGo, Timeout.InfiniteTimeSpan);

            // Add the timer to the dictionary of alarm timers.
            alarmTimers[alarm] = timer;
        }



        /// <summary>
        /// Handles a triggered alarm by stopping its corresponding timer and updating the UI.
        /// </summary>
        /// <param name="alarm">The alarm that has triggered. Cannot be null.</param>
        /// <remarks>
        /// This method performs the following actions:
        /// 1. Tries to fetch the corresponding timer for the given alarm from the alarmTimers dictionary.
        /// 2. If the timer exists, it is disposed of, effectively stopping the alarm.
        /// 3. The UI is updated to inform that the alarm has been triggered.
        /// 4. Enables or disables buttons on the UI.
        /// Note: The alarm parameter is expected to be non-null and properly initialized.
        /// </remarks>
        private void HandleAlarm(Alarm alarm)
        {
            
            if (alarmTimers.TryGetValue(alarm, out ThreadingTimer timer)) // try to get the alarm from the dictioanry, as well as the timer associated
            {
                timer.Dispose(); // Stop the corresponding timer

            }
            MessageEvent.Invoke("Alarm " + alarm.OriginalTriggerTime.ToString("hh mm: tt") + " is going off with sound " + alarm.AlarmSound.ToString() + ". You can not edit alarms that are going off"); // Update the TextBox with the information that an alarm has been triggered
            UpdateButtonsEvent.Invoke(); // update button ability in the view, should disable stop and snooze buttons
            UpdateAlarmEvent.Invoke(FetchAllAlarms()); // update the alarm list with updated alarms
           
        }

        /// <summary>
        /// Stops all currently running alarms by disposing of their associated timers.
        /// </summary>
        /// <remarks>
        /// This method performs the following actions:
        /// 1. Iterates over all the timers in the alarmTimers dictionary.
        /// 2. Disposes of each timer, effectively stopping each associated alarm.
        /// 3. Clears the alarmTimers dictionary to remove references to the disposed timers.
        /// </remarks>
        private void StopAllAlarms()
        {
            //iterate over all the timers in the alarms
            foreach (ThreadingTimer timer in alarmTimers.Values)
            {
                //dispose of the timer
                if(timer != null)
                {
                    timer.Dispose();
                }
               
            }
            alarmTimers.Clear(); // Clear the dictionary to remove references to disposed timers
        }



        /// <summary>
        /// Adds a new alarm to the system.
        /// </summary>
        /// <param name="triggerTime">The time when the alarm should be triggered.</param>
        /// <param name="isActive">Flag indicating if the alarm should be active upon creation.</param>
        /// <param name="sound">The sound to be played when the alarm is triggered. Can be null.</param>
        /// <remarks>
        /// This method performs the following steps:
        /// 1. Checks if an alarm sound was selected. If not, shows an error message and exits.
        /// 2. If the trigger time has already passed, resets the alarm for tomorrow at the same time.
        /// 3. Creates a new Alarm object and, if active, sets up a timer for it.
        /// 4. Updates the status to indicate that a new alarm has been added.
        /// 5. Raises events to refresh the UI and handle alarms that are about to go off.
        /// </remarks>
        public void AddAlarm(DateTime triggerTime, bool isActive, AlarmSound? sound)
        {
            // Step 1: Check if the alarm sound was selected.
            if (sound == null)
            {
                MessageEvent.Invoke("No alarm sound was selected. Alarm not added.");
                return; // Exit the method after showing the error
            }

            // Step 2: If the trigger time has already passed, set the alarm for tomorrow.
            if (triggerTime <= DateTime.Now)
            {
                triggerTime = triggerTime.AddDays(1);
            }

            // Step 3: Create the alarm object and set up a timer if it's active.
            Alarm newAlarm = new Alarm(triggerTime, isActive, sound.Value);

            if (isActive)
            {
                SetUpTimer(newAlarm); // If the alarm is active, set the timer.
            }
            else
            {
                alarmTimers[newAlarm] = null; // If not, the timer entry is null.
            }

            // Step 4: Update the status to indicate the addition of a new alarm.
            MessageEvent.Invoke("Alarm " + newAlarm.OriginalTriggerTime.ToString("h:mm tt") + " has been added");

            // Step 5: Raise events to refresh UI components and handle imminent alarms.
            UpdateButtonsEvent.Invoke(); //update buttons
            AlertAlarmGoingOff(); //after the message in step 4, there might be other alarms going off, alert to those alarms going off
        }




        /// <summary>
        /// Initiates the process to add a new alarm by opening the Add/Edit Alarm form.
        /// </summary>
        /// <remarks>
        /// This method performs the following steps:
        /// 1. Checks if the total number of active alarms in the system exceeds the limit of 5.
        ///    If it does, an error message is shown, and the method exits.
        /// 2. Initializes the AddEditAlarm form with null alarm and an index of -1 (indicating a new alarm).
        /// 3. Sets up delegates for the AddEditAlarm form so it can communicate back with this class.
        /// 4. Opens the AddEditAlarm form for the user to input alarm details.
        /// 5. After the AddEditAlarm form is closed, the system's alarm list is updated and an event is triggered to reflect the changes in the UI.
        /// </remarks>
        public void startAddAlarm()
        {
            // Check if the number of current alarm timers exceeds the limit of 5.
            if (alarmTimers.Count >= 5)
            {
                MessageEvent.Invoke("You may not have more than 5 alarms at a time.");
                return;  // Exit the method if the maximum alarm limit is reached.
            }

            AddEditAlarm addEditAlarm = new AddEditAlarm(null, -1); // Indicates a new alarm addition
            DelegateHolderAddEditAlarm delegateHolderForAddEdit = new DelegateHolderAddEditAlarm
            {
                AddAlarm = this.AddAlarm,
                CloseForm = addEditAlarm.Close,
                GetSelectedTime = addEditAlarm.GetSelectedTime,
                IsChecked = addEditAlarm.IsCheckBoxChecked,
                AlarmSound = addEditAlarm.GetSound
            };

            // Open the Add/Edit Alarm form for user interaction.
            using (addEditAlarm)
            {
                addEditAlarm.SetUpDelegate(delegateHolderForAddEdit);
                addEditAlarm.ShowDialog();
            }

            // Update the list of alarms and invoke the update event.
            UpdateAlarmEvent.Invoke(FetchAllAlarms());
        }




        /// <summary>
        /// Initiates the snooze operation for a specific alarm by opening the SnoozeForm.
        /// </summary>
        /// <param name="index">The index or identifier of the alarm to be snoozed.</param>
        /// <remarks>
        /// This method performs the following steps:
        /// 1. Initializes the SnoozeForm with the provided alarm index.
        /// 2. Sets up delegates for the SnoozeForm to facilitate communication between the form and this controller class.
        /// 3. Opens the SnoozeForm for the user to set a snooze duration.
        /// 4. The snooze operation is processed either within the SnoozeForm or after its closure based on the user's interactions.
        /// </remarks>
        public void startSnooze(int index)
        {
            // Initialize the SnoozeForm for the given alarm index.
            SnoozeForm snoozeform = new SnoozeForm(index);

            // Set up the delegate holder for the SnoozeForm.
            DelegateHolderSnooze bucket = new DelegateHolderSnooze()
            {
                GetSnoozeDurationMethod = snoozeform.parseDateTime,
                FinishSnooze = HandleSnooze
                
            };
            snoozeform.SetUpDelegates(bucket);

            // Open the SnoozeForm for user interaction.
            using (snoozeform)
            {
                // Display the SnoozeForm and await user interactions.
                snoozeform.ShowDialog();
                // The rest of snooze logic is handled in HandleSnooze()
            }
        }



        /// <summary>
        /// Decides the enablement states of the Edit, Snooze, and Stop buttons based on the state of the selected alarm.
        /// </summary>
        /// <param name="selectedAlarm">The currently selected alarm for which button states are to be determined.</param>
        /// <returns>
        /// A tuple containing booleans for each button (Edit, Snooze, and Stop) indicating their enablement states.
        /// </returns>
        /// <remarks>
        /// The logic for deciding button states is as follows:
        /// - Edit button: Enabled if there's a selected alarm.
        /// - Snooze and Stop buttons: Enabled if the alarm/snooze time has passed and the alarm is active.
        /// </remarks>
        public (bool EditEnabled, bool SnoozeEnabled, bool StopEnabled) DecideButtonStates(Alarm selectedAlarm)
        {
            // If no alarm is selected, all buttons are disabled.
            if (selectedAlarm == null)
            {
                return (false, false, false);
            }

            // By default, the Edit button is enabled if there is a selected alarm.
            bool editEnabled = true;

            // Initialize Snooze and Stop buttons to disabled.
            bool snoozeEnabled = false;
            bool stopEnabled = false;

            // Get the current time for comparison with the alarm time.
            DateTime currentTime = DateTime.Now;

            // Determine which alarm time to consider: the snooze time (if available) or the original trigger time.
            DateTime alarmTimeToCheck = selectedAlarm.SnoozeTime ?? selectedAlarm.OriginalTriggerTime;

            // Enable the Snooze and Stop buttons if the alarm/snooze time has passed and the alarm is still active.
            if (alarmTimeToCheck <= currentTime && selectedAlarm.IsActive)
            {
                snoozeEnabled = true;
                stopEnabled = true; // Stop button shares the same enablement criteria as the Snooze button.
            }

            if(snoozeEnabled || stopEnabled)
            {
                editEnabled = false;
            }

            // Return the decided states for the buttons.
            return (editEnabled, snoozeEnabled, stopEnabled);
        }





        /// <summary>
        /// Returns the alarms from the dictionary as a list. This is used for the UxAlarm list to update its view via a delegate
        /// </summary>
        /// <returns>the list of alarms</returns>
        public List<Alarm> FetchAllAlarms()
        {
            return alarmTimers.Keys.ToList();
        }


        /// <summary>
        /// Handles the editing of an existing alarm based on the provided index.
        /// </summary>
        /// <param name="index">The index of the alarm to be edited in the alarmTimers collection.</param>
        /// <remarks>
        /// This method performs the following steps:
        /// 1. Validates the provided index against the range of existing alarms.
        /// 2. If the index is valid, retrieves the corresponding Alarm object.
        /// 3. Initializes and configures the AddEditAlarm form with the existing alarm data and necessary delegates.
        /// 4. Opens the AddEditAlarm dialog to allow the user to modify the alarm details.
        /// 5. After the dialog closure, if the user confirmed the changes, the alarm properties are updated accordingly.
        /// 6. Raises an event to refresh the alarms list in the UI.
        /// Note: If the index is found invalid, it issues a warning message.
        /// </remarks>
        public void HandleEdit(int index)
        {
            // Validate the provided index.
            if (index >= 0 && index < alarmTimers.Keys.Count)
            {
                // Retrieve the corresponding Alarm object for the valid index.
                Alarm selectedAlarm = alarmTimers.Keys.ElementAt(index);

                // Initialize the AddEditAlarm form with existing alarm data.
                AddEditAlarm addEditAlarm = new AddEditAlarm(selectedAlarm, index);

                // Configure the delegate holder for the AddEditAlarm form.
                DelegateHolderAddEditAlarm delegateHolderForAddEdit = new DelegateHolderAddEditAlarm
                {
                    UpdateAlarm = this.UpdateAlarm,
                    AddAlarm = this.AddAlarm,
                    CloseForm = addEditAlarm.Close,
                    GetSelectedTime = addEditAlarm.GetSelectedTime,
                    IsChecked = addEditAlarm.IsCheckBoxChecked,
                    AlarmSound = addEditAlarm.GetSound
                };

                // Open the AddEditAlarm form for user interaction.
                using (addEditAlarm)
                {
                    addEditAlarm.SetUpDelegate(delegateHolderForAddEdit);
                    addEditAlarm.ShowDialog();
                    //rest of the logic is handled in the AddEditForm
                }

                // Close the form after the user has made modifications.
                delegateHolderForAddEdit.CloseForm();
            }
            else
            {
                // Issue a warning if an invalid index is selected.
                MessageEvent.Invoke("Warning: Invalid index selected.");
                AlertAlarmGoingOff(); //alert to other alarms going off 3 seconds after the message event
            }

            // Raise the event to update the alarms list in the UI.
            UpdateAlarmEvent.Invoke(FetchAllAlarms());
        }




        /// <summary>
        /// Updates an existing alarm with new properties based on the provided index.
        /// </summary>
        /// <param name="index">The index of the alarm to be updated.</param>
        /// <param name="isChecked">Indicates whether the alarm is active.</param>
        /// <param name="trigger">The new trigger time for the alarm.</param>
        /// <param name="sound">The sound to be played when the alarm triggers.</param>
        /// <remarks>
        /// This method performs the following steps:
        /// 1. Validates the provided index against the range of existing alarms.
        /// 2. Ensures that the updated properties (trigger time and sound) are not null.
        /// 3. Retrieves the existing Alarm object using the index.
        /// 4. Constructs a new Alarm object with the provided updated properties.
        /// 5. Compares the new Alarm object with the existing one. If they are different, updates the alarm in the collection and resets the timer.
        /// 6. Constructs and dispatches a status message based on the updated alarm's properties.
        /// 7. Notifies any interested parties about changes to the alarm state.
        /// </remarks>
        public void UpdateAlarm(int index, bool isChecked, DateTime? trigger, AlarmSound? sound)
        {
            // Step 1: Validate the provided index.
            if (index < 0 || index >= alarmTimers.Keys.Count) return;

            // Step 2: Ensure that the updated properties are not null.
            if (trigger == null || sound == null) return;

            // Step 3: Fetch the existing Alarm object using the provided index.
            Alarm existingAlarm = alarmTimers.Keys.ElementAt(index);

            // Step 4: Construct a new Alarm object with the updated properties.
            Alarm newAlarm = new Alarm(trigger.Value, isChecked, sound.Value);

            // Step 5: Update the alarm in the collection if it differs from the existing one.
            if (!existingAlarm.Equals(newAlarm))
            {
                if (alarmTimers.TryGetValue(existingAlarm, out var existingTimer))
                {
                    existingTimer?.Dispose();
                }

                alarmTimers.Remove(existingAlarm);
                SetUpTimer(newAlarm, false); // Assuming SetUpTimer updates alarmTimers internally
            }

            // Step 6: Construct a status message based on the updated alarm's properties.
            string statusMessage;
            if (trigger <= DateTime.Now)
            {
                trigger = trigger.Value.AddDays(1);
                statusMessage = isChecked ? $"Time has already passed. Alarm set for tomorrow at {newAlarm.OriginalTriggerTime:h:mm tt}" : "Alarm set for tomorrow but not active.";
            }
            else
            {
                statusMessage = isChecked ? $"Alarm set for {newAlarm.OriginalTriggerTime:h:mm tt}" : "Alarm has been updated, but not active.";
            }

            // Dispatch the status message.
            MessageEvent.Invoke(statusMessage);

            // Step 7: Notify other components about the alarm update.
            AlertAlarmGoingOff();
        }






        /// <summary>
        /// Handles the snooze operation for a selected alarm by setting up a new snooze timer.
        /// </summary>
        /// <param name="time">The duration of the snooze.</param>
        /// <param name="index">The index of the alarm to be snoozed in the alarmTimers collection.</param>
        /// <remarks>
        /// This method performs the following steps:
        /// 1. Validates the provided index against the range of existing alarms.
        /// 2. Ensures that a valid snooze duration is provided.
        /// 3. Retrieves the existing Alarm object using the index.
        /// 4. Validates the alarm's state, ensuring it's active and currently ringing.
        /// 5. Disposes of the old timer associated with the alarm.
        /// 6. Updates the alarm's SnoozeTime based on the provided snooze duration.
        /// 7. Resets the timer to respect the new SnoozeTime.
        /// 8. Constructs and dispatches a status message indicating the snooze action.
        /// 9. Notifies any interested parties about changes to the alarm state.
        /// </remarks>
        public void HandleSnooze(TimeSpan? time, int index)
        {
            // Step 1: Validate the provided index.
            if (index < 0 || index >= alarmTimers.Count)
            {
                MessageEvent.Invoke("Warning: Invalid index for alarm.");
                return;
            }

            // Step 2: Ensure a valid snooze duration is provided.
            if (!time.HasValue)
            {
                MessageEvent.Invoke("No time was selected to snooze with.");
                return;
            }

            // Step 3: Retrieve the existing Alarm object.
            Alarm alarmToSnooze = alarmTimers.Keys.ElementAt(index);

            // Step 4: Validate the alarm's state.
            if (!(alarmToSnooze.IsActive && DateTime.Now >= alarmToSnooze.OriginalTriggerTime))
            {
                MessageEvent.Invoke("Warning: Cannot snooze an alarm that is not currently ringing.");
                return;
            }

            // Step 5: Dispose of the old timer.
            if (alarmTimers.TryGetValue(alarmToSnooze, out var oldTimer))
            {
                oldTimer?.Dispose();
            }
            else
            {
                MessageEvent.Invoke("Warning: Alarm to snooze not found.");
                return;
            }

            // Step 6 & 7: Update SnoozeTime and reset the timer.
            alarmToSnooze.SnoozeTime = DateTime.Now.Add(time.Value);
            alarmToSnooze.IsActive = true;
            SetUpTimer(alarmToSnooze, true);  // Indicate this is a snooze operation.

            // Step 8: Dispatch a status message about the snooze action.
            MessageEvent.Invoke($"Alarm {alarmToSnooze.OriginalTriggerTime:h:mm tt} has been snoozed for {time.Value.TotalSeconds} seconds");

            // Step 9: Notify other components about the alarm update.
            AlertAlarmGoingOff(); //other alarms might be going off, alert after 3 seconds
            UpdateButtonsEvent.Invoke(); //update buttons ability/disability
        }








        /// <summary>
        /// Stops the currently ringing alarm, reschedules it for the next day, and updates the UI.
        /// </summary>
        /// <param name="index">The index of the alarm in the alarmTimers collection to be stopped.</param>
        /// <remarks>
        /// This method performs the following steps:
        /// 1. Validates the provided index against the range of existing alarms.
        /// 2. Retrieves the corresponding Alarm object using the validated index.
        /// 3. Checks if the alarm is currently ringing and is active.
        /// 4. If the alarm meets the criteria, disposes of its associated timer and reschedules the alarm for the same time the next day.
        /// 5. Updates the status message to inform the user that the alarm has been stopped and rescheduled.
        /// 6. Updates the UI elements, like buttons, based on the updated alarm state.
        /// Note: If the alarm is not currently ringing or is inactive, an appropriate message is provided.
        /// </remarks>
        public void StopAlarm(int index)
        {
            // Step 1: Validate the provided index.
            if (index < 0 || index >= alarmTimers.Count)
            {
                MessageEvent.Invoke("Invalid index for alarm.");
                return;
            }

            // Step 2: Retrieve the Alarm object corresponding to the selected index.
            Alarm alarmToStop = alarmTimers.Keys.ElementAt(index);

            // Step 3: Check if the alarm is ringing and is active.
            if (DateTime.Now >= alarmToStop.OriginalTriggerTime && alarmToStop.IsActive)
            {
                // Step 4: Dispose the associated timer and reschedule the alarm.
                if (alarmTimers.TryGetValue(alarmToStop, out ThreadingTimer existingTimer))
                {
                    existingTimer?.Dispose();
                    alarmToStop.OriginalTriggerTime = alarmToStop.OriginalTriggerTime.AddDays(1);
                    alarmToStop.IsActive = true;
                    alarmToStop.SnoozeTime = null;
                    SetUpTimer(alarmToStop);  // Reinitialize the timer for the next day.

                    // Step 5: Update the status message.
                    MessageEvent.Invoke($"Alarm stopped and rescheduled for the next day at {alarmToStop.OriginalTriggerTime:h:mm tt}");
                }
                else
                {
                    MessageEvent.Invoke("Alarm to stop not found.");
                }
            }
            else
            {
                MessageEvent.Invoke("Alarm is not currently going off. To stop this alarm, you must edit it and change the check box.");
            }

            // Step 6: Update the UI elements based on the new alarm state.
            UpdateButtonsEvent.Invoke();
        }

        #endregion
    }
}
