using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace KMP.Interface
{
    public class ModuleProject : ModuleCollection
    {
        public bool isChanged = false;
        public bool IsChanged
        {
            get
            {
                return this.isChanged;
            }
            set
            {
                this.isChanged = value;

            }
        }

        public override void ModulePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.IsChanged = true;
            base.ModulePropertyChanged(sender, e);
        }

        public void Create()
        {
            if (this.Count > 0)
            {
                this.First().CreateModule();
            }
            
        }
    }
}
