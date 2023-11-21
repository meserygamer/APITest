using System;
using System.Collections.Generic;

namespace ASPNet;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public string? ProductDescription { get; set; }

    public virtual ICollection<UsersProduct> UsersProducts { get; set; } = new List<UsersProduct>();
}
