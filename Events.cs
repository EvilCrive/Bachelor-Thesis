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
        


        private void SBO_Application_ItemEvent(string FormUID, ref SAPbouiCOM.ItemEvent pVal, out bool BubbleEvent)
        {

            //throw new NotImplementedException();
            BubbleEvent = true;
            if ((pVal.FormType == 60150 && pVal.EventType != SAPbouiCOM.BoEventTypes.et_FORM_UNLOAD) && pVal.BeforeAction == true)
                btnPrintOnForm.formAttrezzatura.EventsAttrezzatura.Events(ref pVal, ref SBO_Application);
            else if ((pVal.FormType == 60110 && pVal.EventType != SAPbouiCOM.BoEventTypes.et_FORM_UNLOAD) && pVal.BeforeAction == true)
                btnPrintOnForm.formChiamate.EventsChiamate.Events(ref pVal, ref SBO_Application);
            else if (pVal.FormType == 0 && pVal.ItemUID == "1" && pVal.BeforeAction == true)
                throw new Exception();
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
