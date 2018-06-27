using Infranstructure.Models;
using KMP.Interface;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace KMP.DatabaseBrowser
{
    [Export]
    class BrowserViewModel : NotificationObject
    {
        private IDatabaseService _databaseService;
        private List<Project> _projs;
        private Project _selProj;
        [ImportingConstructor]
        public BrowserViewModel(IDatabaseService databaseService)
        {
            this._databaseService = databaseService;
            BrowserInit();
        }

        public List<Project> Projs
        {
            get { return this._projs; }
            set
            {
                this._projs = value;
                this.RaisePropertyChanged(() => this.Projs);
            }
        }
        private List<Comment> _comments;
        public List<Comment> Comments
        {
            get
            {
                return this._comments;
            }
            set
            {
                this._comments = value;
                this.RaisePropertyChanged(() => this.Comments);
            }
        }
        public Project SeledProj
        {
            get { return this._selProj; }
            set
            {
                this._selProj = value;
                this.SelProjChanged();
                this.RaisePropertyChanged(() => this.SeledProj);
            }
        }

        private void SelProjChanged()
        {
            this.Comments = _databaseService.GetComments(this._selProj);
        }
        private void BrowserInit()
        {
            this.Projs = _databaseService.GetProjs();
        }

        public void ProjectTypeChanged(string projType)
        {
            this.Projs = _databaseService.GetProjs(projType);
        }

    }
}
