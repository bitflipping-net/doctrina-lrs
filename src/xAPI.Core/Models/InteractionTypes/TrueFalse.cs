﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xAPI.Core.Models.InteractionTypes
{
    public class TrueFalse : InteractionTypeBase
    {
        protected override InteractionType INTERACTION_TYPE => InteractionType.TrueFalse;
    }
}