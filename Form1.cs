using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace GoogleTranslate
{
    public partial class Translate : Form
    {
        public Translate()
        {
            InitializeComponent();
        }

        public string TranslateText(string input)
        {            
            string url = String.Format
            ("https://translate.googleapis.com/translate_a/single?client=gtx&sl={0}&tl={1}&dt=t&q={2}",
             "vi", "en", Uri.EscapeUriString(input));
            HttpClient httpClient = new HttpClient();
            string result = httpClient.GetStringAsync(url).Result;            
            var jsonData = new JavaScriptSerializer().Deserialize<List<dynamic>>(result);            
            var translationItems = jsonData[0];            
            string translation = "";           
            foreach (object item in translationItems)
            {                
                IEnumerable translationLineObject = item as IEnumerable;                
                IEnumerator translationLineString = translationLineObject.GetEnumerator();              
                translationLineString.MoveNext();                
                translation += string.Format(" {0}", Convert.ToString(translationLineString.Current));
            }
            if (translation.Length > 1) { translation = translation.Substring(1); };           
            return translation;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = TranslateText(textBox1.Text);
        }
    }
}
