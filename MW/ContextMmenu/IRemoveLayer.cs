using System;
namespace MW.ContextMmenu
{
    interface IRemoveLayer
    {
        ESRI.ArcGIS.Controls.IMapControl3 getSetMapControl { get; set; }
        void OnClick();
        void OnCreate(object hook);
    }
}
