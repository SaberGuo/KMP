using Infranstructure.Models;
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

        private Project _ProjInfo = new Project();
        public Project ProjInfo
        {
            get
            {
                return this._ProjInfo;
            }
            set
            {
                this.ProjInfo = value;
                
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
        public void DeSerialization(string path)
        {
            
            if (this.Count > 0)
            {
                
                //this.First().DeSerialization();
            }
            else
            {
                
            }
        }
        public void Serialization()
        {
            if (this.Count > 0)
            {
                this.First().Serialization(ProjectPath);
            }
        }
        public void Serialization(string path)
        {
            if (this.Count > 0)
            {
                this.ProjectPath = path;
                this.First().Serialization(ProjectPath);
            }
            

        }
        private string projectPath;
        public string ProjectPath
        {
            get
            {
                return projectPath;
            }
            set
            {
                this.projectPath = value;
            }
        }
    }
}
