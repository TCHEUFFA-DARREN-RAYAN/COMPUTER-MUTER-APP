# Mute Scheduler Application

**Author: TCHEUFFA DARREN**  
**Date: April 2025**

A Windows Forms desktop application that automatically mutes or unmutes the computer's system volume based on a user-defined schedule.

## Features

### 1. Mute Scheduler by Day
- Choose specific days of the week (Monday through Sunday)
- Set specific mute times
- Optional automatic unmute after a specified duration
- NAudio integration for system volume control

### 2. Mute Scheduler for Weekdays
- Quickly schedule for all weekdays (Monday to Friday) at once
- Set specific mute times for weekdays
- Optional automatic unmute after a specified duration

### 3. Frequency Selection
- One-time mute (executes only once and then removes itself)
- Recurring mute (executes daily or weekly at the selected time)

### 4. Schedule Management
- View all active mute schedules in a list view
- Each entry shows the day(s), time, duration, and frequency
- Delete functionality to remove schedules

### 5. System Tray Integration
- Application minimizes to system tray
- Notifications when mute/unmute actions occur
- Context menu for quick access to application functions

## Technical Details

- Platform: Windows Forms App (.NET Framework 4.7.2)
- Language: C#
- Library: NAudio for audio control
- Scheduling: System.Timers for monitoring and executing scheduled tasks

## Installation

1. Ensure you have .NET Framework 4.7.2 or later installed
2. Extract the zip file to a location of your choice
3. Open the solution in Visual Studio
4. Restore NuGet packages
5. Build and run the application

## Usage

1. Launch the application
2. Use the tabs to select between day-specific scheduling or weekday scheduling
3. Set your desired mute times and options
4. Add schedules using the appropriate button
5. View and manage your schedules in the Schedules tab
6. The application will continue running in the system tray when minimized

## Dependencies

- NAudio 2.2.1
- NAudio.Core 2.2.1
- NAudio.Wasapi 2.2.1
- NAudio.WinForms 2.2.1
- NAudio.WinMM 2.2.1

## Building from Source

1. Clone or download the source code
2. Open the solution in Visual Studio
3. Restore NuGet packages
4. Build the solution

## License

This project is available for personal and commercial use.
