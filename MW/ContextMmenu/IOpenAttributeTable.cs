using System;
namespace MW.ContextMmenu
{
    interface IOpenAttributeTable
    {
        ESRI.ArcGIS.Controls.ITOCControl2 getSetITOCControl2 { get; set; }
        ESRI.ArcGIS.Controls.IMapControl3 getSetMapControl { get; set; }
        void OnClick();
        void OnCreate(object hook);
    }
}
