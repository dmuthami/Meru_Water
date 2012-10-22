using System;
namespace MW.Common
{
    interface ICreateNewDocument
    {
        /// <summary>
        /// 
        /// </summary>
        ESRI.ArcGIS.Controls.IHookHelper getSetHookHelper { get; set; }
        void OnClick();
        void OnCreate(object hook);
    }
}
