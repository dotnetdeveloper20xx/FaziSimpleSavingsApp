﻿using FaziSimpleSavings.Core.Entities;

namespace Application.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}