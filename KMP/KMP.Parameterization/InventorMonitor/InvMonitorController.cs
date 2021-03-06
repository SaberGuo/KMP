﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

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
                _documents.Add(invMonVM);
            }
            else
            {
                invMonVM.FilePath = filePath;
            }
            
            
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
    }
}
