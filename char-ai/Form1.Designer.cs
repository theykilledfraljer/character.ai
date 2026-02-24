namespace char_ai;

partial class MainForm
{

    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region design

    private void InitializeComponent()
    {
        SuspendLayout();
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1366, 786);
        Icon = Resources.cai_logo;
        Name = "MainForm";
        Text = "(character.ai)";
        ResumeLayout(false);
    }

    #endregion
}
