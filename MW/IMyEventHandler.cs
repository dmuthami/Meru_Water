using System;
namespace MW
{
    interface IMyEventHandler
    {
        ESRI.ArcGIS.Controls.ITOCControl2 getSetITOCControl2 { get; set; }
        void showAttributeForm();
    }
}
