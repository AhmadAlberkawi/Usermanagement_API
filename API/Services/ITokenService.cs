﻿using DataAccess.Models;

namespace API.Services
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}