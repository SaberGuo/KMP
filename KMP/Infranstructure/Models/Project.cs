using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace Infranstructure.Models
{
    [SugarTable("project")]
    public class Project
    {
        [DisplayName("ID")]
        [SugarColumn(ColumnName = "id", IsIdentity=true, IsNullable = false, IsPrimaryKey = true)]
        public int Id { get; set; }

        [DisplayName("名称")]
        [SugarColumn(ColumnName = "name")]
        public string Name { get; set; }

        [SugarColumn(ColumnName = "path")]
        public string Path { get; set; }

        [SugarColumn(ColumnName = "proj_type")]
        public string ProjType { get; set; }

        [SugarColumn(ColumnName = "created_at")]
        public DateTime CreatedAt { get; set; }

        [SugarColumn(ColumnName = "is_editing")]
        public bool IsEditing { get; set; }

        [Browsable(false)]
        [SugarColumn(ColumnName ="user_id")]
        public int UserId { get; set; }

        private List<Comment> _comments;

        [Browsable(false)]
        [SugarColumn(IsIgnore =true)]
        public List<Comment> Comments
        {
            get
            {
                return this._comments;
            }
            set
            {
                this._comments = value;
            }
        }
        private User _builder;

        [SugarColumn(IsIgnore = true)]
        [ExpandableObject]
        public User Builder
        {
            get
            {
                return this._builder;
            }
            set
            {
                this._builder = value;
            }
        }
    }
}
