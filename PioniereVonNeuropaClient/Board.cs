using System.Drawing.Imaging;
using System.Net;
using CatanTests;

namespace PioniereVonNeuropaClient;

public partial class Board : Form{
	private int NodeSize   = 100;
	private int NodeMargin = 15;

	public Board(Game board) {
		InitializeComponent();

		standardEinstellungen();

		Controls.Add(CloseButton());

		PictureBox[] tiles = new PictureBox[board.Tiles.Length];

		Panel panel = new Panel();
		panel.Size = new Size(
			board.Width  * (NodeSize + NodeMargin) - NodeMargin,
			board.Height * (NodeSize + NodeMargin) - NodeMargin);
		panel.Anchor = AnchorStyles.None;

		panel.BackColor = Color.Black;
		Controls.Add(panel);

		for (int y = 0; y < board.Height; y++){
			for (int x = 0; x < board.Width; x++){
				tiles[y * board.Height + x] = new PictureBox();

				tiles[y * board.Height + x].BackColor = Color.DarkGray;
				tiles[y * board.Height + x].Width     = NodeSize;
				tiles[y * board.Height + x].Height    = NodeSize;

				tiles[y * board.Height + x].Location = new Point(
					y * (NodeSize + NodeMargin),
					x * (NodeSize + NodeMargin));


				Label id = new Label();
				id.Text     = (y * board.Height + x).ToString();
				id.AutoSize = true;

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
}