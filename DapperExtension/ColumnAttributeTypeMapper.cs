using Dapper;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Linq;
using System.Reflection;

namespace DapperExtension
{
    public class ColumnAttributeTypeMapper : FallbackTypeMapper
    {
        public ColumnAttributeTypeMapper(Type type)
            : base(new SqlMapper.ITypeMap[]
            {
                new CustomPropertyTypeMap(type,  PropertySelector),
                new DefaultTypeMap(type)
            })
        {
        }

        private static PropertyInfo PropertySelector(Type type, string columnName)
        {
            return type.GetProperties()
                    .FirstOrDefault(prop => prop.GetCustomAttributes(false)
                    .OfType<ColumnMappingAttribute>()
                    .Any(attr => attr.Name == columnName));
        }
    }
}