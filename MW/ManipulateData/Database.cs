using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Geoprocessing;

namespace MW.ManipulateData
{
    public partial class Database : Form
    {
        public Database()
        {
            InitializeComponent();
        }

        private void Database_Load(object sender, EventArgs e)
        {

        }

        private void btnListButton_Click(object sender, EventArgs e)
        {
            try
            {
                //Connect connect = new Connect("192.168.56.101", "192.168.56.101", "postgres", "postgres", "eauc", "SDE.Default");
                Connect connect = new Connect("localhost", "localhost", "sde", "sde", "eauc", "SDE.Default");
                /// <param name="server">For example, server = "Kona".</param>
                /// <param name="instance">Database = "SDE" or "" if Oracle.</param>
                /// <param name="user">Instance = "5151".</param>
                /// <param name="password">User = "vtest".</param>
                /// <param name="database">Password = "go".</param>
                /// <param name="version">Version = "SDE.DEFAULT".</param>
                /// 

                //Get the workspace object
                IWorkspace iWorkspace = connect.ConnectToTransactionalVersion();
                
                ListData listData = new ListData(iWorkspace);
                IGeoProcessor gp = new GeoProcessor();

                IGpEnumList datasets = listData.listDatasets(gp);

                //Check that the enumeration list is not null;
                if (datasets!=null)
                {
                    string dataset = datasets.Next();
                    while (dataset != "")
                    {
                        // Put the name of the dataset on the checked list box
                        this.clbDatasets.Items.Add(dataset, false);
                        // Set input raster dataset.
                        dataset = datasets.Next();
                    }                
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                IGeoProcessor gp = new GeoProcessor();
                ListData listData = new ListData();
                IGpEnumList datasets = listData.listDatasetsFGDB(gp);

                //Check that the enumeration list is not null;
                if (datasets != null)
                {
                    string dataset = datasets.Next();
                    while (dataset != "")
                    {
                        // Put the name of the dataset on the checked list box
                        this.clbDatasets.Items.Add(dataset,false);
                        // Set input raster dataset.
                        dataset = datasets.Next();
                    }
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

        private void clbDatasets_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //this.clbDatasets.
            // Determine if there are any items checked.

            if (clbDatasets.CheckedItems.Count != 0)
            {
                // If so, loop through all checked items and print results.

                string dataset = "";
                for (int x = 0; x <= clbDatasets.CheckedItems.Count - 1; x++)
                {

                    dataset = clbDatasets.CheckedItems[x].ToString();
                    rtbDatasetsToLoad.AppendText(dataset + "\r\n\t");

                    //raise a call to load data from the workspace
                    //loop through to load the database
                    IGeoProcessor gp = new GeoProcessor();
                    ListData listData = new ListData();
                    IGpEnumList fcs = listData.listFeatureClassesFGDB(gp, dataset);
                    
                    string fc = fcs.Next();

                    while (fc != "")
                    {
                        rtbDatasetsToLoad.AppendText(fc + "\r\n\t");
                        //Console.WriteLine(fc);
                        fc = fcs.Next();
                    }
                        
                }
                //MessageBox.Show(s);
            }

        }
    }
}
