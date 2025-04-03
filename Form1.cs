using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using System.Timers;
using NAudio.Wave;

namespace MultiTimerApp
{
    public partial class Form1 : Form
    {
        private List<TimerControl> timers = new List<TimerControl>();

        public Form1()
        {
            InitializeComponent();
            btnAddTimer.Click += BtnAddTimer_Click;
        }

        private void BtnAddTimer_Click(object sender, EventArgs e)
        {
            TimerControl newTimer = new TimerControl();
            timers.Add(newTimer);
            flpTimers.Controls.Add(newTimer);
        }
    }

    public class TimerControl : FlowLayoutPanel
    {
        private TextBox txtTime;
        private TextBox txtName;
        private Label lblNameDisplay;
        private Button btnStart, btnPause, btnClear;
        private System.Windows.Forms.Timer timer;
        private TimeSpan timeSpan;
        private string _soundFilePath = "D:\\Code\\MultiTimer\\MultiTimerApp\\Resources\\simplealarm.mp3"; // Adjust this to your actual path or make it resource-based

        public TimerControl()
        {
            // Set the height of the TimerControl to give more vertical space
            this.Height = 200; // Increased height to prevent clipping
            this.AutoSize = true; // Allow the panel to grow as needed
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.Padding = new Padding(15); // Add padding to the panel to avoid edge clipping

            // Name TextBox
            txtName = new TextBox();
            txtName.Width = 200;
            txtName.TextAlign = HorizontalAlignment.Center;
            txtName.Text = "";
            txtName.Margin = new Padding(15); // Add margin for spacing
            this.Controls.Add(txtName);

            // Time TextBox
            txtTime = new TextBox();
            txtTime.Text = "000:00:00";
            txtTime.Width = 200;
            txtTime.TextAlign = HorizontalAlignment.Center;
            txtTime.Margin = new Padding(15);
            this.Controls.Add(txtTime);

            // Name Display Label (hidden initially)
            lblNameDisplay = new Label();
            lblNameDisplay.AutoSize = true;
            lblNameDisplay.Font = new Font(FontFamily.GenericSansSerif, 8.25f, FontStyle.Bold);
            lblNameDisplay.Visible = false;
            lblNameDisplay.Margin = new Padding(15);
            this.Controls.Add(lblNameDisplay);

            // Start button
            btnStart = new Button { Text = "Start" };
            btnStart.Size = new Size(150, 70); // Explicit size to ensure full visibility
            btnStart.TextAlign = ContentAlignment.MiddleCenter;
            btnStart.Click += BtnStart_Click;
            btnStart.Margin = new Padding(3);
            this.Controls.Add(btnStart);

            // Pause button
            btnPause = new Button { Text = "Pause", Enabled = false };
            btnPause.Size = new Size(150, 70); // Explicit size
            btnPause.TextAlign = ContentAlignment.MiddleCenter;
            btnPause.Click += BtnPause_Click;
            btnPause.Margin = new Padding(3);
            this.Controls.Add(btnPause);

            // Clear button
            btnClear = new Button { Text = "Clear", Enabled = false };
            btnClear.Size = new Size(150, 70); // Explicit size
            btnClear.TextAlign = ContentAlignment.MiddleCenter;
            btnClear.Click += BtnClear_Click;
            btnClear.Margin = new Padding(3);
            this.Controls.Add(btnClear);

            // Timer setup
            timer = new System.Windows.Forms.Timer();
            timer.Tick += Timer_Tick;
            timer.Interval = 1000; // 1 second tick

            // Set FlowDirection and ensure wrapping
            this.FlowDirection = FlowDirection.LeftToRight;
            this.WrapContents = true; // Ensure controls wrap if needed
        }

        private bool TryParseCustomTime(string input, out TimeSpan result)
        {
            result = TimeSpan.Zero;
            string[] parts = input.Split(':');
            if (parts.Length != 3)
                return false;

            if (!int.TryParse(parts[0], out int hours) || hours < 0 ||
                !int.TryParse(parts[1], out int minutes) || minutes < 0 || minutes > 59 ||
                !int.TryParse(parts[2], out int seconds) || seconds < 0 || seconds > 59)
            {
                return false;
            }

            result = new TimeSpan(hours, minutes, seconds);
            return true;
        }

        private string FormatTimeSpan(TimeSpan ts)
        {
            return $"{(int)ts.TotalHours:D3}:{ts.Minutes:D2}:{ts.Seconds:D2}";
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (timer == null)
            {
                throw new InvalidOperationException("Timer was not properly initialized.");
            }

            // Check if name is empty
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please enter a name first.", "Missing Name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (TryParseCustomTime(txtTime.Text, out timeSpan))
            {
                // Hide the name input and show the bolded name
                txtName.Visible = false;
                lblNameDisplay.Text = txtName.Text;
                lblNameDisplay.Visible = true;

                timer.Start();
                btnPause.Enabled = true;
                btnClear.Enabled = true;
                btnStart.Enabled = false;
            }
            else
            {
                MessageBox.Show("Please enter a valid time in the format HHH:MM:SS (e.g., 123:45:00).", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnPause_Click(object sender, EventArgs e)
        {
            if (timer == null)
            {
                throw new InvalidOperationException("Timer was not properly initialized.");
            }

            timer.Enabled = !timer.Enabled;  // Toggle pause
            btnPause.Text = timer.Enabled ? "Pause" : "Resume";
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (timer == null)
            {
                throw new InvalidOperationException("Timer was not properly initialized.");
            }

            timer.Stop();
            timeSpan = TimeSpan.Zero;
            txtTime.Text = "000:00:00";
            txtTime.ForeColor = System.Drawing.Color.Black;
            btnStart.Enabled = true;
            btnPause.Enabled = false;
            btnClear.Enabled = false;

            // Reset name display
            txtName.Text = "";
            txtName.Visible = true;
            lblNameDisplay.Visible = false;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (timer == null)
            {
                throw new InvalidOperationException("Timer was not properly initialized.");
            }

            timeSpan = timeSpan.Subtract(TimeSpan.FromSeconds(1));
            if (timeSpan <= TimeSpan.Zero)
            {
                timeSpan = TimeSpan.Zero;
                timer.Stop();
                txtTime.ForeColor = System.Drawing.Color.Red;
            }
            txtTime.Text = FormatTimeSpan(timeSpan);
        }
    }
}