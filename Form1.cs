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
        private Button btnStart, btnPause, btnClear;
        private System.Windows.Forms.Timer timer;
        private TimeSpan timeSpan;
        private string _soundFilePath = "D:\\Code\\MultiTimer\\MultiTimerApp\\Resources\\simplealarm.mp3"; // mp3 path


        public TimerControl()
        {
            // TextBox for input
            txtTime = new TextBox();
            txtTime.Text = "00:00:00";
            txtTime.Width = 100; // Set an appropriate width 
            txtTime.TextAlign = HorizontalAlignment.Center; // Center text in the TextBox
            this.Controls.Add(txtTime);

            // Start button
            btnStart = new Button { Text = "Start", AutoSize = true };
            btnStart.TextAlign = ContentAlignment.MiddleCenter;
            btnStart.Click += BtnStart_Click;
            this.Controls.Add(btnStart);

            // Pause button
            btnPause = new Button { Text = "Pause", AutoSize = true, Enabled = false };
            btnPause.TextAlign = ContentAlignment.MiddleCenter;
            btnPause.Click += BtnPause_Click;
            this.Controls.Add(btnPause);

            // Clear button
            btnClear = new Button { Text = "Clear", AutoSize = true, Enabled = false };
            btnClear.TextAlign = ContentAlignment.MiddleCenter;
            btnClear.Click += BtnClear_Click;
            this.Controls.Add(btnClear);

            // Timer setup
            timer = new System.Windows.Forms.Timer();
            timer.Tick += Timer_Tick;
            timer.Interval = 1000; // 1 second tick

            // Set FlowDirection so controls are arranged horizontally
            this.FlowDirection = FlowDirection.LeftToRight;
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (timer == null)
            {
                throw new InvalidOperationException("Timer was not properly initialized.");
            }

            if (TimeSpan.TryParseExact(txtTime.Text, @"hh\:mm\:ss", null, out timeSpan))
            {
                timer.Start();
                btnPause.Enabled = true;
                btnClear.Enabled = true;
                btnStart.Enabled = false;
            }
            else
            {
                MessageBox.Show("Please enter a valid time in the format HH:MM:SS.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            txtTime.Text = "00:00:00";
            txtTime.ForeColor = System.Drawing.Color.Black;
            btnStart.Enabled = true;
            btnPause.Enabled = false;
            btnClear.Enabled = false;
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
            txtTime.Text = timeSpan.ToString(@"hh\:mm\:ss");
        }
    }
}