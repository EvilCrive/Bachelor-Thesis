using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace btnPrintOnForm.formAttrezzatura
{
    class formAttrezzatura
    {
        private Dictionary<String, String> form;

        public void insert(string key, string value)
        {
            form[key] = value;
        }

        public bool isEmpty(string key)
        {
            return form[key] == null;
        }
        public string at(string key){
            return form[key];
        }

        public formAttrezzatura()
        {
            form = null;
        }

    }
}
