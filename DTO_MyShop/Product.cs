using System;
using System.Collections.Generic;

namespace DTO_MyShop;

public partial class Product
{
    public string Id { get; set; } = null!;

    public string? ProductName { get; set; }

    public string? Author { get; set; }

    public int? PublishYear { get; set; }

    public string? Publisher { get; set; }

    public int? CostPrice { get; set; }

    public int? SellingPrice { get; set; }

    public string? CategoryId { get; set; }

    public int? Quantity { get; set; }

    public string? ImagePath { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
