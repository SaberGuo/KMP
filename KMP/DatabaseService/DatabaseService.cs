using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseService
{
    public class DatabaseService
    {
        [Export(typeof(IModuleService))]
        [PartCreationPolicy(CreationPolicy.Shared)]
    }
}
