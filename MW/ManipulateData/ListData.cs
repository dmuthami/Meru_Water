using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.DataManagementTools;
using ESRI.ArcGIS.Geodatabase;

namespace MW.ManipulateData
{
    public class ListData
    {
        IWorkspace m_iWorkspace;

        public IWorkspace getSetIWorkspace
        {
            get { return m_iWorkspace; }
            set { m_iWorkspace = value; }
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public ListData()
        {
            //To Do List
        }

        /// <summary>
        /// Overloaded Constructor
        /// </summary>
        /// <param name="iWorkspace">Argument of type workspace</param>
        public ListData(IWorkspace iWorkspace)
        {
            try
            {
                getSetIWorkspace = iWorkspace;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public void listFeatureClasses(IGeoProcessor iGeoProcessor)
        {
            // List all feature classes in the workspace starting with d.
            iGeoProcessor.SetEnvironmentValue("workspace", @"C:\Uc2003\Portland_OR.gdb");
            IGpEnumList fcs = iGeoProcessor.ListFeatureClasses("d*", "", "");
            string fc = fcs.Next();

            while (fc != "")
            {

                Console.WriteLine(fc);
                fc = fcs.Next();

            }
        }

        public IGpEnumList listFeatureClassesFGDB(IGeoProcessor iGeoProcessor,string dataset)
        {
            try
            {
                // List all feature classes in the workspace starting with d.
                iGeoProcessor.SetEnvironmentValue("workspace", @"E:\gisdata\FM\GIS\mewasco\datasource\MERU.gdb");
                IGpEnumList fcs = iGeoProcessor.ListFeatureClasses("*", "", dataset);
                return fcs;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public void listRasters(Geoprocessor GP) {

            // List all TIFF files in the workspace and build pyramids.
            GP.SetEnvironmentValue("workspace", @"C:\Corey\Ccm\results");
            IGpEnumList rasters = GP.ListRasters("*", "TIFF");
            string raster = rasters.Next();

            // Intialize the BuildPyramids tool.
            BuildPyramids pyramids = new BuildPyramids();

            while (raster != "")
            {

                // Set input raster dataset.
                pyramids.in_raster_dataset = raster;
                GP.Execute(pyramids, null);
                raster = rasters.Next();
            }

        }

        public IGpEnumList listDatasets(IGeoProcessor iGeoProcessor)
        {
            try
            {
                // List all TIFF files in the workspace and build pyramids.
                iGeoProcessor.SetEnvironmentValue("workspace", getSetIWorkspace.ToString());
                IGpEnumList datasets = iGeoProcessor.ListDatasets("*", "Feature");
                return datasets;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public IGpEnumList listDatasetsFGDB(IGeoProcessor iGeoProcessor)
        {
            try
            {
                // List all TIFF files in the workspace and build pyramids.
                iGeoProcessor.SetEnvironmentValue("workspace", @"E:\gisdata\FM\GIS\mewasco\datasource\MERU.gdb");
                IGpEnumList datasets = iGeoProcessor.ListDatasets("*", "All");
                return datasets;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
