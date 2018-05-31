using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KMP.Interface
{
   public interface IModuleService
    {
        void Create();
        void Serialization();
        void DeSerialization();
        IParamedModule CreateProject(string projType, string projPath);
    }
}
