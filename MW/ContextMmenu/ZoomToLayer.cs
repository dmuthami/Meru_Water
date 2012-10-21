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
	public sealed class ZoomToLayer : BaseCommand, MW.ContextMmenu.IZoomToLayer  
	{
		#region Member Variables
		private IMapControl3 m_mapControl; 
		#endregion

		#region Constructor and Destructor Methods
		/// <summary>
		/// Default constructor
		/// </summary>
		public ZoomToLayer()
		{
			base.m_caption = "Zoom To Layer";
		} 
		#endregion

		#region Getter and Setter Method
		/// <summary>
		/// Get and set the map control
		/// </summary>
		public IMapControl3 getSetMapControl
		{
			get { return m_mapControl; }
			set { m_mapControl = value; }
		} 
		#endregion

		#region Overridden Methods
		/// <summary>
		/// Overide base OnClick Method
		/// </summary>
		public override void OnClick()
		{
			ILayer layer = (ILayer)m_mapControl.CustomProperty;
			m_mapControl.Extent = layer.AreaOfInterest;
		}

		/// <summary>
		/// Overide base OnCreate Method
		/// </summary>
		/// <param name="hook"> Pass hook as argument</param>
		public override void OnCreate(object hook)
		{
			m_mapControl = (IMapControl3)hook;
		} 
		#endregion
	}
}

