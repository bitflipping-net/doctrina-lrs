using Doctrina.Domain.Entities.OwnedTypes;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Doctrina.Persistence.ValueComparer
{
    public class StringArrayValueComparator : ValueComparer<string[]>
    {
        public StringArrayValueComparator(bool favorStructuralComparisons)
        : base(favorStructuralComparisons)
        {
        }

        public StringArrayValueComparator([NotNullAttribute] Expression<Func<string[], string[], bool>> equalsExpression, [NotNullAttribute] Expression<Func<string[], int>> hashCodeExpression)
        : base(equalsExpression, hashCodeExpression)
        {
        }

        public StringArrayValueComparator([NotNullAttribute] Expression<Func<string[], string[], bool>> equalsExpression, [NotNullAttribute] Expression<Func<string[], int>> hashCodeExpression, [NotNullAttribute] Expression<Func<string[], string[]>> snapshotExpression)
        : base(equalsExpression, hashCodeExpression, snapshotExpression)
        {
        }
    }
}
