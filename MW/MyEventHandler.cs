﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Controls;

namespace MW
{
	public class MyEventHandler : MW.IMyEventHandler
	{

		#region Member Variables
		private ITOCControl2 m_iTOCControl2 = null;
		#endregion

		#region Constructor/Destructor
		/// <summary>
		/// Default Constructor
		/// </summary>
		public MyEventHandler()
		{
			//TODO Code
		} 
		#endregion

		#region Getter and Setter Methods
		/// <summary>
		/// Getter and setter method for Toc object
		/// </summary>
		public ITOCControl2 getSetITOCControl2
		{
			get { return m_iTOCControl2; }
			set { m_iTOCControl2 = value; }
		} 
		#endregion

		#region Form Methods
		public void showAttributeForm()
		{
			try
			{
				MW.ManipulateData.MyTOCClass myTOCClass;

				myTOCClass = new MW.ManipulateData.MyTOCClass(getSetITOCControl2);

				MW.ManipulateData.AttributeTable attributeTable = new ManipulateData.AttributeTable(myTOCClass.getSelectedLayerInTOC());
				attributeTable.ShowDialog();
			}
			catch (Exception)
			{
				
				throw;
			}
		} 
		#endregion
	}
}
