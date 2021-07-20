
using Microsoft.VisualBasic;
using System;
namespace btnPrintOnForm.formStatusbar {/*
    public class SBOForm  { 
        private SAPbouiCOM.Form oForm; //  Addon Form
        private SAPbouiCOM.Item txtMsg; //  The text to be displayed
        private SAPbouiCOM.Item cmbType; //  A combo box to choose display type
         
        private void CreateMySimpleForm() { 
            
            SAPbouiCOM.Item oItem = null; 
            
            // *******************************************
            // we will use the following objects to set
            // the specific values of every item
            // we add.
            // this is the best way to do so
            //*********************************************
            
            SAPbouiCOM.Button oButton = null; 
            SAPbouiCOM.StaticText oStaticText = null; 
            SAPbouiCOM.EditText oEditText = null; 
            SAPbouiCOM.ComboBox oComboBox = null; 
            
            
            // add a new form
            try { 
                oForm = SBO_Application.Forms.Add( "frmDisplayStatus", SAPbouiCOM.BoFormTypes.ft_Fixed, -1 ); 
            } 
            catch  { // Form can not be added
                // Take care of terminating your AddOn application
                System.Windows.Forms.MessageBox.Show( "An open form exists in the application", "Can not load Addon", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation ); 
                System.Environment.Exit( 0 ); 
            } 
            
            // add a User Data Source to the form
            oForm.DataSources.UserDataSources.Add( "EditSource", SAPbouiCOM.BoDataType.dt_SHORT_TEXT, 40 ); 
            oForm.DataSources.UserDataSources.Add( "CombSource", SAPbouiCOM.BoDataType.dt_SHORT_TEXT, 20 ); 
            
            // set the form properties
            oForm.Title = "Status Bar Sample"; 
            oForm.Left = 400; 
            oForm.ClientWidth = 350; 
            oForm.Top = 100; 
            oForm.ClientHeight = 80; 
            
            //*****************************************
            // Adding Items to the form
            // and setting their properties
            //*****************************************
            
            
            // /**********************
            // Adding an Ok button
            //*********************
            
            // We get automatic event handling for
            // the Ok and Cancel Buttons by setting
            // their UIDs to 1 and 2 respectively
            
            oItem = oForm.Items.Add( "1", SAPbouiCOM.BoFormItemTypes.it_BUTTON ); 
            oItem.Left = 6; 
            oItem.Width = 65; 
            oItem.Top = 53; 
            oItem.Height = 19; 
            oItem.AffectsFormMode = false; 
            
            oButton = ( ( SAPbouiCOM.Button )( oItem.Specific ) ); 
            
            oButton.Caption = "Ok"; 
            
            //**************************************
            // Adding a 'Display Status Bar' button
            //**************************************
            
            oItem = oForm.Items.Add( "BTN_STATUS", SAPbouiCOM.BoFormItemTypes.it_BUTTON ); 
            oItem.Left = 90; 
            oItem.Width = 150; 
            oItem.Top = 53; 
            oItem.Height = 19; 
            oItem.AffectsFormMode = false; 
            
            oButton = ( ( SAPbouiCOM.Button )( oItem.Specific ) ); 
            
            oButton.Caption = "Display Status Bar"; 
            
            //************************
            // Adding a Rectangle
            //***********************
            
            oItem = oForm.Items.Add( "Rect1", SAPbouiCOM.BoFormItemTypes.it_RECTANGLE ); 
            oItem.Left = 0; 
            oItem.Width = 344; 
            oItem.Top = 1; 
            oItem.Height = 45; 
            oItem.AffectsFormMode = false; 
            
            //*************************
            // Adding a Combo Box item
            //*************************
            
            oItem = oForm.Items.Add( "ComboBox1", SAPbouiCOM.BoFormItemTypes.it_COMBO_BOX ); 
            oItem.Left = 157; 
            oItem.Width = 163; 
            oItem.Top = 24; 
            oItem.Height = 14; 
            oItem.AffectsFormMode = false; 
            oItem.DisplayDesc = true; 
            cmbType = oItem; 
            
            oItem.DisplayDesc = false; 
            
            oComboBox = ( ( SAPbouiCOM.ComboBox )( oItem.Specific ) ); 
            
            //*************************
            // Adding a Text Edit item
            //*************************
            
            oItem = oForm.Items.Add( "EditText1", SAPbouiCOM.BoFormItemTypes.it_EDIT ); 
            oItem.Left = 157; 
            oItem.Width = 163; 
            oItem.Top = 8; 
            oItem.Height = 14; 
            oItem.AffectsFormMode = false; 
            txtMsg = oItem; 
            
            oEditText = ( ( SAPbouiCOM.EditText )( oItem.Specific ) ); 
            
            // bind the text edit item to the defined used data source
            oEditText.DataBind.SetBound( true, "", "EditSource" ); 
            oEditText.String = "Type Your message Here"; 
            
            //***************************
            // Adding a Static Text item
            //***************************
            
            oItem = oForm.Items.Add( "StaticTxt1", SAPbouiCOM.BoFormItemTypes.it_STATIC ); 
            oItem.Left = 7; 
            oItem.Width = 148; 
            oItem.Top = 8; 
            oItem.Height = 14; 
            oItem.AffectsFormMode = false; 
            
            oItem.LinkTo = "EditText1"; 
            
            oStaticText = ( ( SAPbouiCOM.StaticText )( oItem.Specific ) ); 
            
            oStaticText.Caption = "Message Text"; 
            
            //**********************************
            // Adding another Static Text item
            //**********************************
            
            oItem = oForm.Items.Add( "StaticTxt2", SAPbouiCOM.BoFormItemTypes.it_STATIC ); 
            oItem.Left = 7; 
            oItem.Width = 148; 
            oItem.Top = 24; 
            oItem.Height = 14; 
            oItem.AffectsFormMode = false; 
            
            oItem.LinkTo = "ComboBox1"; 
            
            oStaticText = ( ( SAPbouiCOM.StaticText )( oItem.Specific ) ); 
            
            oStaticText.Caption = "Message Type"; 
            
            
            
            // bind the Combo Box item to the defined used data source
            oComboBox.DataBind.SetBound( true, "", "CombSource" ); 
            
            oComboBox.ValidValues.Add( System.Convert.ToString( SAPbouiCOM.BoStatusBarMessageType.smt_Warning ), "Warning" ); 
            oComboBox.ValidValues.Add( System.Convert.ToString( SAPbouiCOM.BoStatusBarMessageType.smt_Error ), "Error" ); 
            oComboBox.ValidValues.Add( System.Convert.ToString( SAPbouiCOM.BoStatusBarMessageType.smt_None ), "None" ); 
            oComboBox.ValidValues.Add( System.Convert.ToString( SAPbouiCOM.BoStatusBarMessageType.smt_Success ), "Success" ); 
            oComboBox.Select(0,SAPbouiCOM.BoSearchKey.psk_Index);
            
        } 
        
        
        // Catching item events in our AddOn
        private void SBO_Application_ItemEvent( string FormUID, ref SAPbouiCOM.ItemEvent pVal, out bool BubbleEvent ) { 
            BubbleEvent = true;
            // Catching the Click on the 'Display Status Bar' button
            if ( ( pVal.ItemUID == "BTN_STATUS" ) & ( pVal.EventType == SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED ) & ( pVal.Before_Action == false ) ) { 
                SAPbouiCOM.EditText EditTxtMsg = (SAPbouiCOM.EditText)txtMsg.Specific; 
                SAPbouiCOM.ComboBox CmbMsgType = ( ( SAPbouiCOM.ComboBox )( cmbType.Specific ) ); 

                // Present the ststus line
  
				SAPbouiCOM.BoStatusBarMessageType mt;
				string SelectedValue;
				SelectedValue = CmbMsgType.Selected.Value;

				mt = SAPbouiCOM.BoStatusBarMessageType.smt_Warning;

				if (SelectedValue == "smt_Error")
				{mt = SAPbouiCOM.BoStatusBarMessageType.smt_Error;}
				else if (SelectedValue == "smt_None")
				{mt = SAPbouiCOM.BoStatusBarMessageType.smt_None;}
				else if (SelectedValue == "smt_Success")
				{mt = SAPbouiCOM.BoStatusBarMessageType.smt_Success;}

				SBO_Application.StatusBar.SetText( EditTxtMsg.Value.ToString(),SAPbouiCOM.BoMessageTime.bmt_Short,(SAPbouiCOM.BoStatusBarMessageType)mt ); 
			} 
            
            // Catching the Unload event of the form
            if ( ( pVal.EventType == SAPbouiCOM.BoEventTypes.et_FORM_UNLOAD ) & ( pVal.FormUID == "frmDisplayStatus" ) ) { 
                
                SBO_Application.MessageBox( "Terminating StatusBar Sample", 1, "Ok", "", "" ); 
                
                // Take care of terminating your AddOn application
                
                System.Windows.Forms.Application.Exit(); 
            } 
        } 
        
        
        // Catching application events in our AddOn
        private void SBO_Application_AppEvent( SAPbouiCOM.BoAppEventTypes EventType ) { 
            
            if ( EventType == SAPbouiCOM.BoAppEventTypes.aet_ShutDown ) { 
                
                SBO_Application.MessageBox( "A Shut Down Event has been caught" + Environment.NewLine + "Terminating Add On...", 1, "Ok", "", "" ); 
                
                // Take care of terminating your AddOn application
                
                System.Windows.Forms.Application.Exit(); 
                
            } 
        } 
        
        
        public SBOForm() { 
            
            
            //*************************************************************
            // set SBO_Application with an initialized application object
            //*************************************************************
            
            SetApplication(); 
            
            //*************************************************************
            // Create the simple form
            //*************************************************************
            
            CreateMySimpleForm(); 
            
            oForm.Visible = true; 
            
            
            SBO_Application.StatusBar.SetText( "Status Bar Sample is now active", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success ); 
            // events handled by SBO_Application_ItemEvent
            SBO_Application.ItemEvent += new SAPbouiCOM._IApplicationEvents_ItemEventEventHandler( SBO_Application_ItemEvent ); 
            // events handled by SBO_Application_AppEvent
            SBO_Application.AppEvent += new SAPbouiCOM._IApplicationEvents_AppEventEventHandler( SBO_Application_AppEvent ); 
        } 
    }     */
} 
