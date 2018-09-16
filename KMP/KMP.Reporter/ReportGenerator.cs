using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using MSWord = Microsoft.Office.Interop.Word;
using System.Text.RegularExpressions;
using KMP.Interface;

namespace KMP.Reporter
{
    public class ReportGenerator
    {
        private object missing = System.Reflection.Missing.Value;
        WordHelper wdHelp;
        private string docPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "preview/word.doc");


        private string pattern_value = @"^value_\w*";

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


        public void Generate()
        {
            if(!wdHelp.OpenAndActive(this.docPath, false, false))
            {
                //error
                return;
            }
            addPars();
            addPics();
            wdHelp.SaveAs(Path);
           // wdHelp.WordDocument.Bookmarks

        }


        private void addPars()
        {
            foreach (MSWord.Bookmark bk in wdHelp.WordDocument.Bookmarks)
            {
                if(Regex.IsMatch(bk.Name, pattern_value))
                {
                    string[] pars = bk.Name.Split('_');
                    if (pars.Length == 4)
                    {
                        IParamedModule module = Root.FindModule(pars[1]);
                        string value  = module.GetValueByDisplayName(module, pars[3], pars[2]);
                        bk.Range.InsertAfter(value);
                    }
                }
            }
        }

        private void addPics()
        {

        }
        
    }
}
