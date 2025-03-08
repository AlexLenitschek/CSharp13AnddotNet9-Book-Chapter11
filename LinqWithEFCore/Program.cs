ConfigureConsole();
//FilterAndSort();
//JoinCategoriesAndProducts();
//GroupJoinCategoriesAndProducts();
//ProductsLookup();
//AggregateProducts();
//PagingProducts();
//FilterAndSortWithOwnExtension(); // Same output as FilterAndSort(); because method doesn't modify the sequence.
//CustomExtensionMethods();
//OutputProductsAsXml();
ProcessSettings();


#region Information on LINQ extensions and lambda expressions vs. query comprehension syntax
//Consider the following array of string values:
//```cs
//string[] names = new[] { "Michael", "Pam", "Jim", "Dwight",
//  "Angela", "Kevin", "Toby", "Creed" };
//```

//To filter and sort the names, you could use extension methods and lambda expressions, as shown in the following code:
//```cs
//var query = names
//  .Where(name => name.Length > 4)
//  .OrderBy(name => name.Length)
//  .ThenBy(name => name);
//```

//Or you could achieve the same results by using query comprehension syntax, as shown in the following code:
//```cs
//var query = from name in names
//  where name.Length > 4
//  orderby name.Length, name
//  select name;
//```
#endregion
