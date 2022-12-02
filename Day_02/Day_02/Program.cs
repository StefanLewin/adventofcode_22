using System.Numerics;

class RockPaperScissors
{
    static string[]? strategy;

    static void Main(string[] args)
    {
        //Check, for suitable number of arguments.
        switch (args.Length)
        {
            case 0:
                Console.Error.WriteLine(String.Format("No arguments given!"));
                return;

            case > 1:
                Console.Error.WriteLine(String.Format("Too many arguments!"));
                return;

            default:
                break;
        }

        string path = (args[0] != null) ? Path.GetFullPath(args[0]) : "";
        Console.WriteLine(String.Format("Trying path: {0}\n", path));

        //Check, if given path leads to an actual file.
        if (File.Exists(path))
        {
            strategy = File.ReadAllLines(path);
            Game game = new Game(strategy);
            int result = game.CalculateGamePoints();
            Console.WriteLine(String.Format("Result of the Strategy Plan: {0} points!", result));

            game.ActivateCorrectDecryption();
            result = game.CalculateGamePoints();
            Console.WriteLine(String.Format("Result of the Correct Decryption: {0} points!", result));
        }
        else
        {
            Console.Error.WriteLine(String.Format("No acceptable path: {0}", path));
        }
    }
}

/*
 *      [- OPONENT -]      |         [- PLAYER -]               |        [- OUTCOMES -]
 *                         |                                    |
 * A = Rock                |     X = Rock         | 1 Point     |    Lose             | 0 Points
 * B = Paper               |     Y = Paper        | 2 Points    |    Draw             | 3 Points
 * C = Scissors            |     Z = Scissors     | 3 Points    |    Win              | 6 Points
 * 
 */
class Game
{
    #region MEMBER VARIABLES
    private string[] oponentMoves = { "A", "B", "C" };
    private string[] playerMoves = { "X", "Y", "Z" };
    private string[] _strategy;
    private bool correctDecryption;
    #endregion

    #region PUBLIC METHODS
    public Game(string[] strategy)
    {
        _strategy = strategy;
        correctDecryption = false;

    }
    public int CalculateGamePoints()
    {
        int points = 0;
        foreach (string line in _strategy)
        {
            string[] round = line.Split(' ');
            int moveOponent = oponentMoves.ToList().IndexOf(round[0]) + 1;
            int movePlayer = -1;

            movePlayer = correctDecryption ? DeterminePlayerMove(moveOponent, playerMoves.ToList().IndexOf(round[1]) + 1) :
                                                movePlayer = playerMoves.ToList().IndexOf(round[1]) + 1;

            points += CalculateRoundPoints(moveOponent, movePlayer);
        }

        return points;
    }

    public void ActivateCorrectDecryption()
    {
        correctDecryption = true;
    }
    #endregion

    #region PRIVATE VARIABLES
    private int CalculateRoundPoints(int oponent, int player)
    {
        int points = player;
        int gameResult = Math.Abs(oponent - player);

        /*
         * Rock     = 1
         * Paper    = 2
         * Scissors = 3
         * 
         * 1 beats 3    1 - 3 = -2
         * 2 beats 1    2 - 1 = 1
         * 3 beats 2    3 - 2 = 1
         * 
         * Draw results in 0
         */

        switch (gameResult)
        {
            case 0:
                points += 3;
                break;

            case 1:
                points += oponent > player ? 0 : 6;
                break;

            case 2:
                points += oponent > player ? 6 : 0;
                break;
        }
        return points;
    }

    /*
     * Simulate a game with a for loop and compare the outcome with the specified outcome. 
     * If Outcomes match, return the proper player move.
     */
    private int DeterminePlayerMove(int oponent, int specifiedOutcome)
    {

        for (int simulatedPlayer = 1; simulatedPlayer <= 3; simulatedPlayer++)
        {
            int result = Math.Abs(oponent - simulatedPlayer);
            int gameResult = -1; //  | -1 = Error | 1 = Lose | 2 = Draw | 3 = Win | 

            switch (result)
            {
                case 0: //Draw
                    gameResult = 2;
                    break;

                case 1: // Bigger Number wins
                    gameResult = (oponent > simulatedPlayer) ? 1 : 3;
                    break;

                case 2: //Little Number wins
                    gameResult = (oponent > simulatedPlayer) ? 3 : 1;
                    break;
            }

            if (gameResult == specifiedOutcome)
            {
                return simulatedPlayer;
            }
        }

        return -1;
    }

    #endregion

}