
namespace DDtank_Ba_Decompress
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.loadFile = new System.Windows.Forms.Button();
            this.decryptXml = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // loadFile
            // 
            this.loadFile.Location = new System.Drawing.Point(12, 12);
            this.loadFile.Name = "loadFile";
            this.loadFile.Size = new System.Drawing.Size(362, 73);
            this.loadFile.TabIndex = 0;
            this.loadFile.Text = "Decrypt Language";
            this.loadFile.UseVisualStyleBackColor = true;
            this.loadFile.Click += new System.EventHandler(this.loadFile_Click);
            // 
            // decryptXml
            // 
            this.decryptXml.Location = new System.Drawing.Point(12, 101);
            this.decryptXml.Name = "decryptXml";
            this.decryptXml.Size = new System.Drawing.Size(362, 73);
            this.decryptXml.TabIndex = 3;
            this.decryptXml.Text = "Decrypt XML";
            this.decryptXml.UseVisualStyleBackColor = true;
            this.decryptXml.Click += new System.EventHandler(this.decryptXml_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 200);
            this.Controls.Add(this.decryptXml);
            this.Controls.Add(this.loadFile);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "SplushTools";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button loadFile;
        private System.Windows.Forms.Button decryptXml;
    }
}

