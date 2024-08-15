

using System.Drawing.Drawing2D;

namespace michele.natale.CyrcleFitters;

public abstract class FrmMainBase : Form
{
  protected GraphicsPath GPPoints = null!;
  protected GraphicsPath GPCenter = null!;
  protected GraphicsPath GPCircle = null!;
  protected LeastSquaresSolver Lss = null!;

  protected void FrmMain_Paint(object sender, PaintEventArgs e)
  {
    this.GPPoints.StartFigure();
    this.GPCircle.StartFigure();
    this.GPCenter.StartFigure();
    {
      e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
      e.Graphics.DrawPath(new Pen(Color.Red, 1), this.GPPoints);
      e.Graphics.DrawPath(new Pen(Color.Blue, 1), this.GPCircle);
      e.Graphics.DrawPath(new Pen(Color.Green, 1), this.GPCenter);
    }
    this.GPPoints.CloseFigure();
    this.GPCircle.CloseFigure();
    this.GPCenter.CloseFigure();
  }

  public void StartLss(List<PointF> points2d)
  {
    this.Lss = new LeastSquaresSolver(points2d);
    this.Lss.StartLSS();
  } 

  protected void SetPoints(List<PointF> points2d)
  {
    this.GPPoints?.Dispose();
    this.GPPoints = new GraphicsPath();
    this.GPPoints.StartFigure();
    points2d.ForEach(p => this.GPPoints.AddArc(
        new RectangleF(p.X - 1, p.Y - 1, 2, 2), 0, 360));
    this.GPPoints.CloseFigure();
  }

  protected void SetSolverCircle(LeastSquaresSolver lss)
  {
    this.GPCircle?.Dispose();
    this.GPCircle = new GraphicsPath();
    var r = lss.ResultXYRLSQ2;
    this.GPCircle.StartFigure();
    this.GPCircle.AddArc(new RectangleF(
      r.X - r.Z, r.Y - r.Z,
      r.Z * 2, r.Z * 2), 0, 360);
    this.GPCircle.CloseFigure();
  }

  protected void SetCenter(LeastSquaresSolver lss)
  {
    this.GPCenter?.Dispose();
    int len = 10;
    this.GPCenter = new GraphicsPath();
    var r = lss.ResultXYRLSQ2;
    this.GPCenter.StartFigure();
    this.GPCenter.AddLine(r.X - len, r.Y, r.X + len, r.Y);
    this.GPCenter.CloseFigure();
    this.GPCenter.StartFigure();
    this.GPCenter.AddLine(r.X, r.Y - len, r.X, r.Y + len);
    this.GPCenter.CloseFigure();
  }
}