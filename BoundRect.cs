using System.Drawing;

namespace MasterMindCS
{
/***
This is the class that will assess the position of the mouse pointer if it's within the bounds of 
the board area. It will also determine where the position of the pointer is at the moment its methods 
are called (isWithinBounds & setColoumn()) respectively. The methods receive a Point object that 
represent the coordinates of the last pointer position it will be passed to by a mouse event.
*/
    public class BoundRect
    {
        private int x = 0;
        private int y = 0;
        private int width;
        private int height;

        private int colPos;
        private bool boundStatus = false;

        public BoundRect()
        {
            //this.x = 0;
            //this.y = 0;
            //this.width = 0;
            //this.height = 0;
        }

        public BoundRect(int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        public bool IsWithinBounds(Point point)
        {
            int leftBound = this.x;
            int rightBound = this.x + this.width;
            int upperBound = this.y;
            int lowerBound = this.y + this.height;

            // Ignore mouse click if above or below 
            // & beyond left or beyond right bounds 
            // of proper board coordinates
            if ((point.X < leftBound) || (point.X > rightBound))
                return false;
            else if ((point.Y < upperBound) || (point.Y > lowerBound))
                return false;
            else
                return true;
        }

        /*  Perform some calculations to determine which coloumn 
            the mouse pointer was on when it's released to determine
            which coloumn in the temporary breaker's array the color value should be placed. 
        */
        public void SetColoumn(Point point, int rowPos)
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
                colPos = 0;
            }
            else if ((point.X > colSpan) && (point.X < colSpan + 40)
                    && (point.Y > rowSpan) && (point.Y < rowSpan + 40))
            {
                colPos = 1;
            }
            else if ((point.X > colSpan + 40) && (point.X < colSpan + 40 * 2)
                    && (point.Y > rowSpan) && (point.Y < rowSpan + 40))
            {
                colPos = 2;
            }
            else if ((point.X > colSpan + 40 * 2) && (point.X < colSpan + 40 * 3)
                    && (point.Y > rowSpan) && (point.Y < rowSpan + 40))
            {
                colPos = 3;
            }
        }

        public void SetBoundStatus(BoundRect bRect, bool bStatus)
        {
            bRect.boundStatus = bStatus;
            bRect.x = 0;
            bRect.y = 0;
            bRect.width = 0;
            bRect.height = 0;
        }

        public bool GetBoundStatus()
        {
            return boundStatus;
        }

        public int GetColoumn()
        {
            return colPos;
        }

        public int GetBoundRectX()
        {
            return this.x;
        }

        public int GetBoundRectY()
        {
            return this.y;
        }

        public int GetBoundRectWidth()
        {
            return this.width;
        }

        public int GetBoundRectHeight()
        {
            return this.height;
        }
    }
}