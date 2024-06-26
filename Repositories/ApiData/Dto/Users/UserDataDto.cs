﻿using AicaDocsUI.Repositories.ApiData.Dto.IdentityCommons;

namespace AicaDocsUI.Repositories.ApiData.Dto.Users;

public class UserDataDto
{
    public required string FullName { get; set; }
    
    public required string Email { get; set; }

    public required bool IsConfirmed { get; set; }

    public required UserRole Role { get; set; }
}