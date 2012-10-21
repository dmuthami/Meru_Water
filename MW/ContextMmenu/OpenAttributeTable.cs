// Copyright 2012 ESRI
// 
// All rights reserved under the copyright laws of the United States
// and applicable international laws, treaties, and conventions.
// 
// You may freely redistribute and use this sample code, with or
// without modification, provided you include the original copyright
// notice and use restrictions.
// 
// See the use restrictions at <your ArcGIS install location>/DeveloperKit10.1/userestrictions.txt.
// 

using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;

namespace MW.ContextMmenu
{
	public sealed class OpenAttributeTable : BaseCommand, MW.ContextMmenu.IOpenAttributeTable  
	{

		#region Member Variables
		private IMapControl3 m_mapControl;
		private ITOCControl2 m_iTOCControl2 = null; 
		#endregion

		#region Getter and Setter Methods
		/// <summary>
		/// Get and set map control
		/// </summary>
		public IMapControl3 getSetMapControl
		{
			get { return m_mapControl; }
			set { m_mapControl = value; }
		}

		/// <summary>
		/// Get and set toc control
		/// </summary>
		public ITOCControl2 getSetITOCControl2
		{
			get { return m_iTOCControl2; }
			set { m_iTOCControl2 = value; }
		} 
		#endregion

		#region Methods
		/// <summary>
		/// Overloaded  OpenAttributeTable Constructor class
		/// </summary>
		/// <param name="iTOCControl2"> pass by reference the toc control</param>
		public OpenAttributeTable(ITOCControl2 iTOCControl2)
		{
			base.m_caption = "Open Attribute Table";
			getSetITOCControl2 = iTOCControl2;
		}

		/// <summary>
		/// Overloaded  OpenAttributeTable Constructor class
		/// </summary>
		public OpenAttributeTable()
		{
			base.m_caption = "Open Attribute Table";
		}
		
		#endregion	

		#region Overridden Methods
		/// <summary>
		/// Click handler event on the context menu
		/// </summary>
		public override void OnClick()
		{
			MyEventHandler myEventHandler = new MyEventHandler();
			myEventHandler.getSetITOCControl2 = getSetITOCControl2;

			myEventHandler.showAttributeForm();

			//MW.ManipulateData.MyTOCClass myTOCClass;

			//myTOCClass = new MW.ManipulateData.MyTOCClass(getSetITOCControl2);

			//MW.ManipulateData.AttributeTable attributeTable = new ManipulateData.AttributeTable(myTOCClass.getSelectedLayerInTOC());
			//attributeTable.ShowDialog();
		}

		/// <summary>
		/// The hooker
		/// </summary>
		/// <param name="hook"></param>
		public override void OnCreate(object hook)
		{
			getSetMapControl = (IMapControl3)hook;
		} 
		#endregion
	
	}
}

