namespace Alarm501
{
    partial class SnoozeForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.secondsLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Label = new System.Windows.Forms.Label();
            this.secondsTxtBox = new System.Windows.Forms.TextBox();
            this.MinutesTxtBox = new System.Windows.Forms.TextBox();
            this.HoursTxtBox = new System.Windows.Forms.TextBox();
            this.uxSnoozeCancelButton = new System.Windows.Forms.Button();
            this.uxSnoozeOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(112, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(187, 33);
            this.label1.TabIndex = 0;
            this.label1.Text = "Snooze Time";
            // 
            // secondsLabel
            // 
            this.secondsLabel.AutoSize = true;
            this.secondsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.secondsLabel.Location = new System.Drawing.Point(21, 90);
            this.secondsLabel.Name = "secondsLabel";
            this.secondsLabel.Size = new System.Drawing.Size(218, 25);
            this.secondsLabel.TabIndex = 1;
            this.secondsLabel.Text = "Seconds To Snooze: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(21, 157);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(210, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Minutes To Snooze: ";
            // 
            // Label
            // 
            this.Label.AutoSize = true;
            this.Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label.Location = new System.Drawing.Point(21, 231);
            this.Label.Name = "Label";
            this.Label.Size = new System.Drawing.Size(191, 25);
            this.Label.TabIndex = 3;
            this.Label.Text = "Hours To Snooze: ";
            // 
            // secondsTxtBox
            // 
            this.secondsTxtBox.Location = new System.Drawing.Point(246, 96);
            this.secondsTxtBox.Name = "secondsTxtBox";
            this.secondsTxtBox.Size = new System.Drawing.Size(100, 20);
            this.secondsTxtBox.TabIndex = 4;
            // 
            // MinutesTxtBox
            // 
            this.MinutesTxtBox.Location = new System.Drawing.Point(246, 163);
            this.MinutesTxtBox.Name = "MinutesTxtBox";
            this.MinutesTxtBox.Size = new System.Drawing.Size(100, 20);
            this.MinutesTxtBox.TabIndex = 5;
            // 
            // HoursTxtBox
            // 
            this.HoursTxtBox.Location = new System.Drawing.Point(246, 237);
            this.HoursTxtBox.Name = "HoursTxtBox";
            this.HoursTxtBox.Size = new System.Drawing.Size(100, 20);
            this.HoursTxtBox.TabIndex = 6;
            // 
            // uxSnoozeCancelButton
            // 
            this.uxSnoozeCancelButton.Location = new System.Drawing.Point(47, 270);
            this.uxSnoozeCancelButton.Name = "uxSnoozeCancelButton";
            this.uxSnoozeCancelButton.Size = new System.Drawing.Size(75, 23);
            this.uxSnoozeCancelButton.TabIndex = 7;
            this.uxSnoozeCancelButton.Text = "Cancel";
            this.uxSnoozeCancelButton.UseVisualStyleBackColor = true;
            this.uxSnoozeCancelButton.Click += new System.EventHandler(this.uxSnoozeCancelButton_Click);
            // 
            // uxSnoozeOk
            // 
            this.uxSnoozeOk.Location = new System.Drawing.Point(246, 270);
            this.uxSnoozeOk.Name = "uxSnoozeOk";
            this.uxSnoozeOk.Size = new System.Drawing.Size(75, 23);
            this.uxSnoozeOk.TabIndex = 8;
            this.uxSnoozeOk.Text = "OK";
            this.uxSnoozeOk.UseVisualStyleBackColor = true;
            this.uxSnoozeOk.Click += new System.EventHandler(this.uxSnoozeOk_Click);
            // 
            // SnoozeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 305);
            this.Controls.Add(this.uxSnoozeOk);
            this.Controls.Add(this.uxSnoozeCancelButton);
            this.Controls.Add(this.HoursTxtBox);
            this.Controls.Add(this.MinutesTxtBox);
            this.Controls.Add(this.secondsTxtBox);
            this.Controls.Add(this.Label);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.secondsLabel);
            this.Controls.Add(this.label1);
            this.Name = "SnoozeForm";
            this.Text = "SnoozeForm";
            this.Load += new System.EventHandler(this.SnoozeForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label secondsLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label Label;
        private System.Windows.Forms.TextBox secondsTxtBox;
        private System.Windows.Forms.TextBox MinutesTxtBox;
        private System.Windows.Forms.TextBox HoursTxtBox;
        private System.Windows.Forms.Button uxSnoozeCancelButton;
        private System.Windows.Forms.Button uxSnoozeOk;
    }
}