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
	public sealed class RemoveLayer : BaseCommand, MW.ContextMmenu.IRemoveLayer  
	{
		#region Member Variables
		private IMapControl3 m_mapControl; 
		#endregion

		#region Constructors
		/// <summary>
		/// Default Constructor
		/// </summary>
		public RemoveLayer()
		{
			base.m_caption = "Remove Layer";
		} 
		#endregion

		#region Getter and Setter Methods
		/// <summary>
		/// Get and set the map control
		/// </summary>
		public IMapControl3 getSetMapControl
		{
			get { return m_mapControl; }
			set { m_mapControl = value; }
		} 
		#endregion

		#region Overriden Methods
		/// <summary>
		/// Overridden Onclick event
		/// </summary>
		public override void OnClick()
		{
			ILayer layer = (ILayer)getSetMapControl.CustomProperty;
			getSetMapControl.Map.DeleteLayer(layer);
		}

		/// <summary>
		/// Overridden Oncreate event
		/// </summary>
		/// <param name="hook"></param>
		public override void OnCreate(object hook)
		{
			getSetMapControl = (IMapControl3)hook;
		} 
		#endregion

	}
}


