using Microsoft.VisualBasic;
using System;
namespace btnPrintOnForm.formStatusbar
{
    class form
    {
        private SAPbouiCOM.Form oForm;
        private SAPbouiCOM.Item tMsg;
        private SAPbouiCOM.Item cType;
        private SAPbouiCOM.Application oApp;

        static public void click_Statusbar(SAPbouiCOM.ItemEvent pVal, ref SAPbouiCOM.Application SBO_Application)
        {
            form statusBar = new form(ref SBO_Application);
        }

        private form(ref SAPbouiCOM.Application SBO_Application)
        {
            try
            {
                oForm = SBO_Application.Forms.Add("frmDisplayStatus", SAPbouiCOM.BoFormTypes.ft_Fixed, -1);
                createForm();

                oForm.Visible = true;
                oApp = SBO_Application;
                SBO_Application.StatusBar.SetText("Status Bar Sample is now active", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                SBO_Application.ItemEvent += new SAPbouiCOM._IApplicationEvents_ItemEventEventHandler(SBO_Application_ItemEvent);
                SBO_Application.AppEvent += new SAPbouiCOM._IApplicationEvents_AppEventEventHandler(SBO_Application_AppEvent);
            }
            catch
            {
                SBO_Application.MessageBox("The form is already opened",0);
            }
        }

        // Catching item events in our AddOn
        private void SBO_Application_ItemEvent(string FormUID, ref SAPbouiCOM.ItemEvent pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            // Catching the Click on the 'Display Status Bar' button
            if ((pVal.ItemUID == "BTN_STATUS") & (pVal.EventType == SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED) & (pVal.Before_Action == false))
            {
                SAPbouiCOM.EditText EditTxtMsg = (SAPbouiCOM.EditText)tMsg.Specific;
                SAPbouiCOM.ComboBox CmbMsgType = ((SAPbouiCOM.ComboBox)(cType.Specific));

                // Present the ststus line

                SAPbouiCOM.BoStatusBarMessageType mt;
                string SelectedValue;
                SelectedValue = CmbMsgType.Selected.Value;

                mt = SAPbouiCOM.BoStatusBarMessageType.smt_Warning;

                if (SelectedValue == "smt_Error")
                { mt = SAPbouiCOM.BoStatusBarMessageType.smt_Error; }
                else if (SelectedValue == "smt_None")
                { mt = SAPbouiCOM.BoStatusBarMessageType.smt_None; }
                else if (SelectedValue == "smt_Success")
                { mt = SAPbouiCOM.BoStatusBarMessageType.smt_Success; }

                oApp.StatusBar.SetText(EditTxtMsg.Value.ToString(), SAPbouiCOM.BoMessageTime.bmt_Short, (SAPbouiCOM.BoStatusBarMessageType)mt);
            }

        }

        // Catching application events in our AddOn
        private void SBO_Application_AppEvent(SAPbouiCOM.BoAppEventTypes EventType)
        {

            if (EventType == SAPbouiCOM.BoAppEventTypes.aet_ShutDown)
            {

                oApp.MessageBox("A Shut Down Event has been caught" + Environment.NewLine + "Terminating Add On...", 1, "Ok", "", "");

                // Take care of terminating your AddOn application

                System.Windows.Forms.Application.Exit();

            }
        }

        private void createForm()
        {
            SAPbouiCOM.Item oItem = null;
            SAPbouiCOM.Button oButton = null;
            SAPbouiCOM.StaticText oStaticText = null;
            SAPbouiCOM.EditText oEditText = null;
            SAPbouiCOM.ComboBox oComboBox = null;
            // add a User Data Source to the form
            oForm.DataSources.UserDataSources.Add("EditSource", SAPbouiCOM.BoDataType.dt_SHORT_TEXT, 40);
            oForm.DataSources.UserDataSources.Add("CombSource", SAPbouiCOM.BoDataType.dt_SHORT_TEXT, 20);
            // set the form properties
            oForm.Title = "Status Bar Add-On";
            oForm.Left = 450;
            oForm.ClientWidth = 400;
            oForm.Top = 120;
            oForm.ClientHeight = 100;
            // display status bar button
            oItem = oForm.Items.Add("BTN_STATUS", SAPbouiCOM.BoFormItemTypes.it_BUTTON);
            oItem.Left = 90;
            oItem.Width = 150;
            oItem.Top = 53;
            oItem.Height = 19;
            oItem.AffectsFormMode = false;
            oButton = ((SAPbouiCOM.Button)(oItem.Specific));
            oButton.Caption = "Display Status Bar";
            //rect
            oItem = oForm.Items.Add("Rect1", SAPbouiCOM.BoFormItemTypes.it_RECTANGLE);
            oItem.Left = 0;
            oItem.Width = 344;
            oItem.Top = 1;
            oItem.Height = 45;
            oItem.AffectsFormMode = false;
            // comboBox element
            oItem = oForm.Items.Add("ComboBox1", SAPbouiCOM.BoFormItemTypes.it_COMBO_BOX);
            oItem.Left = 157;
            oItem.Width = 163;
            oItem.Top = 24;
            oItem.Height = 14;
            oItem.AffectsFormMode = false;
            oItem.DisplayDesc = true;
            cType = oItem;
            oItem.DisplayDesc = false;
            oComboBox = ((SAPbouiCOM.ComboBox)(oItem.Specific));
            // edit text of message
            oItem = oForm.Items.Add("EditText1", SAPbouiCOM.BoFormItemTypes.it_EDIT);
            oItem.Left = 157;
            oItem.Width = 163;
            oItem.Top = 8;
            oItem.Height = 14;
            oItem.AffectsFormMode = false;
            tMsg = oItem;
            oEditText = ((SAPbouiCOM.EditText)(oItem.Specific));
            // Edit text used data source
            oEditText.DataBind.SetBound(true, "", "EditSource");
            oEditText.String = "Type Your message Here";
            // 1st static text item
            oItem = oForm.Items.Add("StaticTxt1", SAPbouiCOM.BoFormItemTypes.it_STATIC);
            oItem.Left = 7;
            oItem.Width = 148;
            oItem.Top = 8;
            oItem.Height = 14;
            oItem.AffectsFormMode = false;
            oItem.LinkTo = "EditText1";
            oStaticText = ((SAPbouiCOM.StaticText)(oItem.Specific));
            oStaticText.Caption = "Message Text";
            // 2nd static text item
            oItem = oForm.Items.Add("StaticTxt2", SAPbouiCOM.BoFormItemTypes.it_STATIC);
            oItem.Left = 7;
            oItem.Width = 148;
            oItem.Top = 24;
            oItem.Height = 14;
            oItem.AffectsFormMode = false;
            oItem.LinkTo = "ComboBox1";
            oStaticText = ((SAPbouiCOM.StaticText)(oItem.Specific));
            oStaticText.Caption = "Message Type";

            // bind the Combo Box item to the defined used data source and bind combobox to type of message
            oComboBox.DataBind.SetBound(true, "", "CombSource");
            oComboBox.ValidValues.Add(System.Convert.ToString(SAPbouiCOM.BoStatusBarMessageType.smt_Warning), "Warning");
            oComboBox.ValidValues.Add(System.Convert.ToString(SAPbouiCOM.BoStatusBarMessageType.smt_Error), "Error");
            oComboBox.ValidValues.Add(System.Convert.ToString(SAPbouiCOM.BoStatusBarMessageType.smt_None), "None");
            oComboBox.ValidValues.Add(System.Convert.ToString(SAPbouiCOM.BoStatusBarMessageType.smt_Success), "Success");
            oComboBox.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
        }

    }
}