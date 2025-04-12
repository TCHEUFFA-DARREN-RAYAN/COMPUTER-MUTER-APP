# Mute Scheduler App

A simple and efficient Windows Forms application that allows users to schedule automatic system muting at specific times, whether on a particular date or on recurring weekdays. This app uses the NAudio library for controlling the system volume and provides a user-friendly interface for managing mute schedules.

## Features

- **Schedule Specific Date Mutes**: Allows you to set a one-time mute schedule for a specific date and time.
- **Schedule Weekdays Mutes**: Set recurring mutes for selected weekdays at a specified time.
- **Save & Load Schedules**: The app allows you to save your schedules to a file, so they persist between sessions.
- **User-Friendly Interface**: A simple and intuitive design, including a ListBox to view scheduled mutes, and options to delete or modify schedules.
- **Background Execution**: The app runs in the background after launching, ensuring that your schedules are maintained even when the app is minimized or closed.

## Installation

To run the Mute Scheduler App locally, follow these steps:

1. **Clone TCHEUFFA's  repository**:
   ```bash
   https://github.com/TCHEUFFA-DARREN-RAYAN/COMPUTER-MUTER-APP.git
**Open the project:**

Open the solution file **MuteSchedulerApp.sln** in Visual Studio.

**Install Dependencies:**

The app uses the NAudio library for volume control. Make sure that the required NuGet packages are restored. If not, run the following command in the NuGet Package Manager:

bash
Copy
Edit
Install-Package NAudio
Run the App:

Press F5 in Visual Studio to build and run the app.

**Usage**
Add a Schedule:

Click the "Specific Date" button to select a specific date and time for the system to mute.

Click the "Weekdays" button to choose weekdays and set a recurring mute schedule at a specified time.

View & Manage Schedules:

The ListBox displays all your mute schedules.

You can delete a schedule by right-clicking the list item and confirming the deletion.

**Save & Load:**

Your mute schedules are saved automatically to a JSON file named schedule.json so that your settings persist between app sessions.

Running in the Background:

Once the app is launched, it minimizes to the system tray and continues running in the background, ensuring scheduled mutes happen even if the app is not actively in the foreground.

Screenshots
(Include screenshots of the app interface here for better user experience)

Main Form:

Scheduling Window:

Contributing
We welcome contributions! Feel free to fork the repository and submit pull requests. To report bugs or suggest features, please use the Issues tab.

Fork the repository.

Create a new branch (git checkout -b feature-branch).

Commit your changes (git commit -am 'Add new feature').

Push to the branch (git push origin feature-branch).

Create a new Pull Request.

**License**

This project is licensed under the MIT License - see the LICENSE file for details.

Acknowledgements
NAudio: For audio-related functionalities in the application.

Windows Forms: For creating the GUI and providing the necessary event-driven programming model.

JSON: For storing and loading the mute schedule.

For any questions or support, feel free to contact me at [tcheuffadarren1@gmail.com]


### Customizing the Template:
- Replace `TCHEUFFA DARREN RAYAN` with your actual GitHub username.
- Add screenshots to the "Screenshots" section if you want to show visuals of your app. You can upload the images directly to your GitHub repository and reference them in the markdown.
- You can update the **License** section to reflect the actual license you chose (MIT, Apache, etc.).

## NOTE
THIS WAS CREATED BY **TCHEUFFA DARREN RAYAN** A SOFTWARE DEVELOPER AND THE MAIN PURPOSE FOR ME TO CREATE THIS IS TO SHOW THE POTENTIAL I HAVE AND MY ABILITY TO SOLVE REAL WORLD SITUATUIONS, INCASE YOU NEED ANY HELP CONTACT ME HERE [tcheuffadarren1@gmail.com] 
**LINKEDIN PROFILE:** [https://www.linkedin.com/in/tcheuffa-darren-112214327/]



