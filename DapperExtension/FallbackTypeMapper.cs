﻿using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DapperExtension
{
    public class FallbackTypeMapper : SqlMapper.ITypeMap
    {
        private readonly IEnumerable<SqlMapper.ITypeMap> _mappers;

        public FallbackTypeMapper(IEnumerable<SqlMapper.ITypeMap> mappers)
        {
            this._mappers = mappers;
        }

        public ConstructorInfo FindConstructor(string[] names, Type[] types)
        {
            foreach (var mapper in this._mappers)
            {
                try
                {
                    ConstructorInfo result = mapper.FindConstructor(names, types);
                    if (result != null)
                    {
                        return result;
                    }
                }
                catch (NotImplementedException)
                {
                }
            }

            return null;
        }

        public ConstructorInfo FindExplicitConstructor()
        {
            return this._mappers
                .Select(mapper => mapper.FindExplicitConstructor())
                .FirstOrDefault(result => result != null);
        }

        public SqlMapper.IMemberMap GetConstructorParameter(ConstructorInfo constructor, string columnName)
        {
            foreach (var mapper in this._mappers)
            {
                try
                {
                    var result = mapper.GetConstructorParameter(constructor, columnName);
                    if (result != null)
                    {
                        return result;
                    }
                }
                catch (NotImplementedException)
                {
                }
            }

            return null;
        }

        public SqlMapper.IMemberMap GetMember(string columnName)
        {
            foreach (var mapper in this._mappers)
            {
                try
                {
                    string propertyName = DapperExtension.ToProperty(columnName);
                    var result = mapper.GetMember(propertyName);
                    if (result != null)
                    {
                        return result;
                    }
                }
                catch (NotImplementedException)
                {
                }
            }

            return null;
        }
    }
}