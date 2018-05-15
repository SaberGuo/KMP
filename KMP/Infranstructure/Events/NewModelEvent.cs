using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infranstructure.Events
{
    public class ProjectInfo
    {
        public string ProjectPath{ get; set; }
        public string ProjectType { get; set; }  
    }

    public class NewModelEvent: CompositePresentationEvent<ProjectInfo>
    {
    }
}
