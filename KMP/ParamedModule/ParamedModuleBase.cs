using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inventor;
using KMP.Interface;
using KMP.Interface.Model;
using Microsoft.Practices.Prism.ViewModel;
using System.Collections.ObjectModel;
using Infranstructure.Events;
using Infranstructure.Tool;
using System.Xml.Serialization;
using System.Reflection;
using KMP.Interface.Model.Container;
namespace ParamedModule
{
    public abstract class ParamedModuleBase :NotificationObject, IParamedModule
    {

        ParameterBase parameter;
        ModuleCollection subParameModules =new ModuleCollection();
        string modelPath="";
        string name="";
        string projectType = "";
        public ParamedModuleBase()
        {
            this.SubParamedModules.Root = this;
        }

        public IParamedModule FindModule(string projType)
        {
            IParamedModule res = null;
            foreach (var subModule in this.subParameModules)
            {
                if(subModule.ProjectType == projectType)
                {
                    return subModule;
                }
                res = subModule.FindModule(projType);
                if (res != null)
                {
                    return res;
                }
            }
            return res;

        }

        public bool AddModule(IParamedModule pm)
        {
            bool res = false;
            for (int i=0;i<this.subParameModules.Count;++i)
            {
                if (subParameModules[i].ProjectType == pm.ProjectType)
                {
                    pm.ModelPath = subParameModules[i].ModelPath;
                    subParameModules[i] = pm;
                    return true;
                }
                res = subParameModules[i].AddModule(pm);
                if (res)
                {
                    return res;
                }
            }
            return res;
        }
        /*public ComponentOccurrence Occurrence
        {
            get
            {
             throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }*/
        //  public ComponentOccurrence Occurrence { get; set; }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
                RaisePropertyChanged(() => this.Name);
            }
        }

        public string ProjectType
        {
            get
            {
                return projectType;
            }
            set
            {
                this.projectType = value;
            }
        }
        [XmlIgnore]
        public ParameterBase Parameter
        {
            get
            {
                return parameter;
            }

            set
            {
                parameter = value;
                parameter.PropertyChanged += Parameter_PropertyChanged;
                this.RaisePropertyChanged(() => this.Parameter);
            }
        }

        private void Parameter_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            this.RaisePropertyChanged(e.PropertyName);
        }

        public event EventHandler<GeneratorEventArgs> GeneratorChanged;
       public event EventHandler<GeneratorEventArgs> ParErrorHappen;
        public void GeneratorProgress(object sender, string info)
        {
            this.GeneratorChanged?.Invoke(sender, new GeneratorEventArgs { ProgressInfo = info });
        }
        public void ParErrorChanged(object sender, string info)
        {
            throw new MyException(this.Name + "参数设置错误 : " + info, ExceptionType.WARNING);
            //this.ParErrorHappen?.Invoke(sender, new GeneratorEventArgs { ProgressInfo = info });
        }
        public string PreviewImagePath { get; set; }

        public string ModelPath
        {
            get
            {
                return modelPath;
            }

            set
            {
                modelPath = value;
                foreach (var item in subParameModules)
                {
                    item.ModelPath = modelPath;
                }
                this.RaisePropertyChanged(() => this.ModelPath);
            }
        }
        public virtual string FullPath { get; }
        public double UsMM(double value)
        {
            return value / 1;
        }
        [XmlIgnore]
        public ModuleCollection SubParamedModules
        {
            get
            {
                return subParameModules;
            }

            set
            {
                subParameModules = value;
                this.RaisePropertyChanged(() => this.SubParamedModules);
            }
        }

        public int GetGeneratorCount()
        {
            int count = 0;
            foreach (var item in subParameModules)
            {
                count +=item.GetGeneratorCount();
            }
            count += 1;
            return count;
        }
        public abstract void CreateModule();
        public abstract bool CheckParamete();
        protected bool CheckParZero()
        {
            string message;
            if (!CommonTool.CheckParameterValue(Parameter, out message))
            {
                ParErrorChanged(this, message);
                return false;
            }
            return true;
        }
        public void Serialization()
        {
            string path = System.IO.Path.Combine(ModelPath, this.Name + ".kmp");
           
            XMLDeserializerHelper.Serialization<ParamedModule.ParamedModuleBase>(this, path);
        }
        public void Serialization(string path)
        {
            XMLDeserializerHelper.Serialization<ParamedModule.ParamedModuleBase>(this, path);
        }
        public void DeSerialization(string path= "")
        {
          ParamedModuleBase module=  XMLDeserializerHelper.Deserialization<ParamedModule.ParamedModuleBase>(this, path);
          if(module!=null)
            SetValue(module, this);
        }
        public void GetSubField()
        {

        }
        //public void SetValue(object a, object b)
        //{
        //  Type A=  a.GetType();
        //    Type B = b.GetType();
        //    PropertyInfo[] Ainfos=  A.GetProperties();
        //   List<PropertyInfo> list= Ainfos.Where(ss => ss.Name != "SubParamedModules" && ss.Name != "Parameter").ToList();
        //    foreach (var item in list)
        //    {
        //        Type temp = item.PropertyType;
        //        PropertyInfo Binfo = B.GetProperty(item.Name);
        //        if(temp.IsPrimitive||temp==typeof(string))
        //        {
        //            object value = item.GetValue(a, null);
        //            try
        //            {
        //                Binfo.SetValue(b, value, null);
        //            }
        //            catch (Exception)
        //            {

                        
        //            }

        //        }
        //        else
        //        {
        //          if(item.PropertyType.Name == "ObservableCollection`1")
        //            {
                       
        //                object c = item.GetValue(a, null);
        //                object d = Binfo.GetValue(b, null);
        //                dynamic x1 = c;
        //                dynamic x2 = d;
        //                for (int i = 0; i < x1.Count; i++)
        //                {
        //                    SetValue(x1[i], x2[i]);
        //                }
                       
        //            }   
        //          else
        //            {
                       
        //                object c = item.GetValue(a, null);
        //                object d = Binfo.GetValue(b, null);
                     
        //                SetValue(c, d);
        //            }
                  
        //        }

        //    }

        //    FieldInfo[] AFields = A.GetFields();
        //    foreach (var item in AFields)
        //    {
        //        Type temp = item.FieldType;
        //        FieldInfo BField = B.GetField(item.Name);
        //        if (temp.IsPrimitive || temp == typeof(string))
        //        {
        //            object value = item.GetValue(a);
        //            try
        //            {
        //                BField.SetValue(b, value);
        //            }
        //            catch (Exception)
        //            {


        //            }

        //        }
        //        else
        //        {
        //            object c = item.GetValue(a);
        //            object d = BField.GetValue(b);
        //            SetValue(c, d);
        //        }
        //    }
        //}

        public void SetValue(object a, object b)
        {
            Type objType = a.GetType();
            PropertyInfo[] propertys = objType.GetProperties();
            FieldInfo[] fields = objType.GetFields();
            List<PropertyInfo> list = propertys.Where(ss => ss.Name != "SubParamedModules" && ss.Name != "Parameter").ToList();
            PropertyInfo flanchDN = propertys.Where(ss => ss.Name == "FlanchDN").FirstOrDefault();
            if (flanchDN != null)
            {
                object c = flanchDN.GetValue(a, null);
                //  object d = flanchDN.GetValue(b, null);
                flanchDN.SetValue(b, c, null);
            }
            foreach (var item in list)
            {
                if (item.Name == "FlanchDN") continue;
                Type ItemType = item.PropertyType;
                object c = item.GetValue(a, null);
                object d = item.GetValue(b, null);
                if (ItemType.IsPrimitive || ItemType == typeof(string))
                {
                    try
                    {
                         item.SetValue(b, c, null);
                    }
                    catch (Exception)
                    {
                    }

                }
                else if (item.PropertyType.Name == "ObservableCollection`1")
                {
                    dynamic x1 = c;
                    dynamic x2 = d;
                    for (int i = 0; i < x1.Count; i++)
                    {
                        SetValue(x1[i], x2[i]);
                    }
                }
                else
                {
                    SetValue(c, d);
                }

            }

           
            foreach (var item in fields)
            {
                Type FieldType = item.FieldType;
                object c = item.GetValue(a);
                object d = item.GetValue(b);
                if (FieldType.IsPrimitive || FieldType == typeof(string))
                {
                    try
                    {
                        item.SetValue(b, c);
                    }
                    catch (Exception)
                    {
                    }
                }
                else
                {
                    SetValue(c, d);
                }
            }
        }
        internal abstract void CloseSameNameDocment();
       



    }
}
