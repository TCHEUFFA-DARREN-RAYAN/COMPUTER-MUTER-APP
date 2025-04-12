using System;
using System.Windows.Forms;

public partial class Form2 : Form
{
    public DateTime SelectedDate { get; set; } // Property to store the selected date

    public Form2()
    {
        InitializeComponent();
    }

    // Event handler for the 'Set Date' button
    private void btnSetDate_Click(object sender, EventArgs e)
    {
        // Set the selected date and close the form
        SelectedDate = dateTimePicker.Value;
        DialogResult = DialogResult.OK; // Indicate success
        Close();
    }
}
