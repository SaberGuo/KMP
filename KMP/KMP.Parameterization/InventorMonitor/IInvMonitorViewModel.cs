using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KMP.Parameterization.InventorMonitor
{
    public interface IInvMonitorViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        string FileName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        string FilePath { get; set; }

        Int32 HWnd { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnMouseDown(object sender, System.Windows.Forms.MouseEventArgs e);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnMouseUp(object sender, System.Windows.Forms.MouseEventArgs e);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnMouseMove(object sender, System.Windows.Forms.MouseEventArgs e);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnMouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e);

        void OnSizeChanged(object sender, EventArgs e);
    }
}
