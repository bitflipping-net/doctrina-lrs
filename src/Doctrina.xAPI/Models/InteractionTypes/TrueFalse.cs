﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctrina.xAPI.Models.InteractionTypes
{
    public class TrueFalse : InteractionTypeBase
    {
        protected override InteractionType INTERACTION_TYPE => InteractionType.TrueFalse;
    }
}