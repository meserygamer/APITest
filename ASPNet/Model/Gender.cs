using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ASPNet;

public partial class Gender
{
    public int GenderId { get; set; }

    public string GenderName { get; set; } = null!;

    [System.Text.Json.Serialization.JsonIgnore]
    [IgnoreDataMember]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
