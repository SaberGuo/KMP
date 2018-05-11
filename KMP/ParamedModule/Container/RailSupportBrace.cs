﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KMP.Interface.Model.Container;
using KMP.Interface.Model;
using Infranstructure.Tool;
using Inventor;
using KMP.Interface;
using System.ComponentModel.Composition;

namespace ParamedModule.Container
{
    /// <summary>
    /// 导轨支架支撑
    /// </summary>

  public  class RailSupportBrace : PartModulebase
    {
        internal ParRailSupportBrace par = new ParRailSupportBrace();
        [ImportingConstructor]
        public RailSupportBrace():base()
        {
            this.Parameter = par;
            init();
        }
        void init()
        {
            par.Height = 245;
            par.InRadius = 35;
            par.Thickness = 15;
        }

        public override void CreateModule()
        {
            CreateDoc();
            PlanarSketch osketch = Definition.Sketches.Add(Definition.WorkPlanes[3]);
          ExtrudeFeature cyling=  CreateCyling(osketch, UsMM(par.InRadius), UsMM(par.Thickness), UsMM(par.Height));
            cyling.Name = "Brace";
            Face sideface = InventorTool.GetFirstFromIEnumerator<Face>(cyling.SideFaces.GetEnumerator());
            WorkAxis axis = Definition.WorkAxes.AddByRevolvedFace(sideface);
            axis.Name = "BraceAxis";
            axis.Visible = false;
           

            SetMate(cyling);
            SaveDoc();
        }
        /// <summary>
        /// 创建筒状体
        /// </summary>
        /// <param name="osketch"></param>
        /// <param name="inRadius"></param>
        /// <param name="thickness"></param>
        /// <param name="height"></param>
        private ExtrudeFeature CreateCyling(PlanarSketch osketch,double inRadius,double thickness,double height)
        {
            SketchCircle cir1 = osketch.SketchCircles.AddByCenterRadius(InventorTool.Origin, inRadius);
            SketchCircle cir2 = osketch.SketchCircles.AddByCenterRadius(InventorTool.Origin, inRadius + thickness);
            
            osketch.GeometricConstraints.AddConcentric((SketchEntity)cir1, (SketchEntity)cir2);
            Profile pro = osketch.Profiles.AddForSolid();
            foreach (ProfilePath item in pro)
            {
                if (item.Count == 1)
                {
                    item.AddsMaterial = false;
                }
                else
                {
                    item.AddsMaterial = true;
                }
            }
            ExtrudeDefinition extrudedef = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(pro, PartFeatureOperationEnum.kNewBodyOperation);
            extrudedef.SetDistanceExtent(height , PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            ExtrudeFeature cylinder = Definition.Features.ExtrudeFeatures.Add(extrudedef);
            PlanarSketch holeSketch = Definition.Sketches.Add(Definition.WorkPlanes[2]);
            holeSketch.SketchCircles.AddByCenterRadius(InventorTool.TranGeo.CreatePoint2d(0, height / 2), 0.8);
            Profile holePro = holeSketch.Profiles.AddForSolid();
            ExtrudeDefinition holeDef = Definition.Features.ExtrudeFeatures.CreateExtrudeDefinition(holePro, PartFeatureOperationEnum.kCutOperation);
            holeDef.SetThroughAllExtent(PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            Definition.Features.ExtrudeFeatures.Add(holeDef);
            return cylinder;
        }
        private void SetMate(ExtrudeFeature cyling)
        {

            Face endFace = InventorTool.GetFirstFromIEnumerator<Face>(cyling.EndFaces.GetEnumerator());
            Face startFace = InventorTool.GetFirstFromIEnumerator<Face>(cyling.StartFaces.GetEnumerator());
            MateiMateDefinition mateC = Definition.iMateDefinitions.AddMateiMateDefinition(startFace, 0);
            mateC.Name = "mateC";
            MateiMateDefinition mateD = Definition.iMateDefinitions.AddMateiMateDefinition(endFace, 0);
            mateD.Name = "mateD";

        }
        public override bool CheckParamete()
        {
          return   CommonTool.CheckParameterValue(par);
        }
    }
}
