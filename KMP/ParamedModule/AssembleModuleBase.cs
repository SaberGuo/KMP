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
        protected static void SetFlushiMate(ComponentOccurrence co1,object entity1, ComponentOccurrence co2, object entity2,string name,double offset)
        {
            FlushiMateDefinition flush1 = ((PartComponentDefinition)co1.Definition).iMateDefinitions.AddFlushiMateDefinition(entity1, offset + "mm");
            FlushiMateDefinition flush2 = ((PartComponentDefinition)co2.Definition).iMateDefinitions.AddFlushiMateDefinition(entity2, offset + "mm");
            flush1.Name = name;
            flush2.Name = name;

        }
        protected static void SetMateiMate(ComponentOccurrence co1, object entity1, ComponentOccurrence co2, object entity2, string name, double offset)
        {
            MateiMateDefinition flush1 = ((PartComponentDefinition)co1.Definition).iMateDefinitions.AddMateiMateDefinition(entity1, offset + "mm");
            MateiMateDefinition flush2 = ((PartComponentDefinition)co2.Definition).iMateDefinitions.AddMateiMateDefinition(entity2, offset + "mm");
            flush1.Name = name;
            flush2.Name = name;

        }
        protected static  List<Face> GetSideFaces(ComponentOccurrence occ,string name)
        {
            List<PartFeature> features = InventorTool.GetCollectionFromIEnumerator<PartFeature>(((PartComponentDefinition)occ.Definition).Features.GetEnumerator());
            PartFeature feature = features.Where(a => a.Name == name).FirstOrDefault();
            if(feature.Type==ObjectTypeEnum.kExtrudeFeatureObject)
            {
                return InventorTool.GetCollectionFromIEnumerator<Face>(((ExtrudeFeature)feature).SideFaces.GetEnumerator());
            }
            else
            {
                return InventorTool.GetCollectionFromIEnumerator<Face>(feature.Faces.GetEnumerator());
            }
           
        }
        protected void SaveDoc()
        {

        }
    }
}
