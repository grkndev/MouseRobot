namespace MouseRobot
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
            startRecord = new Button();
            saveRecord = new Button();
            newRecord = new Button();
            openRecord = new Button();
            eventLog = new ListBox();
            contextMenuStrip1 = new ContextMenuStrip(components);
            deleteLineToolStripMenuItem = new ToolStripMenuItem();
            clearLogToolStripMenuItem = new ToolStripMenuItem();
            runRecord = new Button();
            statusLabel = new Label();
            delayInput = new NumericUpDown();
            label1 = new Label();
            loopBox = new CheckBox();
            label2 = new Label();
            contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)delayInput).BeginInit();
            SuspendLayout();
            // 
            // startRecord
            // 
            startRecord.Location = new Point(12, 16);
            startRecord.Name = "startRecord";
            startRecord.Size = new Size(184, 71);
            startRecord.TabIndex = 0;
            startRecord.Text = "Start Record";
            startRecord.UseVisualStyleBackColor = true;
            startRecord.Click += startRecord_Click;
            // 
            // saveRecord
            // 
            saveRecord.Enabled = false;
            saveRecord.Location = new Point(12, 247);
            saveRecord.Name = "saveRecord";
            saveRecord.Size = new Size(184, 71);
            saveRecord.TabIndex = 1;
            saveRecord.Text = "Save Record";
            saveRecord.UseVisualStyleBackColor = true;
            saveRecord.Click += saveRecord_Click;
            // 
            // newRecord
            // 
            newRecord.Location = new Point(12, 170);
            newRecord.Name = "newRecord";
            newRecord.Size = new Size(184, 71);
            newRecord.TabIndex = 1;
            newRecord.Text = "New Record";
            newRecord.UseVisualStyleBackColor = true;
            newRecord.Click += newRecord_Click;
            // 
            // openRecord
            // 
            openRecord.Location = new Point(12, 324);
            openRecord.Name = "openRecord";
            openRecord.Size = new Size(184, 71);
            openRecord.TabIndex = 1;
            openRecord.Text = "Open Record";
            openRecord.UseVisualStyleBackColor = true;
            openRecord.Click += openRecord_Click;
            // 
            // eventLog
            // 
            eventLog.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            eventLog.ContextMenuStrip = contextMenuStrip1;
            eventLog.FormattingEnabled = true;
            eventLog.ItemHeight = 15;
            eventLog.Location = new Point(202, 16);
            eventLog.Name = "eventLog";
            eventLog.Size = new Size(515, 379);
            eventLog.TabIndex = 2;
            eventLog.SelectedIndexChanged += eventLog_SelectedIndexChanged;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { deleteLineToolStripMenuItem, clearLogToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(133, 48);
            // 
            // deleteLineToolStripMenuItem
            // 
            deleteLineToolStripMenuItem.Enabled = false;
            deleteLineToolStripMenuItem.Name = "deleteLineToolStripMenuItem";
            deleteLineToolStripMenuItem.Size = new Size(132, 22);
            deleteLineToolStripMenuItem.Text = "Delete Line";
            deleteLineToolStripMenuItem.Click += deleteLineToolStripMenuItem_Click;
            // 
            // clearLogToolStripMenuItem
            // 
            clearLogToolStripMenuItem.Name = "clearLogToolStripMenuItem";
            clearLogToolStripMenuItem.Size = new Size(132, 22);
            clearLogToolStripMenuItem.Text = "Clear Log";
            clearLogToolStripMenuItem.Click += clearLogToolStripMenuItem_Click;
            // 
            // runRecord
            // 
            runRecord.Enabled = false;
            runRecord.Location = new Point(12, 93);
            runRecord.Name = "runRecord";
            runRecord.Size = new Size(184, 71);
            runRecord.TabIndex = 0;
            runRecord.Text = "Run Record";
            runRecord.UseVisualStyleBackColor = true;
            runRecord.Click += runRecord_Click;
            // 
            // statusLabel
            // 
            statusLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            statusLabel.AutoSize = true;
            statusLabel.Location = new Point(12, 427);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(39, 15);
            statusLabel.TabIndex = 3;
            statusLabel.Text = "Ready";
            // 
            // delayInput
            // 
            delayInput.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            delayInput.Location = new Point(636, 419);
            delayInput.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            delayInput.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            delayInput.Name = "delayInput";
            delayInput.Size = new Size(81, 23);
            delayInput.TabIndex = 4;
            delayInput.Value = new decimal(new int[] { 1000, 0, 0, 0 });
            delayInput.ValueChanged += delayInput_ValueChanged;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(594, 424);
            label1.Name = "label1";
            label1.Size = new Size(36, 15);
            label1.TabIndex = 3;
            label1.Text = "Delay";
            // 
            // loopBox
            // 
            loopBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            loopBox.AutoSize = true;
            loopBox.Location = new Point(512, 423);
            loopBox.Name = "loopBox";
            loopBox.Size = new Size(53, 19);
            loopBox.TabIndex = 5;
            loopBox.Text = "Loop";
            loopBox.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label2.AutoSize = true;
            label2.Location = new Point(12, 404);
            label2.Name = "label2";
            label2.Size = new Size(70, 15);
            label2.TabIndex = 3;
            label2.Text = "Shortcut: F9";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(723, 448);
            Controls.Add(loopBox);
            Controls.Add(delayInput);
            Controls.Add(label1);
            Controls.Add(label2);
            Controls.Add(statusLabel);
            Controls.Add(eventLog);
            Controls.Add(openRecord);
            Controls.Add(newRecord);
            Controls.Add(saveRecord);
            Controls.Add(runRecord);
            Controls.Add(startRecord);
            MinimumSize = new Size(504, 487);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MouseRobot";
            contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)delayInput).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button startRecord;
        private Button saveRecord;
        private Button newRecord;
        private Button openRecord;
        private ListBox eventLog;
        private Button runRecord;
        private Label statusLabel;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem deleteLineToolStripMenuItem;
        private ToolStripMenuItem clearLogToolStripMenuItem;
        private NumericUpDown delayInput;
        private Label label1;
        private CheckBox loopBox;
        private Label label2;
    }
}
