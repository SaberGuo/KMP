using Infranstructure.Tool;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Infranstructure.Models
{
    [TypeConverterAttribute(typeof(ModelConverter)), Description("名称")]
    [SugarTable("user")]
    public class User
    {
        [SugarColumn(ColumnName ="id",IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [SugarColumn(ColumnName = "name")]
        public string Name { get; set; }

        [SugarColumn(ColumnName = "created_at")]
        public DateTime CreatedAt { get; set; }

        public List<Role> Roles(SqlSugarClient db)
        {
            return db.Queryable<User, User_Role, Role>((us, us_ro, ro) => us.Id == us_ro.UserId && us_ro.RoleId == ro.Id)
                .Where((us, us_ro, ro) => us.Id == this.Id)
                .Select((us, us_ro, ro) => ro).ToList();
        }
    }


    public class User_Role
    {
        [SugarColumn(ColumnName = "user_id")]
        public int UserId { get; set; }

        [SugarColumn(ColumnName = "role_id")]
        public int RoleId { get; set; }
    }
}
