using System;
namespace MW.ManipulateData
{
    interface IMyTOCClass
    {
        ESRI.ArcGIS.Carto.ILayer getSelectedLayerInTOC();
        ESRI.ArcGIS.Carto.ILayer getSelectedLayerInTOC(ESRI.ArcGIS.Controls.ITOCControl2 iTOCControl2);
        ESRI.ArcGIS.Controls.ITOCControl2 getSetITOCControl2 { get; set; }
    }
}
