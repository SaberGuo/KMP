using Infranstructure;
using Inventor;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KMP.Parameterization.InventorMonitor
{
    class InvMonitorViewModel: NotificationObject, IInvMonitorViewModel
    {
        public InvMonitorViewModel()
        {
            _oserver = new ApprenticeServerComponent();
            AddInventorPath();
            _odocument = null;
            _oview = null;
            _ocamera = null;
            _odrawingDocument = null;
        }

        
        private Int32 _HWnd;

        public Int32 HWnd
        {
            get
            {
                return this._HWnd;
            }
            set
            {
                this._HWnd = value;
            }
        }

        private string _fileName;
        public string FileName
        {
            get
            {
                return this._fileName;
            }
            set
            {
                this._fileName = value;
                RaisePropertyChanged(()=>this.FileName);
            }
        }

        private string _filePath;
        public string FilePath
        {
            get
            {
                return this._filePath;
            }
            set
            {
                if (this._filePath != "")
                {
                    this.CloseDocument();
                }
                this._filePath = value;
                this.OpenDocument();


            }
        }

        #region Apprentice
        private ApprenticeServerComponent _oserver;
        private ApprenticeServerDocument _odocument;
        private ApprenticeServerDrawingDocument _odrawingDocument;
        private ClientView _oview;
        private Camera _ocamera;
        private Point2d _previousPoint;
        private bool _bmouseDown;
        private bool _rmouseDown;

        private void AddInventorPath()
        {
            // Note: System.Environment.Is64BitProcess and
            // System.Environment.Is64BitProcess were
            // introduced in .NET Framework 4.0
            string path = System.Environment.GetEnvironmentVariable("PATH");

            // In case process and OS bitness match it's
            // C:\Program Files\Autodesk\Inventor 2015
            // otherwise it's
            // C:\Program Files\Autodesk\Inventor 2015\bin
            string inventorPath = _oserver.InstallPath;

            if (System.Environment.Is64BitOperatingSystem &&
                !System.Environment.Is64BitProcess)
            {
                // If you are running the app as a 32 bit process 
                // on a 64 bit OS then you'll need this
                path += ";" + inventorPath + "Bin32";
            }
            else
            {
                // Otherwise you need this
                path += ";" + inventorPath + "Bin";
            }

            System.Environment.SetEnvironmentVariable("PATH", path);
        }


        private void CloseDocument()
        {
            if(_odocument != null){
                _odocument.Close();
                _odocument = null;
            }
            if (_odrawingDocument != null)
            {
                _odrawingDocument.Close();
                _odrawingDocument = null;
            }

        }

        private void OpenDocument()
        {
            
            if (_oserver == null)
            {
                throw new ArgumentNullException("oserver isnot initialized");
            }
            if(_HWnd == 0){
                throw new ArgumentNullException("hwnd isnot initialized");
            }

            _odocument = _oserver.Open(this._filePath);
            if(_odocument.DocumentType == DocumentTypeEnum.kDrawingDocumentObject)
            {
                _odrawingDocument = (Inventor.ApprenticeServerDrawingDocument)_odocument;
                _oview = _odrawingDocument.Sheets[1].ClientViews.Add(this.HWnd);
                _ocamera = _oview.Camera;
                _ocamera.Fit();
                _ocamera.Apply();
                _ocamera.Perspective = false;
            }
            else
            {
                _oview = _odocument.ClientViews.Add(this.HWnd);
                _ocamera = _oview.Camera;
                _ocamera.Fit();
                _ocamera.Apply();
                _ocamera.ViewOrientationType = ViewOrientationTypeEnum.kDefaultViewOrientation;
            }

        }

        #endregion
        #region event handler
        public void OnMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && _oserver != null)
            {
                _bmouseDown = true;
                _previousPoint = _oserver.TransientGeometry.CreatePoint2d(e.X, e.Y);
            }

        }
        public void OnMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (_oview != null)
            {
                _oview.Update(false);
                _bmouseDown = false;
                _rmouseDown = false;
            }
        }

        public void OnMouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (_bmouseDown == true && _oserver != null && _oview != null)
            {
                _ocamera = _oview.Camera;
                Point2d opoint = _oserver.TransientGeometry.CreatePoint2d(e.X, e.Y);
                _ocamera.ComputeWithMouseInput(_previousPoint, opoint, 0, ViewOperationTypeEnum.kRotateViewOperation);

                _previousPoint = opoint;
                _ocamera.Apply();
                _oview.Update(true);
            }
        }

        public void OnMouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (_oserver != null && _oview != null)
            {
                _ocamera = _oview.Camera;
                _ocamera.Fit();
                _ocamera.Apply();
                _oview.Update(true);
            }
        }

        public void OnSizeChanged(object sender, EventArgs e)
        {
            if (_oserver != null && _oview != null)
            {
                _oview.Update(false);
            }
        }
        #endregion



    }
}
