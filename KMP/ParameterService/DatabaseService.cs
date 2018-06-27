using KMP.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using SqlSugar;
using Infranstructure.Models;

namespace ParameterService
{
    [Export(typeof(IDatabaseService))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class DatabaseService : IDatabaseService
    {
        SqlSugarClient db = null;
        [ImportingConstructor]
        public DatabaseService()
        {
            bool ExistDB = System.IO.File.Exists(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "kmp.sqlite"));
            //init connect
            db = new SqlSugarClient(new ConnectionConfig
            {
                ConnectionString = @"DataSource="+System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"kmp.sqlite"),
                DbType = DbType.Sqlite,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute
            });
            if (!ExistDB)
            {
                this.CreateTable();
            }
            
        }

        private void CreateTable()
        {
            if(db == null)
            {
                return;
            }
            
            db.CodeFirst.InitTables(typeof(User), typeof(Project), typeof(Role),typeof(Permission),
                typeof(User_Role),typeof(Role_Permission), typeof(Comment), typeof(Operation));
        }
        public List<Project> GetProjs()
        {
            return db.Queryable<Project>().ToList();
        }
        public  List<Project> GetProjs(string projType)
        {
            return db.Queryable<Project>().Where(pj=> pj.ProjType == projType).ToList();
        }

        public List<Project> Getprojs(string projType, DateTime startTime, DateTime endTime)
        {

            List<Project> ps = db.Queryable<Project>().Where(pj => pj.ProjType == projType && SqlFunc.Between(pj.CreatedAt, startTime, endTime)).ToList();
            foreach (var p in ps)
            {
                p.Builder = GetUser(p.UserId);
            }
            return ps;
        }

        public List<Comment> GetComments(Project proj)
        {
            if(proj== null)
            {
                return null;
            }
            //return db.Queryable<Comment>().ToList();
            List<Comment> comments = db.Queryable<Project, Comment>((pj, ct) => new object[]
                {
                    JoinType.Left, pj.Id==ct.ProjectId
                }).Where((pj, ct) => pj.Id == proj.Id).Select((pj, ct) => ct).ToList();
            foreach (var ct in comments)
            {
                ct.User = this.GetUser(ct.UserId);
            }
            return comments;
        }

        public User GetUser(int id)
        {
            return db.Queryable<User>().InSingle(id);
        }
        public void UploadProj(Project proj)
        {
            db.Insertable(proj).ExecuteCommand();
        }
    }
}
