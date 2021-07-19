using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAPbouiCOM;

namespace btnPrintOnForm.formAttrezzatura
{
    //interface with main methods
    interface iFormItems{
        string at(string key);

        void readAllForm(SAPbouiCOM.Form oForm);
        //void writeMessage();

    }
    public class form : iFormItems
    {
        //data-field
        internal Dictionary<String, String> formData;

        //public methods
        public form() { formData = new Dictionary<string,string>();}

        public string at(string key) { return formData[key];}

        public void readAllForm(SAPbouiCOM.Form oForm)
        {
            insertBase(oForm);
            insertIndirizzo(oForm);
            //insertChiamateDiServizio();
        }

        static public void writeMessage() { }

        //implementation methods
        private void insert(string key, string value) { formData[key] = value; }

        private bool isEmpty(string key) { return formData[key] == null; }

        private void insertByIds(SAPbouiCOM.Form oForm, string idStaticText, string idEditText)
            { insert(((SAPbouiCOM.StaticText)oForm.Items.Item(idStaticText).Specific).Caption, ((SAPbouiCOM.EditText)oForm.Items.Item(idEditText).Specific).Value);}
        private void insertByIdsCBox(SAPbouiCOM.Form oForm, string idStaticText, string idComboBox)
            { insert(((SAPbouiCOM.StaticText)oForm.Items.Item(idStaticText).Specific).Caption, ((SAPbouiCOM.ComboBox)oForm.Items.Item(idComboBox).Specific).Value);}

        private void insertBase(SAPbouiCOM.Form oForm) {
            BoFormItemTypes aaa = oForm.Items.Item("173").Type;
            insert(((SAPbouiCOM.StaticText)oForm.Items.Item("234000123").Specific).Caption + " Vendite", ((SAPbouiCOM.OptionBtn)oForm.Items.Item("234000124").Specific).Selected.ToString());
            
            insert(((SAPbouiCOM.StaticText)oForm.Items.Item("234000123").Specific).Caption + " Acquisti", ((SAPbouiCOM.OptionBtn)oForm.Items.Item("234000125").Specific).Selected.ToString());
            //result in False o True
                                                    //tipo attrezzatura (vendite) [N]
                                                    //tipo attrezzatura (vendite) [P]

            insertByIds(oForm, "9", "43");          //Numero serie produttore
            insertByIds(oForm, "4", "44");          //Numero di Fabbrica
            insertByIds(oForm, "16", "45");         //Codice Articolo
            insertByIds(oForm, "15", "46");                             //Descrizione Macchina

            insertByIds(oForm, "6", "48");                              //Codice Business Partner
            insertByIds(oForm, "20", "49");                             //Nome del business partner
            insertByIdsCBox(oForm, "7", "93");                          //Contatto
            insertByIds(oForm, "19", "54");                             //Numero di telefono

            insertByIdsCBox(oForm, "162", "178");                       //Stato    (Attivo [A], Reso [R], Terminato [T], In prestito [L], In riparazione [I])
            insertByIds(oForm, "163", "171");                           //N. serie precedente
            insertByIds(oForm, "164", "172");                           //N. serie nuovo
            insertByIds(oForm, "167", "173");                           //Tecnico
            insertByIds(oForm, "168", "174");                           //Area

        }
        
        private void insertIndirizzo(SAPbouiCOM.Form oForm){
            insertByIds(oForm, "29", "63");                             //Via
            insertByIds(oForm, "2005", "2006");                         //N. civico
            insertByIds(oForm, "2000", "2001");                         //Ospedale
            insertByIds(oForm, "25", "69");                             //CAP
            insertByIds(oForm, "28", "66");                             //Reparto
            insertByIds(oForm, "27", "67");                             //Città
            insertByIdsCBox(oForm, "24", "75");                         //Provincia
            insertByIds(oForm, "26", "68");                             //Regione
            insertByIdsCBox(oForm, "23", "76");                         //Paese/Regione
            insertByIds(oForm, "38", "65");                             //Collocazione

        }
        private void insertChiamateDiServizio(){
            //matrix

        }
        
    }


}
