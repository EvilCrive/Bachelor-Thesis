using SAPbouiCOM;
using System;
using System.Collections.Generic;
using System.Text;


namespace btnPrintOnForm.formAttrezzatura
{
    static class EventsAttrezzatura
    {
        public static void Events(ref SAPbouiCOM.ItemEvent pVal, ref SAPbouiCOM.Application SBO_Application) 
        {
            SAPbouiCOM.Form oForm = SBO_Application.Forms.GetFormByTypeAndCount(pVal.FormType, pVal.FormTypeCount);
            if (pVal.EventType == SAPbouiCOM.BoEventTypes.et_FORM_DRAW && pVal.BeforeAction == true)
                create_btnPrint(oForm);
            if (pVal.ItemUID == "btnPrint" && (pVal.EventType == SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED || pVal.EventType == SAPbouiCOM.BoEventTypes.et_CLICK) && pVal.BeforeAction == true)
                click_btnPrint(oForm, ref SBO_Application);
        }
       private static void create_btnPrint(SAPbouiCOM.Form oForm) {
            SAPbouiCOM.Button obt = null;
            SAPbouiCOM.Item oItem = null;
            SAPbouiCOM.Item oOldItem = null;
            oOldItem = oForm.Items.Item("2");
            oItem = oForm.Items.Add("btnPrint", SAPbouiCOM.BoFormItemTypes.it_BUTTON); 
            
            oItem.Top = oOldItem.Top;
            oItem.Height = oOldItem.Height;
            oItem.Left = oOldItem.Left + oOldItem.Width + 5;
            oItem.Width = oOldItem.Width + 20;
            obt = (SAPbouiCOM.Button)oItem.Specific;
            obt.Caption = "Stampa";
        }
       private static void click_btnPrint(SAPbouiCOM.Form oForm, ref SAPbouiCOM.Application SBO_Application) {
            formAttrezzatura.form form = new formAttrezzatura.form();
            form.readAllForm(oForm);
            form.writeMessage(ref SBO_Application);
       }

    }
}
