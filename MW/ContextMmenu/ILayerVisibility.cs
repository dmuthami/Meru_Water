using System;
namespace MW.ContextMmenu
{
    interface ILayerVisibility
    {
        string Caption { get; }
        bool Enabled { get; }
        int GetCount();
        ESRI.ArcGIS.Controls.IHookHelper getSetHookHelper { get; set; }
        long getSetSubType { get; set; }
        void OnClick();
        void OnCreate(object hook);
        void SetSubType(int SubType);
    }
}
