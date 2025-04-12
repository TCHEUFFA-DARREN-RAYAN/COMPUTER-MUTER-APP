namespace AutoMuteScheduler
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnSpecificDay;
        private System.Windows.Forms.Button btnWeekdays;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
        private System.Windows.Forms.ListBox listBoxSchedules;
        private System.Windows.Forms.DateTimePicker timePicker;
        private System.Windows.Forms.CheckBox chkMonday;
        private System.Windows.Forms.CheckBox chkTuesday;
        private System.Windows.Forms.CheckBox chkWednesday;
        private System.Windows.Forms.CheckBox chkThursday;
        private System.Windows.Forms.CheckBox chkFriday;
        private System.Windows.Forms.CheckBox chkSaturday;
        private System.Windows.Forms.CheckBox chkSunday;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnSpecificDay = new System.Windows.Forms.Button();
            this.btnWeekdays = new System.Windows.Forms.Button();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.listBoxSchedules = new System.Windows.Forms.ListBox();
            this.timePicker = new System.Windows.Forms.DateTimePicker();
            this.chkMonday = new System.Windows.Forms.CheckBox();
            this.chkTuesday = new System.Windows.Forms.CheckBox();
            this.chkWednesday = new System.Windows.Forms.CheckBox();
            this.chkThursday = new System.Windows.Forms.CheckBox();
            this.chkFriday = new System.Windows.Forms.CheckBox();
            this.chkSaturday = new System.Windows.Forms.CheckBox();
            this.chkSunday = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnSpecificDay
            // 
            this.btnSpecificDay.Location = new System.Drawing.Point(20, 20);
            this.btnSpecificDay.Name = "btnSpecificDay";
            this.btnSpecificDay.Size = new System.Drawing.Size(120, 30);
            this.btnSpecificDay.TabIndex = 0;
            this.btnSpecificDay.Text = "Select Specific Day";
            this.btnSpecificDay.Click += new System.EventHandler(this.btnSpecificDay_Click);
            // 
            // btnWeekdays
            // 
            this.btnWeekdays.Location = new System.Drawing.Point(150, 20);
            this.btnWeekdays.Name = "btnWeekdays";
            this.btnWeekdays.Size = new System.Drawing.Size(120, 30);
            this.btnWeekdays.TabIndex = 1;
            this.btnWeekdays.Text = "Select Weekdays";
            this.btnWeekdays.Click += new System.EventHandler(this.btnWeekdays_Click);
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker.Location = new System.Drawing.Point(20, 60);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(200, 23);
            this.dateTimePicker.TabIndex = 2;
            // 
            // timePicker
            // 
            this.timePicker.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.timePicker.Location = new System.Drawing.Point(20, 90);
            this.timePicker.Name = "timePicker";
            this.timePicker.ShowUpDown = true;
            this.timePicker.Size = new System.Drawing.Size(200, 23);
            this.timePicker.TabIndex = 3;
            // 
            // chkMonday
            // 
            this.chkMonday.AutoSize = true;
            this.chkMonday.Location = new System.Drawing.Point(20, 130);
            this.chkMonday.Name = "chkMonday";
            this.chkMonday.Size = new System.Drawing.Size(68, 19);
            this.chkMonday.TabIndex = 4;
            this.chkMonday.Text = "Monday";
            this.chkMonday.UseVisualStyleBackColor = true;
            // 
            // chkTuesday
            // 
            this.chkTuesday.AutoSize = true;
            this.chkTuesday.Location = new System.Drawing.Point(20, 155);
            this.chkTuesday.Name = "chkTuesday";
            this.chkTuesday.Size = new System.Drawing.Size(74, 19);
            this.chkTuesday.TabIndex = 5;
            this.chkTuesday.Text = "Tuesday";
            this.chkTuesday.UseVisualStyleBackColor = true;
            // 
            // chkWednesday
            // 
            this.chkWednesday.AutoSize = true;
            this.chkWednesday.Location = new System.Drawing.Point(20, 180);
            this.chkWednesday.Name = "chkWednesday";
            this.chkWednesday.Size = new System.Drawing.Size(91, 19);
            this.chkWednesday.TabIndex = 6;
            this.chkWednesday.Text = "Wednesday";
            this.chkWednesday.UseVisualStyleBackColor = true;
            // 
            // chkThursday
            // 
            this.chkThursday.AutoSize = true;
            this.chkThursday.Location = new System.Drawing.Point(20, 205);
            this.chkThursday.Name = "chkThursday";
            this.chkThursday.Size = new System.Drawing.Size(79, 19);
            this.chkThursday.TabIndex = 7;
            this.chkThursday.Text = "Thursday";
            this.chkThursday.UseVisualStyleBackColor = true;
            // 
            // chkFriday
            // 
            this.chkFriday.AutoSize = true;
            this.chkFriday.Location = new System.Drawing.Point(20, 230);
            this.chkFriday.Name = "chkFriday";
            this.chkFriday.Size = new System.Drawing.Size(60, 19);
            this.chkFriday.TabIndex = 8;
            this.chkFriday.Text = "Friday";
            this.chkFriday.UseVisualStyleBackColor = true;
            // 
            // chkSaturday
            // 
            this.chkSaturday.AutoSize = true;
            this.chkSaturday.Location = new System.Drawing.Point(20, 255);
            this.chkSaturday.Name = "chkSaturday";
            this.chkSaturday.Size = new System.Drawing.Size(78, 19);
            this.chkSaturday.TabIndex = 9;
            this.chkSaturday.Text = "Saturday";
            this.chkSaturday.UseVisualStyleBackColor = true;
            // 
            // chkSunday
            // 
            this.chkSunday.AutoSize = true;
            this.chkSunday.Location = new System.Drawing.Point(20, 280);
            this.chkSunday.Name = "chkSunday";
            this.chkSunday.Size = new System.Drawing.Size(70, 19);
            this.chkSunday.TabIndex = 10;
            this.chkSunday.Text = "Sunday";
            this.chkSunday.UseVisualStyleBackColor = true;
            // 
            // listBoxSchedules
            // 
            this.listBoxSchedules.FormattingEnabled = true;
            this.listBoxSchedules.ItemHeight = 15;
            this.listBoxSchedules.Location = new System.Drawing.Point(230, 60);
            this.listBoxSchedules.Name = "listBoxSchedules";
            this.listBoxSchedules.Size = new System.Drawing.Size(200, 139);
            this.listBoxSchedules.TabIndex = 11;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(450, 320);
            this.Controls.Add(this.listBoxSchedules);
            this.Controls.Add(this.chkSunday);
            this.Controls.Add(this.chkSaturday);
            this.Controls.Add(this.chkFriday);
            this.Controls.Add(this.chkThursday);
            this.Controls.Add(this.chkWednesday);
            this.Controls.Add(this.chkTuesday);
            this.Controls.Add(this.chkMonday);
            this.Controls.Add(this.timePicker);
            this.Controls.Add(this.dateTimePicker);
            this.Controls.Add(this.btnWeekdays);
            this.Controls.Add(this.btnSpecificDay);
            this.Name = "Form1";
            this.Text = "Mute Scheduler";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
