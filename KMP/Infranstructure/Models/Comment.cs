using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infranstructure.Models
{
    [SugarTable("comment")]
    public class Comment
    {
        [SugarColumn(ColumnName = "id", IsIdentity = true, IsNullable = false, IsPrimaryKey = true)]
        public int Id { get; set; }

        [SugarColumn(ColumnName = "project_id")]
        public int ProjectId { get; set; }

        [SugarColumn(ColumnName = "user_id")]
        public int UserId { get; set; }

        [SugarColumn(ColumnName = "content")]
        public string Content { get; set; }

        [SugarColumn(ColumnName = "created_at")]
        public DateTime CreatedAt { get; set; }

        private User _user;
        [SugarColumn(IsIgnore =true)]
        public User User
        {
            get { return this._user; }
            set
            {
                this._user = value;
            }
        }
    }
}
