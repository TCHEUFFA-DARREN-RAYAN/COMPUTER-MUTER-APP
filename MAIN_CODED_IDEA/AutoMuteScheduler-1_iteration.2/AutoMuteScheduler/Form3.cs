using System;
using System.Collections.Generic;
using System.Windows.Forms;

public partial class Form3 : Form
{
    public List<DayOfWeek> SelectedDays { get; set; } // Store selected weekdays
    public TimeSpan SelectedTime { get; set; } // Store the selected time

    public Form3()
    {
        InitializeComponent();
        SelectedDays = new List<DayOfWeek>();
    }

    // Event handler for the 'Set Weekdays' button
    private void btnSetWeekdays_Click(object sender, EventArgs e)
    {
        // Get selected days from checkboxes
        if (chkMonday.Checked) SelectedDays.Add(DayOfWeek.Monday);
        if (chkTuesday.Checked) SelectedDays.Add(DayOfWeek.Tuesday);
        if (chkWednesday.Checked) SelectedDays.Add(DayOfWeek.Wednesday);
        if (chkThursday.Checked) SelectedDays.Add(DayOfWeek.Thursday);
        if (chkFriday.Checked) SelectedDays.Add(DayOfWeek.Friday);
        if (chkSaturday.Checked) SelectedDays.Add(DayOfWeek.Saturday);
        if (chkSunday.Checked) SelectedDays.Add(DayOfWeek.Sunday);

        // Get selected time from the time picker control
        SelectedTime = timePicker.Value.TimeOfDay;

        DialogResult = DialogResult.OK; // Indicate success
        Close();
    }
}
