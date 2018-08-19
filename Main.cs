using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CaptchaSolve
{
    public Main
    {
		string api_key = "12345678901234567890123456789012";
        string api_secret = "12345678";

        public Form1()
        {
            InitializeComponent();
        }

        private void CaptchaSolve()
        {
            System.Net.ServicePointManager.Expect100Continue = false;
            
            string result = FormUpload.MultipartFormDataPost("MyApp", api_key, api_secret, txtPath.Text);
			
			Console.WriteLine(result);
        }

        public async void http_post()
        {
            HttpClient httpClient = new HttpClient();
            MultipartFormDataContent form = new MultipartFormDataContent();
            System.Net.ServicePointManager.Expect100Continue = false;

            form.Add(new StringContent("upload"), "p");
            form.Add(new StringContent(api_key), "key");
            form.Add(new StringContent(api_secret), "secret");
            form.Add(new StringContent("text"), "out");


            byte[] fileBytes = File.ReadAllBytes("1.bmp");

            form.Add(new ByteArrayContent(fileBytes, 0, fileBytes.Length), "captcha", "1.bmp");
            HttpResponseMessage response = await httpClient.PostAsync("http://api.captchasolutions.com/solve", form);

            response.EnsureSuccessStatusCode();
            httpClient.Dispose();
            string sd = response.Content.ReadAsStringAsync().Result;

            Console.WriteLine(result);
        }

        private void Open_Browser()
        {
            using (var selectFileDialog = new OpenFileDialog())
            {
                selectFileDialog.Filter = "Image files|*.bmp;*.jpg";
                selectFileDialog.Title = "Output file...";
                if (selectFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtPath.Text = selectFileDialog.FileName;
                }
            }
        }
    }
}
