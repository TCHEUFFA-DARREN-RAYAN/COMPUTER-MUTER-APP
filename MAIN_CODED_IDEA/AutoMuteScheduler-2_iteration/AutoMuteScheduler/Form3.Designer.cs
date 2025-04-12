partial class Form3
{
    private System.ComponentModel.IContainer components = null;
    private CheckBox chkMonday, chkTuesday, chkWednesday, chkThursday, chkFriday, chkSaturday, chkSunday;
    private DateTimePicker timePicker;
    private Button btnSetWeekdays;

    private void InitializeComponent()
    {
        this.chkMonday = new CheckBox() { Text = "Monday", Location = new System.Drawing.Point(30, 30) };
        this.chkTuesday = new CheckBox() { Text = "Tuesday", Location = new System.Drawing.Point(30, 60) };
        this.chkWednesday = new CheckBox() { Text = "Wednesday", Location = new System.Drawing.Point(30, 90) };
        this.chkThursday = new CheckBox() { Text = "Thursday", Location = new System.Drawing.Point(30, 120) };
        this.chkFriday = new CheckBox() { Text = "Friday", Location = new System.Drawing.Point(30, 150) };
        this.chkSaturday = new CheckBox() { Text = "Saturday", Location = new System.Drawing.Point(30, 180) };
        this.chkSunday = new CheckBox() { Text = "Sunday", Location = new System.Drawing.Point(30, 210) };
        this.timePicker = new DateTimePicker() { Format = DateTimePickerFormat.Time, Location = new System.Drawing.Point(150, 250) };
        this.btnSetWeekdays = new Button() { Text = "Set Weekdays", Location = new System.Drawing.Point(30, 300) };

        this.btnSetWeekdays.Click += new EventHandler(this.btnSetWeekdays_Click);

        this.ClientSize = new System.Drawing.Size(300, 400);
        this.Controls.AddRange(new Control[] {
            chkMonday, chkTuesday, chkWednesday, chkThursday, chkFriday, chkSaturday, chkSunday,
            timePicker, btnSetWeekdays
        });
    }
}
