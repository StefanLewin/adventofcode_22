class RucksackReorganization
{
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
            int doubleItems = 0;
            int allBatches = 0;

            string[] rucksackContents = File.ReadAllLines(path);
            RucksackSearch search = new RucksackSearch(rucksackContents);

            doubleItems = search.GetSumOfDoubleItems();
            allBatches = search.GetSumOfBatches();

            Console.WriteLine(String.Format("Result of the double items in rucksacks: {0} points!", doubleItems));
            Console.WriteLine(String.Format("Result of the batches in rucksacks: {0} points!", allBatches));

        }
        else
        {
            Console.Error.WriteLine(String.Format("No acceptable path: {0}", path));
        }
    }
}

class RucksackSearch
{
    readonly string[] _rucksacklist;

    public RucksackSearch(string[] rucksack_list)
    {
        _rucksacklist = rucksack_list;
    }

    public int GetSumOfDoubleItems()
    {
        int prioritySum = 0;

        foreach (string rucksack in _rucksacklist)
        {
            prioritySum += GetItemPriority( GetCommonItemInCompartments(rucksack) );
        }

        return prioritySum;
    }

    public int GetSumOfBatches()
    {
        int batchSum = 0;

        for (int i = 0,  j = 3; j <= _rucksacklist.Length; i += 3, j += 3)
        {
            batchSum += GetItemPriority( FindGroupBatch(_rucksacklist[i..j]) );
        }

        return batchSum;
    }

    private char FindGroupBatch(string[] groupRucksacks)
    {
        foreach (char firstItem in groupRucksacks[0])
        {
            foreach (char secondItem in groupRucksacks[1])
            {
                foreach (char thirdItem in groupRucksacks[2])
                {
                    if (firstItem == secondItem && firstItem == thirdItem)
                    {
                        return firstItem;
                    }
                }
            }
        }
        return ' ';
    }


    private char GetCommonItemInCompartments(string rucksack)
    {
        int length = rucksack.Length;

        for (int i = 0; i < length / 2; i++)
        {
            for (int j = length / 2; j < length; j++)
            {
                if (rucksack[i].Equals(rucksack[j])) return rucksack[i];
            }
        }
        return ' ';
    }
    
    private int GetItemPriority(char item)
    {
        // a - z : 97 - 122 | A - Z : 65 - 90
        if ((int)item >= 97 && (int)item <= 122)    return (int)item - 96;
        if ((int)item >= 65 && (int)item <= 90)     return (int)item - 38;
        return -1;
    }
}