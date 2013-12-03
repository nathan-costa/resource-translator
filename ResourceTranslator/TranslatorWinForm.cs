using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Windows.Forms;
using ResourceTranslator.Abs;
using ResourceTranslator.Data;
using ResourceTranslator.Impl;
using ResourceTranslator.Static;

namespace ResourceTranslator
{
    public partial class TranslatorWinForm : Form, TranslatorForm
    {
        private ResourceTranslator.Impl.ResourceTranslator _app;

        public TranslatorWinForm()
        {
            InitializeComponent();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            Error.Clear();

            if(textBox1.Text == String.Empty)
                Error.SetError(this.textBox1, "Please enter string to translate");

            else if (textBox2.Text == String.Empty)
                Error.SetError(this.textBox2, "Please enter resource key to write");

            else
            {
                try
                {
                    _app = new Impl.ResourceTranslator(new GbrFilesInfo(new GbrLanguages()), new ResxInputValidator());
                    _app.ValidateResources(textBox2.Text, chkWriteToDefault.Checked);
                    var translations =_app.Translate(new BingTranslator(), textBox1.Text);
                    if (chkWriteToDefault.Checked)
                        Helper.UpdateResourceFile(new Hashtable() {{textBox2.Text, textBox1.Text}}, "c:\\translationtemp\\gbr.resx", this );
                    _app.WriteToFile(new ResxWriter(), translations, this);

                }
                catch(Exception ex)
                {
                    Error.SetError(textBox2, ex.Message);
                }
            }
        }


        public string TextOutput
        {
            get { return txtOutput.Text; }
            set {  txtOutput.AppendText(value + Environment.NewLine); }
        }
    }
}
