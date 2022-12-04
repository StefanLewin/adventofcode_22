class CampCleanUp
{
    static string[]? sectionAssignments;

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
            sectionAssignments = File.ReadAllLines(path);
            AssignmentCheck check = new AssignmentCheck(sectionAssignments);
            Console.WriteLine(String.Format("Number of assignments with one range fully containing the other: {0}", check.checkAllAssignmentsforContainment()));
            Console.WriteLine(String.Format("Number of assignments with overlap: {0}", check.checkAllAssignmentsforOverlap()));
        }
        else
        {
            Console.Error.WriteLine(String.Format("No acceptable path: {0}", path));
        }
    }
}

class AssignmentCheck{

    string[] _assignments;
    public AssignmentCheck(string[] assignments)
    {
        _assignments = assignments;
    }

    public int checkAllAssignmentsforContainment()
    {
        int result = 0;

        foreach (string item in _assignments)
            if (fullConatinmentCheck(item)) result++;

        return result;
    }

    public int checkAllAssignmentsforOverlap()
    {
        int result = 0;

        foreach (string item in _assignments)
            if (overlapCheck(item)) result++;

        return result;
    }

    private bool fullConatinmentCheck(string assignmentPair)
    {
        int[] ranges = Array.ConvertAll(assignmentPair.Split('-',','), int.Parse);

        if ( (ranges[0] >= ranges[2] && ranges[1] <= ranges[3]) || (ranges[2] >= ranges[0] && ranges[3] <= ranges[1]) ) 
            return true;

        return false;
    }

    private bool overlapCheck(string assignmentPair)
    {
        int[] ranges = Array.ConvertAll(assignmentPair.Split('-', ','), int.Parse);

        if (ranges[2] > ranges[1] || ranges[2] < ranges[1] && ranges[3] < ranges[0])
            return false;

        return true;
    }

}