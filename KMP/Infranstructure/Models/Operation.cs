using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infranstructure.Models
{
    [SugarTable("operation")]
    public class Operation
    {
        [SugarColumn(ColumnName = "id", IsIdentity = true, IsNullable = false, IsPrimaryKey = true)]
        public int Id { get; set; }

        [SugarColumn(ColumnName = "project_id")]
        public int ProjectId { get; set; }

        [SugarColumn(ColumnName = "user_id")]
        public int UserId { get; set; }

        [SugarColumn(ColumnName = "content")]
        public int Content { get; set; }
    }
}
