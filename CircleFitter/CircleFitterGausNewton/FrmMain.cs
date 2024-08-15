

namespace michele.natale.CyrcleFitters;


using static RandomPoints;


public partial class FrmMain : FrmMainBase
{

  private List<PointF> Points2D = [];

  /// <summary>
  /// C-Tor
  /// </summary>
  public FrmMain()
  {
    this.InitializeComponent();
    this.InitializeNewPoints(-1);
  }

  /// <summary>
  /// Moves the TLP to the correct location when the client size changes.
  /// </summary>
  /// <param name="e"></param>
  protected override void OnResize(EventArgs e)
  {
    var edge = 5;
    base.OnResize(e);
    var pos = new Point(
      this.ClientSize.Width - this.TlpButton.Width - edge,
      this.ClientSize.Height - this.TlpButton.Height - edge);
    this.TlpButton.Location = pos;
  }

  /// <summary>
  /// Starts the StartNewCircleFitter sequence
  /// </summary>
  /// <param name="e"></param>
  protected override void OnLoad(EventArgs e)
  {
    base.OnLoad(e);
    this.StartNewCircleFitter();
  }

  /// <summary>
  /// Creates a new list of points according to the random criteria.
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void Frm_Button_Click(object sender, EventArgs e)
  {
    switch (sender)
    {
      case Button obj when obj == this.BtRngData: this.InitializeNewPoints(1); break;
      case Button obj when obj == this.BtRngCircleData: this.InitializeNewPoints(0); break;
    }
    this.StartNewCircleFitter();
  }

  /// <summary>
  /// Initializes the variable Points2D with new values.
  /// </summary>
  /// <param name="choise"></param>
  private void InitializeNewPoints(int choise)
  {
    var sz = Rand.Next(5, 30);
    var f = Math.Abs(Convert.ToSingle(Distortion(0.3f, 0.55f)));
    var csz = new Size(this.ClientSize.Width, this.ClientSize.Height);
    var cmax = Math.Min(this.ClientSize.Width, this.ClientSize.Height);
    this.Points2D = choise switch
    {
      0 => RngCirclePoints(sz, cmax / 2.0f * f, csz),
      1 => RngXYData(sz, 10, (cmax / 4) * 3),
      _ => RngCirclePoints(sz, (cmax / 2.0f) * f, csz),
    };
  }

  /// <summary>
  /// Starts the StartNewCircleFitter sequence
  /// </summary>
  private void StartNewCircleFitter()
  {
    this.StartLss(this.Points2D);
    this.SetPoints(this.Points2D);
    this.SetSolverCircle(this.Lss);
    this.SetCenter(this.Lss);
    this.Invalidate();
  }

}

