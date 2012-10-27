using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;

namespace MW.ManipulateData
{
    public class Connect
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Connect()
        {
            //ToDo code
        }
        /// <summary>
        /// Overloaded constructor
        /// </summary>
        /// <param name="server"></param>
        /// <param name="instance"></param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="database"></param>
        /// <param name="version"></param>
        public Connect(String server, String instance,
            String user, String password, String database, String version)
        {
            getSetServer = server;
            getSetInstance = instance;
            getSetUser = user;
            getSetPassword = password;
            getSetDatabase = database;
            getSetVersion = version;
        }

        private String m_Server;

        public String getSetServer
        {
            get { return m_Server; }
            set { m_Server = value; }
        }
       
        private String m_Instance;

        public String getSetInstance
        {
            get { return m_Instance; }
            set { m_Instance = value; }
        }
        
        private String m_User;

        public String getSetUser
        {
            get { return m_User; }
            set { m_User = value; }
        }
        
        private String m_Password;

        public String getSetPassword
        {
            get { return m_Password; }
            set { m_Password = value; }
        }
        
        private String m_Database;

        public String getSetDatabase
        {
            get { return m_Database; }
            set { m_Database = value; }
        }
        
        private String m_Version;

        public String getSetVersion
        {
            get { return m_Version; }
            set { m_Version = value; }
        }

        /// <summary>
        /// Connects to a transactional instance of a an enterprise geodatabase
        /// </summary>
        /// <param name="server">For example, server = "Kona".</param>
        /// <param name="instance">Database = "SDE" or "" if Oracle.</param>
        /// <param name="user">Instance = "5151".</param>
        /// <param name="password">User = "vtest".</param>
        /// <param name="database">Password = "go".</param>
        /// <param name="version">Version = "SDE.DEFAULT".</param>
        /// <returns></returns>
        public IWorkspace ConnectToTransactionalVersion(String server, String instance, String user, String password, String database, String version){
            try
            {
                IPropertySet propertySet = new PropertySetClass();
                propertySet.SetProperty("SERVER", server);
                //propertySet.SetProperty("INSTANCE", instance);
                propertySet.SetProperty("DATABASE", database);
                propertySet.SetProperty("USER", user);
                propertySet.SetProperty("PASSWORD", password);
                propertySet.SetProperty("VERSION", version);

                Type factoryType = Type.GetTypeFromProgID(
                    "esriDataSourcesGDB.SdeWorkspaceFactory");
                IWorkspaceFactory2 workspaceFactory = (IWorkspaceFactory2)Activator.CreateInstance
                    (factoryType);
                return workspaceFactory.Open(propertySet, 0);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        /// <summary>
        /// Overloaded arguments without arguments
        /// </summary>
        /// <returns></returns>
        public IWorkspace ConnectToTransactionalVersion() {
            try
            {
                IPropertySet propertySet = new PropertySetClass();
                propertySet.SetProperty("SERVER", getSetServer);
                //propertySet.SetProperty("DB_CONNECTION_PROPERTIES", DBConnProp);
                //propertySet.SetProperty("INSTANCE", getSetInstance);
                propertySet.SetProperty("DATABASE", getSetDatabase);
                propertySet.SetProperty("USER", getSetUser);
                propertySet.SetProperty("PASSWORD", getSetPassword);
                propertySet.SetProperty("VERSION", getSetVersion);

                Type factoryType = Type.GetTypeFromProgID(
                    "esriDataSourcesGDB.SdeWorkspaceFactory");
                IWorkspaceFactory workspaceFactory = (IWorkspaceFactory)Activator.CreateInstance
                    (factoryType);
                return workspaceFactory.Open(propertySet, 0);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
