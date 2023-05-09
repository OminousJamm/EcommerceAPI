using System;
using System.Collections.Generic;

namespace E_CommerceAPI.Models;

public partial class User
{
    public int IdUser { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public byte[] PasswordHash { get; set; } = null!;
}
