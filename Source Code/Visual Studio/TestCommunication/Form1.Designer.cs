
namespace TestCommunication
{
    partial class TestCommunicationForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.getRequest = new System.Windows.Forms.Button();
            this.postRequest = new System.Windows.Forms.Button();
            this.messageDisplay = new System.Windows.Forms.Label();
            this.getResponse = new System.Windows.Forms.TextBox();
            this.postData = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // getRequest
            // 
            this.getRequest.BackColor = System.Drawing.Color.Firebrick;
            this.getRequest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.getRequest.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.getRequest.ForeColor = System.Drawing.Color.Transparent;
            this.getRequest.Location = new System.Drawing.Point(125, 300);
            this.getRequest.Name = "getRequest";
            this.getRequest.Size = new System.Drawing.Size(100, 30);
            this.getRequest.TabIndex = 0;
            this.getRequest.Text = "GET";
            this.getRequest.UseVisualStyleBackColor = false;
            this.getRequest.Click += new System.EventHandler(this.getRequest_Click);
            // 
            // postRequest
            // 
            this.postRequest.BackColor = System.Drawing.Color.Firebrick;
            this.postRequest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.postRequest.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.postRequest.ForeColor = System.Drawing.Color.Transparent;
            this.postRequest.Location = new System.Drawing.Point(500, 300);
            this.postRequest.Name = "postRequest";
            this.postRequest.Size = new System.Drawing.Size(100, 30);
            this.postRequest.TabIndex = 1;
            this.postRequest.Text = "POST";
            this.postRequest.UseVisualStyleBackColor = false;
            this.postRequest.Click += new System.EventHandler(this.postRequest_Click);
            // 
            // messageDisplay
            // 
            this.messageDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.messageDisplay.ForeColor = System.Drawing.Color.Transparent;
            this.messageDisplay.Location = new System.Drawing.Point(12, 333);
            this.messageDisplay.Name = "messageDisplay";
            this.messageDisplay.Size = new System.Drawing.Size(710, 69);
            this.messageDisplay.TabIndex = 2;
            this.messageDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // getResponse
            // 
            this.getResponse.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.getResponse.Location = new System.Drawing.Point(12, 12);
            this.getResponse.Multiline = true;
            this.getResponse.Name = "getResponse";
            this.getResponse.ReadOnly = true;
            this.getResponse.Size = new System.Drawing.Size(354, 282);
            this.getResponse.TabIndex = 3;
            // 
            // postData
            // 
            this.postData.AcceptsReturn = true;
            this.postData.AcceptsTab = true;
            this.postData.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.postData.Location = new System.Drawing.Point(372, 12);
            this.postData.Multiline = true;
            this.postData.Name = "postData";
            this.postData.Size = new System.Drawing.Size(350, 282);
            this.postData.TabIndex = 2;
            // 
            // TestCommunicationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Firebrick;
            this.ClientSize = new System.Drawing.Size(734, 411);
            this.Controls.Add(this.postData);
            this.Controls.Add(this.getResponse);
            this.Controls.Add(this.messageDisplay);
            this.Controls.Add(this.postRequest);
            this.Controls.Add(this.getRequest);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(750, 450);
            this.MinimumSize = new System.Drawing.Size(600, 300);
            this.Name = "TestCommunicationForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Test Communication";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button getRequest;
        private System.Windows.Forms.Button postRequest;
        private System.Windows.Forms.Label messageDisplay;
        private System.Windows.Forms.TextBox getResponse;
        private System.Windows.Forms.TextBox postData;
    }
}

