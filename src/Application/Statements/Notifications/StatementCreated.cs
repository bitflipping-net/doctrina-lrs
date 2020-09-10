using Doctrina.Domain.Entities;
using Doctrina.ExperienceApi.Data;
using MediatR;
using System;

namespace Doctrina.Application.Statements.Notifications
{
    public class StatementCreated : INotification
    {
        public StatementEntity Created { get; private set; }

        public static StatementCreated Create(StatementEntity statement)
        {
            return new StatementCreated()
            {
                Created = statement
            };
        }
    }
}
