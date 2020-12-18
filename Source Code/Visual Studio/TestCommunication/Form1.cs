using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Forms;

namespace TestCommunication
{
    public partial class TestCommunicationForm : Form
    {
        HttpClient client = new HttpClient();
        Uri uri = new Uri("https://testcommunications.glitch.me");

        public TestCommunicationForm()
        {
            InitializeComponent();
            client.BaseAddress = uri;
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(new ProductHeaderValue("AnyStringOrName", "1.0")));
        }
        private async void getRequest_Click(object sender, EventArgs e)
        {
            var response = await client.GetAsync("/Messages");

            var data = response.Content.ReadAsStringAsync().Result;
            var message = "HTTP Response: " + response.StatusCode.ToString();

            messageDisplay.Text = message;
            getResponse.Text = data;
        }

        private async void postRequest_Click(object sender, EventArgs e)
        {
            var json = JsonConvert.SerializeObject(postData.Text);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            postData.Text = "";

            var response = await client.PostAsync("/Messages", data);

            postData.Text = response.Content.ReadAsStringAsync().Result;
            messageDisplay.Text = "HTTP Response: " + response.StatusCode.ToString();
        }
    }
}