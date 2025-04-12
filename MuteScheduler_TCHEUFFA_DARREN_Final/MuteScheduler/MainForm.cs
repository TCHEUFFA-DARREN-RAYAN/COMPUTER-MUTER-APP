using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NAudio.CoreAudioApi;
using Microsoft.Win32;

/// <summary>
/// Mute Scheduler Application
/// Author: TCHEUFFA DARREN
/// Date: April 2025
/// Description: Windows Forms application that automatically mutes or unmutes 
/// the computer's system volume based on a user-defined schedule.
/// </summary>
namespace MuteScheduler
{
    public partial class MainForm : Form
    {
        private List<ScheduleItem> scheduleItems = new List<ScheduleItem>();
        private System.Timers.Timer scheduleTimer;
        private NotifyIcon trayIcon;
        private ContextMenuStrip trayMenu;

        public MainForm()
        {
            InitializeComponent();
            InitializeScheduleTimer();
            InitializeTrayIcon();
            
            // Check if auto-start registry key exists
            if (!IsAutoStartEnabled())
            {
                // Ask user if they want to enable auto-start
                if (MessageBox.Show("Would you like this application to start automatically when Windows boots?", 
                                   "Auto-Start Configuration", 
                                   MessageBoxButtons.YesNo, 
                                   MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SetAutoStart(true);
                }
            }
            
            // Start minimized
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            this.Hide();
        }

        private void InitializeComponent()
        {
            this.Text = "Mute Scheduler by TCHEUFFA DARREN";
            this.Size = new Size(600, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(500, 400);
            this.FormClosing += MainForm_FormClosing;

            // Create tabs for different scheduling options
            TabControl tabControl = new TabControl();
            tabControl.Dock = DockStyle.Fill;
            
            // Create tab pages
            TabPage daySpecificTab = new TabPage("Day Specific");
            TabPage weekdaysTab = new TabPage("Weekdays");
            TabPage schedulesTab = new TabPage("Schedules");
            
            tabControl.TabPages.Add(daySpecificTab);
            tabControl.TabPages.Add(weekdaysTab);
            tabControl.TabPages.Add(schedulesTab);
            
            this.Controls.Add(tabControl);
            
            // Set up day specific tab
            SetupDaySpecificTab(daySpecificTab);
            
            // Set up weekdays tab
            SetupWeekdaysTab(weekdaysTab);
            
            // Set up schedules tab
            SetupSchedulesTab(schedulesTab);
        }

        private void SetupDaySpecificTab(TabPage tab)
        {
            // Main container
            TableLayoutPanel mainPanel = new TableLayoutPanel();
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.Padding = new Padding(10);
            mainPanel.ColumnCount = 1;
            mainPanel.RowCount = 3;
            mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            
            // Day selection group
            GroupBox daySelectionGroup = new GroupBox();
            daySelectionGroup.Text = "Select Days";
            daySelectionGroup.Dock = DockStyle.Fill;
            
            FlowLayoutPanel daysPanel = new FlowLayoutPanel();
            daysPanel.Dock = DockStyle.Fill;
            daysPanel.Padding = new Padding(10);
            
            string[] days = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            foreach (string day in days)
            {
                CheckBox dayCheckBox = new CheckBox();
                dayCheckBox.Text = day;
                dayCheckBox.AutoSize = true;
                dayCheckBox.Margin = new Padding(5);
                daysPanel.Controls.Add(dayCheckBox);
            }
            
            daySelectionGroup.Controls.Add(daysPanel);
            mainPanel.Controls.Add(daySelectionGroup, 0, 0);
            
            // Time selection group
            GroupBox timeSelectionGroup = new GroupBox();
            timeSelectionGroup.Text = "Select Time";
            timeSelectionGroup.Dock = DockStyle.Fill;
            
            TableLayoutPanel timePanel = new TableLayoutPanel();
            timePanel.Dock = DockStyle.Fill;
            timePanel.Padding = new Padding(10);
            timePanel.ColumnCount = 2;
            timePanel.RowCount = 3;
            timePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            timePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            
            // Mute time
            Label muteTimeLabel = new Label();
            muteTimeLabel.Text = "Mute Time:";
            muteTimeLabel.AutoSize = true;
            muteTimeLabel.Anchor = AnchorStyles.Left;
            
            DateTimePicker muteTimePicker = new DateTimePicker();
            muteTimePicker.Format = DateTimePickerFormat.Time;
            muteTimePicker.ShowUpDown = true;
            muteTimePicker.Dock = DockStyle.Fill;
            
            timePanel.Controls.Add(muteTimeLabel, 0, 0);
            timePanel.Controls.Add(muteTimePicker, 1, 0);
            
            // Duration
            Label durationLabel = new Label();
            durationLabel.Text = "Duration (hours):";
            durationLabel.AutoSize = true;
            durationLabel.Anchor = AnchorStyles.Left;
            
            NumericUpDown durationPicker = new NumericUpDown();
            durationPicker.Minimum = 0;
            durationPicker.Maximum = 24;
            durationPicker.Value = 2;
            durationPicker.DecimalPlaces = 1;
            durationPicker.Increment = 0.5m;
            durationPicker.Dock = DockStyle.Fill;
            
            timePanel.Controls.Add(durationLabel, 0, 1);
            timePanel.Controls.Add(durationPicker, 1, 1);
            
            // Frequency
            Label frequencyLabel = new Label();
            frequencyLabel.Text = "Frequency:";
            frequencyLabel.AutoSize = true;
            frequencyLabel.Anchor = AnchorStyles.Left;
            
            ComboBox frequencyComboBox = new ComboBox();
            frequencyComboBox.Items.AddRange(new object[] { "One-time", "Recurring" });
            frequencyComboBox.SelectedIndex = 1; // Default to recurring
            frequencyComboBox.Dock = DockStyle.Fill;
            
            timePanel.Controls.Add(frequencyLabel, 0, 2);
            timePanel.Controls.Add(frequencyComboBox, 1, 2);
            
            timeSelectionGroup.Controls.Add(timePanel);
            mainPanel.Controls.Add(timeSelectionGroup, 0, 1);
            
            // Button panel
            FlowLayoutPanel buttonPanel = new FlowLayoutPanel();
            buttonPanel.Dock = DockStyle.Fill;
            buttonPanel.FlowDirection = FlowDirection.RightToLeft;
            buttonPanel.Padding = new Padding(10);
            
            Button addButton = new Button();
            addButton.Text = "Add Schedule";
            addButton.AutoSize = true;
            addButton.Padding = new Padding(10, 5, 10, 5);
            addButton.Click += (sender, e) => AddDaySpecificSchedule(daysPanel, muteTimePicker, durationPicker, frequencyComboBox);
            
            buttonPanel.Controls.Add(addButton);
            mainPanel.Controls.Add(buttonPanel, 0, 2);
            
            tab.Controls.Add(mainPanel);
        }

        private void SetupWeekdaysTab(TabPage tab)
        {
            // Main container
            TableLayoutPanel mainPanel = new TableLayoutPanel();
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.Padding = new Padding(10);
            mainPanel.ColumnCount = 1;
            mainPanel.RowCount = 2;
            mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 80F));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            
            // Time selection group
            GroupBox timeSelectionGroup = new GroupBox();
            timeSelectionGroup.Text = "Weekday Schedule (Monday to Friday)";
            timeSelectionGroup.Dock = DockStyle.Fill;
            
            TableLayoutPanel timePanel = new TableLayoutPanel();
            timePanel.Dock = DockStyle.Fill;
            timePanel.Padding = new Padding(10);
            timePanel.ColumnCount = 2;
            timePanel.RowCount = 3;
            timePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            timePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            
            // Mute time
            Label muteTimeLabel = new Label();
            muteTimeLabel.Text = "Mute Time:";
            muteTimeLabel.AutoSize = true;
            muteTimeLabel.Anchor = AnchorStyles.Left;
            
            DateTimePicker muteTimePicker = new DateTimePicker();
            muteTimePicker.Format = DateTimePickerFormat.Time;
            muteTimePicker.ShowUpDown = true;
            muteTimePicker.Dock = DockStyle.Fill;
            
            timePanel.Controls.Add(muteTimeLabel, 0, 0);
            timePanel.Controls.Add(muteTimePicker, 1, 0);
            
            // Duration
            Label durationLabel = new Label();
            durationLabel.Text = "Duration (hours):";
            durationLabel.AutoSize = true;
            durationLabel.Anchor = AnchorStyles.Left;
            
            NumericUpDown durationPicker = new NumericUpDown();
            durationPicker.Minimum = 0;
            durationPicker.Maximum = 24;
            durationPicker.Value = 2;
            durationPicker.DecimalPlaces = 1;
            durationPicker.Increment = 0.5m;
            durationPicker.Dock = DockStyle.Fill;
            
            timePanel.Controls.Add(durationLabel, 0, 1);
            timePanel.Controls.Add(durationPicker, 1, 1);
            
            // Frequency
            Label frequencyLabel = new Label();
            frequencyLabel.Text = "Frequency:";
            frequencyLabel.AutoSize = true;
            frequencyLabel.Anchor = AnchorStyles.Left;
            
            ComboBox frequencyComboBox = new ComboBox();
            frequencyComboBox.Items.AddRange(new object[] { "One-time", "Recurring" });
            frequencyComboBox.SelectedIndex = 1; // Default to recurring
            frequencyComboBox.Dock = DockStyle.Fill;
            
            timePanel.Controls.Add(frequencyLabel, 0, 2);
            timePanel.Controls.Add(frequencyComboBox, 1, 2);
            
            timeSelectionGroup.Controls.Add(timePanel);
            mainPanel.Controls.Add(timeSelectionGroup, 0, 0);
            
            // Button panel
            FlowLayoutPanel buttonPanel = new FlowLayoutPanel();
            buttonPanel.Dock = DockStyle.Fill;
            buttonPanel.FlowDirection = FlowDirection.RightToLeft;
            buttonPanel.Padding = new Padding(10);
            
            Button addButton = new Button();
            addButton.Text = "Add Weekday Schedule";
            addButton.AutoSize = true;
            addButton.Padding = new Padding(10, 5, 10, 5);
            addButton.Click += (sender, e) => AddWeekdaySchedule(muteTimePicker, durationPicker, frequencyComboBox);
            
            buttonPanel.Controls.Add(addButton);
            mainPanel.Controls.Add(buttonPanel, 0, 1);
            
            tab.Controls.Add(mainPanel);
        }

        private void SetupSchedulesTab(TabPage tab)
        {
            // Main container
            TableLayoutPanel mainPanel = new TableLayoutPanel();
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.Padding = new Padding(10);
            mainPanel.ColumnCount = 1;
            mainPanel.RowCount = 1;
            mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            
            // Schedules list view
            ListView scheduleListView = new ListView();
            scheduleListView.Dock = DockStyle.Fill;
            scheduleListView.View = View.Details;
            scheduleListView.FullRowSelect = true;
            scheduleListView.GridLines = true;
            
            // Add columns
            scheduleListView.Columns.Add("Days", 150);
            scheduleListView.Columns.Add("Time", 100);
            scheduleListView.Columns.Add("Duration", 100);
            scheduleListView.Columns.Add("Frequency", 100);
            scheduleListView.Columns.Add("Actions", 100);
            
            // Context menu for delete action
            ContextMenuStrip contextMenu = new ContextMenuStrip();
            ToolStripMenuItem deleteItem = new ToolStripMenuItem("Delete");
            deleteItem.Click += (sender, e) => 
            {
                if (scheduleListView.SelectedItems.Count > 0)
                {
                    int index = scheduleListView.SelectedItems[0].Index;
                    scheduleItems.RemoveAt(index);
                    scheduleListView.Items.RemoveAt(index);
                }
            };
            contextMenu.Items.Add(deleteItem);
            scheduleListView.ContextMenuStrip = contextMenu;
            
            mainPanel.Controls.Add(scheduleListView, 0, 0);
            tab.Controls.Add(mainPanel);
            
            // Store reference to the list view for updating
            scheduleListView.Tag = "ScheduleListView";
        }

        private void AddDaySpecificSchedule(FlowLayoutPanel daysPanel, DateTimePicker timePicker, NumericUpDown durationPicker, ComboBox frequencyComboBox)
        {
            List<DayOfWeek> selectedDays = new List<DayOfWeek>();
            
            // Get selected days
            foreach (Control control in daysPanel.Controls)
            {
                if (control is CheckBox checkBox && checkBox.Checked)
                {
                    switch (checkBox.Text)
                    {
                        case "Monday": selectedDays.Add(DayOfWeek.Monday); break;
                        case "Tuesday": selectedDays.Add(DayOfWeek.Tuesday); break;
                        case "Wednesday": selectedDays.Add(DayOfWeek.Wednesday); break;
                        case "Thursday": selectedDays.Add(DayOfWeek.Thursday); break;
                        case "Friday": selectedDays.Add(DayOfWeek.Friday); break;
                        case "Saturday": selectedDays.Add(DayOfWeek.Saturday); break;
                        case "Sunday": selectedDays.Add(DayOfWeek.Sunday); break;
                    }
                }
            }
            
            if (selectedDays.Count == 0)
            {
                MessageBox.Show("Please select at least one day.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            // Create schedule item
            ScheduleItem item = new ScheduleItem
            {
                Days = selectedDays,
                Time = timePicker.Value,
                Duration = (double)durationPicker.Value,
                IsRecurring = frequencyComboBox.SelectedIndex == 1 // 1 = Recurring
            };
            
            // Add to list and update UI
            scheduleItems.Add(item);
            UpdateScheduleListView();
        }

        private void AddWeekdaySchedule(DateTimePicker timePicker, NumericUpDown durationPicker, ComboBox frequencyComboBox)
        {
            // Create schedule item for weekdays (Monday to Friday)
            ScheduleItem item = new ScheduleItem
            {
                Days = new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday },
                Time = timePicker.Value,
                Duration = (double)durationPicker.Value,
                IsRecurring = frequencyComboBox.SelectedIndex == 1 // 1 = Recurring
            };
            
            // Add to list and update UI
            scheduleItems.Add(item);
            UpdateScheduleListView();
        }

        private void UpdateScheduleListView()
        {
            // Find the schedule list view
            ListView scheduleListView = null;
            foreach (Control control in this.Controls)
            {
                if (control is TabControl tabControl)
                {
                    foreach (TabPage tabPage in tabControl.TabPages)
                    {
                        if (tabPage.Text == "Schedules")
                        {
                            foreach (Control tabControl2 in tabPage.Controls)
                            {
                                if (tabControl2 is TableLayoutPanel panel)
                                {
                                    foreach (Control panelControl in panel.Controls)
                                    {
                                        if (panelControl is ListView listView && listView.Tag?.ToString() == "ScheduleListView")
                                        {
                                            scheduleListView = listView;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            
            if (scheduleListView == null) return;
            
            // Clear and repopulate
            scheduleListView.Items.Clear();
            
            foreach (ScheduleItem item in scheduleItems)
            {
                ListViewItem listItem = new ListViewItem(item.GetDaysString());
                listItem.SubItems.Add(item.Time.ToString("HH:mm"));
                listItem.SubItems.Add(item.Duration.ToString("0.0") + " hours");
                listItem.SubItems.Add(item.IsRecurring ? "Recurring" : "One-time");
                listItem.SubItems.Add("Delete");
                
                scheduleListView.Items.Add(listItem);
            }
        }

        private void InitializeScheduleTimer()
        {
            scheduleTimer = new System.Timers.Timer(60000); // Check every minute
            scheduleTimer.Elapsed += ScheduleTimer_Elapsed;
            scheduleTimer.AutoReset = true;
            scheduleTimer.Start();
        }

        private void ScheduleTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            CheckSchedules();
        }

        private void CheckSchedules()
        {
            DateTime now = DateTime.Now;
            
            foreach (ScheduleItem item in scheduleItems.ToList())
            {
                // Check if today is in the scheduled days
                if (item.Days.Contains(now.DayOfWeek))
                {
                    // Check if it's time to mute
                    DateTime scheduleTime = new DateTime(
                        now.Year, now.Month, now.Day,
                        item.Time.Hour, item.Time.Minute, 0);
                    
                    // If it's exactly the scheduled time (within the minute)
                    if (now.Hour == scheduleTime.Hour && now.Minute == scheduleTime.Minute)
                    {
                        MuteSystem();
                        
                        // If there's a duration, schedule unmute
                        if (item.Duration > 0)
                        {
                            System.Threading.Tasks.Task.Delay(TimeSpan.FromHours(item.Duration))
                                .ContinueWith(t => UnmuteSystem());
                        }
                        
                        // Remove if one-time
                        if (!item.IsRecurring)
                        {
                            scheduleItems.Remove(item);
                            this.Invoke(new Action(UpdateScheduleListView));
                        }
                    }
                }
            }
        }

        private void MuteSystem()
        {
            try
            {
                MMDeviceEnumerator deviceEnumerator = new MMDeviceEnumerator();
                MMDevice device = deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
                device.AudioEndpointVolume.Mute = true;
                
                // Show notification
                if (trayIcon != null)
                {
                    trayIcon.ShowBalloonTip(3000, "Mute Scheduler", "System has been muted according to schedule.", ToolTipIcon.Info);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error muting system: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UnmuteSystem()
        {
            try
            {
                MMDeviceEnumerator deviceEnumerator = new MMDeviceEnumerator();
                MMDevice device = deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
                device.AudioEndpointVolume.Mute = false;
                
                // Show notification
                if (trayIcon != null)
                {
                    trayIcon.ShowBalloonTip(3000, "Mute Scheduler", "System has been unmuted according to schedule.", ToolTipIcon.Info);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error unmuting system: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeTrayIcon()
        {
            trayMenu = new ContextMenuStrip();
            
            ToolStripMenuItem openItem = new ToolStripMenuItem("Open");
            openItem.Click += (sender, e) => 
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
            };
            
            ToolStripMenuItem exitItem = new ToolStripMenuItem("Exit");
            exitItem.Click += (sender, e) => Application.Exit();
            
            trayMenu.Items.Add(openItem);
            trayMenu.Items.Add(new ToolStripSeparator());
            trayMenu.Items.Add(exitItem);
            
            trayIcon = new NotifyIcon
            {
                Text = "Mute Scheduler",
                Icon = SystemIcons.Application, // Replace with custom icon if available
                ContextMenuStrip = trayMenu,
                Visible = true
            };
            
            trayIcon.DoubleClick += (sender, e) => 
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
            };
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
                trayIcon.ShowBalloonTip(3000, "Mute Scheduler", "Application is still running in the system tray.", ToolTipIcon.Info);
            }
            else
            {
                trayIcon.Dispose();
            }
        }
        
        /// <summary>
        /// Checks if the application is set to run at Windows startup
        /// </summary>
        private bool IsAutoStartEnabled()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
                {
                    return key.GetValue("MuteScheduler") != null;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        /// <summary>
        /// Sets or removes the application from Windows startup
        /// </summary>
        /// <param name="enable">True to add to startup, false to remove</param>
        private void SetAutoStart(bool enable)
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
                {
                    if (enable)
                    {
                        string appPath = Application.ExecutablePath;
                        key.SetValue("MuteScheduler", appPath);
                    }
                    else
                    {
                        key.DeleteValue("MuteScheduler", false);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error configuring auto-start: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    public class ScheduleItem
    {
        public List<DayOfWeek> Days { get; set; }
        public DateTime Time { get; set; }
        public double Duration { get; set; }
        public bool IsRecurring { get; set; }
        
        public string GetDaysString()
        {
            if (Days.Count == 5 && 
                Days.Contains(DayOfWeek.Monday) && 
                Days.Contains(DayOfWeek.Tuesday) && 
                Days.Contains(DayOfWeek.Wednesday) && 
                Days.Contains(DayOfWeek.Thursday) && 
                Days.Contains(DayOfWeek.Friday))
            {
                return "Weekdays";
            }
            
            List<string> dayNames = new List<string>();
            foreach (DayOfWeek day in Days)
            {
                dayNames.Add(day.ToString());
            }
            
            return string.Join(", ", dayNames);
        }
    }
}
