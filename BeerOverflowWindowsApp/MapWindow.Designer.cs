namespace WebApi
{
    partial class MapWindow
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
            this.elementMap = new System.Windows.Forms.Integration.ElementHost();
            this.mapControl = new WebApi.MapControl();
            this.SuspendLayout();
            // 
            // elementMap
            // 
            this.elementMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementMap.Location = new System.Drawing.Point(0, 0);
            this.elementMap.Name = "elementMap";
            this.elementMap.Size = new System.Drawing.Size(459, 453);
            this.elementMap.TabIndex = 0;
            this.elementMap.Text = "elementMap";
            this.elementMap.Child = this.mapControl;
            // 
            // MapWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 453);
            this.Controls.Add(this.elementMap);
            this.Name = "MapWindow";
            this.Text = "MapWindow";
            this.ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Integration.ElementHost elementMap;
        private MapControl mapControl;
    }
}