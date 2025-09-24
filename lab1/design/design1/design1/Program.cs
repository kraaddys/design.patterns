using System;
using Xunit;
using System.IO;
using System.Collections.Generic;

public class ProductTests
{
    // Простейшая локальная "БД" + модель (можно подтянуть из основного проекта, если сделал общий класс-библиотеку)
    enum Status { Draft, Active, Archived }
    class Product { public int Id; public string Name = ""; public float Price; public Status Status; public DateTime CreatedAt; }

    class ProductDb
    {
        private readonly List<Product> _items = new();
        public void Add(Product p) => _items.Add(p);
        public List<Product> FindByName(string name)
        {
            var list = new List<Product>();
            if (string.IsNullOrWhiteSpace(name)) return list;
            foreach (var p in _items)
                if (!string.IsNullOrWhiteSpace(p.Name) &&
                    p.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                    list.Add(p);
            return list;
        }
    }

    [Flags]
    enum ProductFields { None = 0, Id = 1, Name = 2, Price = 4, Status = 8, CreatedAt = 16, All = 31 }

    static void Print(Product p, ProductFields mask)
    {
        var parts = new List<string>();
        if (mask.HasFlag(ProductFields.Id)) parts.Add($"Id: {p.Id}");
        if (mask.HasFlag(ProductFields.Name)) parts.Add($"Name: {p.Name}");
        if (mask.HasFlag(ProductFields.Price)) parts.Add($"Price: {p.Price:0.00}");
        if (mask.HasFlag(ProductFields.Status)) parts.Add($"Status: {p.Status}");
        if (mask.HasFlag(ProductFields.CreatedAt)) parts.Add($"Created: {p.CreatedAt:yyyy-MM-dd HH:mm}");
        Console.WriteLine(parts.Count > 0 ? string.Join(" | ", parts) : "<no fields selected>");
    }

    [Fact]
    public void FindByName_IsCaseInsensitive()
    {
        var db = new ProductDb();
        db.Add(new Product { Name = "Coffee" });
        db.Add(new Product { Name = "Tea" });
        db.Add(new Product { Name = "Cocoa" });

        var result = db.FindByName("TEA");

        Assert.Single(result);
        Assert.Equal("Tea", result[0].Name);
    }

    [Fact]
    public void Print_RespectsBitMask()
    {
        var p = new Product { Id = 1, Name = "Tea", Price = 3.5f, Status = Status.Draft, CreatedAt = new DateTime(2025, 1, 2, 3, 4, 0) };
        var mask = ProductFields.Name | ProductFields.Price;

        using var sw = new StringWriter();
        Console.SetOut(sw);

        Print(p, mask);
        var output = sw.ToString();

        Assert.Contains("Name:", output);
        Assert.Contains("Price:", output);

        Assert.DoesNotContain("Id:", output);
        Assert.DoesNotContain("Status:", output);
        Assert.DoesNotContain("Created:", output);
    }
}
