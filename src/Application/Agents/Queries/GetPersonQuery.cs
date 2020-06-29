using AutoMapper;
using Doctrina.Application.Common.Interfaces;
using Doctrina.Domain.Entities;
using Doctrina.ExperienceApi.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.Agents.Queries
{
    public class GetPersonQuery : IRequest<Person>
    {
        public Agent Agent { get; set; }

        public static GetPersonQuery Create(Agent agent)
        {
            return new GetPersonQuery()
            {
                Agent = agent
            };
        }
    }
}
