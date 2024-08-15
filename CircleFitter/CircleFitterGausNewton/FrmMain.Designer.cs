

namespace michele.natale.CyrcleFitters;


partial class FrmMain
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
    this.TlpButton = new TableLayoutPanel();
    this.BtRngData = new Button();
    this.BtRngCircleData = new Button();
    this.TlpButton.SuspendLayout();
    this.SuspendLayout();
    // 
    // TlpButton
    // 
    this.TlpButton.Anchor = AnchorStyles.None;
    this.TlpButton.ColumnCount = 1;
    this.TlpButton.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
    this.TlpButton.Controls.Add(this.BtRngData, 0, 0);
    this.TlpButton.Controls.Add(this.BtRngCircleData, 0, 1);
    this.TlpButton.Location = new Point(647, 410);
    this.TlpButton.Margin = new Padding(5, 4, 5, 4);
    this.TlpButton.Name = "TlpButton";
    this.TlpButton.RowCount = 2;
    this.TlpButton.RowStyles.Add(new RowStyle(SizeType.Percent, 50.5263176F));
    this.TlpButton.RowStyles.Add(new RowStyle(SizeType.Percent, 49.4736824F));
    this.TlpButton.Size = new Size(205, 150);
    this.TlpButton.TabIndex = 4;
    // 
    // BtRngData
    // 
    this.BtRngData.Anchor = AnchorStyles.None;
    this.BtRngData.Location = new Point(5, 8);
    this.BtRngData.Margin = new Padding(5, 4, 5, 4);
    this.BtRngData.Name = "BtRngData";
    this.BtRngData.Size = new Size(195, 58);
    this.BtRngData.TabIndex = 0;
    this.BtRngData.Text = "RngData";
    this.BtRngData.UseVisualStyleBackColor = true;
    this.BtRngData.Click += this.Frm_Button_Click;
    // 
    // BtRngCircleData
    // 
    this.BtRngCircleData.Anchor = AnchorStyles.None;
    this.BtRngCircleData.Location = new Point(5, 83);
    this.BtRngCircleData.Margin = new Padding(5, 4, 5, 4);
    this.BtRngCircleData.Name = "BtRngCircleData";
    this.BtRngCircleData.Size = new Size(195, 58);
    this.BtRngCircleData.TabIndex = 1;
    this.BtRngCircleData.Text = "RngCircleData";
    this.BtRngCircleData.UseVisualStyleBackColor = true;
    this.BtRngCircleData.Click += this.Frm_Button_Click;
    // 
    // FrmMain
    // 
    this.AutoScaleDimensions = new SizeF(13F, 26F);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.BackColor = Color.White;
    this.ClientSize = new Size(866, 573);
    this.Controls.Add(this.TlpButton);
    this.Font = new Font("Arial", 14F);
    this.Margin = new Padding(5, 4, 5, 4);
    this.Name = "FrmMain";
    this.Text = "© CyrcleFitter 2024 Created by © Michele Natale 2024";
    this.Paint += this.FrmMain_Paint;
    this.TlpButton.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  #endregion

  private Button BtRngData;
  private Button BtRngCircleData;
  private TableLayoutPanel TlpButton;
}
