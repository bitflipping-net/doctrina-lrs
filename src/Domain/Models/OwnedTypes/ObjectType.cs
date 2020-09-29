using System;
using System.ComponentModel;
using System.Globalization;

namespace Doctrina.Domain.Models
{
    public enum ObjectType
    {
        None = 0,
        Agent = 1,
        Group = 2,
        Activity = 3,
        SubStatement = 4,
        StatementRef = 5,
        Statement = 6,
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
                if (Enum.TryParse(typeof(ObjectType), value as string, out object converted))
                {
                    return (ObjectType)converted;
                }
            }

            return base.ConvertFrom(context, culture, value);
        }
    }
}
