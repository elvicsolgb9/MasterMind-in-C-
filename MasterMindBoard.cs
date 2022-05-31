namespace MasterMindCS
{
    public class MasterMindBoard
    {
        /**----------------------------------------------------------------------
        // Implementation of the MasterMind Game Board // Data or Model Class
           ---------------------------------------------------------------------*/
        private int currentRowPos;

        private int[] hidden_colors_holder = new int[4];

        private int[,] breaker_colors_holder = new int[10, 4];
        private int[,] keycolors_holder = new int[10, 4];

        private int[] tempo_breaker_colors_holder = new int[4];
        private int[] tempo_keycolors_holder = new int[4];

        private readonly int BLACK = 0, WHITE = 1;
        private readonly int LOST = 0, WIN = 1;

        private int guessAttempts;
        private bool win_status;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /* setter & getter Properties that access the private variables of this class
        ****************************************************************************************************
        */
        public int Win
        {
            get { return WIN; }
        }

        public int Lost
        {
            get { return LOST; }
        }

        public int[,] BreakColorsHolder
        {
            get { return breaker_colors_holder; }
            set { breaker_colors_holder = value; }
        }

        public int[,] KeyColorsHolder
        {
            get { return keycolors_holder; }
            set { keycolors_holder = value; }
        }

        public int[] HiddenColorsHolder
        {
            get { return hidden_colors_holder; }
            set { hidden_colors_holder = value; }
        }

        // -- Constructor Initialization --  
        public MasterMindBoard()
        {
            int i, j;

            // Start the current row position from the bottom part of the board.
            currentRowPos = 9;
            guessAttempts = 0;

            SetHiddenColors(); // Generate the hidden colors code with random color values.

            // Initialize the color holders on the board to empty contents.
            for (i = 0; i <= 9; i++)
                for (j = 0; j <= 3; j++)
                {
                    this.BreakColorsHolder[i, j] = -1;
                    this.KeyColorsHolder[i, j] = -1;

                    // For test purpose
                    //Random randGen = new();
                    //this.BreakColorsHolder[i, j] = (int)(randGen.Next(6));
                    //this.KeyColorsHolder[i, j] = (int)(randGen.Next(2));
                }

            // Initialize the two arrays to be used for comparing or evaluation.
            // The value -1 means the holder-array element is empty.
            for (i = 0; i <= 3; i++)
            {
                tempo_breaker_colors_holder[i] = -1;
                tempo_keycolors_holder[i] = -1;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /* This section contains the methods of this class that performs data manipulation for the game.
        ****************************************************************************************************
        */
        public void EvaluateBoard()
        {
            /**	Check if all the temporary breaker-color holes, 
            used for comparison, were filled out; -1 means it's empty	*/
            if (tempo_breaker_colors_holder[0] == -1 || tempo_breaker_colors_holder[1] == -1 ||
                tempo_breaker_colors_holder[2] == -1 || tempo_breaker_colors_holder[3] == -1)
            {
                return; /*Don't do anything yet until all colour holes were filled out*/
            }
            else
            {
                EvaluateTheGuessColors(); // Execute the main algorithm for comparing the guess attempt against the hidden code.
            }

            DetermineRowPos(); // Move the current active row upward (which is decrease the y-axis)
            ResetBreakerColorsHolder();
        }

        /** Compare the positional values in the temporary tempo_breaker_colors_holder 
        & temporary tempo_keycolors_holder arrays to determine what colour keys will be 
        inserted into the key_colors holder. This is the main algorithm that calculates 
        the colour values guessed against the hidden colours code */
        public void EvaluateTheGuessColors()
        {
            int i, j;
            int index = 0;
            int[] temp_hidden_code = new int[4];

            /* Assign or copy the element values of the hidden_colors_holder array to another array(temp_hidden_code[]) 
            so as not to change the original colour value of the original hidden_colors_holder[] which must remain the same
            throughout the game & use instead the copy temp_hidden_code[] for comparison. */
            for (i = 0; i < temp_hidden_code.Length; i++)
            {
                temp_hidden_code[i] = hidden_colors_holder[i];
            }

            //initialize the temporary keycolors_holder to empty values
            for (i = 0; i < tempo_keycolors_holder.Length; i++)
            {
                tempo_keycolors_holder[i] = -1;
            }

            /* This is the algorithm that inserts the BLACK color value to the temporary key colors holder 
            which will be inserted to the main keycolors_holder later */
            
            for (i = 0; i < tempo_breaker_colors_holder.Length; i++)
            {
                if ((tempo_breaker_colors_holder[i] == temp_hidden_code[i]) &&
                    (temp_hidden_code[i] != -1) && (tempo_breaker_colors_holder[i] != -1))
                {
                    tempo_keycolors_holder[index] = BLACK;
                    index++;
                    tempo_breaker_colors_holder[i] = -1;
                    temp_hidden_code[i] = -1;
                }
            }
            
            /* This is the algorithm that inserts the WHITE color value to the temporary key colors holder 
            which will be inserted to the main keycolors_holder later */
           for (i = 0; i < hidden_colors_holder.Length; i++)
            {
                for (j = 0; j < hidden_colors_holder.Length; j++)
                {
                    if ((tempo_breaker_colors_holder[i] == temp_hidden_code[j]) &&
                        (i != j) && (tempo_breaker_colors_holder[i] != temp_hidden_code[i]) &&
                        ((temp_hidden_code[j] != -1) && (tempo_breaker_colors_holder[i] != -1)))
                    {
                        tempo_keycolors_holder[index] = WHITE;
                        index++;
                        temp_hidden_code[j] = -1;
                        tempo_breaker_colors_holder[i] = -1;
                    }
                }
            }
           
            // Insert the BLACK or WHITE colors into the keycolors_holder array for the scoreboard.
            // currentRowPos is a member variable  that tracks the current active row position to fill pegs.
            SetKeyColors(tempo_keycolors_holder, GetCurrentRowPos());

            DetermineGameResult(tempo_keycolors_holder);
        }

        // This is called after Board Evaluation was done for a specific row,
        public void ResetBreakerColorsHolder()
        {
            for (int i = 0; i <= 3; i++)
                tempo_breaker_colors_holder[i] = -1;
        }

        public void DetermineGameResult(int[] key_array)
        {
            if (key_array[0] == BLACK && key_array[1] == BLACK && key_array[2] == BLACK && key_array[3] == BLACK)
            {
                SetWinStatus(true);
                // Set the active row position in such a way that no more guess attempts can be made.
                DetermineRowPos();
            }
            else
            {
                SetGuessAttempts(); // Increment the guessAttempt value by 1, max is 10.
            }
        }

        // This method is called when a peg hole was filled out by the guessing player at the click of the mouse
        public void SetTempoBreakerColors(int index, int iColor)
        {
            tempo_breaker_colors_holder[index] = iColor;
        }

        public int[] GetTempoBreakerColors()
        {
            return tempo_breaker_colors_holder;
        }

        public void SetBreakerColors(int[] breakColorsArray, int colPos, int rowPos)
        {
            // This is to ensure that an OutOfIndexException will be prevented
            // once the current row position is set to -1 after the hidden code
            // was correctly guessed by the player. No more attempts can be made.
            if (currentRowPos < 0)
                return;
            else
                // Assign the values in the temporary breaker array to the mainboard array.
                breaker_colors_holder[rowPos, colPos] = breakColorsArray[colPos];
        }

        public int[,] GetBreakerColors()
        {
            return breaker_colors_holder;
        }

        public void SetKeyColors(int[] keyColorsArray, int rowPos)
        {
            // This is to ensure that an OutOfIndexException will be prevented
            // once the current row position is set to -1 after the hidden code
            // was correctly guessed by the player. No more attempts can be made.
            if (rowPos < 0)
                return;
            else
            {
                //Assign the values in the temporary keyholder array to the main keyholder array.
                for (int i = 0; i <= 3; i++)
                {
                    keycolors_holder[rowPos, i] = keyColorsArray[i];
                }
            }
        }

        public int[,] GetKeyColors()
        {
            return keycolors_holder;
        }

        public void SetHiddenColors()
        {
            // Insert random colors out of the available ones to hidden colors array
            Random randGen = new();
            for (int i = 0; i <= 3; i++)
                HiddenColorsHolder[i] = (int)(randGen.Next(6));
        }

        public int[] GetHiddenColors()
        {
            return hidden_colors_holder;
        }

        public void DetermineRowPos()
        {
            if (win_status == true)
                currentRowPos = -1; // Disable the active row so no more guess can be attempted.
            else
                --currentRowPos; // Just decrease the y-axis length or move the active row up.
        }

        public int GetCurrentRowPos()
        {
            return currentRowPos;
        }

        public void SetGuessAttempts()
        {
            guessAttempts++;
        }

        public int GetGuessAttempts()
        {
            return guessAttempts;
        }

        public void SetWinStatus(bool bstatus)
        {
            win_status = bstatus;
        }

        public bool GetWinStatus()
        {
            return win_status;
        }

        /* End of MasterMind Board class definitions.
        **********************************************/
    }
}