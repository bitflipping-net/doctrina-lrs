﻿using Doctrina.Persistence.Entities;
using Doctrina.xAPI;

namespace Doctrina.Persistence.Services
{
    public interface ISubStatementService
    {
        SubStatementEntity CreateSubStatement(SubStatement subStatement);
    }
}
