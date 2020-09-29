using Doctrina.Domain.Models;
using Doctrina.ExperienceApi.Data;
using MediatR;
using System;

namespace Doctrina.Application.Statements.Notifications
{
    public class StatementCreated : INotification
    {
        public StatementModel Model { get; private set; }

        public static StatementCreated Create(StatementModel statement)
        {
            return new StatementCreated()
            {
                Model = statement
            };
        }
    }
}
