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
         public frmMain()
        {
            InitializeComponent();
        }       
        #endregion

        #region Main Menu event handlers

        private void menuNewDoc_Click(object sender, EventArgs e)
        {
            //execute New Document command
            
            ICommand command = new  CreateNewDocument();
            command.OnCreate(m_mapControl.Object);
            command.OnClick();
        }

        private void menuOpenDoc_Click(object sender, EventArgs e)
        {
            //execute Open Document command
            ICommand command = new ControlsOpenDocCommandClass();
            command.OnCreate(m_mapControl.Object);
            command.OnClick();
        }

        private void menuSaveDoc_Click(object sender, EventArgs e)
        {
            //execute Save Document command
            if (m_mapControl.CheckMxFile(m_mapDocumentName))
            {
                //create a new instance of a MapDocument
                IMapDocument mapDoc = new MapDocumentClass();
                mapDoc.Open(m_mapDocumentName, string.Empty);

                //Make sure that the MapDocument is not readonly
                if (mapDoc.get_IsReadOnly(m_mapDocumentName))
                {
                    MessageBox.Show("Map document is read only!");
                    mapDoc.Close();
                    return;
                }

                //Replace its contents with the current map
                mapDoc.ReplaceContents((IMxdContents)m_mapControl.Map);

                //save the MapDocument in order to persist it
                mapDoc.Save(mapDoc.UsesRelativePaths, false);

                //close the MapDocument
                mapDoc.Close();
            }
        }

        private void menuSaveAs_Click(object sender, EventArgs e)
        {
            //execute SaveAs Document command
            ICommand command = new ControlsSaveAsDocCommandClass();
            command.OnCreate(m_mapControl.Object);
            command.OnClick();
        }

        private void menuExitApp_Click(object sender, EventArgs e)
        {
            //exit the application
            Application.Exit();
        }
   
        #endregion

        #region Form Event Handlers
        
        private void frmMain_Load(object sender, EventArgs e)
        {
            //reference existing map ctrl and toc ctrl
            m_tocControl = (ITOCControl2)axTOCControl1.Object;
            m_mapControl = (IMapControl3)axMapControl1.Object;

            //Set buddy control
            m_tocControl.SetBuddyControl(m_mapControl);
            axToolbarControl1.SetBuddyControl(m_mapControl);

            //Add pre-defined control commands to the ToolbarControl
            axToolbarControl1.AddItem("esriControls.ControlsSelectFeaturesTool", -1, 0, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddToolbarDef("esriControls.ControlsMapNavigationToolbar", 0, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("esriControls.ControlsOpenDocCommand", -1, 0, false, 0, esriCommandStyles.esriCommandStyleIconOnly);

            //Add custom commands to the map menu
            m_menuMap = new ToolbarMenuClass();
            m_menuMap.AddItem(new LayerVisibility(), 1, 0, false, esriCommandStyles.esriCommandStyleTextOnly);
            m_menuMap.AddItem(new LayerVisibility(), 2, 1, false, esriCommandStyles.esriCommandStyleTextOnly);
            //Add pre-defined menu to the map menu as a sub menu 
            m_menuMap.AddSubMenu("esriControls.ControlsFeatureSelectionMenu", 2, true);
            //Add custom commands to the map menu
            m_menuLayer = new ToolbarMenuClass();
            m_menuLayer.AddItem(new RemoveLayer(), -1, 0, false, esriCommandStyles.esriCommandStyleTextOnly);
            m_menuLayer.AddItem(new ScaleThresholds(), 1, 1, true, esriCommandStyles.esriCommandStyleTextOnly);
            m_menuLayer.AddItem(new ScaleThresholds(), 2, 2, false, esriCommandStyles.esriCommandStyleTextOnly);
            m_menuLayer.AddItem(new ScaleThresholds(), 3, 3, false, esriCommandStyles.esriCommandStyleTextOnly);
            m_menuLayer.AddItem(new LayerSelectable(), 1, 4, true, esriCommandStyles.esriCommandStyleTextOnly);
            m_menuLayer.AddItem(new LayerSelectable(), 2, 5, false, esriCommandStyles.esriCommandStyleTextOnly);
            m_menuLayer.AddItem(new ZoomToLayer(), -1, 6, true, esriCommandStyles.esriCommandStyleTextOnly);

            //Set the hook of each menu
            m_menuLayer.SetHook(m_mapControl);
            m_menuMap.SetHook(m_mapControl);

        }
        
        private void axMapControl1_OnMapReplaced(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMapReplacedEvent e)
        {

            //get the current document name from the MapControl
            m_mapDocumentName = m_mapControl.DocumentFilename;

            //if there is no MapDocument, diable the Save menu and clear the statusbar
            if (m_mapDocumentName == string.Empty)
            {
                menuSaveDoc.Enabled = false;
                statusBarXY.Text = string.Empty;
            }
            else
            {
                //enable the Save manu and write the doc name to the statusbar
                menuSaveDoc.Enabled = true;
                statusBarXY.Text = Path.GetFileName(m_mapDocumentName);
            }

        }

        private void axMapControl1_OnMouseMove(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseMoveEvent e)
        {
            statusBarXY.Text = string.Format("{0}, {1}  {2}", e.mapX.ToString("#######.##"), e.mapY.ToString("#######.##"), axMapControl1.MapUnits.ToString().Substring(4));
        }
        
        private void axTOCControl1_OnMouseDown(object sender, ITOCControlEvents_OnMouseDownEvent e)
                {

                    if (e.button != 2) return;

                    esriTOCControlItem item = esriTOCControlItem.esriTOCControlItemNone;
                    IBasicMap map = null; ILayer layer = null;
                    object other = null; object index = null;

                    //Determine what kind of item is selected
                    m_tocControl.HitTest(e.x, e.y, ref item, ref map, ref layer, ref other, ref index);

                    //Ensure the item gets selected 
                    if (item == esriTOCControlItem.esriTOCControlItemMap)
                        m_tocControl.SelectItem(map, null);
                    else
                        m_tocControl.SelectItem(layer, null);

                    //Set the layer into the CustomProperty (this is used by the custom layer commands)			
                    m_mapControl.CustomProperty = layer;

                    //Popup the correct context menu
                    if (item == esriTOCControlItem.esriTOCControlItemMap) m_menuMap.PopupMenu(e.x, e.y, m_tocControl.hWnd);
                    if (item == esriTOCControlItem.esriTOCControlItemLayer) m_menuLayer.PopupMenu(e.x, e.y, m_tocControl.hWnd);

                }
        
        #endregion     
   
    }
}
