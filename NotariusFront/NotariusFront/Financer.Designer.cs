namespace NotariusFront
{
    partial class Financer
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
            this.PriceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MoneyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PriceToolStripMenuItem,
            this.MoneyToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(501, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // PriceToolStripMenuItem
            // 
            this.PriceToolStripMenuItem.Name = "PriceToolStripMenuItem";
            this.PriceToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.PriceToolStripMenuItem.Text = "Цены";
            this.PriceToolStripMenuItem.Click += new System.EventHandler(this.PriceToolStripMenuItem_Click);
            // 
            // MoneyToolStripMenuItem
            // 
            this.MoneyToolStripMenuItem.Name = "MoneyToolStripMenuItem";
            this.MoneyToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.MoneyToolStripMenuItem.Text = "Доходы";
            this.MoneyToolStripMenuItem.Click += new System.EventHandler(this.MoneyToolStripMenuItem_Click);
            // 
            // Financer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.menuStrip1);
            this.Name = "Financer";
            this.Size = new System.Drawing.Size(501, 398);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem PriceToolStripMenuItem;
        private ToolStripMenuItem MoneyToolStripMenuItem;
    }
}
