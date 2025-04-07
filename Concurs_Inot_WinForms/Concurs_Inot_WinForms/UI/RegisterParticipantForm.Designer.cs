namespace Concurs_Inot_WinForms.UI
{
    partial class RegisterParticipantForm
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
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            errorLabel = new System.Windows.Forms.Label();
            button1 = new System.Windows.Forms.Button();
            textBox3 = new System.Windows.Forms.TextBox();
            textBox2 = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            textBox1 = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            eventsFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer1.Location = new System.Drawing.Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(errorLabel);
            splitContainer1.Panel1.Controls.Add(button1);
            splitContainer1.Panel1.Controls.Add(textBox3);
            splitContainer1.Panel1.Controls.Add(textBox2);
            splitContainer1.Panel1.Controls.Add(label3);
            splitContainer1.Panel1.Controls.Add(label2);
            splitContainer1.Panel1.Controls.Add(textBox1);
            splitContainer1.Panel1.Controls.Add(label1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(eventsFlowPanel);
            splitContainer1.Size = new System.Drawing.Size(640, 450);
            splitContainer1.SplitterDistance = 295;
            splitContainer1.TabIndex = 0;
            // 
            // errorLabel
            // 
            errorLabel.AutoSize = true;
            errorLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            errorLabel.ForeColor = System.Drawing.Color.Red;
            errorLabel.Location = new System.Drawing.Point(14, 142);
            errorLabel.Name = "errorLabel";
            errorLabel.Size = new System.Drawing.Size(0, 21);
            errorLabel.TabIndex = 7;
            // 
            // button1
            // 
            button1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            button1.Location = new System.Drawing.Point(12, 404);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(97, 34);
            button1.TabIndex = 6;
            button1.Text = "Register";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textBox3
            // 
            textBox3.Font = new System.Drawing.Font("Segoe UI", 12F);
            textBox3.Location = new System.Drawing.Point(59, 105);
            textBox3.Name = "textBox3";
            textBox3.Size = new System.Drawing.Size(50, 29);
            textBox3.TabIndex = 5;
            // 
            // textBox2
            // 
            textBox2.Font = new System.Drawing.Font("Segoe UI", 12F);
            textBox2.Location = new System.Drawing.Point(102, 70);
            textBox2.Name = "textBox2";
            textBox2.Size = new System.Drawing.Size(170, 29);
            textBox2.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Segoe UI", 12F);
            label3.Location = new System.Drawing.Point(12, 108);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(40, 21);
            label3.TabIndex = 3;
            label3.Text = "Age:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Segoe UI", 12F);
            label2.Location = new System.Drawing.Point(12, 73);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(84, 21);
            label2.TabIndex = 2;
            label2.Text = "Last name:";
            // 
            // textBox1
            // 
            textBox1.Font = new System.Drawing.Font("Segoe UI", 12F);
            textBox1.Location = new System.Drawing.Point(102, 35);
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(170, 29);
            textBox1.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Segoe UI", 12F);
            label1.Location = new System.Drawing.Point(12, 38);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(86, 21);
            label1.TabIndex = 0;
            label1.Text = "First name:";
            // 
            // eventsFlowPanel
            // 
            eventsFlowPanel.AutoScroll = true;
            eventsFlowPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            eventsFlowPanel.Location = new System.Drawing.Point(3, 3);
            eventsFlowPanel.Name = "eventsFlowPanel";
            eventsFlowPanel.Size = new System.Drawing.Size(335, 447);
            eventsFlowPanel.TabIndex = 0;
            // 
            // RegisterParticipantForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(640, 450);
            Controls.Add(splitContainer1);
            Name = "RegisterParticipantForm";
            Text = "RegisterParticipantForm";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FlowLayoutPanel eventsFlowPanel;
        private System.Windows.Forms.Label errorLabel;
    }
}