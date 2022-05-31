namespace MasterMindCS
{
	public class ActionController
	{
		private MasterMindBoard mmBoard = new();

		private int activeColor;

		private static readonly Brush[] pegColors = {
			Brushes.Red, Brushes.Brown,
			Brushes.Yellow, Brushes.Green,
			Brushes.Blue, Brushes.Magenta
		};

		private static readonly Brush[] keyColors = {
			Brushes.Black, Brushes.White
		};

		/**********************************************************************/
		/* ActionController Properties
		**********************************************************************/
		public MasterMindBoard MMBoard
        {
			get { return mmBoard; }
        }

		public int[,] BreakerColors
        {
			get { return mmBoard.BreakColorsHolder;  }
			set { mmBoard.BreakColorsHolder = value; }
        }

		public int[] TempoBreaker
        {
			get { return mmBoard.GetTempoBreakerColors(); }
        }

		public int ActiveColor
        {
			get { return activeColor; }
        }

		/***
		/*  Perform some calculations to determine which coloumn 
            the mouse pointer was on while it's being dragged to determine
            which coloumn in the temporary breaker's array the color value should be placed. 
        */
		public void SetColorOnTempoBreaker(Point point, int rowPos)
		{
			int startX = 160;
			int startY = 60;

			int colSpan = startX + 40;
			int rowSpan = (rowPos * 40 + startY);

			//Translate logical coordinate 
			//into Mastermind board array units.    
			if ((point.X > startX) && (point.X < colSpan)
					&& (point.Y > rowSpan) && (point.Y < rowSpan + 40))
			{
				mmBoard.SetTempoBreakerColors(0, activeColor);
				return;
			}
			else if ((point.X > colSpan) && (point.X < colSpan + 40)
					&& (point.Y > rowSpan) && (point.Y < rowSpan + 40))
			{
				mmBoard.SetTempoBreakerColors(1, activeColor);
				return;
			}
			else if ((point.X > colSpan + 40) && (point.X < colSpan + 40 * 2)
					&& (point.Y > rowSpan) && (point.Y < rowSpan + 40))
			{
				mmBoard.SetTempoBreakerColors(2, activeColor);
				return;
			}
			else if ((point.X > colSpan + 40 * 2) && (point.X < colSpan + 40 * 3)
					&& (point.Y > rowSpan) && (point.Y < rowSpan + 40))
			{
				mmBoard.SetTempoBreakerColors(3, activeColor);
				return;
			}
		}

		public void SetValueForTheSelectedColor(Point point)
		{
			int startX = 360;
			int startY = 420;

			int colSpan = (startX + 40);
			int rowSpan = (startY + 40);

			//Translate board coordinates 
			//into Mastermind board array units.    
			if ((point.X > startX) && (point.X < colSpan)
					&& (point.Y > startY) && (point.Y < rowSpan))
			{
				activeColor = 0;
				return;
			}
			else if ((point.X > startX) && (point.X < colSpan + 40 * 1)
					&& (point.Y > startY) && (point.Y < rowSpan))
			{
				activeColor = 1;
				return;
			}
			else if ((point.X > startX) && (point.X < colSpan + 40 * 2)
					&& (point.Y > startY) && (point.Y < rowSpan))
			{
				activeColor = 2;
				return;
			}
			else if ((point.X > startX) && (point.X < colSpan + 40 * 3)
					&& (point.Y > startY) && (point.Y < rowSpan))
			{
				activeColor = 3;
				return;
			}

			else if ((point.X > startX) && (point.X < colSpan + 40 * 4)
					&& (point.Y > startY) && (point.Y < rowSpan))
			{
				activeColor = 4;
				return;
			}

			else if ((point.X > startX) && (point.X < colSpan + 40 * 5)
				   && (point.Y > startY) && (point.Y < rowSpan))
			{

				activeColor = 5;
				return;
			}
		}
		/**********************************************************************/

		public void DrawBoundaries(Graphics g, BoundRect rect, int numOfBoxes)
		{
			int x = rect.GetBoundRectX();
			int y = rect.GetBoundRectY();
			int width = rect.GetBoundRectWidth();
			int height = rect.GetBoundRectHeight();

			Pen p = Pens.Blue;

			for (int i = 0; i <= numOfBoxes; i++)
			{
				g.DrawRectangle(p, x, y, width * i, height);
			}
		}

		public void DrawmmBoardArea(Graphics g)
		{
			Pen p = Pens.BlueViolet;
			g.DrawRectangle(p, 30, 15, 300, 460);
		}

		public void DrawSelectedColor(Graphics g, int x, int y, int selectedColor, bool clickStarted, bool withinDraggable)
		{
			Brush brushColor = pegColors[selectedColor];

			int wxh = 40;
			if ((clickStarted) && (withinDraggable))
			{
				g.FillEllipse(brushColor, (x - (wxh/2)), (y - (wxh/2)), wxh, wxh);
			}
			else
				return;
		}

		public void DrawSelectableColors(Graphics g, int x, int y, int width, int height, int iPegColor)
		{
			int StartX = 360;
			int StartY = 380;

            Brush brushColor = pegColors[iPegColor];
			g.FillEllipse(brushColor, (StartX + x), (StartY + y), width, height);
		}

		public void DrawColorPegs(Graphics g, int x, int y, int width, int height, int iPegColor)
		{
			int StartX = 160;
			int StartY = 60;

			/***	Draw colored circles contained in the breaker colors array
			representing the color pegs filled in the breaker holes		***/
			Brush brushcolor = pegColors[iPegColor];
			g.FillEllipse(brushcolor, (StartX + x), (StartY + y), width, height);
		}

		public void DrawKeyColorPegs(Graphics g, int iKeyColor, int x, int y, int width, int height)
		{
			int StartX = 40;
			int StartY = 70;

			/***	Draw colored circles contained in the key colors array
			representing the color pegs filled in the key holes		***/
			Brush brushcolor = keyColors[iKeyColor];
			g.FillEllipse(brushcolor, (StartX + x), (StartY + y), width, height);
		}

		public void DisplayGameResults(Graphics g, int status)
		{
			{
				int i;
				int wxh = 40; // Set the width & height of the color circles.
				int[] hiddenColors = MMBoard.GetHiddenColors();

				switch (status)
				{
					case 0:	// Player Lost
						g.DrawString("Sorry, you didn't get it right. Try again !!!",
							new Font("Times New Roman", 12),
							Brushes.Red, 350, 90);
						for (i = 0; i <= 3; i++)
							DrawHiddenColors(g, i * 40, 20,
								wxh, wxh, hiddenColors[i]);
						g.DrawString("Correct Colors: ", new Font("Times New Roman", 12),
							Brushes.Red, 35, 30);
						MMBoard.DetermineRowPos();
						//m_bttnNew.EnableWindow(TRUE);
						//m_bttnQuit.EnableWindow(TRUE);
						break;
					case 1:	// Player Won
						g.DrawString("Well done, you got it right. Try again !!!",
							new Font("Times New Roman", 12),
							Brushes.Blue, 350, 90);
						for (i = 0; i <= 3; i++)
							DrawHiddenColors(g, i * 40, 20,
								wxh, wxh, hiddenColors[i]);
						break;
				}
			}
		}

		public void DrawCurrentRow(Graphics g, int x, int y, int width, int height)
		{
			int StartX = 160;
			int StartY = 60;

			// Draw empty circles to represent the current active row position.
			if (mmBoard.GetCurrentRowPos() >= 0 && mmBoard.GetCurrentRowPos() != -1)
			{
				Pen pen = Pens.DarkOrange;
				g.DrawEllipse(pen, StartX + x, StartY + y, width, height);
			}
			else return;
		}

		public void DrawHiddenColors(Graphics g, int x, int y, int width, int height, int iColor)
		{
			int StartX = 160;
			int StartY = 0;

			// Draw the hidden colors contained in the hidden colors array if guess isn't successful.
			Brush brushColor = pegColors[iColor];
			g.FillEllipse(brushColor, (StartX + x), (StartY + y), width, height);
		}

	}
}