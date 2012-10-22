using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using MW.ContextMmenu;
using System.Runtime.InteropServices;
using MW.Common;

namespace MW
{
    public partial class frmMain : Form
    {

        #region class private members
        private ITOCControl2 m_tocControl;
        private IMapControl3 m_mapControl = null;
        private IToolbarMenu m_menuMap;
        private IToolbarMenu m_menuLayer;
        private string m_mapDocumentName = string.Empty;
        #endregion

        #region class constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public frmMain()
        {
            InitializeComponent();
        }       
        #endregion

        #region Getter and Setter Methods 

        /// <summary>
        /// get and set by reference the toc control
        /// </summary>
        public ITOCControl2 getSetTocControl
        {
            get { return m_tocControl; }
            set { m_tocControl = value; }
        }

        /// <summary>
        /// get and set by reference the map control
        /// </summary>
        public IMapControl3 getSetMapControl
        {
            get { return m_mapControl; }
            set { m_mapControl = value; }
        }

        /// <summary>
        /// get and set by reference the Menu Map 
        /// </summary>
        public IToolbarMenu getSetMenuMap
        {
            get { return m_menuMap; }
            set { m_menuMap = value; }
        }

        /// <summary>
        /// get and set by reference a pointer to current map document
        /// </summary>
        public string getSetMapDocumentName
        {
            get { return m_mapDocumentName; }
            set { m_mapDocumentName = value; }
        }

        /// <summary>
        /// get and set by reference a pointer to the Menu layer
        /// </summary>
        public IToolbarMenu getSetMenuLayer
        {
            get { return m_menuLayer; }
            set { m_menuLayer = value; }
        }

        #endregion

        #region Main Menu event handlers
        
        /// <summary>
        /// Event handler for creating a new document
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuNewDoc_Click(object sender, EventArgs e)
        {
            //execute New Document command
            ICreateNewDocument iCreateNewDocument = new CreateNewDocument ();
            ICommand command = (ICommand)iCreateNewDocument;
            command.OnCreate(getSetMapControl.Object);
            command.OnClick();
        }

        /// <summary>
        /// Event handler for opening an existing document
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuOpenDoc_Click(object sender, EventArgs e)
        {
            //execute Open Document command
            ICommand command = new ControlsOpenDocCommandClass();
            command.OnCreate(getSetMapControl.Object);
            command.OnClick();
        }

        /// <summary>
        /// Event handler for saving a document
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuSaveDoc_Click(object sender, EventArgs e)
        {
            //execute Save Document command
            if (getSetMapControl.CheckMxFile(getSetMapDocumentName))
            {
                //create a new instance of a MapDocument
                IMapDocument mapDoc = new MapDocumentClass();
                mapDoc.Open(getSetMapDocumentName, string.Empty);

                //Make sure that the MapDocument is not readonly
                if (mapDoc.get_IsReadOnly(getSetMapDocumentName))
                {
                    MessageBox.Show("Map document is read only!");
                    mapDoc.Close();
                    return;
                }

                //Replace its contents with the current map
                mapDoc.ReplaceContents((IMxdContents)getSetMapControl.Map);

                //save the MapDocument in order to persist it
                mapDoc.Save(mapDoc.UsesRelativePaths, false);

                //close the MapDocument
                mapDoc.Close();
            }
        }

        /// <summary>
        /// Event handler for saving a new document
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuSaveAs_Click(object sender, EventArgs e)
        {
            //execute SaveAs Document command
            ICommand command = new ControlsSaveAsDocCommandClass();
            command.OnCreate(getSetMapControl.Object);
            command.OnClick();
        }

        /// <summary>
        /// Closes the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuExitApp_Click(object sender, EventArgs e)
        {
            //exit the application
            Application.Exit();
        }
   
        #endregion

        #region Form Event Handlers
        
        /// <summary>
        /// Form load event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                //reference existing map ctrl and toc ctrl
                getSetTocControl = (ITOCControl2)axTOCControl1.Object;
                getSetMapControl = (IMapControl3)axMapControl1.Object;

                //Set buddy control
                getSetTocControl.SetBuddyControl(m_mapControl);
                axToolbarControl1.SetBuddyControl(m_mapControl);

                //Add pre-defined control commands to the ToolbarControl
                axToolbarControl1.AddItem("esriControls.ControlsSelectFeaturesTool", -1, 0, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
                axToolbarControl1.AddToolbarDef("esriControls.ControlsMapNavigationToolbar", 0, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
                axToolbarControl1.AddItem("esriControls.ControlsOpenDocCommand", -1, 0, false, 0, esriCommandStyles.esriCommandStyleIconOnly);

                //Add custom commands to the map menu
                getSetMenuMap = new ToolbarMenuClass();
                getSetMenuMap.AddItem(new LayerVisibility(), 1, 0, false, esriCommandStyles.esriCommandStyleTextOnly);
                getSetMenuMap.AddItem(new LayerVisibility(), 2, 1, false, esriCommandStyles.esriCommandStyleTextOnly);
                //Add pre-defined menu to the map menu as a sub menu 
                getSetMenuMap.AddSubMenu("esriControls.ControlsFeatureSelectionMenu", 2, true);
                //Add custom commands to the map menu
                getSetMenuLayer = new ToolbarMenuClass();
                getSetMenuLayer.AddItem(new RemoveLayer(), -1, 0, false, esriCommandStyles.esriCommandStyleTextOnly);
                getSetMenuLayer.AddItem(new ScaleThresholds(), 1, 1, true, esriCommandStyles.esriCommandStyleTextOnly);
                getSetMenuLayer.AddItem(new ScaleThresholds(), 2, 2, false, esriCommandStyles.esriCommandStyleTextOnly);
                getSetMenuLayer.AddItem(new ScaleThresholds(), 3, 3, false, esriCommandStyles.esriCommandStyleTextOnly);
                getSetMenuLayer.AddItem(new LayerSelectable(), 1, 4, true, esriCommandStyles.esriCommandStyleTextOnly);
                getSetMenuLayer.AddItem(new LayerSelectable(), 2, 5, false, esriCommandStyles.esriCommandStyleTextOnly);
                getSetMenuLayer.AddItem(new ZoomToLayer(), -1, 6, true, esriCommandStyles.esriCommandStyleTextOnly);

                //Add Open attribute table command
                getSetMenuLayer.AddItem(new OpenAttributeTable(getSetTocControl), -1, 7, true, esriCommandStyles.esriCommandStyleTextOnly);
                
                //Set the hook of each menu
                getSetMenuLayer.SetHook(getSetMapControl);
                getSetMenuMap.SetHook(getSetMapControl);
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
        /// On map replaced event for the map control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void axMapControl1_OnMapReplaced(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMapReplacedEvent e)
        {
            try
            {
                //get the current document name from the MapControl
                getSetMapDocumentName = getSetMapControl.DocumentFilename;

                //if there is no MapDocument, diable the Save menu and clear the statusbar
                if (getSetMapDocumentName == string.Empty)
                {
                    menuSaveDoc.Enabled = false;
                    statusBarXY.Text = string.Empty;
                }
                else
                {
                    //enable the Save manu and write the doc name to the statusbar
                    menuSaveDoc.Enabled = true;
                    statusBarXY.Text = Path.GetFileName(getSetMapDocumentName);
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
        /// Mouse move event for the map control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void axMapControl1_OnMouseMove(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseMoveEvent e)
        {
            try
            {
            statusBarXY.Text = string.Format("{0}, {1}  {2}", e.mapX.ToString("#######.##"), 
                e.mapY.ToString("#######.##"), axMapControl1.MapUnits.ToString().Substring(4));
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
        /// Mouse down event for the TOC control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void axTOCControl1_OnMouseDown(object sender, ITOCControlEvents_OnMouseDownEvent e)
        {
            try
            {
                //right click button
                if (e.button != 2) return;

                esriTOCControlItem item = esriTOCControlItem.esriTOCControlItemNone;
                IBasicMap map = null; ILayer layer = null;
                object other = null; object index = null;

                //Determine what kind of item is selected
                getSetTocControl.HitTest(e.x, e.y, ref item, ref map, ref layer, ref other, ref index);

                //Ensure the item gets selected 
                if (item == esriTOCControlItem.esriTOCControlItemMap)
                    getSetTocControl.SelectItem(map, null);
                else
                    getSetTocControl.SelectItem(layer, null);

                //Set the layer into the CustomProperty (this is used by the custom layer commands)			
                getSetMapControl.CustomProperty = layer;

                //Popup the correct context menu
                if (item == esriTOCControlItem.esriTOCControlItemMap) getSetMenuMap.PopupMenu(e.x, e.y, getSetTocControl.hWnd);
                if (item == esriTOCControlItem.esriTOCControlItemLayer) getSetMenuLayer.PopupMenu(e.x, e.y, getSetTocControl.hWnd);
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
        /// Show the attribute window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuAttributeTable_Click(object sender, EventArgs e)
        {
            try
            {
                MyEventHandler myEventHandler = new MyEventHandler();
                myEventHandler.getSetITOCControl2 = getSetTocControl;

                myEventHandler.showAttributeForm();
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

        #region snippets

        //try
        //{

        //}
        //catch (COMException COMex)
        //{
        //    MessageBox.Show("Error " + COMex.ErrorCode.ToString() + ": " + COMex.Message);

        //}
        //catch (Exception ex)
        //{
        //    MessageBox.Show("Error: " + ex.Message);
        //} 
        #endregion

    }
}
