namespace MedicalSystem
{
	partial class MainForm
	{
		/// <summary>
		/// Обязательная переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором форм Windows

		/// <summary>
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.imageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.enterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.messageLabel = new System.Windows.Forms.Label();
			this.pictureBoxAfter = new System.Windows.Forms.PictureBox();
			this.pictureBoxBefore = new System.Windows.Forms.PictureBox();
			this.menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxAfter)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxBefore)).BeginInit();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.оПрограммеToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(652, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// файлToolStripMenuItem
			// 
			this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.imageToolStripMenuItem,
            this.enterToolStripMenuItem,
            this.exitToolStripMenuItem});
			this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
			this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
			this.файлToolStripMenuItem.Text = "Файл";
			// 
			// оПрограммеToolStripMenuItem
			// 
			this.оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
			this.оПрограммеToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
			this.оПрограммеToolStripMenuItem.Text = "О программе";
			this.оПрограммеToolStripMenuItem.Click += new System.EventHandler(this.оПрограммеToolStripMenuItem_Click);
			// 
			// imageToolStripMenuItem
			// 
			this.imageToolStripMenuItem.Name = "imageToolStripMenuItem";
			this.imageToolStripMenuItem.Size = new System.Drawing.Size(251, 22);
			this.imageToolStripMenuItem.Text = "Проверить изображение клетки";
			this.imageToolStripMenuItem.Click += new System.EventHandler(this.imageToolStripMenuItem_Click);
			// 
			// enterToolStripMenuItem
			// 
			this.enterToolStripMenuItem.Name = "enterToolStripMenuItem";
			this.enterToolStripMenuItem.Size = new System.Drawing.Size(251, 22);
			this.enterToolStripMenuItem.Text = "Ввести данные о работе сердца";
			this.enterToolStripMenuItem.Click += new System.EventHandler(this.enterToolStripMenuItem_Click);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(251, 22);
			this.exitToolStripMenuItem.Text = "Выход";
			// 
			// openFileDialog
			// 
			this.openFileDialog.Filter = "Images|*.png;*jpg";
			// 
			// messageLabel
			// 
			this.messageLabel.AutoSize = true;
			this.messageLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
			this.messageLabel.Location = new System.Drawing.Point(97, 58);
			this.messageLabel.Name = "messageLabel";
			this.messageLabel.Size = new System.Drawing.Size(389, 18);
			this.messageLabel.TabIndex = 1;
			this.messageLabel.Text = "Шанс заражения клетки малярией составляет: 100.0%";
			// 
			// pictureBoxAfter
			// 
			this.pictureBoxAfter.Location = new System.Drawing.Point(74, 111);
			this.pictureBoxAfter.Name = "pictureBoxAfter";
			this.pictureBoxAfter.Size = new System.Drawing.Size(150, 150);
			this.pictureBoxAfter.TabIndex = 2;
			this.pictureBoxAfter.TabStop = false;
			// 
			// pictureBoxBefore
			// 
			this.pictureBoxBefore.Location = new System.Drawing.Point(413, 111);
			this.pictureBoxBefore.Name = "pictureBoxBefore";
			this.pictureBoxBefore.Size = new System.Drawing.Size(150, 150);
			this.pictureBoxBefore.TabIndex = 3;
			this.pictureBoxBefore.TabStop = false;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(652, 365);
			this.Controls.Add(this.pictureBoxBefore);
			this.Controls.Add(this.pictureBoxAfter);
			this.Controls.Add(this.messageLabel);
			this.Controls.Add(this.menuStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Smart Medical System (SMS)";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxAfter)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxBefore)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem оПрограммеToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem imageToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem enterToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.Label messageLabel;
		private System.Windows.Forms.PictureBox pictureBoxAfter;
		private System.Windows.Forms.PictureBox pictureBoxBefore;
	}
}

