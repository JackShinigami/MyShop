using System;
using System.Collections.Generic;

namespace DTO_MyShop;

public partial class Order
{
    public string Id { get; set; } = null!;

    public DateTime? OrderDate { get; set; }

    public string? CustomerId { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
