using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Carto;

namespace MW.ManipulateData
{
    public partial class AttributeTable : Form
    {
        #region Private Members
        /// <summary>
        /// This is used to bind our ITable to the binding source. We need to keep
        /// a reference to it as we will need to re-attach it to the binding source
        /// to force a refresh whenever we change from displaying coded value domain
        /// values to displaying their text equivalents and vice versa.
        /// </summary>
        /// 

        private ArcDataBinding.TableWrapper tableWrapper;

        /// <summary>
        /// This binding object sets the data member within the data source for the 
        /// text box. We need to keep a reference to this as it needs to be reset
        /// whenever viewing of coded value domains is changed.
        /// </summary>
        private Binding txtBoxBinding;

        private ILayer m_iLayer = null;

        #endregion Private Members

        #region Constructor Methods
        /// <summary>
        /// Default Constructor
        /// </summary>
        public AttributeTable()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Overloaded Constructor method
        /// </summary>
        /// <param name="iLayer"> Argument is passed by reference for layer file selected</param>
        public AttributeTable(ILayer iLayer)
        {
            InitializeComponent();

            //intialize variables
            m_iLayer = iLayer;
        }
        
        #endregion

        #region Form Event Handler Methods
        /// <summary>
        /// Switch on or off to activate or deactivate editing in domain mode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkDomain_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                // Set usage of CV domain on or off
                tableWrapper.UseCVDomains = chkDomain.Checked;

                // Refresh the binding source by setting it to null and then rebinding
                // to the data. This will refresh all the field types and ensures that all
                // the fields are using the correct type converters (we will need different
                // type converters depending on whether we are showing cv domain values or
                // their text equivalents). Note that as we will be setting the binding source
                // to null, the text box binding will fail as the "FULL_NAME" field will no
                // longer be present. To prevent any problems here, we need to remove and
                // reset the text box's binding.

                bindingSource1.DataSource = null;
                bindingSource1.DataSource = tableWrapper;

                /*
                 * Comment textbox binding
                 * 
                 * textBox1.DataBindings.Clear();
                 * textBox1.DataBindings.Add(txtBoxBinding);
                 */
                dtgAttributeTable.Refresh();
            }
            catch (COMException COMex)
            {
                MessageBox.Show("Error " + COMex.ErrorCode.ToString() + ": " + COMex.Message);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        
        #endregion

        #region Methods
        /// <summary>
        /// This method gets the selectd layer, casts it into a feature layer 
        /// Therefater accesses the table and binds it to a .NET DataGrid View Control
        /// </summary>
        public void bindTable()
        {
            try
            {
                IFeatureLayer iFeatureLayer = (IFeatureLayer)m_iLayer;

                //open the Geodatabase table
                ITable foundITable = iFeatureLayer.FeatureClass as ITable;

                if (null != foundITable)
                {
                    // Bind dataset to the binding source
                    tableWrapper = new ArcDataBinding.TableWrapper(foundITable);
                    bindingSource1.DataSource = tableWrapper;

                    // Bind binding source to grid. Alternatively it is possible to bind TableWrapper
                    // directly to the grid to this offers less flexibility
                    dtgAttributeTable.DataSource = bindingSource1;

                    // Bind binding source to text box, we are binding the NAME
                    // field.
                    //txtBoxBinding = new Binding("Text", bindingSource1, "FACILITYNAME");
                    //textBox1.DataBindings.Add(txtBoxBinding);
                }
            }
            catch (COMException COMex)
            {
                MessageBox.Show("Error " + COMex.ErrorCode.ToString() + ": " + COMex.Message);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }


        }

        /// <summary>
        /// Form load event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AttributeTable_Load(object sender, EventArgs e)
        {
            try
            {
                bindTable();
            }
            catch (COMException COMex)
            {
                MessageBox.Show("Error " + COMex.ErrorCode.ToString() + ": " + COMex.Message);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        
        #endregion    
    }
}
