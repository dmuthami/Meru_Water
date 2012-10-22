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
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.SystemUI;

namespace MW.ContextMmenu
{
	public sealed class LayerVisibility : BaseCommand, ICommandSubType, MW.ContextMmenu.ILayerVisibility
	{
		#region Member Variables
		private IHookHelper m_hookHelper = new HookHelperClass();
		private long m_subType;
		#endregion

		#region Getter and Setter Methods
		/// <summary>
		/// Get and set for the hooker
		/// </summary>
		public IHookHelper getSetHookHelper
		{
			get { return m_hookHelper; }
			set { m_hookHelper = value; }
		}
		/// <summary>
		/// Get and set for the subtype
		/// </summary>
		public long getSetSubType
		{
            get { return m_subType; }
            set { m_subType = value; }
		} 
		#endregion

		#region Constructor and Destructors
		/// <summary>
		/// Default Constructor
		/// </summary>
		public LayerVisibility()
		{
		} 
		#endregion

		#region Overidded Events
		/// <summary>
		/// Overidded click event
		/// </summary>
		public override void OnClick()
		{
			for (int i=0; i <= getSetHookHelper.FocusMap.LayerCount - 1; i++)
			{
				if (getSetSubType == 1) getSetHookHelper.FocusMap.get_Layer(i).Visible = true;
				if (getSetSubType == 2) getSetHookHelper.FocusMap.get_Layer(i).Visible = false;
			}
			getSetHookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography,null,null);
		}
	
		/// <summary>
		/// Overidded Create event
		/// </summary>
		/// <param name="hook"></param>
		public override void OnCreate(object hook)
		{
			getSetHookHelper.Hook = hook;
		}

		/// <summary>
		/// Overidded String event
		/// </summary>
		public override string Caption
		{
			get
			{
				if (getSetSubType == 1) return "Turn All Layers On";
				else  return "Turn All Layers Off";
			}
		}
	
		/// <summary>
		/// Overidded Enabled event
		/// </summary>
		public override bool Enabled
		{
			get
			{
				bool enabled = false; int i;
				if (getSetSubType == 1) 
				{
					for (i=0;i<=getSetHookHelper.FocusMap.LayerCount - 1;i++)
					{
						if (getSetHookHelper.ActiveView.FocusMap.get_Layer(i).Visible == false)
						{
							enabled = true;
							break;
						}
					}
				}
				else 
				{
					for (i=0;i<=getSetHookHelper.FocusMap.LayerCount - 1;i++)
					{
						if (getSetHookHelper.ActiveView.FocusMap.get_Layer(i).Visible == true)
						{
							enabled = true;
							break;
						}
					}
				}
				return enabled;
			}
		}
		#endregion

		#region Methods
		/// <summary>
		/// Returns
		/// </summary>
		/// <returns></returns>
		public int GetCount()
		{
			return 2;
		}
	
		/// <summary>
		/// Get and Set subtype
		/// </summary>
		/// <param name="SubType"> subtype parameter</param>
		public void SetSubType(int SubType)
		{
			getSetSubType = SubType;
		}

		#endregion

	}
}
