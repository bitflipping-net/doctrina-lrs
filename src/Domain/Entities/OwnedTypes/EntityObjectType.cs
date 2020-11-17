

using System;
using System.ComponentModel;
using System.Globalization;

namespace Doctrina.Domain.Entities
{
    public enum EntityObjectType
    {
        Invalid,

        Agent,

        Group,

        Activity,

        SubStatement,

        StatementRef,

        Person,

        Statement
    }

    public class ObjectTypeTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }

            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                if (Enum.TryParse(typeof(EntityObjectType), value as string, out object converted))
                {
                    return (EntityObjectType)converted;
                }
            }

            return base.ConvertFrom(context, culture, value);
        }
    }
}
