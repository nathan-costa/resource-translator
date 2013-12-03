using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ResourceTranslator.Data;
using ResourceTranslator.Impl;

namespace ResourceTranslator
{
    public partial class Form1 : Form
    {
        private ResourceTranslator.Impl.ResourceTranslator _app;

        public Form1()
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
                    _app.ValidateResources(textBox2.Text);
                    var translations =_app.Translate(new ScrapeTranslator(), textBox1.Text);
                    _app.WriteToFile(new ResxWriter(), translations);

                }
                catch(Exception ex)
                {
                    Error.SetError(textBox2, ex.Message);
                }
            }
        }



    }
}
