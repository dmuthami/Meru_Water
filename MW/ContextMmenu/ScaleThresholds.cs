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
using ESRI.ArcGIS.SystemUI;

namespace MW.ContextMmenu
{
	public class ScaleThresholds : BaseCommand, ICommandSubType, MW.ContextMmenu.IScaleThresholds
	{
		#region Member Variables
		private IMapControl3 m_mapControl;
		private long m_subType; 
		#endregion

		#region Getter and Setter Methods
		/// <summary>
		/// Get and Set map control
		/// </summary>
		public IMapControl3 getSetMapControl
		{
			get { return m_mapControl; }
			set { m_mapControl = value; }
		}

		/// <summary>
		/// Get and Set map sub type
		/// </summary>
		public long getSetSubType
		{
			get { return m_subType; }
			set { m_subType = value; }
		} 
		#endregion

		#region Constructor and Destructor
		/// <summary>
		/// Default Constructor
		/// </summary>
		public ScaleThresholds()
		{

		} 
		#endregion

		#region Overridden Events
		/// <summary>
		/// Overide Base Onclick event
		/// </summary>
		public override void OnClick()
		{
			ILayer layer = (ILayer)getSetMapControl.CustomProperty;
			if (getSetSubType == 1) layer.MaximumScale = getSetMapControl.MapScale;
			if (getSetSubType == 2) layer.MinimumScale = getSetMapControl.MapScale;
			if (getSetSubType == 3)
			{
				layer.MaximumScale = 0;
				layer.MinimumScale = 0;
			}
			getSetMapControl.Refresh(esriViewDrawPhase.esriViewGeography, null, null);
		}

		/// <summary>
		/// Overide Base Oncreate event
		/// </summary>
		/// <param name="hook"></param>
		public override void OnCreate(object hook)
		{
			getSetMapControl = (IMapControl3)hook;
		}
		
		/// <summary>
		/// Override property Caption
		/// </summary>
		public override string Caption
		{
			get
			{
				if (getSetSubType == 1) return "Set Maximum Scale";
				else if (getSetSubType == 2) return "Set Minimum Scale";
				else return "Remove Scale Thresholds";
			}
		}

		/// <summary>
		/// Override Enabled property from base class
		/// </summary>
		public override bool Enabled
		{
			get
			{
				bool enabled = true;
				ILayer layer = (ILayer)getSetMapControl.CustomProperty;

				if (getSetSubType == 3)
				{
					if ((layer.MaximumScale == 0) & (layer.MinimumScale == 0)) enabled = false;
				}
				return enabled;
			}
		}

		#endregion

		#region Method
		/// <summary>
		/// Returns integre 3
		/// </summary>
		/// <returns></returns>
		public int GetCount()
		{
			return 3;
		}

		/// <summary>
		/// sets Subtype
		/// </summary>
		/// <param name="SubType"></param>
		public void SetSubType(int SubType)
		{
			getSetSubType = SubType;
		} 
		#endregion
	
	
	}
}

