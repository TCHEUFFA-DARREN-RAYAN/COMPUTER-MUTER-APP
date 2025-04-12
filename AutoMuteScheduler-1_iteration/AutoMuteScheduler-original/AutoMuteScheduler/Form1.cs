using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Win32;
using NAudio.CoreAudioApi; // Ensure you installed NAudio via NuGet

namespace AutoMuteScheduler
{
    public partial class Form1 : Form
    {
        private List<DateTime> muteSchedule = new List<DateTime>();
        private System.Windows.Forms.Timer checkTimer;  // Explicitly define Windows Forms Timer
        private NotifyIcon trayIcon;
        private ContextMenuStrip trayMenu;

        public Form1()
        {
            InitializeComponent();
            SetStartup();
            InitializeTrayIcon();
            InitializeTimer();
        }

        private void InitializeTimer()
        {
            // Initialize the Timer
            checkTimer = new System.Windows.Forms.Timer();
            checkTimer.Interval = 60000; // Check every 1 minute
            checkTimer.Tick += CheckMuteSchedule;
            checkTimer.Start();
        }

        private void btnAddSchedule_Click(object sender, EventArgs e)
        {
            if (dateTimePicker.Value > DateTime.Now)
            {
                muteSchedule.Add(dateTimePicker.Value);
                listBoxSchedules.Items.Add(dateTimePicker.Value.ToString("yyyy-MM-dd HH:mm"));
            }
            else
            {
                MessageBox.Show("Please choose a future date and time.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnWeekdays_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            List<DayOfWeek> selectedDays = new List<DayOfWeek>();

            // Collect selected weekdays
            if (chkMonday.Checked) selectedDays.Add(DayOfWeek.Monday);
            if (chkTuesday.Checked) selectedDays.Add(DayOfWeek.Tuesday);
            if (chkWednesday.Checked) selectedDays.Add(DayOfWeek.Wednesday);
            if (chkThursday.Checked) selectedDays.Add(DayOfWeek.Thursday);
            if (chkFriday.Checked) selectedDays.Add(DayOfWeek.Friday);
            if (chkSaturday.Checked) selectedDays.Add(DayOfWeek.Saturday);
            if (chkSunday.Checked) selectedDays.Add(DayOfWeek.Sunday);

            foreach (var day in selectedDays)
            {
                DateTime muteTime = new DateTime(now.Year, now.Month, now.Day, timePicker.Value.Hour, timePicker.Value.Minute, 0);
                if (muteTime < DateTime.Now)
                    muteTime = muteTime.AddDays(7); // Ensure it's in the future if the time has passed today
                muteSchedule.Add(muteTime);
                listBoxSchedules.Items.Add(muteTime.ToString("yyyy-MM-dd HH:mm"));
            }
        }

        private void btnSpecificDay_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = dateTimePicker.Value;
            if (selectedDate > DateTime.Now)
            {
                muteSchedule.Add(selectedDate);
                listBoxSchedules.Items.Add(selectedDate.ToString("yyyy-MM-dd HH:mm"));
            }
            else
            {
                MessageBox.Show("Please choose a future date.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CheckMuteSchedule(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;

            // Allow a 10-second window for matching
            var toMute = muteSchedule.FirstOrDefault(d =>
                d.Date == now.Date &&
                d.Hour == now.Hour &&
                d.Minute == now.Minute &&
                Math.Abs((d - now).TotalSeconds) <= 10);

            if (toMute != default)
            {
                MuteSystem();
                muteSchedule.Remove(toMute);
                listBoxSchedules.Items.Remove(toMute.ToString("yyyy-MM-dd HH:mm"));
            }
        }

        private void MuteSystem()
        {
            MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
            MMDevice device = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            device.AudioEndpointVolume.Mute = true;
            MessageBox.Show("System Muted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SetStartup()
        {
            string appName = "AutoMuteScheduler";
            string exePath = Application.ExecutablePath;

            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
            key.SetValue(appName, exePath);
        }

        private void InitializeTrayIcon()
        {
            trayMenu = new ContextMenuStrip();
            trayMenu.Items.Add("Open", null, ShowWindow);
            trayMenu.Items.Add("Exit", null, OnExit);

            trayIcon = new NotifyIcon();
            trayIcon.Text = "Auto Mute Scheduler";
            trayIcon.Icon = SystemIcons.Information; // Change to your app icon
            trayIcon.ContextMenuStrip = trayMenu;
            trayIcon.Visible = true;

            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }

        private void ShowWindow(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            this.Activate();
        }

        private void OnExit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = false;
                trayIcon.Visible = true;
            }
        }
    }
}
