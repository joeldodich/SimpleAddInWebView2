using System;
using System.Runtime.InteropServices;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using Inventor;

namespace SimpleAddIn
{
	/// <summary>
	/// Base class for command buttons
	/// </summary>

	internal abstract class Button
	{
		#region Data Members

		//private data members:
		private static Inventor.Application m_inventorApplication;

		private ButtonDefinition m_buttonDefinition;

        private ButtonDefinitionSink_OnExecuteEventHandler ButtonDefinition_OnExecuteEventDelegate;

		#endregion		

		#region "Properties"

		public static Inventor.Application InventorApplication
		{
			set
			{
                m_inventorApplication = value;
			}
			get
			{
                return m_inventorApplication;
			}
		}

		public Inventor.ButtonDefinition ButtonDefinition
		{
			get
			{
                return m_buttonDefinition;
			}
		}

		#endregion
        
		#region "Methods"

		public Button(string displayName, string internalName, CommandTypesEnum commandType, string clientId, string description, string tooltip, Icon standardIcon, Icon largeIcon, ButtonDisplayEnum buttonDisplayType)
		{
			try
			{
				//get IPictureDisp for icons
				stdole.IPictureDisp standardIconIPictureDisp = null;
				System.Drawing.Image smallBitmapImage = standardIcon.ToBitmap();
                standardIconIPictureDisp = Utilities.ConvertImage.ConvertImageToIPictureDisp(smallBitmapImage);

                stdole.IPictureDisp largeIconIPictureDisp;
				System.Drawing.Image largeBitmapImage = largeIcon.ToBitmap();
                largeIconIPictureDisp = Utilities.ConvertImage.ConvertImageToIPictureDisp(largeBitmapImage);

                //create button definition
                m_buttonDefinition = m_inventorApplication.CommandManager.ControlDefinitions.AddButtonDefinition(displayName, internalName, commandType, clientId, description, tooltip, standardIconIPictureDisp , largeIconIPictureDisp, buttonDisplayType);
												
				//enable the button
                m_buttonDefinition.Enabled = true;
				
				//connect the button event sink
                ButtonDefinition_OnExecuteEventDelegate = new ButtonDefinitionSink_OnExecuteEventHandler(ButtonDefinition_OnExecute);
                m_buttonDefinition.OnExecute += ButtonDefinition_OnExecuteEventDelegate;
			}
			catch(Exception e)
			{
				MessageBox.Show(e.ToString());
			}
		}

		public Button(string displayName, string internalName, CommandTypesEnum commandType, string clientId, string description, string tooltip, ButtonDisplayEnum buttonDisplayType)
		{
			try
			{			
				//create button definition
                m_buttonDefinition = m_inventorApplication.CommandManager.ControlDefinitions.AddButtonDefinition(displayName, internalName, commandType, clientId, description, tooltip, Type.Missing, Type.Missing, buttonDisplayType);
								
				//enable the button
                m_buttonDefinition.Enabled = true;
				
				//connect the button event sink
				ButtonDefinition_OnExecuteEventDelegate = new ButtonDefinitionSink_OnExecuteEventHandler(ButtonDefinition_OnExecute);
                m_buttonDefinition.OnExecute += ButtonDefinition_OnExecuteEventDelegate;
			}
			catch(Exception e)
			{
				MessageBox.Show(e.ToString());
			}
		}

		abstract protected void ButtonDefinition_OnExecute(NameValueMap context);

		#endregion
	}

}
