using System;
using System.Collections.Generic;

namespace ASPNet;

public partial class User
{
    public int UserId { get; set; }

    public string Surname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Patronymic { get; set; }

    public int GenderId { get; set; }

    public DateTime? Birthdate { get; set; }

    public virtual Gender Gender { get; set; } = null!;

    public virtual ICollection<UsersProduct> UsersProducts { get; set; } = new List<UsersProduct>();
}
