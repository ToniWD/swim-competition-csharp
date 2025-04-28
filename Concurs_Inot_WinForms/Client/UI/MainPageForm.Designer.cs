namespace UI
{
    partial class MainPageForm
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
            searchParticipantTextBox = new System.Windows.Forms.TextBox();
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            disconectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            registerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            eventsFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
            participantsFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // searchParticipantTextBox
            // 
            searchParticipantTextBox.Location = new System.Drawing.Point(365, 12);
            searchParticipantTextBox.Name = "searchParticipantTextBox";
            searchParticipantTextBox.Size = new System.Drawing.Size(256, 23);
            searchParticipantTextBox.TabIndex = 2;
            searchParticipantTextBox.KeyDown += textBox1_KeyDown;
            // 
            // menuStrip1
            // 
            menuStrip1.Dock = System.Windows.Forms.DockStyle.Left;
            menuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { disconectToolStripMenuItem, registerToolStripMenuItem });
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            menuStrip1.Size = new System.Drawing.Size(81, 440);
            menuStrip1.TabIndex = 3;
            menuStrip1.Text = "menuStrip1";
            // 
            // disconectToolStripMenuItem
            // 
            disconectToolStripMenuItem.Name = "disconectToolStripMenuItem";
            disconectToolStripMenuItem.Size = new System.Drawing.Size(74, 19);
            disconectToolStripMenuItem.Text = "Disconnect";
            disconectToolStripMenuItem.Click += disconectToolStripMenuItem_Click;
            // 
            // registerToolStripMenuItem
            // 
            registerToolStripMenuItem.Name = "registerToolStripMenuItem";
            registerToolStripMenuItem.Size = new System.Drawing.Size(74, 19);
            registerToolStripMenuItem.Text = "Register";
            registerToolStripMenuItem.Click += registerToolStripMenuItem_Click;
            // 
            // eventsFlowPanel
            // 
            eventsFlowPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            eventsFlowPanel.Location = new System.Drawing.Point(105, 12);
            eventsFlowPanel.Name = "eventsFlowPanel";
            eventsFlowPanel.Size = new System.Drawing.Size(255, 416);
            eventsFlowPanel.TabIndex = 4;
            // 
            // participantsFlowPanel
            // 
            participantsFlowPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            participantsFlowPanel.Location = new System.Drawing.Point(366, 41);
            participantsFlowPanel.Name = "participantsFlowPanel";
            participantsFlowPanel.Size = new System.Drawing.Size(255, 387);
            participantsFlowPanel.TabIndex = 5;
            // 
            // MainPageForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(631, 440);
            Controls.Add(participantsFlowPanel);
            Controls.Add(eventsFlowPanel);
            Controls.Add(searchParticipantTextBox);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "MainPageForm";
            Text = "MainPageForm";
            FormClosing += MainPageForm_FormClosing;
            Load += MainPageForm_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.TextBox searchParticipantTextBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem disconectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem registerToolStripMenuItem;
        private System.Windows.Forms.FlowLayoutPanel eventsFlowPanel;
        private System.Windows.Forms.FlowLayoutPanel participantsFlowPanel;
    }
}