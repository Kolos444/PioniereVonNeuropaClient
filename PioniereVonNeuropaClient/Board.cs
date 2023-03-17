using System.Drawing.Imaging;
using System.Net;
using CatanTests;

namespace PioniereVonNeuropaClient;

public partial class Board : Form{
	private int NodeSize   = 150;
	private int NodeMargin = 0;

	public Board(Game board) {
		InitializeComponent();

		standardEinstellungen();
		Controls.Add(CloseButton());

		Panel[] tiles = new Panel[board.Tiles.Length];

		Panel panel = new Panel();
		panel.Size = new Size(
			board.Width * (NodeSize + NodeMargin) - NodeMargin + (NodeSize + NodeMargin)/2,
			board.Height * (NodeSize + NodeMargin)             - NodeMargin);
		panel.Anchor = AnchorStyles.Right;

		panel.BackColor = Color.Black;
		Controls.Add(panel);

		for (int y = 0; y < board.Height; y++){
			for (int x = 0; x < board.Width; x++){
				tiles[y * board.Height + x] = Hexagon();

				Point point;
				if (y % 2 == 0)
					point = new Point(
						x * (NodeSize + NodeMargin),
						y * (NodeSize + NodeMargin));
				else
					point = new Point(
						x * (NodeSize + NodeMargin) + (NodeSize + NodeMargin) / 2,
						y * (NodeSize + NodeMargin));

				tiles[y * board.Height + x].Location = point;
				Label id = new Label();
				id.Text      = (y * board.Height + x).ToString();
				id.Size      = tiles[y * board.Height + x].Size;
				id.TextAlign = ContentAlignment.MiddleCenter;

				tiles[y * board.Height + x].Controls.Add(id);

				panel.Controls.Add(tiles[y * board.Height + x]);
			}
		}
	}

	private static Button CloseButton() {
		Button kill = new Button();
		kill.Text      =  "I'm sorry little one";
		kill.ForeColor =  Color.Azure;
		kill.Click     += (sender, args) => { Application.Exit(); };
		WebRequest request =
			WebRequest.Create("https://i.kym-cdn.com/entries/icons/original/000/031/212/snipes.jpg");

		using (WebResponse response = request.GetResponse())
			using (Stream stream = response.GetResponseStream()){
				kill.Image = Image.FromStream(stream);
			}

		kill.AutoSize = true;

		return kill;
	}

	private void standardEinstellungen() {
		Controls.Owner.BackColor = Color.FromArgb(41, 28, 140);
		Controls.Owner.Size      = new Size(1280, 720);
		Controls.Owner.Font      = new Font("Calibri", 20, FontStyle.Bold);
		FormBorderStyle          = FormBorderStyle.None;
		WindowState              = FormWindowState.Maximized;
	}


	private Panel Hexagon() {
		Panel hexagon = new Panel();
		hexagon.BackColor = Color.Transparent;
		hexagon.Size      = new Size(NodeSize, NodeSize);
		hexagon.Paint += (_, e) => {
			Graphics graphics = e.Graphics;

			//Get the middle of the panel
			int x_0 = hexagon.Width  / 2;
			int y_0 = hexagon.Height / 2;

			PointF[] shape = new PointF[6];

			int r = hexagon.Width / 2; //70 px radius

			//Create 6 points
			for (int a = 0; a < 6; a++){
				shape[a] = new PointF(
					x_0 + r * (float)Math.Cos(a * 60 * Math.PI / 180f),
					y_0 + r * (float)Math.Sin(a * 60 * Math.PI / 180f));
			}

			graphics.FillPolygon(Brushes.Red, shape);
		};

		return hexagon;
	}
}