using Infranstructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KMP.Interface
{
    public interface IDatabaseService
    {
        List<Project> GetProjs();
        List<Project> GetProjs(string projType);
        List<Project> Getprojs(string projType, DateTime startTime, DateTime endTime);

        List<Comment> GetComments(Project proj);
        void UploadProj(Project proj);
    }
}
