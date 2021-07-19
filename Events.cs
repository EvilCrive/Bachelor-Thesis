using System;
using System.Collections.Generic;
using System.Text;
using SAPbouiCOM;

namespace btnPrintOnForm
{
    class Events
    {
        private SAPbouiCOM.Application SBO_Application;
        private void setApplication()
        {
            SAPbouiCOM.SboGuiApi sboGuiApi = null;
            string sConnectionString = null;
            sboGuiApi = new SAPbouiCOM.SboGuiApi();

            sConnectionString = System.Convert.ToString(Environment.GetCommandLineArgs().GetValue(1));
            sboGuiApi.Connect(sConnectionString);

            SBO_Application = sboGuiApi.GetApplication();
        }

        public Events()
        {
            setApplication();

            SBO_Application.AppEvent += new SAPbouiCOM._IApplicationEvents_AppEventEventHandler(SBO_Application_AppEvent);
            SBO_Application.ItemEvent += new SAPbouiCOM._IApplicationEvents_ItemEventEventHandler(SBO_Application_ItemEvent);
        }
        private static SAPbouiCOM.Form oForm;
        private static SAPbouiCOM.Item oItem;
        private static SAPbouiCOM.Item oOldItem;


        private void SBO_Application_ItemEvent(string FormUID, ref SAPbouiCOM.ItemEvent pVal, out bool BubbleEvent)
        {

            //throw new NotImplementedException();
            BubbleEvent = true;
            if ((pVal.FormType == 60110 && pVal.EventType != SAPbouiCOM.BoEventTypes.et_FORM_UNLOAD) && pVal.BeforeAction == true)
            {
                oForm = SBO_Application.Forms.GetFormByTypeAndCount(pVal.FormType, pVal.FormTypeCount);
                if (pVal.EventType == SAPbouiCOM.BoEventTypes.et_FORM_LOAD && pVal.BeforeAction == true)
                {
                    SAPbouiCOM.Button obt = null;
                    oOldItem = oForm.Items.Item("8");
                    oItem = oForm.Items.Add("btnPrint", SAPbouiCOM.BoFormItemTypes.it_BUTTON);
                    oItem.Top = oOldItem.Top;
                    oItem.Height = oOldItem.Height;
                    oItem.Left = oOldItem.Left + oOldItem.Width + 5;
                    oItem.Width = oOldItem.Width + 20;
                    obt = (SAPbouiCOM.Button)oItem.Specific;
                    obt.Caption = "Stampa";
                }
                if (pVal.ItemUID == "btnPrint" && (pVal.EventType == SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED || pVal.EventType == SAPbouiCOM.BoEventTypes.et_CLICK) && pVal.BeforeAction == true)
                      formAttrezzatura.formAttrezzatura form = new formAttrezzatura.formAttrezzatura();

                    form.readAllForm(oForm);
                    //Application.SBO_Application.MessageBox(valoreCodiceArticolo.Value, 1, "ok", "", "");
          
            }

        }

        private void btnPrint_Clicked(){
            StringBuilder date = new StringBuilder(((SAPbouiCOM.EditText)oForm.Items.Item("71").Specific).Value);
            date.Insert(4, "-");
            date.Insert(7, "-");
            string msg = "Ciao!\nSono un addon.\nLa chiamata di servizio è stata effettuata in data: " + date + ".\nL'id della chiamata è " + ((SAPbouiCOM.EditText)oForm.Items.Item("12").Specific).Value;
            SBO_Application.MessageBox(msg, 1, "OK", "", "");
        }
        private void SBO_Application_AppEvent(SAPbouiCOM.BoAppEventTypes EventType)
        {
            switch (EventType)
            {
                case SAPbouiCOM.BoAppEventTypes.aet_ShutDown:
                    //Exit Add-On
                    System.Windows.Forms.Application.Exit();
                    break;
                case SAPbouiCOM.BoAppEventTypes.aet_CompanyChanged:
                    break;
                case SAPbouiCOM.BoAppEventTypes.aet_FontChanged:
                    break;
                case SAPbouiCOM.BoAppEventTypes.aet_LanguageChanged:
                    break;
                case SAPbouiCOM.BoAppEventTypes.aet_ServerTerminition:
                    break;
                default:
                    break;
            }
        }
    }
}
