using System;
using System.Collections.Generic;

namespace ASPNet;

public partial class UsersProduct
{
    public int RecordId { get; set; }

    public int UserId { get; set; }

    public int ProductId { get; set; }

    public virtual Product? Product { get; set; } = null!;

    public virtual User? User { get; set; } = null!;
}
