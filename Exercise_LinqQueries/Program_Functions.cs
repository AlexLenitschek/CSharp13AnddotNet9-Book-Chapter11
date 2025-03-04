using Northwind.EntityModels; // To use NorthwindDb, Category and Product.
using Microsoft.EntityFrameworkCore; // To use DbSet<T>.
partial class Program
{
    private static void customersInCity()
    {
        SectionTitle("Customers in City");

        using NorthwindDb db = new();


        IQueryable<string?> distinctCities = db.Customers.
            Select(c => c.City).Distinct();

        WriteLine("A list of cities that at least one customer resides in:");
        WriteLine($"{string.Join(", ", distinctCities)}");
        WriteLine();


        Write("Enter the name of a city: ");
        string inputCity = ReadLine()!;

        IQueryable<Customer> customersInSpecificCity = db.Customers
            .Where(customer => customer.City == inputCity);

        var filteredCustomers = customersInSpecificCity
            .Select(customer => new
            {
                customer.CompanyName,
                customer.City
            });

        WriteLine($"There are {filteredCustomers.Count()} customers in {inputCity}:");

        foreach (var customer in filteredCustomers)
        {
            WriteLine($"  {customer.CompanyName}");
        }
    }
}
