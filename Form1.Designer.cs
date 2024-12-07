namespace MultiTimerApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            btnAddTimer = new Button();
            flpTimers = new FlowLayoutPanel();
            bindingSource1 = new BindingSource(components);
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // btnAddTimer
            // 
            btnAddTimer.Location = new Point(6, 7);
            btnAddTimer.Margin = new Padding(6, 7, 6, 7);
            btnAddTimer.Name = "btnAddTimer";
            btnAddTimer.Size = new Size(325, 113);
            btnAddTimer.TabIndex = 0;
            btnAddTimer.Text = "Add Timer";
            btnAddTimer.UseVisualStyleBackColor = true;
            // 
            // flpTimers
            // 
            flpTimers.AutoScroll = true;
            flpTimers.AutoSize = true;
            flpTimers.Location = new Point(344, 7);
            flpTimers.Margin = new Padding(6, 7, 6, 7);
            flpTimers.MinimumSize = new Size(433, 492);
            flpTimers.Name = "flpTimers";
            flpTimers.Padding = new Padding(6, 7, 6, 7);
            flpTimers.Size = new Size(968, 492);
            flpTimers.TabIndex = 1;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.EL_2024_logo_flat;
            pictureBox1.Location = new Point(6, 130);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(325, 279);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1336, 420);
            Controls.Add(pictureBox1);
            Controls.Add(flpTimers);
            Controls.Add(btnAddTimer);
            Margin = new Padding(6, 7, 6, 7);
            Name = "Form1";
            Text = "Game Timers";
            ((System.ComponentModel.ISupportInitialize)bindingSource1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnAddTimer;
        private FlowLayoutPanel flpTimers;
        private BindingSource bindingSource1;
        private PictureBox pictureBox1;
    }
}