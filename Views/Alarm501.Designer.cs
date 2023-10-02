namespace Alarm501
{
    partial class Alarm501
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.UxEditBtn = new System.Windows.Forms.Button();
            this.UxAddBtn = new System.Windows.Forms.Button();
            this.UxAlarmList = new System.Windows.Forms.ListBox();
            this.UxSnoozeButton = new System.Windows.Forms.Button();
            this.UxStopButton = new System.Windows.Forms.Button();
            this.uxAlarmAlert = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // UxEditBtn
            // 
            this.UxEditBtn.Location = new System.Drawing.Point(24, 17);
            this.UxEditBtn.Margin = new System.Windows.Forms.Padding(2);
            this.UxEditBtn.Name = "UxEditBtn";
            this.UxEditBtn.Size = new System.Drawing.Size(72, 37);
            this.UxEditBtn.TabIndex = 0;
            this.UxEditBtn.Text = "Edit";
            this.UxEditBtn.UseVisualStyleBackColor = true;
            this.UxEditBtn.Click += new System.EventHandler(this.UxEditBtn_Click);
            // 
            // UxAddBtn
            // 
            this.UxAddBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UxAddBtn.Location = new System.Drawing.Point(144, 17);
            this.UxAddBtn.Margin = new System.Windows.Forms.Padding(2);
            this.UxAddBtn.Name = "UxAddBtn";
            this.UxAddBtn.Size = new System.Drawing.Size(70, 37);
            this.UxAddBtn.TabIndex = 1;
            this.UxAddBtn.Text = "+";
            this.UxAddBtn.UseVisualStyleBackColor = true;
            this.UxAddBtn.Click += new System.EventHandler(this.UxAddBtn_Click);
            // 
            // UxAlarmList
            // 
            this.UxAlarmList.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UxAlarmList.FormattingEnabled = true;
            this.UxAlarmList.ItemHeight = 25;
            this.UxAlarmList.Location = new System.Drawing.Point(24, 87);
            this.UxAlarmList.Margin = new System.Windows.Forms.Padding(2);
            this.UxAlarmList.Name = "UxAlarmList";
            this.UxAlarmList.ScrollAlwaysVisible = true;
            this.UxAlarmList.Size = new System.Drawing.Size(191, 154);
            this.UxAlarmList.TabIndex = 2;
            this.UxAlarmList.SelectedIndexChanged += new System.EventHandler(this.UxAlarmList_SelectedIndexChanged);
            // 
            // UxSnoozeButton
            // 
            this.UxSnoozeButton.Location = new System.Drawing.Point(24, 298);
            this.UxSnoozeButton.Margin = new System.Windows.Forms.Padding(2);
            this.UxSnoozeButton.Name = "UxSnoozeButton";
            this.UxSnoozeButton.Size = new System.Drawing.Size(72, 37);
            this.UxSnoozeButton.TabIndex = 3;
            this.UxSnoozeButton.Text = "Snooze";
            this.UxSnoozeButton.UseVisualStyleBackColor = true;
            this.UxSnoozeButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // UxStopButton
            // 
            this.UxStopButton.Location = new System.Drawing.Point(142, 298);
            this.UxStopButton.Margin = new System.Windows.Forms.Padding(2);
            this.UxStopButton.Name = "UxStopButton";
            this.UxStopButton.Size = new System.Drawing.Size(72, 37);
            this.UxStopButton.TabIndex = 4;
            this.UxStopButton.Text = "Stop";
            this.UxStopButton.UseVisualStyleBackColor = true;
            this.UxStopButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // uxAlarmAlert
            // 
            this.uxAlarmAlert.AutoSize = true;
            this.uxAlarmAlert.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxAlarmAlert.Location = new System.Drawing.Point(52, 264);
            this.uxAlarmAlert.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.uxAlarmAlert.Name = "uxAlarmAlert";
            this.uxAlarmAlert.Size = new System.Drawing.Size(0, 13);
            this.uxAlarmAlert.TabIndex = 6;
            // 
            // Alarm501
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(239, 359);
            this.Controls.Add(this.uxAlarmAlert);
            this.Controls.Add(this.UxStopButton);
            this.Controls.Add(this.UxSnoozeButton);
            this.Controls.Add(this.UxAlarmList);
            this.Controls.Add(this.UxAddBtn);
            this.Controls.Add(this.UxEditBtn);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Alarm501";
            this.Text = "Alarm501";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Alarm501_FormClosed);
            this.Load += new System.EventHandler(this.Alarm501_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button UxEditBtn;
        private System.Windows.Forms.Button UxAddBtn;
        private System.Windows.Forms.ListBox UxAlarmList;
        private System.Windows.Forms.Button UxSnoozeButton;
        private System.Windows.Forms.Button UxStopButton;
        private System.Windows.Forms.Label uxAlarmAlert;
    }
}

