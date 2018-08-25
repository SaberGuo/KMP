using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KMP.Parameterization.Events
{
    public class ProjectEventArgs: EventArgs
    {
        public string ProjectDir { get; set; }
        public List<string> ProjectTypes { get; set; }
        public string ProjectName { get; set; }
      //  public string ProjectType { get; set; }
    }
}
