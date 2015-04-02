namespace tree_builder
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listBoxSkills = new System.Windows.Forms.ListBox();
            this.buttonModify = new System.Windows.Forms.Button();
            this.descriptionBox = new System.Windows.Forms.RichTextBox();
            this.panelDrawingSection = new System.Windows.Forms.Panel();
            this.listBoxMolecules = new System.Windows.Forms.ListBox();
            this.labelSkills = new System.Windows.Forms.Label();
            this.labelMolecules = new System.Windows.Forms.Label();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.createNew = new System.Windows.Forms.Button();
            this.form1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.form1BindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1213, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportToolStripMenuItem,
            this.importToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.exportToolStripMenuItem.Text = "Export";
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.importToolStripMenuItem.Text = "Import";
            // 
            // listBoxSkills
            // 
            this.listBoxSkills.FormattingEnabled = true;
            this.listBoxSkills.Location = new System.Drawing.Point(12, 49);
            this.listBoxSkills.Name = "listBoxSkills";
            this.listBoxSkills.Size = new System.Drawing.Size(154, 212);
            this.listBoxSkills.TabIndex = 1;
            // 
            // buttonModify
            // 
            this.buttonModify.Location = new System.Drawing.Point(12, 527);
            this.buttonModify.Name = "buttonModify";
            this.buttonModify.Size = new System.Drawing.Size(154, 23);
            this.buttonModify.TabIndex = 3;
            this.buttonModify.Text = "Modify";
            this.buttonModify.UseVisualStyleBackColor = true;
            // 
            // descriptionBox
            // 
            this.descriptionBox.Location = new System.Drawing.Point(967, 27);
            this.descriptionBox.Name = "descriptionBox";
            this.descriptionBox.Size = new System.Drawing.Size(234, 570);
            this.descriptionBox.TabIndex = 4;
            this.descriptionBox.Text = "";
            // 
            // panelDrawingSection
            // 
            this.panelDrawingSection.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panelDrawingSection.Location = new System.Drawing.Point(172, 27);
            this.panelDrawingSection.Name = "panelDrawingSection";
            this.panelDrawingSection.Size = new System.Drawing.Size(789, 570);
            this.panelDrawingSection.TabIndex = 5;
            // 
            // listBoxMolecules
            // 
            this.listBoxMolecules.FormattingEnabled = true;
            this.listBoxMolecules.Location = new System.Drawing.Point(12, 280);
            this.listBoxMolecules.Name = "listBoxMolecules";
            this.listBoxMolecules.Size = new System.Drawing.Size(154, 212);
            this.listBoxMolecules.TabIndex = 6;
            // 
            // labelSkills
            // 
            this.labelSkills.AutoSize = true;
            this.labelSkills.Location = new System.Drawing.Point(12, 28);
            this.labelSkills.Name = "labelSkills";
            this.labelSkills.Size = new System.Drawing.Size(31, 13);
            this.labelSkills.TabIndex = 7;
            this.labelSkills.Text = "Skills";
            // 
            // labelMolecules
            // 
            this.labelMolecules.AutoSize = true;
            this.labelMolecules.Location = new System.Drawing.Point(9, 264);
            this.labelMolecules.Name = "labelMolecules";
            this.labelMolecules.Size = new System.Drawing.Size(55, 13);
            this.labelMolecules.TabIndex = 8;
            this.labelMolecules.Text = "Molecules";
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(12, 556);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(154, 23);
            this.buttonDelete.TabIndex = 9;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            // 
            // createNew
            // 
            this.createNew.Location = new System.Drawing.Point(12, 498);
            this.createNew.Name = "createNew";
            this.createNew.Size = new System.Drawing.Size(154, 23);
            this.createNew.TabIndex = 10;
            this.createNew.Text = "Create ...";
            this.createNew.UseVisualStyleBackColor = true;
            this.createNew.Click += new System.EventHandler(this.createNew_Click);
            // 
            // form1BindingSource
            // 
            this.form1BindingSource.DataSource = typeof(tree_builder.Form1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1213, 627);
            this.Controls.Add(this.createNew);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.labelMolecules);
            this.Controls.Add(this.labelSkills);
            this.Controls.Add(this.listBoxMolecules);
            this.Controls.Add(this.panelDrawingSection);
            this.Controls.Add(this.descriptionBox);
            this.Controls.Add(this.buttonModify);
            this.Controls.Add(this.listBoxSkills);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Tree Builder";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.form1BindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ListBox listBoxSkills;
        private System.Windows.Forms.Button buttonModify;
        private System.Windows.Forms.RichTextBox descriptionBox;
        private System.Windows.Forms.Panel panelDrawingSection;
        private System.Windows.Forms.ListBox listBoxMolecules;
        private System.Windows.Forms.Label labelSkills;
        private System.Windows.Forms.Label labelMolecules;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.BindingSource form1BindingSource;
        private System.Windows.Forms.Button createNew;

    }
}

