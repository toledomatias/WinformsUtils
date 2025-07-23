namespace TestClient
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
            gr = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)gr).BeginInit();
            SuspendLayout();
            // 
            // gr
            // 
            gr.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gr.Location = new Point(12, 12);
            gr.Name = "gr";
            gr.RowHeadersWidth = 51;
            gr.Size = new Size(776, 426);
            gr.TabIndex = 0;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(gr);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)gr).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView gr;
    }
}
