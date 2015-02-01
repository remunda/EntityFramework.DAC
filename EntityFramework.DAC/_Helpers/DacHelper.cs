using EntityFramework.DAC.Entities;
using Microsoft.SqlServer.Dac;
using Microsoft.SqlServer.Dac.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.DAC
{
    public static class DacHelper
    {
        public static IEnumerable<TSqlObject> GetAllTables(string dacPath)
        {
            //string dacPath = string.Format(@"..\..\..\{0}\bin\Debug\{0}.dacpac", projectName);
            //dacPath = FileHelper.GetFilePathInCurrentDirectory(dacPath);  

            if (!File.Exists(dacPath))
            {
                throw new FileNotFoundException("DACPAC not found. Try Rebuild SQL project.", dacPath);
            }

            var model = TSqlModel.LoadFromDacpac(dacPath, new ModelLoadOptions() { });
            var tables = model.GetObjects(DacQueryScopes.All, ModelSchema.Table);

            return tables;
        }

        public static DacPackage GetPackage(string dacPath)
        {
            return DacPackage.Load(dacPath, DacSchemaModelStorageType.File, System.IO.FileAccess.Read);
        }

        public static List<EntityInfo> GetDacEntitiesInfo(string dacProject)
        {
            var coreTables = DacHelper.GetAllTables(dacProject);
            //var contentTables = DacHelper.GetAllTables(@"Webcms.Db.Content");

            var entitiesInfo = new List<EntityInfo>();

            foreach (var table in coreTables)
            {
                var entity = new EntityInfo()
                {
                    FQN = table.Name.ToString(),
                    SchemaName = table.Name.Parts[0],
                    Name = table.Name.Parts[1]
                };

                var children = table.GetChildren();

                var entityPropInfos = new Dictionary<ObjectIdentifier, EntityPropertyInfo>();
                var primaryKeys = new List<ObjectIdentifier>();
                var foreignKeys = new Dictionary<ObjectIdentifier, ObjectIdentifier>();

                foreach (var child in children)
                {
                    if (child.ObjectType == Column.TypeClass)
                    {
                        var prop = new EntityPropertyInfo();
                        entityPropInfos.Add(child.Name, prop);

                        prop.Name = child.Name.Parts.Last();
                        prop.IsNullable = child.GetProperty<bool>(Column.Nullable);
                        prop.IsIdentity = child.GetProperty<bool>(Column.IsIdentity);

                        var columnType = child.GetMetadata<ColumnType>(Column.ColumnType);

                        prop.IsComputed = columnType == ColumnType.ComputedColumn;

                        if (columnType == ColumnType.Column)
                        {
                            var dataType = child.GetReferenced(Column.DataType).First();
                            prop.DbType = dataType.Name.Parts.Single();
                        }
                        else if (columnType == ColumnType.ComputedColumn)
                        {
                            var expressionDependencies = child.GetReferenced(Column.ExpressionDependencies);
                            var scalarFn = expressionDependencies.First(d => d.ObjectType == ScalarFunction.TypeClass);
                            var returnType = scalarFn.GetReferenced(ScalarFunction.ReturnType).First();

                            prop.DbType = returnType.Name.Parts.Single();
                        }
                        else
                        {
                            throw new NotSupportedException(columnType.ToString());
                        }


                        prop.ClrType = ClrTypeHelper.GetCSharpClrType(prop.DbType, prop.IsNullable);
                    }
                    else if (child.ObjectType == PrimaryKeyConstraint.TypeClass)
                    {
                        var columns = child.GetReferenced(PrimaryKeyConstraint.Columns);

                        foreach (var column in columns)
                        {
                            primaryKeys.Add(column.Name);
                        }
                    }
                    else if (child.ObjectType == ForeignKeyConstraint.TypeClass)
                    {
                        bool isDisabled = child.GetProperty<bool>(ForeignKeyConstraint.Disabled);
                        if (!isDisabled)
                        {
                            ObjectIdentifier name = null;
                            var foreignTable = child.GetReferenced(ForeignKeyConstraint.ForeignTable).SingleOrDefault();

                            if (foreignTable != null)
                            {
                                name = foreignTable.Name;                                
                            }
                            else
                            {
                                var rf = child.GetReferencedRelationshipInstances(ForeignKeyConstraint.ForeignTable, DacExternalQueryScopes.SameDatabase).SingleOrDefault();
                                if (rf != null)
                                {
                                    name = rf.ObjectName;
                                }
                            }

                            var column = child.GetReferenced(ForeignKeyConstraint.Columns).Single();


                            foreignKeys.Add(column.Name, name);
                        }
                    }
                    else
                    {
                        var c = child;
                    }
                }

                foreach (var key in primaryKeys)
                {
                    var keyColumn = entityPropInfos.First(e => e.Key.ToString() == key.ToString());
                    keyColumn.Value.IsKey = true;
                }

                foreach (var fk in foreignKeys)
                {
                    var fkColumn = entityPropInfos.First(e => e.Key.ToString() == fk.Key.ToString());
                    fkColumn.Value.ForeignKeys.Add(new ForeignKeyInfo()
                    {
                        ReferencedTableSchema = fk.Value.Parts.First(),
                        ReferencedTable = fk.Value.Parts.Last(),
                        Name = fk.Key.Parts.Last()
                    });
                }

                if (entityPropInfos.Any())
                {
                    entity.Properties.AddRange(entityPropInfos.Select(e => e.Value));
                    entitiesInfo.Add(entity); 
                }
            }

            return entitiesInfo;
        }
    }
}
