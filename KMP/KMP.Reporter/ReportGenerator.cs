using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using MSWord = Microsoft.Office.Interop.Word;

namespace KMP.Reporter
{
    public class ReportGenerator
    {
        MSWord.Application wordApp;
        MSWord.Document wordDoc;//Word文档变量


        private object path;
        Object Nothing = Missing.Value;

        public object Path
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

        public ReportGenerator()
        {
            wordApp = new MSWord.ApplicationClass();//初始化
        }


        public void Generate()
        {
            wordDoc = wordApp.Documents.Open(path,
          ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing,
          ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing,
          ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing);

            object range = wordDoc.Paragraphs.Last.Range;
        }


        private void addPicture()
        {

        }

        private void addContainerInfo()
        {

        }

        private void addHeatSinkInfo()
        {

        }

        private void addVacuumInfo()
        {

        }

        private void addNitrogenInfo()
        {

        }
        
    }
}
