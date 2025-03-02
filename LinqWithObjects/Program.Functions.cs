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
}