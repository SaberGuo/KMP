using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KMP.Interface.Model;
using KMP.Interface.Model.Container;
using Infranstructure.Tool;
using Inventor;
using KMP.Interface;
using System.ComponentModel.Composition;
namespace ParamedModule.Container
{
    [Export(typeof(IParamedModule))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public  class Cylinder: PartModulebase
    {
       internal ParCylinder par=new ParCylinder();
        [ImportingConstructor]
        public Cylinder():base()
        {
            this.Parameter = par;
            init();
        }
        private void init()
        {
            par.InRadius = 100;
            par.CapRadius = 70;
            par.Thickness = 2;
            par.Length = 500;
            par.RibWidth = 2;
            par.RibHeight = 2;
            par.RibBraceHeight = 1;
            par.RibBraceWidth = 1;
            par.RibNumber = 3;
            par.RibFirstDistance = 50;
            par.FlanchWidth = 4;
        }
        public override void CreateModule()
        {
            CreateDoc();
          RevolveFeature cyling= CreateCyling();
            cyling.Name = "Cylinder";
            List<Face> sideFace = InventorTool.GetCollectionFromIEnumerator<Face>(cyling.SideFaces.GetEnumerator());
            WorkAxis Axis = Definition.WorkAxes.AddByRevolvedFace(sideFace[0]);
            Axis.Visible = false;
            Definition.iMateDefinitions.AddMateiMateDefinition(Axis, 0).Name = "mateH";
            //Definition.iMateDefinitions.AddMateiMateDefinition(Axis, 0).Name = "mateM";
            Definition.iMateDefinitions.AddMateiMateDefinition(sideFace[4],0).Name = "mateK";
            //Definition.iMateDefinitions.AddMateiMateDefinition(sideFace[0], 0).Name = "mateI";
       
        }

        private RevolveFeature CreateCyling()
        {
            PlanarSketch osketch = Definition.Sketches.Add(Definition.WorkPlanes[3]);
            SketchEllipticalArc Arc1, Arc2;
            SketchLine Line1, Line2;
            CreateLines(osketch, out Arc1, out Line1, par.CapRadius, par.InRadius, par.Length);

            //SketchLine line5= offsetLine<SketchLine>(osketch, Line1, 2, true);
            //SketchOffsetSpline arc5 = (SketchOffsetSpline)offsetLine(osketch, Arc1, 2, true);
            //SketchLine Line3 = osketch.SketchLines.AddByTwoPoints(Arc1.StartSketchPoint, arc5.StartSketchPoint);
            //SketchLine Line4 = osketch.SketchLines.AddByTwoPoints(Line1.EndSketchPoint, line5.EndSketchPoint);
            //osketch.GeometricConstraints.AddHorizontal((SketchEntity)Line3);
            //osketch.GeometricConstraints.AddVertical((SketchEntity)Line4);
            //osketch.GeometricConstraints.AddEqualLength(Line3, Line4);
            //osketch.DimensionConstraints.AddEllipseRadius((SketchEntity)Arc1, true, InventorTool.TranGeo.CreatePoint2d(-parCylinder.CapRadius / 2, 0));
            //osketch.DimensionConstraints.AddEllipseRadius((SketchEntity)Arc1, false, InventorTool.TranGeo.CreatePoint2d(0, -parCylinder.InRadius / 2));
            //Point2d p = InventorTool.TranGeo.CreatePoint2d((Line1.StartSketchPoint.Geometry.X + Line1.EndSketchPoint.Geometry.X) / 2, (Line1.StartSketchPoint.Geometry.Y + Line1.EndSketchPoint.Geometry.Y) / 2 + 1);
            //osketch.DimensionConstraints.AddTwoPointDistance(Line1.StartSketchPoint, Line1.EndSketchPoint, DimensionOrientationEnum.kAlignedDim, p);
            //p = InventorTool.TranGeo.CreatePoint2d((Line4.StartSketchPoint.Geometry.X + Line4.EndSketchPoint.Geometry.X) / 2 + 1, (Line4.StartSketchPoint.Geometry.Y + Line4.EndSketchPoint.Geometry.Y) / 2);
            //osketch.DimensionConstraints.AddTwoPointDistance(Line4.StartSketchPoint, Line4.EndSketchPoint, DimensionOrientationEnum.kAlignedDim, p);

            CreateLines(osketch, out Arc2, out Line2, par.CapRadius + par.Thickness, par.InRadius + par.Thickness, par.Length);
            SketchLine Line3 = osketch.SketchLines.AddByTwoPoints(Arc1.StartSketchPoint, Arc2.StartSketchPoint);
            SketchLine Line4 = osketch.SketchLines.AddByTwoPoints(Line1.EndSketchPoint, Line2.EndSketchPoint);

            osketch.GeometricConstraints.AddHorizontalAlign(Arc1.StartSketchPoint, Arc1.CenterSketchPoint);
            osketch.GeometricConstraints.AddVerticalAlign(Arc1.EndSketchPoint, Arc1.CenterSketchPoint);
            osketch.GeometricConstraints.AddHorizontal((SketchEntity)Line3);
            osketch.GeometricConstraints.AddVertical((SketchEntity)Line4);
            osketch.GeometricConstraints.AddEqualLength(Line3, Line4);
            osketch.GeometricConstraints.AddConcentric((SketchEntity)Arc1, (SketchEntity)Arc2);
            // osketch.GeometricConstraints.AddCoincident((SketchEntity)InventorTool.Origin, (SketchEntity)Arc1.CenterSketchPoint);
            osketch.DimensionConstraints.AddEllipseRadius((SketchEntity)Arc1, true, InventorTool.TranGeo.CreatePoint2d(-par.CapRadius / 2, 0));
            osketch.DimensionConstraints.AddEllipseRadius((SketchEntity)Arc1, false, InventorTool.TranGeo.CreatePoint2d(0, -par.InRadius / 2));
            Point2d p = InventorTool.TranGeo.CreatePoint2d((Line1.StartSketchPoint.Geometry.X + Line1.EndSketchPoint.Geometry.X) / 2, (Line1.StartSketchPoint.Geometry.Y + Line1.EndSketchPoint.Geometry.Y) / 2 + 1);
            osketch.DimensionConstraints.AddTwoPointDistance(Line1.StartSketchPoint, Line1.EndSketchPoint, DimensionOrientationEnum.kAlignedDim, p);
            p = InventorTool.TranGeo.CreatePoint2d((Line4.StartSketchPoint.Geometry.X + Line4.EndSketchPoint.Geometry.X) / 2 + 1, (Line4.StartSketchPoint.Geometry.Y + Line4.EndSketchPoint.Geometry.Y) / 2);
            osketch.DimensionConstraints.AddTwoPointDistance(Line4.StartSketchPoint, Line4.EndSketchPoint, DimensionOrientationEnum.kAlignedDim, p);

            CreateRibs(osketch, Line2);
            SketchEntitiesEnumerator entities = InventorTool.CreateRangle(osketch, par.Thickness, par.FlanchWidth);
            List<SketchLine> lines = InventorTool.GetCollectionFromIEnumerator<SketchLine>(entities.GetEnumerator());
            osketch.GeometricConstraints.AddCollinear((SketchEntity)lines[3], (SketchEntity)Line4);
            ObjectCollection objc = InventorTool.CreateObjectCollection();
            lines.ForEach(a => objc.Add(a));
            osketch.MoveSketchObjects(objc, lines[3].StartSketchPoint.Geometry.VectorTo(Line4.StartSketchPoint.Geometry));
            Profile profile = osketch.Profiles.AddForSolid();
            foreach (ProfilePath item in profile)
            {
                foreach (ProfileEntity sub in item)
                {
                    if (sub.SketchEntity == Arc1||sub.SketchEntity==lines[2] || sub.SketchEntity == lines[1])
                    {
                        item.AddsMaterial = false;
                    }
                    else
                    {
                        item.AddsMaterial = true;
                    }
                }
            }
            RevolveFeature revolve = Definition.Features.RevolveFeatures.AddFull(profile, Line3, PartFeatureOperationEnum.kNewBodyOperation);
            PlanarSketch sketch1 = Definition.Sketches.Add(Definition.WorkPlanes[3]);
            sketch1.AddByProjectingEntity(Arc1);
            sketch1.AddByProjectingEntity(Arc2);
            sketch1.AddByProjectingEntity(Line1);
            sketch1.AddByProjectingEntity(Line2);
            sketch1.AddByProjectingEntity(lines[0]);
            sketch1.AddByProjectingEntity(lines[1]);
            sketch1.AddByProjectingEntity(lines[2]);
            sketch1.AddByProjectingEntity(lines[3]);
            SketchEntity ProjectiongLine3 = sketch1.AddByProjectingEntity(Line3);
            sketch1.AddByProjectingEntity(Line4);
            Profile profile1 = sketch1.Profiles.AddForSolid();
         return   Definition.Features.RevolveFeatures.AddFull(profile1, ProjectiongLine3, PartFeatureOperationEnum.kNewBodyOperation);
        }

        /// <summary>
        /// 创建椭圆线段和直线段
        /// </summary>
        /// <param name="osketch"></param>
        /// <param name="oEllipticalArc"></param>
        /// <param name="oLine"></param>
        /// <param name="RadiusX"></param>
        /// <param name="RadiusY"></param>
        /// <param name="Length"></param>
        private void CreateLines(PlanarSketch osketch,out SketchEllipticalArc oEllipticalArc, out SketchLine oLine,double RadiusX,double RadiusY,double Length)
        {
             oEllipticalArc = osketch.SketchEllipticalArcs.Add(InventorTool.Origin, InventorTool.Left, RadiusX, RadiusY, 0, Math.PI / 2);
             oLine = osketch.SketchLines.AddByTwoPoints(oEllipticalArc.EndSketchPoint.Geometry, InventorTool.TranGeo.CreatePoint2d(Length, oEllipticalArc.EndSketchPoint.Geometry.Y));
            osketch.GeometricConstraints.AddHorizontal((SketchEntity)oLine);
            osketch.GeometricConstraints.AddCoincident((SketchEntity)oEllipticalArc, (SketchEntity)oLine.StartSketchPoint);
            osketch.GeometricConstraints.AddCoincident((SketchEntity)oEllipticalArc.EndSketchPoint, (SketchEntity)oLine);
            osketch.GeometricConstraints.AddTangent((SketchEntity)oEllipticalArc, (SketchEntity)oLine);

            //osketch.DimensionConstraints.AddArcLength((SketchEntity)oEllipticalArc, InventorTool.Origin);
        }
        /// <summary>
        /// 偏移
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="osketch"></param>
        /// <param name="source"></param>
        /// <param name="distance"></param>
        /// <param name="Direction"></param>
        /// <returns></returns>
        private T offsetLine<T>(PlanarSketch osketch,T source,double distance,bool Direction)
        {
            ObjectCollection obj = InventorTool.Inventor.TransientObjects.CreateObjectCollection();
            obj.Add(source);
          SketchEntitiesEnumerator Entities=  osketch.OffsetSketchEntitiesUsingDistance(obj, distance, Direction,true,true);
          return  InventorTool.GetFirstFromIEnumerator<T>(Entities.GetEnumerator());
        }
        /// <summary>
        /// 创建多个加强筋
        /// </summary>
        /// <param name="osketch"></param>
        /// <param name="line"></param>
        private void CreateRibs(PlanarSketch osketch,SketchLine line)
        {
            double distance = (par.Length - par.RibFirstDistance) / par.RibNumber ;
            for (int i = 0; i < par.RibNumber; i++)
            {
                SketchLine L;
                CreateRib(osketch,out L);
                osketch.GeometricConstraints.AddCollinear((SketchEntity)line, (SketchEntity)L);
                CreateTwoPointDistanceConstraint(osketch, line.EndSketchPoint, L.EndSketchPoint, distance*i+par.RibFirstDistance);
              
            }
        }
        /// <summary>
        /// 创建单个加强筋
        /// </summary>
        /// <param name="osketch"></param>
        /// <param name="L"></param>
        private void CreateRib(PlanarSketch osketch,out SketchLine L)
        {
            List<SketchLine> Lines = new List<SketchLine>();
            #region
            Point2d p1 = InventorTool.TranGeo.CreatePoint2d(3, 3);
            Point2d p2 = InventorTool.TranGeo.CreatePoint2d(3, 2);
            Point2d p3 = InventorTool.TranGeo.CreatePoint2d(2, 2);
            Point2d p4 = InventorTool.TranGeo.CreatePoint2d(2, 1);
            Point2d p5 = InventorTool.TranGeo.CreatePoint2d(3, 1);
            Point2d p6 = InventorTool.TranGeo.CreatePoint2d(3, 0);
            Point2d p7 = InventorTool.TranGeo.CreatePoint2d(0, 0);
            Point2d p8 = InventorTool.TranGeo.CreatePoint2d(0, 1);
            Point2d p9 = InventorTool.TranGeo.CreatePoint2d(1, 1);
            Point2d p10 = InventorTool.TranGeo.CreatePoint2d(1, 2);
            Point2d p11 = InventorTool.TranGeo.CreatePoint2d(0, 2);
            Point2d p12 = InventorTool.TranGeo.CreatePoint2d(0, 3);

            L = osketch.SketchLines.AddByTwoPoints(p12, p1);
            SketchLine L1 = osketch.SketchLines.AddByTwoPoints(p1, p2);
            SketchLine L2 = osketch.SketchLines.AddByTwoPoints(p2, p3);
            SketchLine L3 = osketch.SketchLines.AddByTwoPoints(p3, p4);
            SketchLine L4 = osketch.SketchLines.AddByTwoPoints(p4, p5);
            SketchLine L5 = osketch.SketchLines.AddByTwoPoints(p5, p6);
            SketchLine L6 = osketch.SketchLines.AddByTwoPoints(p6, p7); //总宽
            SketchLine L7 = osketch.SketchLines.AddByTwoPoints(p7, p8);
            SketchLine L8 = osketch.SketchLines.AddByTwoPoints(p8, p9);
            SketchLine L9 = osketch.SketchLines.AddByTwoPoints(p9, p10);
            SketchLine L10 = osketch.SketchLines.AddByTwoPoints(p10, p11);
            SketchLine L11 = osketch.SketchLines.AddByTwoPoints(p11, p12);
            Lines.Add(L);
            Lines.Add(L1);
            Lines.Add(L2);
            Lines.Add(L3);
            Lines.Add(L4);
            Lines.Add(L5);
            Lines.Add(L6);
            Lines.Add(L7);
            Lines.Add(L8);
            Lines.Add(L9);
            Lines.Add(L10);
            Lines.Add(L11);
            #endregion
           InventorTool.CreateTwoPointCoinCident(osketch, L11, L);
            for (int i = 1; i < Lines.Count; i++)
            {
               InventorTool.CreateTwoPointCoinCident(osketch, Lines[i], Lines[i - 1]);
                osketch.GeometricConstraints.AddPerpendicular((SketchEntity)Lines[i], (SketchEntity)Lines[i - 1]);
            }

            osketch.GeometricConstraints.AddEqualLength(L2, L4);
            osketch.GeometricConstraints.AddEqualLength(L1, L5);
            osketch.GeometricConstraints.AddEqualLength(L8, L10);
            osketch.GeometricConstraints.AddEqualLength(L7, L11);
            osketch.GeometricConstraints.AddEqualLength(L2, L8);
            osketch.GeometricConstraints.AddEqualLength(L1, L11);

            CreateTwoPointDistanceConstraint(osketch, L6.StartSketchPoint, L6.EndSketchPoint, par.RibWidth);
            CreateTwoPointDistanceConstraint(osketch, L6.EndSketchPoint, L11.EndSketchPoint, par.RibHeight);
            CreateTwoPointDistanceConstraint(osketch, L3.StartSketchPoint, L3.EndSketchPoint, par.RibBraceHeight);
            CreateTwoPointDistanceConstraint(osketch, L2.EndSketchPoint, L10.StartSketchPoint, par.RibBraceWidth);
        }
        /// <summary>
        /// 创建两点间距离约束
        /// </summary>
        /// <param name="osketch"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="value"></param>
        void CreateTwoPointDistanceConstraint(PlanarSketch osketch,SketchPoint p1,SketchPoint p2,double value)
        {
               Point2d p = InventorTool.TranGeo.CreatePoint2d((p1.Geometry.X + p2.Geometry.X) / 2+1, (p1.Geometry.Y + p2.Geometry.Y) / 2+1);
            TwoPointDistanceDimConstraint Constraint1 = osketch.DimensionConstraints.AddTwoPointDistance(p1, p2, DimensionOrientationEnum.kAlignedDim, p);
            Constraint1.Parameter.Value = value;
        }

        public override bool CheckParamete()
        {
            throw new NotImplementedException();
        }
        ///// <summary>
        ///// 创建两条线关联
        ///// </summary>
        ///// <param name="osketch"></param>
        ///// <param name="line1">开始点</param>
        ///// <param name="line2">结束点</param>
        //void CreateTwoPointCoinCident(PlanarSketch osketch,SketchLine line1,SketchLine line2)
        //{
        //    if(line1.StartSketchPoint.Geometry.X==line2.StartSketchPoint.Geometry.X&& line1.StartSketchPoint.Geometry.Y == line2.StartSketchPoint.Geometry.Y)
        //    {
        //        osketch.GeometricConstraints.AddCoincident((SketchEntity)line1.StartSketchPoint, (SketchEntity)line2);
        //        osketch.GeometricConstraints.AddCoincident((SketchEntity)line1, (SketchEntity)line2.StartSketchPoint);
        //    }
        //    else if(line1.EndSketchPoint.Geometry.X == line2.StartSketchPoint.Geometry.X && line1.EndSketchPoint.Geometry.Y == line2.StartSketchPoint.Geometry.Y)
        //    {
        //        osketch.GeometricConstraints.AddCoincident((SketchEntity)line1.EndSketchPoint, (SketchEntity)line2);
        //        osketch.GeometricConstraints.AddCoincident((SketchEntity)line1, (SketchEntity)line2.StartSketchPoint);
        //    }
        //    else if(line1.StartSketchPoint.Geometry.X == line2.EndSketchPoint.Geometry.X && line1.StartSketchPoint.Geometry.Y == line2.EndSketchPoint.Geometry.Y)
        //    {
        //        osketch.GeometricConstraints.AddCoincident((SketchEntity)line1.StartSketchPoint, (SketchEntity)line2);
        //        osketch.GeometricConstraints.AddCoincident((SketchEntity)line1, (SketchEntity)line2.EndSketchPoint);
        //    }
        //    else
        //    {
        //        osketch.GeometricConstraints.AddCoincident((SketchEntity)line1.EndSketchPoint, (SketchEntity)line2);
        //        osketch.GeometricConstraints.AddCoincident((SketchEntity)line1, (SketchEntity)line2.EndSketchPoint);
        //    }


        //}
    }
}
