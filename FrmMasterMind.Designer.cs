using System;

namespace MasterMindCS
{
    partial class FrmMasterMind
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
            this.LblDateToday = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LblDateToday
            // 
            this.LblDateToday.AutoSize = true;
            this.LblDateToday.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.LblDateToday.Location = new System.Drawing.Point(378, 31);
            this.LblDateToday.Name = "LblDateToday";
            this.LblDateToday.Size = new System.Drawing.Size(18, 18);
            this.LblDateToday.TabIndex = 0;
            this.LblDateToday.Text = "\"\"";
            this.LblDateToday.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmMasterMind
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(634, 516);
            this.Controls.Add(this.LblDateToday);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.Name = "FrmMasterMind";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mastermind Game Program in C#";
            this.Load += new System.EventHandler(this.FrmMasterMind_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FrmMasterMind_DragDrop);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FrmMasterMind_Paint);
            this.DoubleClick += new System.EventHandler(this.FrmMasterMind_DoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMasterMind_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FrmMasterMind_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FrmMasterMind_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label LblDateToday;
    }
}