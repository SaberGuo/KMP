using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infranstructure.Events
{
    public class GeneratorEventArgs: EventArgs
    {
        public string ProgressInfo { get; set; }
    }

    public class GeneratorEvent : CompositePresentationEvent<string>
    {

    }
}
