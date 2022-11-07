namespace NotariusFront
{
    partial class Admin
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
            this.NewDealToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.WorkWithDealToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewDealToolStripMenuItem,
            this.WorkWithDealToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(737, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // NewDealToolStripMenuItem
            // 
            this.NewDealToolStripMenuItem.Name = "NewDealToolStripMenuItem";
            this.NewDealToolStripMenuItem.Size = new System.Drawing.Size(93, 20);
            this.NewDealToolStripMenuItem.Text = "Новая сделка";
            this.NewDealToolStripMenuItem.Click += new System.EventHandler(this.NewDealToolStripMenuItem_Click);
            // 
            // WorkWithDealToolStripMenuItem
            // 
            this.WorkWithDealToolStripMenuItem.Name = "WorkWithDealToolStripMenuItem";
            this.WorkWithDealToolStripMenuItem.Size = new System.Drawing.Size(129, 20);
            this.WorkWithDealToolStripMenuItem.Text = "Работа со сделками";
            // 
            // Admin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.menuStrip1);
            this.Name = "Admin";
            this.Size = new System.Drawing.Size(737, 452);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem NewDealToolStripMenuItem;
        private ToolStripMenuItem WorkWithDealToolStripMenuItem;
    }
}
