﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xAPI.Core.Models.InteractionTypes
{
    public class Choice : InteractionTypeBase
    {
        protected override InteractionType INTERACTION_TYPE => InteractionType.Choice;

        public InteractionComponent[] Choices { get; set; }
    }
}