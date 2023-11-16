using System;
using System.Collections.Generic;

namespace DTO_MyShop;

public partial class Category
{
    public string Id { get; set; } = null!;

    public string? CategoryName { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
