using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infranstructure.Models
{
    [SugarTable("role")]
    public class Role
    {
        [SugarColumn(ColumnName = "id", IsIdentity = true, IsNullable = false, IsPrimaryKey = true)]
        public int Id { get; set; }
        [SugarColumn(ColumnName = "name")]
        public string Name { get; set; }
        [SugarColumn(ColumnName = "desc")]
        public string Desc { get; set; }


        public List<Permission> Permissions(SqlSugarClient db)
        {
            return db.Queryable<Role, Role_Permission, Permission>((ro, ro_pe, pe) => ro.Id == ro_pe.RoleId && ro_pe.PermissionId == pe.Id)
                .Where((ro, ro_pe, pe) => ro.Id == this.Id)
                .Select((ro, ro_pe, pe) => pe).ToList();
        }

    }

    [SugarTable("permission")]
    public class Permission
    {
        [SugarColumn(ColumnName ="id")]
        public int Id { get; set; }

        [SugarColumn(ColumnName = "name")]
        public string Name { get; set; }

        [SugarColumn(ColumnName = "desc")]
        public string Desc { get; set; }
    }

    [SugarTable("role_permission")]
    public class Role_Permission
    {
        [SugarColumn(ColumnName = "role_id")]
        public int RoleId { get; set; }

        [SugarColumn(ColumnName ="permission_id")]
        public int PermissionId { get; set; }
    }
}
