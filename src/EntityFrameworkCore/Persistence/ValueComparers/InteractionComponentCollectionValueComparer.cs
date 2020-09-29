using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Doctrina.Domain.Models.ValueObjects;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Doctrina.Persistence.ValueComparer
{
    public class ExtensionsCollectionValueComparer : ValueComparer<ExtensionsCollection>
    {
        public ExtensionsCollectionValueComparer(bool favorStructuralComparisons)
        : base(favorStructuralComparisons)
        {
        }

        public ExtensionsCollectionValueComparer([NotNullAttribute] Expression<Func<ExtensionsCollection, ExtensionsCollection, bool>> equalsExpression, [NotNullAttribute] Expression<Func<ExtensionsCollection, int>> hashCodeExpression)
        : base(equalsExpression, hashCodeExpression)
        {
        }

        public ExtensionsCollectionValueComparer([NotNullAttribute] Expression<Func<ExtensionsCollection, ExtensionsCollection, bool>> equalsExpression, [NotNullAttribute] Expression<Func<ExtensionsCollection, int>> hashCodeExpression, [NotNullAttribute] Expression<Func<ExtensionsCollection, ExtensionsCollection>> snapshotExpression)
        : base(equalsExpression, hashCodeExpression, snapshotExpression)
        {
        }
    }
}
