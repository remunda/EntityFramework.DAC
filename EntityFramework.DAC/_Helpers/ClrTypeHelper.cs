using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Dac.Model;

namespace EntityFramework.DAC
{
    public static class ClrTypeHelper
    {
        public static string GetCSharpClrType(string sqlDataType, bool isNullable)
        {
            var type = (SqlDataType)Enum.Parse(typeof(SqlDataType), sqlDataType, true);
            return GetCSharpClrType(type, isNullable);
        }

        public static string GetCSharpClrType(SqlDataType sqlDataType, bool isNullable)
        {
            string clrName = GetCSharpClrTypeName(sqlDataType);
            if (isNullable)
            {
                switch (sqlDataType)
                {
                    case SqlDataType.BigInt:
                    case SqlDataType.Bit:
                    case SqlDataType.Date:
                    case SqlDataType.DateTime:
                    case SqlDataType.DateTime2:
                    case SqlDataType.SmallDateTime:
                    case SqlDataType.DateTimeOffset:
                    case SqlDataType.Decimal:
                    case SqlDataType.Money:
                    case SqlDataType.Numeric:
                    case SqlDataType.SmallMoney:
                    case SqlDataType.Float:
                    case SqlDataType.Int:
                    case SqlDataType.Real:
                    case SqlDataType.SmallInt:
                    case SqlDataType.Time:
                    case SqlDataType.TinyInt:
                    case SqlDataType.UniqueIdentifier:
                        clrName += "?";
                        break;
                }
            }
            return clrName;
        }
        
        public static string GetCSharpClrTypeName(SqlDataType sqlDataType)
        {            
            switch (sqlDataType)
            {
                case SqlDataType.BigInt:
                    return "long";
                case SqlDataType.Binary:
                case SqlDataType.Image:
                case SqlDataType.Timestamp:
                case SqlDataType.VarBinary:                    
                    return "byte[]";
                case SqlDataType.Bit:
                    return "bool";
                case SqlDataType.Char:
                case SqlDataType.NChar:
                case SqlDataType.NText:
                case SqlDataType.NVarChar:
                case SqlDataType.VarChar:
                case SqlDataType.Text:
                    return "string";
                case SqlDataType.Date:
                case SqlDataType.DateTime:
                case SqlDataType.DateTime2:
                case SqlDataType.SmallDateTime:
                    return "DateTime";
                case SqlDataType.DateTimeOffset:
                    return "DateTimeOffset";
                case SqlDataType.Decimal:
                case SqlDataType.Money:
                case SqlDataType.Numeric:
                case SqlDataType.SmallMoney:
                    return "decimal";
                case SqlDataType.Float:
                    return "float";
                case SqlDataType.Int:
                    return "int";
                case SqlDataType.Real:
                    return "double";
                case SqlDataType.SmallInt:
                    return "short";
                case SqlDataType.Time:
                    return "TimeSpan";
                case SqlDataType.TinyInt:
                    return "byte";
                case SqlDataType.UniqueIdentifier:
                    return "Guid";
                case SqlDataType.Variant:
                    return "object";
                case SqlDataType.Xml:
                    return "System.Xml.XmlDocument";
                default:
                    return null;
            }

        }
    }
}
