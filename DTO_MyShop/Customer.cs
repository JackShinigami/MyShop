using System;
using System.Collections.Generic;

namespace DTO_MyShop;

public partial class Customer
{
    public string Id { get; set; } = null!;

    public string? CustomerName { get; set; }

    public string? TelephoneNumber { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
