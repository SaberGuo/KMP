using Infranstructure.Tool;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Windows;

namespace KMP.Parameterization.InventorMonitor
{
    [Export(typeof(IInvMonitorController))]
    class InvMonitorController : IInvMonitorController
    {
        //private List<string, IInvMonitorViewModel>
        public void UpdateInvModel(string filePath)
        {
            IInvMonitorViewModel invMonVM = FindInvMonitorVM(filePath);
            if(invMonVM == null) {
                invMonVM = new InvMonitorViewModel(filePath);
                Application.Current.Dispatcher.Invoke(new Action<IInvMonitorViewModel>(OnAddDocument), invMonVM);
                //_documents.Add(invMonVM);
            }
            else
            {
                invMonVM.FilePath = filePath;
            }
            
            
        }

        public void UpdateAll()
        {
            foreach (var item in this._documents)
            {
                item.FilePath = item.FilePath;
            }
            captureImages();
        }
        private void OnAddDocument(IInvMonitorViewModel m)
        {
            _documents.Add(m);
            
        }
        private IInvMonitorViewModel FindInvMonitorVM(string filePath)
        {
            foreach (var item in this._documents)
            {
                if(item.FilePath == filePath)
                {
                    return item;
                }
            }
            return null;
        }
        private ObservableCollection<IInvMonitorViewModel> _documents = new ObservableCollection<IInvMonitorViewModel>();
        public ObservableCollection<IInvMonitorViewModel> Documents {
            get
            {
                return _documents;
            }
        }
        Inventor.Color top = InventorTool.Inventor.TransientObjects.CreateColor(255, 255, 255);
        Inventor.Color buttom = InventorTool.Inventor.TransientObjects.CreateColor(255, 255, 255);
        private void captureSingleViewImage(Inventor.Document doc, Inventor.ViewOrientationTypeEnum vot)
        {
            Inventor.View v = doc.Views[1];
            v.DisplayMode = Inventor.DisplayModeEnum.kShadedWithEdgesRendering;
            Inventor.Camera c = v.Camera;
            c.ViewOrientationType = vot;
            c.Fit();
            c.Apply();
            v.Update();
            c.SaveAsBitmap(doc.FullFileName + vot.ToString()+ ".bmp", 640, 480, top, buttom);
        }
        public void captureImages()
        {
            foreach (Inventor.Document doc in InventorTool.Inventor.Documents)
            {

                captureSingleViewImage(doc, Inventor.ViewOrientationTypeEnum.kFrontViewOrientation);
                captureSingleViewImage(doc, Inventor.ViewOrientationTypeEnum.kTopViewOrientation);
                captureSingleViewImage(doc, Inventor.ViewOrientationTypeEnum.kLeftViewOrientation);
                captureSingleViewImage(doc, Inventor.ViewOrientationTypeEnum.kIsoTopRightViewOrientation);

            }
            /*foreach (var item in _documents)
            {
                item.CaptureImage(item.FilePath+".bmp");
            }*/
        }
    }
}
