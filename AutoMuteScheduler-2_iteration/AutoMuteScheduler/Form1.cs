using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;

public partial class Form1 : Form
{
    private List<MuteSchedule> muteSchedule = new List<MuteSchedule>(); // Store the mute schedules

    public Form1()
    {
        InitializeComponent();
        listBoxSchedules.MouseClick += ListBoxSchedules_MouseClick; // Add MouseClick event handler

        // Load the saved schedule when the form is loaded
        muteSchedule = LoadSchedule();

        // Display loaded schedules in the ListBox
        foreach (var schedule in muteSchedule)
        {
            listBoxSchedules.Items.Add(schedule.ScheduleType + ": " + schedule.ScheduleDate.ToString("yyyy-MM-dd HH:mm"));
        }
    }

    // Event handler for the 'Specific Date' button
    private void btnSpecificDay_Click(object sender, EventArgs e)
    {
        // Open 'Form2' to allow the user to select a specific date
        using (Form2 form2 = new Form2())
        {
            if (form2.ShowDialog() == DialogResult.OK)
            {
                DateTime selectedDate = form2.SelectedDate;
                muteSchedule.Add(new MuteSchedule { ScheduleDate = selectedDate, ScheduleType = "Specific Date" });
                listBoxSchedules.Items.Add("Specific Date: " + selectedDate.ToString("yyyy-MM-dd HH:mm"));
                SaveSchedule(muteSchedule); // Save the updated schedule
            }
        }
    }

    // Event handler for the 'Weekdays' button
    private void btnWeekdays_Click(object sender, EventArgs e)
    {
        // Open 'Form3' to allow the user to select weekdays and times
        using (Form3 form3 = new Form3())
        {
            if (form3.ShowDialog() == DialogResult.OK)
            {
                List<DayOfWeek> selectedDays = form3.SelectedDays;
                TimeSpan selectedTime = form3.SelectedTime;

                foreach (var day in selectedDays)
                {
                    // Get the next occurrence of the selected day
                    DateTime nextOccurrence = GetNextDayOfWeek(DateTime.Now, day);
                    DateTime scheduledTime = new DateTime(nextOccurrence.Year, nextOccurrence.Month, nextOccurrence.Day, selectedTime.Hours, selectedTime.Minutes, 0);

                    muteSchedule.Add(new MuteSchedule { ScheduleDate = scheduledTime, ScheduleType = "Weekly" });
                    listBoxSchedules.Items.Add("Weekly: " + scheduledTime.ToString("yyyy-MM-dd HH:mm"));
                }
                SaveSchedule(muteSchedule); // Save the updated schedule
            }
        }
    }

    // Helper method to get the next occurrence of a specified day of the week
    private DateTime GetNextDayOfWeek(DateTime startDate, DayOfWeek day)
    {
        int daysToAdd = ((int)day - (int)startDate.DayOfWeek + 7) % 7;
        return startDate.AddDays(daysToAdd);
    }

    // MouseClick event handler for the ListBox
    private void ListBoxSchedules_MouseClick(object sender, MouseEventArgs e)
    {
        // Get the index of the clicked item
        int index = listBoxSchedules.IndexFromPoint(e.Location);

        // If the clicked item is valid
        if (index != ListBox.NoMatches)
        {
            string item = listBoxSchedules.Items[index].ToString();

            // Check if the item contains a date
            if (item.Contains("Specific Date") || item.Contains("Weekly"))
            {
                DialogResult dialogResult = MessageBox.Show("Do you want to delete this date?", "Delete Confirmation", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    // Remove the item from the ListBox and the mute schedule list
                    listBoxSchedules.Items.RemoveAt(index);
                    muteSchedule.RemoveAt(index);
                    SaveSchedule(muteSchedule); // Save the updated schedule
                }
            }
        }
    }

    // Save the mute schedule list to a file in JSON format
    public void SaveSchedule(List<MuteSchedule> scheduleList)
    {
        string filePath = "schedule.json";  // Path to store the schedule

        // Convert the schedule list to JSON format
        string jsonData = JsonConvert.SerializeObject(scheduleList, Formatting.Indented);

        // Save the JSON data to a file
        File.WriteAllText(filePath, jsonData);
    }

    // Load the mute schedule from the file (if it exists)
    public List<MuteSchedule> LoadSchedule()
    {
        string filePath = "schedule.json";

        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<MuteSchedule>>(jsonData);
        }

        return new List<MuteSchedule>(); // Return an empty list if no schedule exists
    }

    // Class to represent a mute schedule
    public class MuteSchedule
    {
        public DateTime ScheduleDate { get; set; }
        public string ScheduleType { get; set; }  // "Specific Date" or "Weekly"
    }
}
