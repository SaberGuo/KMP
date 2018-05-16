using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inventor;
using Infranstructure.Tool;
namespace ParamedModule
{
  public abstract  class AssembleModuleBase:ParamedModuleBase
    {
       internal AssemblyComponentDefinition Definition;
       internal AssemblyDocument Doc;
        protected Matrix oPos;
        /// <summary>
        /// 没有配对的iMate集合
        /// </summary>
       internal Dictionary<string, iMateDefinition> freeiMates = new Dictionary<string, iMateDefinition>();
       // internal Dictionary<string, PartFeature> partFeatures = new Dictionary<string, PartFeature>();
        public AssembleModuleBase():base()
        {
           
        }
        /// <summary>
        /// 创建装配文档
        /// </summary>
        protected void CreateDoc()
        {
             Doc = InventorTool.CreateAssembly();
            Definition = Doc.ComponentDefinition;
            oPos = InventorTool.TranGeo.CreateMatrix();
        }
        /// <summary>
        /// 加载部件或零件
        /// </summary>
        /// <param name="def"></param>
        /// <returns></returns>
        protected ComponentOccurrence LoadOccurrence(ComponentDefinition def)
        {
            ComponentOccurrence item = Definition.Occurrences.AddByComponentDefinition(def, oPos);
           // GetFeatures(item);
            return item;
        }
        protected ComponentOccurrence LoadOccurrence(ComponentDefinition def,Matrix pos)
        {
            ComponentOccurrence item = Definition.Occurrences.AddByComponentDefinition(def, pos);
            // GetFeatures(item);
            return item;
        }
        /// <summary>
        /// 获取需要用到的特征
        /// </summary>
        /// <param name="occ"></param>
        //private void GetFeatures(ComponentOccurrence occ)
        //{
        //    if (occ.DefinitionDocumentType == DocumentTypeEnum.kPartDocumentObject)
        //    {

        //        List<PartFeature> features = InventorTool.GetCollectionFromIEnumerator<PartFeature>(((PartComponentDefinition)occ.Definition).Features.GetEnumerator());
        //        foreach (var item in features)
        //        {
        //            if (!partFeatures.ContainsKey(item.Name))
        //            {
        //                partFeatures.Add(item.Name, item);
        //            }

        //        }
        //    }
        //    else
        //    {
        //        List<ComponentOccurrence> list = InventorTool.GetCollectionFromIEnumerator<ComponentOccurrence>(occ.SubOccurrences.GetEnumerator());
        //        foreach (var item in list)
        //        {
        //            GetFeatures(item);
        //        }
        //    }
        //}
        /// <summary>
        /// 设置装配 
        /// </summary>
        /// <param name="occ"></param>
        protected  void SetiMateResult(ComponentOccurrence occ)
        {
            if(occ.DefinitionDocumentType==DocumentTypeEnum.kPartDocumentObject)
            {
                List<iMateDefinition> iMates = InventorTool.GetCollectionFromIEnumerator<iMateDefinition>(occ.iMateDefinitions.GetEnumerator());
              
           
                foreach (var item in iMates)
                {
                  
                    if(freeiMates.ContainsKey(item.Name))
                    {
                        iMateDefinition tmp = freeiMates[item.Name];
                        freeiMates.Remove(item.Name);
                        Definition.iMateResults.AddByTwoiMates(tmp, item);
                    }
                    else
                    {
                        freeiMates.Add(item.Name, item);
                    }
                }
            }
            else
            {
                List<ComponentOccurrence> list = InventorTool.GetCollectionFromIEnumerator<ComponentOccurrence>(occ.SubOccurrences.GetEnumerator());
                foreach (var item in list)
                {
                    SetiMateResult(item);
                }
            }
        }
        /// <summary>
        /// 设置对齐配对
        /// </summary>
        /// <param name="co1">零件1</param>
        /// <param name="entity1">依赖特性1</param>
        /// <param name="co2">零件2</param>
        /// <param name="entity2">依赖特性2</param>
        /// <param name="name">配对名称</param>
        /// <param name="offset">偏移量</param>
        protected static void SetFlushiMate(ComponentOccurrence co1,object entity1, ComponentOccurrence co2, object entity2,string name,double offset)
        {
            FlushiMateDefinition flush1, flush2;
            if (co1.DefinitionDocumentType==DocumentTypeEnum.kAssemblyDocumentObject)
            {
                flush1 = ((AssemblyComponentDefinition)co1.Definition).iMateDefinitions.AddFlushiMateDefinition(entity1, offset + "mm");
            }
            else
            {
                flush1 = ((PartComponentDefinition)co1.Definition).iMateDefinitions.AddFlushiMateDefinition(entity1, offset + "mm");
            }
            if(co2.DefinitionDocumentType==DocumentTypeEnum.kAssemblyDocumentObject)
            {
                flush2 = ((AssemblyComponentDefinition)co2.Definition).iMateDefinitions.AddFlushiMateDefinition(entity2, offset + "mm");
            }
            else
            {
                flush2 = ((PartComponentDefinition)co2.Definition).iMateDefinitions.AddFlushiMateDefinition(entity2, offset + "mm");
            }
            //FlushiMateDefinition flush1 = ((PartComponentDefinition)co1.Definition).iMateDefinitions.AddFlushiMateDefinition(entity1, offset + "mm");
            //FlushiMateDefinition flush2 = ((PartComponentDefinition)co2.Definition).iMateDefinitions.AddFlushiMateDefinition(entity2, offset + "mm");
            flush1.Name = name;
            flush2.Name = name;

        }
        /// <summary>
        /// 设置耦合配对
        /// </summary>
        /// <param name="co1"></param>
        /// <param name="entity1"></param>
        /// <param name="co2"></param>
        /// <param name="entity2"></param>
        /// <param name="name"></param>
        /// <param name="offset"></param>
        protected static void SetMateiMate(ComponentOccurrence co1, object entity1, ComponentOccurrence co2, object entity2, string name, double offset)
        {
            MateiMateDefinition flush1 = ((PartComponentDefinition)co1.Definition).iMateDefinitions.AddMateiMateDefinition(entity1, offset + "mm");
            MateiMateDefinition flush2 = ((PartComponentDefinition)co2.Definition).iMateDefinitions.AddMateiMateDefinition(entity2, offset + "mm");
            flush1.Name = name;
            flush2.Name = name;

        }
        /// <summary>
        /// 根据特性名称获取侧边面
        /// </summary>
        /// <param name="occ">零件</param>
        /// <param name="name">特性名称</param>
        /// <returns></returns>
        protected static  List<Face> GetSideFaces(ComponentOccurrence occ,string name)
        {
            if(occ.DefinitionDocumentType==DocumentTypeEnum.kPartDocumentObject)
            {
                List<PartFeature> features = InventorTool.GetCollectionFromIEnumerator<PartFeature>(((PartComponentDefinition)occ.Definition).Features.GetEnumerator());
                PartFeature feature = features.Where(a => a.Name == name).FirstOrDefault();
                if (feature == null) return null;
                if (feature.Type == ObjectTypeEnum.kExtrudeFeatureObject)
                {
                    return InventorTool.GetCollectionFromIEnumerator<Face>(((ExtrudeFeature)feature).SideFaces.GetEnumerator());
                }
                else
                {
                    return InventorTool.GetCollectionFromIEnumerator<Face>(feature.Faces.GetEnumerator());
                }
            }
            else
            {
                foreach (ComponentOccurrence item in occ.SubOccurrences)
                {
                    List<Face> faces = GetSideFaces(item, name);
                    if(faces!=null&&faces.Count>0)
                    {
                        return faces;
                    }
                    
                }
                return null;
            }
     
           
        }
        protected static List<Face> GetSideFacesproxy(ComponentOccurrence occ, string name)
        {
            if (occ.DefinitionDocumentType == DocumentTypeEnum.kPartDocumentObject)
            {
                List<PartFeature> features = InventorTool.GetCollectionFromIEnumerator<PartFeature>(((PartComponentDefinition)occ.Definition).Features.GetEnumerator());
                PartFeature feature = features.Where(a => a.Name == name).FirstOrDefault();
                if (feature == null) return null;
                List<Face> results=new List<Face>(),faces;
                if (feature.Type == ObjectTypeEnum.kExtrudeFeatureObject)
                {
                    faces= InventorTool.GetCollectionFromIEnumerator<Face>(((ExtrudeFeature)feature).SideFaces.GetEnumerator());
                }
                else
                {
                    faces =  InventorTool.GetCollectionFromIEnumerator<Face>(feature.Faces.GetEnumerator());
                }
                foreach (var sub in faces)
                {
                    object obj;
                    occ.CreateGeometryProxy(sub, out obj);
                    results.Add((Face)obj);
                }
                return results;
            }
            else
            {
                foreach (ComponentOccurrence item in occ.SubOccurrences)
                {
                    List<Face> faces = GetSideFaces(item, name);
                    if (faces != null && faces.Count > 0)
                    {
                        List<Face> results = new List<Face>();
                        foreach (var sub in faces)
                        {
                            object obj;
                            item.CreateGeometryProxy(sub, out obj);
                            results.Add((Face)obj);
                        }
                       // occurrence = item;
                        return results;
                    }

                }
                return null;
            }


        }
        /// <summary>
        /// 根据名称获取配对
        /// </summary>
        /// <param name="occ"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        protected static iMateDefinition Getimate(ComponentOccurrence occ, string name)
        {
            if(occ.DefinitionDocumentType==DocumentTypeEnum.kPartDocumentObject)
            {
                List<iMateDefinition> list = InventorTool.GetCollectionFromIEnumerator<iMateDefinition>(occ.iMateDefinitions.GetEnumerator());
                return list.Where(a => a.Name == name).FirstOrDefault();
            }
            else
            {
                foreach (ComponentOccurrence item in occ.SubOccurrences)
                {
                    iMateDefinition mate = Getimate(item, name);
                    if(mate!=null)
                    {
                        return mate;
                    }
                }
                return null;
            }
           
        }
        /// <summary>
        /// 根据名称获取特性
        /// </summary>
        /// <param name="occ"></param>
        /// <param name="name"></param>
        /// <returns></returns>
       protected static T GetFeature<T>(ComponentOccurrence occ, string name,ObjectTypeEnum Type)
        {
            if (occ.DefinitionDocumentType == DocumentTypeEnum.kPartDocumentObject)
            {
              List<PartFeature> features= InventorTool.GetCollectionFromIEnumerator<PartFeature> (((PartComponentDefinition)occ.Definition).Features.GetEnumerator());
              PartFeature feature= features.Where(a => a.Name == name&&a.Type==Type).FirstOrDefault();
                if (feature != null) return (T)feature;
                return default(T);
            }
            else
            {

                foreach (ComponentOccurrence item in occ.SubOccurrences)
                {
                    T result = GetFeature<T>(item, name, Type);
                    if (result != null) return result;
                }
                return default(T);
            }
        }
      
        protected static T GetFeatureproxy<T>(ComponentOccurrence occ, string name, ObjectTypeEnum Type)
        {
            if (occ.DefinitionDocumentType == DocumentTypeEnum.kPartDocumentObject)
            {
                List<PartFeature> features = InventorTool.GetCollectionFromIEnumerator<PartFeature>(((PartComponentDefinition)occ.Definition).Features.GetEnumerator());
                PartFeature feature = features.Where(a => a.Name == name && a.Type == Type).FirstOrDefault();
                if (feature != null)
                {
                    object result;
                    occ.CreateGeometryProxy(feature, out result);
                    return (T)result;
                }
                return default(T);
            }
            else
            {

                foreach (ComponentOccurrence item in occ.SubOccurrences)
                {
                    T result= GetFeatureproxy<T>(item, name, Type);
                    if (result != null) return result;
                }
                return default(T);
            }
        }
        protected static List<T> GetFeatureproxys<T>(ComponentOccurrence occ, string name, ObjectTypeEnum Type)
        {
            List<T> results = new List<T>();
            if (occ.DefinitionDocumentType == DocumentTypeEnum.kPartDocumentObject)
            {
                List<PartFeature> features = InventorTool.GetCollectionFromIEnumerator<PartFeature>(((PartComponentDefinition)occ.Definition).Features.GetEnumerator());
                PartFeature feature = features.Where(a => a.Name == name && a.Type == Type).FirstOrDefault();
                if (feature != null)
                {
                    object result;
                    occ.CreateGeometryProxy(feature, out result);
                   results.Add( (T)result);
                }
                return results;
            }
            else
            {

                foreach (ComponentOccurrence item in occ.SubOccurrences)
                {
                    List<T> result = GetFeatureproxys<T>(item, name, Type);
                    if (result != null) results.AddRange(result);
                }
                return results;
            }
        }
        protected static OccStruct GetOccStruct(ComponentOccurrence occ, string name,int index)
        {
            OccStruct planeStruct = new OccStruct();
            planeStruct.Occurrence = occ;
           List< ExtrudeFeature> features = GetFeatureproxys<ExtrudeFeature>(occ, name, ObjectTypeEnum.kExtrudeFeatureObject);
            ExtrudeFeature feature;
            if (features.Count > index)
                feature = features[index];
            else return planeStruct;
            planeStruct.SideFaces = InventorTool.GetCollectionFromIEnumerator<Face>(feature.SideFaces.GetEnumerator());
            planeStruct.EndFace = InventorTool.GetFirstFromIEnumerator<Face>(feature.EndFaces.GetEnumerator());
            planeStruct.StartFace = InventorTool.GetFirstFromIEnumerator<Face>(feature.StartFaces.GetEnumerator());
            planeStruct.Part = (PartFeature)feature;
            return planeStruct;
        }
        protected void SaveDoc()
        {
            this.ModelPath = AppDomain.CurrentDomain.BaseDirectory + "Project\\" + this.Name + ".iam";
            Doc.FullFileName = ModelPath;
            if (System.IO.File.Exists(ModelPath))
            {
                System.IO.File.Delete(ModelPath);
            }
            Doc.Save2();
        }
        public virtual void DisPose()
        {
            //Doc = null;
            //Definition = null;
            freeiMates.Clear();
        }

        public struct OccStruct
        {
            public ComponentOccurrence Occurrence;
            public List<Face> SideFaces;
            public Face EndFace;
            public Face StartFace;
            public PartFeature Part;
        }
    }
}
