using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SendHttpRequest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private async void buttonReq_Click(object sender, EventArgs e)
        {
            HttpContent content = new StringContent(textBoxContent.Text);
            string url = textBoxUri.Text;
            HttpResponseMessage response = null;

            try
            {
                if(((Button)sender).Name == "buttonPOST")
                    response = await Rest.ReqRest(url, Rest.ReqType.POST, content);
                if (((Button)sender).Name == "buttonGET")
                    response = await Rest.ReqRest(url, Rest.ReqType.GET);
                if (((Button)sender).Name == "buttonPUT")
                    response = await Rest.ReqRest(url, Rest.ReqType.PUT, content);
                if (((Button)sender).Name == "buttonDELETE")
                    response = await Rest.ReqRest(url, Rest.ReqType.DELETE);

                labelResponse.Text = await response.Content.ReadAsStringAsync();

                var heders = response.Headers;

                label1.Text = "-------->Response Heders:\n";
                foreach ( var heder in heders)
                {
                    label1.Text += heder.Key + " = " ;
                    foreach (var val in heder.Value)
                        label1.Text += val + "  ";
                    label1.Text += "\n";
                }

                label1.Text += "\n-------->Content Heders:\n";

                foreach (var heder in response.Content.Headers)
                {

                    label1.Text += heder.Key + " = ";
                    foreach (var val in heder.Value)
                        label1.Text += val + "  ";
                    label1.Text += "\n";
                }                
            }
            catch (Exception exep)
            {
                labelResponse.Text = "Wystapił błąd: " + exep.Message;
            }
        }

        private async void button1_ClickAsync(object sender, EventArgs e)
        {
            HttpClient clientRest = new HttpClient();
            HttpRequestMessage requestMessage = new HttpRequestMessage();

            if (radioButton1.Checked) requestMessage.Method = HttpMethod.Get;
            if (radioButton2.Checked) requestMessage.Method = HttpMethod.Post;
            if (radioButton3.Checked) requestMessage.Method = HttpMethod.Put;
            if (radioButton4.Checked) requestMessage.Method = HttpMethod.Delete;
            //requestMessage.Method = HttpMethod.Head;

            HttpContent content = new StringContent(textBoxContent.Text);
            //
            if(radioButtonJSON.Checked) content.Headers.ContentType.MediaType = "application/json";

            requestMessage.Properties.Add("okokokokk", "qqqqqqqqqqqqqqqqqq");
            requestMessage.Headers.Add("User-Agent", "programHTTPclient");

            string url = textBoxUri.Text;

            requestMessage.RequestUri = new Uri(url);
            if (requestMessage.Method != HttpMethod.Get)
                requestMessage.Content = content;

            var response = await clientRest.SendAsync(requestMessage);
            labelResponse.Text = await response.Content.ReadAsStringAsync();

            var heders = response.Headers;

            label1.Text = "-------->Response Heders:\n";
            foreach (var heder in heders)
            {
                label1.Text += heder.Key + " = ";
                foreach (var val in heder.Value)
                    label1.Text += val + "  ";
                label1.Text += "\n";
            }

            label1.Text += "\n-------->Content Heders:\n";

            foreach (var heder in response.Content.Headers)
            {
                label1.Text += heder.Key + " = ";
                foreach (var val in heder.Value)
                    label1.Text += val + "  ";
                label1.Text += "\n";
            }     
        }
    }
}


