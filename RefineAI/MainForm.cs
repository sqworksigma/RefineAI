using System;
using System.Drawing;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace RefineAI
{
    public partial class MainForm : Form
    {
        // Replace with your actual OpenAI API key
        private const string OpenAI_API_KEY = "your-openai-api-key";
        private const string API_URL = "https://api.openai.com/v1/chat/completions";

        private TextBox userInputBox;
        private Button refineButton;
        private Label infoLabel;

        public MainForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.userInputBox = new System.Windows.Forms.TextBox();
            this.refineButton = new System.Windows.Forms.Button();
            this.infoLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // userInputBox
            this.userInputBox.Location = new System.Drawing.Point(20, 50);
            this.userInputBox.Multiline = true;
            this.userInputBox.Size = new System.Drawing.Size(540, 150);
            this.userInputBox.Font = new System.Drawing.Font("Arial", 12);

            // refineButton
            this.refineButton.Text = "Refine Text";
            this.refineButton.Location = new System.Drawing.Point(20, 220);
            this.refineButton.Size = new System.Drawing.Size(100, 40);
            this.refineButton.BackColor = System.Drawing.Color.Black;
            this.refineButton.ForeColor = System.Drawing.Color.White;
            this.refineButton.Click += new System.EventHandler(this.RefineButton_Click);

            // infoLabel
            this.infoLabel.Text = "Type your text and press refine:";
            this.infoLabel.Location = new System.Drawing.Point(20, 20);
            this.infoLabel.AutoSize = true;
            this.infoLabel.Font = new System.Drawing.Font("Arial", 12);

            // MainForm
            this.Controls.Add(this.userInputBox);
            this.Controls.Add(this.refineButton);
            this.Controls.Add(this.infoLabel);
            this.Text = "RefineAI";
            this.Size = new System.Drawing.Size(600, 400);
            this.BackColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.ResumeLayout(false);
            this.PerformLayout();

            // Bind the event handler to the button
            this.refineButton.Click += new System.EventHandler(this.RefineButton_Click);
        }

        private async void RefineButton_Click(object sender, EventArgs e)
        {
            await RefineText();
        }

        private async Task RefineText()
        {
            string userInput = userInputBox.Text;
            if (string.IsNullOrWhiteSpace(userInput))
            {
                MessageBox.Show("Please enter some text.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string refinedText = await GetAIResponse(userInput);
            if (!string.IsNullOrWhiteSpace(refinedText))
            {
                userInputBox.Text = refinedText;
            }
        }

        private async Task<string> GetAIResponse(string input)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {OpenAI_API_KEY}");

                    var requestData = new
                    {
                        model = "gpt-4o-mini",
                        messages = new[] { new { role = "user", content = $"Make this more formal: {input}" } }
                    };

                    string jsonContent = JsonConvert.SerializeObject(requestData);
                    HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(API_URL, content);

                    if (!response.IsSuccessStatusCode)
                    {
                        string errorContent = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"API Error: {response.StatusCode}\n{errorContent}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }

                    string responseJson = await response.Content.ReadAsStringAsync();
                    dynamic result = JsonConvert.DeserializeObject(responseJson);
                    return result?.choices?[0]?.message?.content?.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
}
