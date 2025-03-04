using Northwind.EntityModels; // To use NorthwindDb, Category and Product.
using Microsoft.EntityFrameworkCore; // To use DbSet<T>.

partial class Program
{
    private static void FilterAndSort()
    {
        SectionTitle("Filter and Sort");

        using NorthwindDb db = new();

        DbSet<Product> allProducts = db.Products;

        IQueryable<Product> filteredProducts = allProducts
            .Where(product => product.UnitPrice < 10M );

        IOrderedQueryable<Product> sortedAndFilteredProducts = filteredProducts
            .OrderByDescending(product => product.UnitPrice);

        WriteLine("Products that cost less than 10$:");

        //WriteLine(sortedAndFilteredProducts.ToQueryString());
        //foreach (Product p in sortedAndFilteredProducts)
        //{
        //    WriteLine("{0}: {1} costs {2:$#,##0.00}", p.ProductId, p.ProductName, p.UnitPrice);
        //}

        #region Projection using Select
        var projectedProducts = sortedAndFilteredProducts
            .Select(product => new
            {
                product.ProductId,
                product.ProductName,
                product.UnitPrice
            });

        WriteLine(projectedProducts.ToQueryString());
        foreach (var p in projectedProducts)
        {
            WriteLine("{0}: {1} costs {2:$#,##0.00}", p.ProductId, p.ProductName, p.UnitPrice);
        }
        #endregion

        WriteLine();
    }


    #region Joining Grouping and lookups.

    #region Join
    private static void JoinCategoriesAndProducts()
    {
        SectionTitle("Join Categories and Products");

        using NorthwindDb db = new();

        // Join every product to its category to return 77 matches.
        var queryJoin = db.Categories.Join(
            inner: db.Products,
            outerKeySelector: category => category.CategoryId,
            innerKeySelector: product => product.CategoryId,
            resultSelector: (c, p) => new { c.CategoryName, p.ProductName, p.ProductId})
            .OrderBy(cp => cp.CategoryName);

        foreach (var p in queryJoin)
        {
            WriteLine($"{p.ProductId}: {p.ProductName} in {p.CategoryName}.");
        }
    }
    #endregion

    #region GroupJoin
    private static void GroupJoinCategoriesAndProducts()
    {
        SectionTitle("GroupJoin Categories and Products");

        using NorthwindDb db = new();

        // Group all products by their category to return 8 matches.
        var queryGroup = db.Categories.AsEnumerable().GroupJoin(
            inner: db.Products,
            outerKeySelector: category => category.CategoryId,
            innerKeySelector: product => product.CategoryId,
            resultSelector: (c, matchingProducts) => new {
                c.CategoryName,
                Products = matchingProducts.OrderBy(p => p.ProductName)
            });

        foreach (var c in queryGroup)
        {
            WriteLine($"{c.CategoryName} has {c.Products.Count()} products.");

            foreach (var product in c.Products)
            {
                WriteLine($"  {product.ProductName}");
            }

        }
    }
    #endregion

    #region Lookups
    private static void ProductsLookup()
    {
        SectionTitle("Products Lookup");

        using NorthwindDb db = new();

        // Join all products to their category to return 77 matches.
        var productQuery = db.Categories.Join(
            inner: db.Products,
            outerKeySelector: category => category.CategoryId,
            innerKeySelector: product => product.CategoryId,
            resultSelector: (c, p) => new { 
                c.CategoryName, 
                Product = p }
            );

        ILookup<string, Product> productLookup = productQuery.ToLookup(
            keySelector: cp => cp.CategoryName,
            elementSelector: cp => cp.Product);

        foreach (IGrouping<string, Product> group in productLookup)
        {
            // Key is Beverages, Condiments, etc.
            WriteLine($"{group.Key} has {group.Count()} products.");

            foreach (Product product in group)
            {
                WriteLine($"  {product.ProductName}");
            }
        }

        Write("Enter a category name: ");
        string categoryName = ReadLine()!;
        WriteLine();
        WriteLine($"Products in {categoryName}:");
        IEnumerable<Product> productsInCategory = productLookup[categoryName];
        foreach (Product product in productsInCategory)
        {
            WriteLine($"  {product.ProductName}");
        }
    }

    #endregion


    #endregion

}