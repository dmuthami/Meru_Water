using System;
namespace MW.ContextMmenu
{
    interface ILayerSelectable
    {
        string Caption { get; }
        bool Enabled { get; }
        int GetCount();
        ESRI.ArcGIS.Controls.IMapControl3 getSetMapControl { get; set; }
        long getSetSubType { get; set; }
        void OnClick();
        void OnCreate(object hook);
        void SetSubType(int SubType);
    }
}
