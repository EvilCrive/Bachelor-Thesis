using System;
using System.Collections.Generic;
using SAPbouiCOM.Framework;

namespace btnPrintOnForm
{
    class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                Application oApp = null;
                if (args.Length < 1)
                {
                    oApp = new Application();
                }
                else
                {
                    oApp = new Application(args[0]);
                }
              
                
                Application.SBO_Application.AppEvent += new SAPbouiCOM._IApplicationEvents_AppEventEventHandler(SBO_Application_AppEvent);
                Application.SBO_Application.ItemEvent += SBO_Application_ItemEvent;
                oApp.Run();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private static SAPbouiCOM.Form oForm;
        private static SAPbouiCOM.Item oItem;
        private static SAPbouiCOM.Item oOldItem;


        static void SBO_Application_ItemEvent(string FormUID, ref SAPbouiCOM.ItemEvent pVal, out bool BubbleEvent)
        {
            //throw new NotImplementedException();
            BubbleEvent = true;
            if ((pVal.FormType == 60150 && pVal.EventType != SAPbouiCOM.BoEventTypes.et_FORM_UNLOAD) && pVal.BeforeAction == true) {
                oForm = Application.SBO_Application.Forms.GetFormByTypeAndCount(pVal.FormType, pVal.FormTypeCount);
                if (pVal.EventType == SAPbouiCOM.BoEventTypes.et_FORM_LOAD && pVal.BeforeAction == true) {
                    SAPbouiCOM.Button obt = null;
                    oOldItem = oForm.Items.Item("2");
                    oItem = oForm.Items.Add("btnPrint", SAPbouiCOM.BoFormItemTypes.it_BUTTON);
                    oItem.Top = oOldItem.Top;
                    oItem.Height = oOldItem.Height;
                    oItem.Left = oOldItem.Left + oOldItem.Width + 5;
                    oItem.Width = oOldItem.Width + 20;
                    obt = (SAPbouiCOM.Button)oItem.Specific;
                    obt.Caption = "Stampa";
                }
                if (pVal.ItemUID == "btnPrint" && (pVal.EventType == SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED || pVal.EventType == SAPbouiCOM.BoEventTypes.et_CLICK) && pVal.BeforeAction == true) {

                    printAttrezzatura();

                }
            }
        
        }

        static void printAttrezzatura()
        {
            btnPrintOnForm.formAttrezzatura.formAttrezzatura form = null;
            readFormItalian(form);
            
            //valoreCodiceArticolo = (SAPbouiCOM.EditText)oItems.Item("45").Specific;
            //Application.SBO_Application.MessageBox(valoreCodiceArticolo.Value, 1, "ok", "", "");
        }

        static void readFormItalian(btnPrintOnForm.formAttrezzatura.formAttrezzatura form)
        {
            insertByIds(form, "2", "2");
        }

        static void insertByIds(btnPrintOnForm.formAttrezzatura.formAttrezzatura form, string idStaticText, string idEditText)
        {
            SAPbouiCOM.Items oItems = oForm.Items;
            form.insert(((SAPbouiCOM.StaticText)oItems.Item("2").Specific).Caption, ((SAPbouiCOM.EditText)oItems.Item("2").Specific).Value);
        }
        static void SBO_Application_AppEvent(SAPbouiCOM.BoAppEventTypes EventType)
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
