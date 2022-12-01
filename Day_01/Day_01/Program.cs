class CalorieCounting
{
    static void Main(string[] args)
    {
        //Check, for suitable number of arguments.
        switch (args.Length)
        {
            case 0:
                Console.Error.WriteLine(String.Format("No arguments given!"));
                return;
            
            case >1:
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
            string[] lines = File.ReadAllLines(path);
            int[] calorieList = GetAddedCalorieList(lines);
            GetBestElve(calorieList);
            GetTopThreeElves(calorieList);
        }
        else
        {
            Console.Error.WriteLine(String.Format("No acceptable path: {0}", path));
        }
    }



    /*
     * Gets an array of strings. 
     * Each string represents the calorie values of an food item, which is carried by an elve.
     * Elves are separated by an empty line.
     * Calculate every elves sum of carried calories. 
     * Save sum in a dedicated array of ints.
     * Output number of elve and their carried calories in the console.
     * Return array of ints 'caloriesPerElves'.
     */
    private static int[] GetAddedCalorieList(string[] calorieList)
    {
        int count = 0, elveCount = 0;

        int[] caloriesPerElve = new int[GetElveCount(calorieList)];
        Console.WriteLine(String.Format("Elve Count: {0}", caloriesPerElve.Length));
        
        for (int i = 0; i < calorieList.Length; i++)
        {
            if (!calorieList[i].Equals(""))
            {
                count += int.Parse(calorieList[i]);
                continue;
            }

            caloriesPerElve[elveCount] = count;
            Console.WriteLine(String.Format("Elve number {0} carries {1} calories.", elveCount + 1, caloriesPerElve[elveCount]));

            count = 0;
            elveCount++;
        }

        return caloriesPerElve;
    }

    /*
     * Get an array of ints. 
     * Each int represents the number of calories, an elve carries.
     * Determine biggest value in the array. 
     * Determine index of biggest value in the array. 
     * Output biggest value and index in the console. 
     * Return biggest value.
     */
    private static int GetBestElve(int[] calorielist)
    {
        int mostCalories = calorielist.Max();
        int bestElve = calorielist.ToList().IndexOf(mostCalories);

        Console.WriteLine(String.Format("\nElve number {0} has the most calories and carries {1} calories.",
                            bestElve + 1, mostCalories));
        return mostCalories;
    }

    /*
     * Get an array of ints. 
     * Each int represents the number of calories, an elve carries.
     * Sort the array and reverse it.
     * Determine the three biggest values in the array. 
     * Ouput the sum of the three biggest values in the console. 
     * Return the sum of the three biggest values.
     */
    private static int GetTopThreeElves(int[] calorieList)
    {
        Array.Sort(calorieList);
        Array.Reverse(calorieList);

        int calories = calorieList[0] + calorieList[1] + calorieList[2];

        Console.WriteLine("\nThe top three Elves carry {0} calories altogether.", calories);

        return calories;
    }

    /*
     * Gets an array of strings.
     * Counts number of emtpy strings n. 
     * Returns n+1
     */
    private static int GetElveCount(string[] calorieLines)
    {
        int count = 0;

        foreach (string item in calorieLines)
        {
            if (item.Equals(""))
            {
                count++;
            }
        }
        return ++count;
    }


}