using System.ComponentModel.DataAnnotations; // To use [Required] and [StringLength]

namespace Northwind.EntityModels;

public class Customer
{
    [Required]
    public string CustomerId { get; set; } = null!;


    [StringLength(40)]
    public string CompanyName { get; set; } = null!;


    [StringLength(30)]
    public string? City { get; set; }
}