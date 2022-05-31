using System.Drawing;
using System.Drawing.Drawing2D;

namespace MasterMindCS
{
    public partial class FrmMasterMind : Form
    {
		//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
		ActionController aController = new();

		/** These are the coordinates of the the previous mouse position */
		private int last_x, last_y;

		private bool clickStarted = false;
		private bool withinDraggable = false;

		//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//

		public FrmMasterMind()
        {
            InitializeComponent();
        }

		public int LastX
        {
			get { return last_x; }
			set { last_x = value; }
        }

		public int LastY
        {
			get { return last_y; }
			set { last_y = value;  }
        }

		private void FrmMasterMind_Load(object sender, EventArgs e)
		{
			string date = DateTime.Today.ToLongDateString();
			this.LblDateToday.Text = "Today is: " + date;
		}

		private void FrmMasterMind_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			g.DrawString("* Welcome to Master Mind *", new Font("Times New Roman", 16), Brushes.Green,
				360, 0);

			int i, j;
			int wxh = 40; // Set the width & height of the color circles.

			// Draw the game board area.
			aController.DrawmmBoardArea(g);

			// Draw the current row indicator.
			for (i = 0; i <= 3; i++)
			{
				aController.DrawCurrentRow(g, i * 40,
					aController.MMBoard.GetCurrentRowPos() * 40, wxh, wxh);
			}

			// Draw the colors available for selection. 
			for (i = 0; i <= 5; i++)
			{
				aController.DrawSelectableColors(g, i * 40, 40, wxh, wxh, i);
			}

			// Define the boundaries for the color selector area.
			//BoundRect selColorsRect = new(360, 420, 40, 40);       
			//aController.DrawBoundaries(g, selColorsRect, 6);

			// Draw the color values stored in the board's color holder array 
			// represented by breaker_colors_holder array in MasterMindBoard class.		
			int[,] breakerColors = aController.MMBoard.BreakColorsHolder;
			for (i = 0; i < aController.MMBoard.BreakColorsHolder.GetLength(1); i++)
			{
				for (j = 0; j < aController.MMBoard.BreakColorsHolder.GetLength(0); j++)
				{
					switch (breakerColors[j, i])
					{
						case 0:
							aController.DrawColorPegs(g, (i * 40), (j * 40), wxh, wxh, 0);
							break;
						case 1:
							aController.DrawColorPegs(g, (i * 40), (j * 40), wxh, wxh, 1);
							break;
						case 2:
							aController.DrawColorPegs(g, (i * 40), (j * 40), wxh, wxh, 2);
							break;
						case 3:
							aController.DrawColorPegs(g, (i * 40), (j * 40), wxh, wxh, 3);
							break;
						case 4:
							aController.DrawColorPegs(g, (i * 40), (j * 40), wxh, wxh, 4);
							break;
						case 5:
							aController.DrawColorPegs(g, (i * 40), (j * 40), wxh, wxh, 5);
							break;
					}
				}
			}

			// Draw the color values stored in the board's key holder array 
			// represented by keycolors_holder array in MasterMindBoard class.	
			int[,] keyColors = aController.MMBoard.KeyColorsHolder;
			for (i = 0; i < aController.MMBoard.KeyColorsHolder.GetLength(1); i++)
			{
				for (j = 0; j < aController.MMBoard.KeyColorsHolder.GetLength(0); j++)
				{
					switch (keyColors[j, i])
					{
						case 0:
							aController.DrawKeyColorPegs(g, 0, (i * 30), (j * 40), 20, 20);
							break;
						case 1:
							aController.DrawKeyColorPegs(g, 1, (i * 30), (j * 40), 20, 20);
							break;
					}
				}
			}

			if (aController.MMBoard.GetWinStatus() == true)
				aController.DisplayGameResults(g, aController.MMBoard.Win);
			else if (aController.MMBoard.GetGuessAttempts() == 10)
				aController.DisplayGameResults(g, aController.MMBoard.Lost);
			/*
			// For diagnostics or test purpose only -- Show the hidden colors array
			for (i = 0; i < aController.MMBoard.GetHiddenColors().Length; i++)
			{
				int hiddencolors = aController.MMBoard.HiddenColorsHolder[i];
				aController.DrawHiddenColors(g, i * 40, 20,
						wxh, wxh, hiddencolors);
			}
			*/
			aController.DrawSelectedColor(g, LastX, LastY, aController.ActiveColor, clickStarted, withinDraggable);

			// TODO
			//******************************************************************
			/*******************************************************************				
				if (gameBoard.getGuessAttempts() == 0) {
					m_bttnNew.EnableWindow(FALSE);
					m_bttnQuit.EnableWindow(FALSE);
				}
			******************************************************************/
		}

		private void FrmMasterMind_DoubleClick(object sender, EventArgs e)
        {
            ////////////////////////////
            // For test purpose only //

            string holderContents = "Hidden Colors:\n\n";

            for (int i= 0; i < aController.MMBoard.HiddenColorsHolder.Length; i++)
                holderContents += aController.MMBoard.HiddenColorsHolder[i] + "* ";

			holderContents += "\n\nBreaker Colors: \n";
			for (int i = 0; i < aController.MMBoard.BreakColorsHolder.GetLength(0); i++)
            {
                for (int j = 0; j < aController.MMBoard.BreakColorsHolder.GetLength(1); j++)
                    holderContents += aController.MMBoard.BreakColorsHolder[i,j].ToString() + " ";
                holderContents += "\n";
            }

			holderContents += "\n\nKey Colors: \n";
			for (int i = 0; i < aController.MMBoard.KeyColorsHolder.GetLength(0); i++)
			{
				for (int j = 0; j < aController.MMBoard.KeyColorsHolder.GetLength(1); j++)
					holderContents += aController.MMBoard.KeyColorsHolder[i, j].ToString() + " ";
					holderContents += "\n";
			}
			MessageBox.Show(holderContents, "Colors Holder Content");

			// For test purpose only //
			////////////////////////////
		}

		// Mouse dragged.
		private void FrmMasterMind_MouseMove(object sender, MouseEventArgs e)
		{
			Graphics gpx = Graphics.FromHwnd(this.Handle); // For test purpose

			LastX = e.X;
			LastY = e.Y;

			if (e.Button == MouseButtons.Left)
			{
				// Get the new location and repaint screen
				LastX = e.X;
				LastY = e.Y;

				Point xy = new Point();
				xy.X = LastX;
				xy.Y = LastY;
				int currentRow = aController.MMBoard.GetCurrentRowPos();

				// Declare & create a bounding rectangle that represents the gameboard area.
				// Mouse coordinates will be assessed if it's within the boundary & will determine 
				// the coloumn position corresponding to the coordinate of the gameboard's color peg hole.					
				BoundRect bRect = new BoundRect(20, 5, 320, 480);
				//aController.DrawBoundaries(gpx, bRect, 1);	// For test purpose.

				if (currentRow < -1)	
				{
					bRect.SetBoundStatus(bRect, false);
				}
				else if (bRect.IsWithinBounds(xy) || bRect.GetBoundStatus() != false)
				{
					//***   BoundRect object bRect has the same logic as setColorOnTempoBreaker()
					//      that uses mouse pointer coordinates to synchronize the assigment of values
					//      to tempobreaker's & the breakerColors arrays.                           **/		
					bRect.SetColoumn(xy, currentRow);
					int colPosition = bRect.GetColoumn();
					aController.SetColorOnTempoBreaker(xy, currentRow);
					aController.MMBoard.SetBreakerColors(aController.TempoBreaker, colPosition, currentRow);
				}

				aController.DrawSelectedColor(gpx, LastX, LastY, aController.ActiveColor, clickStarted, withinDraggable);

				// For testing purpose only //
				int activeColoumn = bRect.GetColoumn();
				string message = "";
				message = "||activeColoumn:" + activeColoumn;
				message += "||" + "currentRow:" + currentRow;
				message += "  last_x: " + last_x + "  last_y: " + last_y;
				///////////////////////////

			}

			// Change cursor to a pointing hand when mouse
			// pointer is within the selectable colors area.
			BoundRect selColorsRect = new(360, 420, 240, 40);
			//aController.DrawBoundaries(gpx, selColorsRect, 1);
			if (selColorsRect.IsWithinBounds(e.Location))
			{
				Cursor = Cursors.Hand;
			} else
            {
				Cursor = Cursors.Default;
            }

			Invalidate();
		}

		// Mouse pressed.
		private void FrmMasterMind_MouseDown(object sender, MouseEventArgs e)
        {				
			LastX = e.X;
			LastY = e.Y;

			// Assign the current x-y coordinates of the mouse pointer to be used
			// as the upper left corner of a BoundRect object.
			Point xy = new Point();
			xy.X = LastX;
			xy.Y = LastY;

			// Declare & create a bounding rectangle that represents the selectable colors.
			// Mouse coordinates will be assessed if it's within the boundary & will determine 
			// the coloumn position corresponding to the coordinate of the selectable colors.
			BoundRect bRect = new BoundRect(360, 420, 240, 40);
			Graphics g = Graphics.FromHwnd(this.Handle);
			aController.DrawSelectedColor(g, LastX, LastY, 
				aController.ActiveColor, clickStarted, withinDraggable);
			
			if (bRect.IsWithinBounds(xy))
			{
				clickStarted = true;
				withinDraggable = true;
				aController.SetValueForTheSelectedColor(xy);
			}
		}

        private void FrmMasterMind_MouseUp(object sender, MouseEventArgs e)
		{
			LastX = e.X;
			LastY = e.Y;
			clickStarted = false;
			withinDraggable = false;

			aController.MMBoard.EvaluateBoard();
			Invalidate();
		}

		private void FrmMasterMind_DragDrop(object sender, DragEventArgs e)
		{

		}
	}
	/**				End Section of Event Handling 			              **/
	////////////////////////////////////////////////////////////////////////
	///////////////////////////////////////////////////////////////////////		
	/**********************************************************************/
}