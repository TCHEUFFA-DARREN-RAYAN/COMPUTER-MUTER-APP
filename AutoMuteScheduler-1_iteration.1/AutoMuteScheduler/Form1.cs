using System;
using System.Collections.Generic;
using System.Windows.Forms;

public partial class Form1 : Form
{
    private List<DateTime> muteSchedule = new List<DateTime>(); // Store the mute schedules

    public Form1()
    {
        InitializeComponent();
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
                muteSchedule.Add(selectedDate);
                listBoxSchedules.Items.Add("Specific Date: " + selectedDate.ToString("yyyy-MM-dd HH:mm"));
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

                    muteSchedule.Add(scheduledTime);
                    listBoxSchedules.Items.Add("Weekly: " + scheduledTime.ToString("yyyy-MM-dd HH:mm"));
                }
            }
        }
    }

    // Helper method to get the next occurrence of a specified day of the week
    private DateTime GetNextDayOfWeek(DateTime startDate, DayOfWeek day)
    {
        int daysToAdd = ((int)day - (int)startDate.DayOfWeek + 7) % 7;
        return startDate.AddDays(daysToAdd);
    }
}
