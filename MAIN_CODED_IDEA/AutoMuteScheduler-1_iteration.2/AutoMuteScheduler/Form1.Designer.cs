partial class Form1
{
    private System.ComponentModel.IContainer components = null;
    private Button btnSpecificDay;
    private Button btnWeekdays;
    private ListBox listBoxSchedules;

    private void InitializeComponent()
    {
        this.btnSpecificDay = new Button();
        this.btnWeekdays = new Button();
        this.listBoxSchedules = new ListBox();

        // 
        // btnSpecificDay
        // 
        this.btnSpecificDay.Location = new System.Drawing.Point(50, 50);
        this.btnSpecificDay.Name = "btnSpecificDay";
        this.btnSpecificDay.Size = new System.Drawing.Size(200, 50);
        this.btnSpecificDay.Text = "Select Specific Date";
        this.btnSpecificDay.Click += new EventHandler(this.btnSpecificDay_Click);

        // 
        // btnWeekdays
        // 
        this.btnWeekdays.Location = new System.Drawing.Point(50, 120);
        this.btnWeekdays.Name = "btnWeekdays";
        this.btnWeekdays.Size = new System.Drawing.Size(200, 50);
        this.btnWeekdays.Text = "Select Weekdays and Time";
        this.btnWeekdays.Click += new EventHandler(this.btnWeekdays_Click);

        // 
        // listBoxSchedules
        // 
        this.listBoxSchedules.Location = new System.Drawing.Point(50, 200);
        this.listBoxSchedules.Name = "listBoxSchedules";
        this.listBoxSchedules.Size = new System.Drawing.Size(300, 150);

        // 
        // Form1
        // 
        this.ClientSize = new System.Drawing.Size(400, 400);
        this.Controls.Add(this.btnSpecificDay);
        this.Controls.Add(this.btnWeekdays);
        this.Controls.Add(this.listBoxSchedules);
        this.Name = "Form1";
        this.Text = "Mute Schedule App";
    }
}
