using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using MSWord = Microsoft.Office.Interop.Word;
using System.Text.RegularExpressions;
using KMP.Interface;
using ParamedModule.Container;
using ParamedModule.HeatSinkSystem;

namespace KMP.Reporter
{
    public class ReportGenerator
    {
        private object missing = System.Reflection.Missing.Value;
        WordHelper wdHelp;
        private string docPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "preview/word.doc");


        private string pattern_value = @"^value_\w*";
        private string pattern_pic = @"^pic_\w*";

        private string path;
       

        public string Path
        {
            get
            {
                return this.path;
            }
            set
            {
                this.path = value;
            }
        }

        private IParamedModule root;
        public IParamedModule Root
        {
            get
            {
                return this.root;
            }
            set
            {
                this.root = value;
            }
        }
        public ReportGenerator()
        {
            wdHelp = new WordHelper();
        }


        public string Generate()
        {
            wdHelp.wordApp = new Microsoft.Office.Interop.Word.ApplicationClass();
            System.IO.File.Copy(this.docPath, this.path,true);
            if(!wdHelp.OpenAndActive(this.path, false, false))
            {
                //error
                return null;
            }
            addPars();
            //addPics();
            wdHelp.SaveAs(path);
            wdHelp.Close();

            // wdHelp.WordDocument.Bookmarks
            return this.Path;
        }


        private void addPars()
        {
            foreach (MSWord.Bookmark bk in wdHelp.WordDocument.Bookmarks)
            {
                if(Regex.IsMatch(bk.Name, pattern_value))
                {
                    string[] pars = bk.Name.Split('_');
                    if (pars.Length >= 4)//"value_[moduleType]_[parName]_[displayName]"
                    {
                        IParamedModule module = Root.FindModule(pars[1]);
                        if(pars[2] == "cPar")
                        {
                            string value = module.GetValueByDisplayName(module, pars[4], pars[2], pars[3]);
                            //wdHelp.GoToBookMark(bk.Name);
                            //wdHelp.InsertText(value);
                            //bk.Range.InsertAfter(value);
                            wdHelp.WordDocument.Bookmarks[bk.Name].Range.InsertAfter(value);
                        }
                        if(pars[2] == "par")
                        {
                            string value = module.GetValueByDisplayName(module, pars[3], pars[2]);
                            //wdHelp.GoToBookMark(bk.Name);
                            //wdHelp.InsertText(value);
                            //bk.Range.InsertAfter(value);
                            wdHelp.WordDocument.Bookmarks[bk.Name].Range.InsertAfter(value);
                        }
                        
                    }
                }
            }
        }

        private void addPics()
        {
            foreach (MSWord.Bookmark bk in wdHelp.WordDocument.Bookmarks)
            {
                if (Regex.IsMatch(bk.Name, pattern_pic))
                {
                    string[] pars = bk.Name.Split('_');
                    if (pars.Length >= 3)//"pic_[moduleType]_[orientType]"
                    {
                        IParamedModule module = Root.FindModule(pars[1]);
                        string picPath = module.GetPicByOrient(module, pars[2]);
                        wdHelp.GoToBookMark(bk.Name);
                        wdHelp.InsertPic(picPath);
                    }
                }
            }

        }


        private void addHoleTable()
        {
            MSWord.Table newTable;
            MSWord.Range tableRange = wdHelp.wordDoc.Bookmarks["hole_table"].Range;
            Cylinder cy = Root.FindModule("CY") as Cylinder;
            
            int rowCount = cy.par.ParHoles.Count;
            int colCount = 6;//序号，孔号，通经，用途，位置，备注
            wdHelp.GoToBookMark("hole_table");
            newTable = wdHelp.AddTable(rowCount+1,colCount);
            newTable.Cell(0, 0).Range.Text = "序号";
            newTable.Cell(0, 1).Range.Text = "孔号";
            newTable.Cell(0, 2).Range.Text = "通经";
            newTable.Cell(0, 3).Range.Text = "用途";
            newTable.Cell(0, 4).Range.Text = "位置";
            newTable.Cell(0, 5).Range.Text = "备注";

            for (int i = 0; i < rowCount; i++)
            {
                newTable.Cell(i + 1, 0).Range.Text = i.ToString();
                newTable.Cell(i + 1, 1).Range.Text = cy.par.ParHoles[i].Name;
                newTable.Cell(i + 1, 2).Range.Text = "DN" + ((int)cy.par.ParHoles[i].FlanchDN).ToString();
            }

        }

        private void addHeatSinkTable()
        {
            HeatSink hs = Root.FindModule("HeatSink") as HeatSink;
            int count = hs.cPar.noumenonMaterials.Count;
            for (int i = 0; i < count; i++)
            {
                wdHelp.GoToBookMark("heatSink_table_num_" + (i + 1).ToString());
                wdHelp.InsertText(hs.cPar.noumenonMaterials[i].num.ToString());
                wdHelp.GoToBookMark("heatSink_table_weight_" + (i + 1).ToString());
                wdHelp.InsertText(hs.cPar.noumenonMaterials[i].weight.ToString());
            }

            count = hs.cPar.frontCapMaterials.Count;
            for (int i = 0; i < count; i++)
            {
                wdHelp.GoToBookMark("heatSink_table_fc_num_" + (i + 1).ToString());
                wdHelp.InsertText(hs.cPar.frontCapMaterials[i].num.ToString());
                wdHelp.GoToBookMark("heatSink_table_fc_weight" + (i + 1).ToString());
                wdHelp.InsertText(hs.cPar.frontCapMaterials[i].weight.ToString());
            }

            count = hs.cPar.capMaterials.Count;
            for (int i = 0; i < count; i++)
            {
                wdHelp.GoToBookMark("heatSink_table_c_num_" + (i + 1).ToString());
                wdHelp.InsertText(hs.cPar.capMaterials[i].num.ToString());
                wdHelp.GoToBookMark("heatSink_table_c_weight" + (i + 1).ToString());
                wdHelp.InsertText(hs.cPar.capMaterials[i].weight.ToString());
            }


        }
        
    }
}
