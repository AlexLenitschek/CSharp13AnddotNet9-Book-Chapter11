partial class Program
{
    private static void DefferedExecution(string[] names)
    {
        SectionTitle("Deffered Execution");

        // Question: Which names end with an M?
        // (using a LINQ extension method)
        var query1 = names.Where(name => name.EndsWith("m"));

        // Question: Which names end with an M?
        // (using a LINQ query comprehension syntax)
        var query2 = from name in names where name.EndsWith("m") select name;


        // Answer returned as an array of strings containing Pam and Jim.
        string[] result1 = query1.ToArray();

        // Answer returned as a list of strings containing Pam and Jim.
        List<string> result2 = query2.ToList();

        // Answer returned as we enumerate over the results.
        foreach (string name in query1)
        {
            WriteLine(name); // Outputs Pam.
            names[2] = "Jimmy"; // Change the name Jim to Jimmy.
            // On the second iteration Jimmy does not end with an "m" so it does not get output.
        }
    }

    private static void FilteringUsingWhere(string[] names)
    {
        SectionTitle("Filtering entities using Where");

        //var query = names.Where(new Func<string, bool>(NameLongerThanFour));
        //var query = names.Where(NameLongerThanFour); // C# compiler instantiates the delegate for you.
        //var query = names.Where(name => name.Length > 4); // Using a lambda expression.
        var query = names   // var could be changed for its real type which is IOrderedEnumerable<string>. No performancegain though.
            .Where(name => name.Length > 4)
            .OrderBy(name => name.Length)
            .ThenBy(name => name);
        foreach (string item in query)
        {
            WriteLine(item);
        }
    }

    static void FilterinByType()
    {
        SectionTitle("Filtering by type");

        List<Exception> exceptions = new()
        {
            new ArgumentException(),
            new SystemException(),
            new IndexOutOfRangeException(),
            new InvalidOperationException(),
            new NullReferenceException(),
            new InvalidCastException(),
            new OverflowException(),
            new DivideByZeroException(),
            new ApplicationException()
        };

        IEnumerable<ArithmeticException> arithmeticExceptionQuery = exceptions.OfType<ArithmeticException>();

        foreach (ArithmeticException exception in arithmeticExceptionQuery)
        {
            WriteLine(exception);
        }
    }

    static bool NameLongerThanFour(string name)
    {
        // Return true if the name is longer than four characters.
        return name.Length > 4;
    }

    #region Working with bags and sets
    static void Output(IEnumerable<string> cohort, string description = "")
    { 
        if (!string.IsNullOrEmpty(description))
        {
            WriteLine(description);
        }
        Write(" ");
        WriteLine(string.Join(", ", cohort.ToArray()));
        WriteLine();
    }

    static void WorkingWithSets()
    {
        string[] cohort1 = { "Racherl", "Gareth", "Jonathan", "George" };
        string[] cohort2 = { "Jack", "Stephen", "Daniel", "Jack", "Jared" };
        string[] cohort3 = { "Declan", "Jack", "Jack", "Jasmine", "Conor" };

        SectionTitle("The cohorts");

        Output(cohort1, "Cohort 1");
        Output(cohort2, "Cohort 2");
        Output(cohort3, "Cohort 3");

        SectionTitle("Set operations");

        Output(cohort2.Distinct(), "cohort2.Distinct()");
        Output(cohort2.DistinctBy(name => name.Substring(0, 2)), "cohort2.DistinctBy(name => name.Substring(0, 2))");
        Output(cohort2.Union(cohort3), "cohort2.Union(cohort3)");
        Output(cohort2.Concat(cohort3), "cohort2.Concat(cohort3)");
        Output(cohort2.Intersect(cohort3), "cohort2.Intersect(cohort3)");
        Output(cohort2.Except(cohort3), "cohort2.Except(cohort3)");
        Output(cohort1.Zip(cohort2, (c1, c2) => $"{c1} matched with {c2}"), "cohort1.Zip(cohort2)");
    }
    #endregion

    #region Getting the Index as well as items
    static void WorkingWithIndices()
    {
        string[] theSeven = { "Homelander", "Black Noir", "The Deep", "A-Train", "Queen Mave", "Starlight", "Stormfront" };

        SectionTitle("Working with indices (old)");

        foreach (var (item, index) in theSeven.Select((item, index) => (item, index))) // In the Select method you need to specify item first and then index.
        {
            WriteLine($"{index}: {item}");
        }

        SectionTitle("Working with indices (new)");

        foreach (var (index, item) in theSeven.Index()) // In the Index method you need to specify index first and then item.
        {
            WriteLine($"{index}: {item}");
        }
    }


    #endregion
}