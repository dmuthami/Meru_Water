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
	public sealed class LayerSelectable : BaseCommand, ICommandSubType, MW.ContextMmenu.ILayerSelectable
	{
		#region Member Variables
		private IMapControl3 m_mapControl;
		private long m_subType; 
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
		/// Get and set subtype
		/// </summary>
		public long getSetSubType
		{
			get { return m_subType; }
			set { m_subType = value; }
		} 
		
		#endregion

		#region Constructor/Destructor
		/// <summary>
		/// Constructor and Destructor method
		/// </summary>
		public LayerSelectable()
		{
		} 
		#endregion

		#region Event Handler
		/// <summary>
		/// Overidded method from the base class
		/// </summary>
		public override void OnClick()
		{
			IFeatureLayer layer = (IFeatureLayer) getSetMapControl.CustomProperty;
			if (getSetSubType == 1)	layer.Selectable = true;
			if (getSetSubType == 2) layer.Selectable = false;
		}
	
		public override void OnCreate(object hook)
		{
			getSetMapControl = (IMapControl3) hook;
		}
		
		public override bool Enabled
		{
			get
			{
				ILayer layer = (ILayer) getSetMapControl.CustomProperty;
				if (layer is IFeatureLayer)
				{
					IFeatureLayer featureLayer = (IFeatureLayer) layer;
					if (getSetSubType == 1) return !featureLayer.Selectable;
					else return featureLayer.Selectable;
				}
				else
				{
					return false;
				}
			}
		}
	
		public int GetCount()
		{
			return 2;
		}
	
		public void SetSubType(int SubType)
		{
			getSetSubType = SubType;
		}
	
		public override string Caption
		{
			get
			{
				if (getSetSubType == 1) return "Layer Selectable";
				else  return "Layer Unselectable";
			}
		}

		#endregion
	
	}
}

