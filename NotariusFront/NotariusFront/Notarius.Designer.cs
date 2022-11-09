namespace NotariusFront
{
    partial class Notarius
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

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ServiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ServiceToolStripMenuItem,
            this.DoToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(746, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ServiceToolStripMenuItem
            // 
            this.ServiceToolStripMenuItem.Name = "ServiceToolStripMenuItem";
            this.ServiceToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.ServiceToolStripMenuItem.Text = "Услуги";
            this.ServiceToolStripMenuItem.Click += new System.EventHandler(this.ServiceToolStripMenuItem_Click);
            // 
            // DoToolStripMenuItem
            // 
            this.DoToolStripMenuItem.Name = "DoToolStripMenuItem";
            this.DoToolStripMenuItem.Size = new System.Drawing.Size(89, 20);
            this.DoToolStripMenuItem.Text = "Выполнение";
            this.DoToolStripMenuItem.Click += new System.EventHandler(this.DoToolStripMenuItem_Click);
            // 
            // Notarius
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.menuStrip1);
            this.Name = "Notarius";
            this.Size = new System.Drawing.Size(746, 470);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem ServiceToolStripMenuItem;
        private ToolStripMenuItem DoToolStripMenuItem;
    }
}
