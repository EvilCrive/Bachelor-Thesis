using SAPbouiCOM;
using System;
using System.Collections.Generic;
using System.Text;

namespace btnPrintOnForm.formChiamate
{
    static class EventsChiamate
    {
        public static void Events(ref ItemEvent pVal, ref Application SBO_Application)
        {
            SAPbouiCOM.Form oForm = SBO_Application.Forms.GetFormByTypeAndCount(pVal.FormType, pVal.FormTypeCount);
            if (pVal.EventType == SAPbouiCOM.BoEventTypes.et_FORM_DRAW && pVal.BeforeAction == true)
                create_btnPrint(oForm);

            if (pVal.ItemUID == "btnPrint" && (pVal.EventType == SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED || pVal.EventType == SAPbouiCOM.BoEventTypes.et_CLICK) && pVal.BeforeAction == true)
                click_btnPrint(ref SBO_Application, oForm);
        }
        private static void create_btnPrint(SAPbouiCOM.Form oForm)
        {
            SAPbouiCOM.Button obt = null;
            SAPbouiCOM.Item oItem = null;
            SAPbouiCOM.Item oOldItem = null;
            oOldItem = oForm.Items.Item("8");
            oItem = oForm.Items.Add("btnPrint", SAPbouiCOM.BoFormItemTypes.it_BUTTON);
            oItem.Top = oOldItem.Top;
            oItem.Height = oOldItem.Height;
            oItem.Left = oOldItem.Left + oOldItem.Width + 5;
            oItem.Width = oOldItem.Width + 20;
            obt = (SAPbouiCOM.Button)oItem.Specific;
            obt.Caption = "Stampa";
        }
        private static void click_btnPrint(ref Application SBO_Application, SAPbouiCOM.Form oForm)
        {
            StringBuilder date = new StringBuilder(((SAPbouiCOM.EditText)oForm.Items.Item("71").Specific).Value);
            date.Insert(4, "-");
            date.Insert(7, "-");
            string msg = "Ciao!\nSono un addon.\nLa chiamata di servizio è stata effettuata in data: " + date + ".\nL'id della chiamata è " + ((SAPbouiCOM.EditText)oForm.Items.Item("12").Specific).Value;
            SBO_Application.MessageBox(msg, 1, "OK", "", "");
        }
    }
}
