using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;

namespace MW.ManipulateData
{
    public class MyTOCClass : MW.ManipulateData.IMyTOCClass
    {

        #region Member Variables
        private ITOCControl2 m_ITOCControl2 = null; 
        #endregion

        #region Constructor Methods
        /// <summary>
        /// Default Constructor
        /// </summary>
        public MyTOCClass()
        {
            //TODO List
        }

        /// <summary>
        /// Overloaded constructor
        /// </summary>
        /// <param name="iTOCControl2">pass by ref the toccontol </param>
        public MyTOCClass(ITOCControl2 iTOCControl2)
        {
            m_ITOCControl2 = iTOCControl2;
        }

        /// <summary>
        /// Getter and Setter method for member variable m_ITOCControl2
        /// </summary>
        public ITOCControl2 getSetITOCControl2
        {
            get { return m_ITOCControl2; }
            set { m_ITOCControl2 = value; }
        } 
        #endregion

        #region Methods
        /// <summary>
        /// Get Selected layer from table of contents
        /// </summary>
        /// <param name="iTOCControl2"> pass by ref the toccontol </param>
        /// <returns></returns>
        public ILayer getSelectedLayerInTOC(ITOCControl2 iTOCControl2)
        {
            try
            {
                return getIlayer(iTOCControl2);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Get Selected layer from table of contents
        /// </summary>
        /// <returns></returns>
        public ILayer getSelectedLayerInTOC()
        {
            try
            {
                return getIlayer(m_ITOCControl2);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Get Ilayer from TOC Control
        /// </summary>
        /// <param name="iTOCControl2">pass by ref the toccontol</param>
        /// <returns></returns>
        private ILayer getIlayer(ITOCControl2 iTOCControl2)
        {
            try
            {
                ILayer iLayer = null;
                IBasicMap map = null;
                ILayer layer = null;
                Object other = null;
                Object index = null;
                esriTOCControlItem item = esriTOCControlItem.esriTOCControlItemNone;

                iTOCControl2.GetSelectedItem(ref item, ref map, ref layer, ref other, ref index);

                //Attempt a cast
                iLayer = layer as ESRI.ArcGIS.Carto.ILayer;

                //return layer of type ilayer
                return iLayer;
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        #endregion    
    }
}
