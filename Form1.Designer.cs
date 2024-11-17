namespace khmarni_lab3
{
    partial class Form1
    {
       private System.ComponentModel.IContainer components = null;
        private TextBox txtFirstName;
        private TextBox txtPhone;
        private Button btnAdd;
        private Button btnDelete;
        private ListView lvContacts;

        private void InitializeComponent()
        {
            txtFirstName = new TextBox();
            txtPhone = new TextBox();
            btnAdd = new Button();
            btnDelete = new Button();
            lvContacts = new ListView();
            FirstName = new ColumnHeader();
            LastName = new ColumnHeader();
            FatherName = new ColumnHeader();
            Number = new ColumnHeader();
            Address = new ColumnHeader();
            txtLastName = new TextBox();
            txtFatherName = new TextBox();
            txtAddress = new TextBox();
            pictureBox1 = new PictureBox();
            openFileDialog1 = new OpenFileDialog();
            uploadPhoto = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            loadButton = new Button();
            UpdateBtn = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // txtFirstName
            // 
            txtFirstName.Location = new Point(138, 14);
            txtFirstName.Margin = new Padding(4, 3, 4, 3);
            txtFirstName.Name = "txtFirstName";
            txtFirstName.Size = new Size(286, 23);
            txtFirstName.TabIndex = 0;
            // 
            // txtPhone
            // 
            txtPhone.Location = new Point(138, 101);
            txtPhone.Margin = new Padding(4, 3, 4, 3);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(286, 23);
            txtPhone.TabIndex = 1;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(138, 159);
            btnAdd.Margin = new Padding(4, 3, 4, 3);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(190, 27);
            btnAdd.TabIndex = 2;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(336, 159);
            btnDelete.Margin = new Padding(4, 3, 4, 3);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(88, 27);
            btnDelete.TabIndex = 3;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // lvContacts
            // 
            lvContacts.Columns.AddRange(new ColumnHeader[] { FirstName, LastName, FatherName, Address, Number });
            lvContacts.FullRowSelect = true;
            lvContacts.Location = new Point(13, 370);
            lvContacts.Margin = new Padding(4, 3, 4, 3);
            lvContacts.Name = "lvContacts";
            lvContacts.Size = new Size(578, 172);
            lvContacts.TabIndex = 4;
            lvContacts.UseCompatibleStateImageBehavior = false;
            lvContacts.View = View.Details;
            lvContacts.SelectedIndexChanged += lvContacts_SelectedIndexChanged_1;
            // 
            // FirstName
            // 
            FirstName.Text = "First Name";
            FirstName.Width = 100;
            // 
            // LastName
            // 
            LastName.Text = "Last Name";
            LastName.Width = 100;
            // 
            // FatherName
            // 
            FatherName.Text = "Father Name";
            FatherName.Width = 100;
            // 
            // Number
            // 
            Number.DisplayIndex = 3;
            Number.Text = "Number";
            // 
            // Address
            // 
            Address.DisplayIndex = 4;
            Address.Text = "Address";
            Address.Width = 100;
            // 
            // txtLastName
            // 
            txtLastName.Location = new Point(138, 43);
            txtLastName.Margin = new Padding(4, 3, 4, 3);
            txtLastName.Name = "txtLastName";
            txtLastName.Size = new Size(286, 23);
            txtLastName.TabIndex = 5;
            // 
            // txtFatherName
            // 
            txtFatherName.Location = new Point(138, 72);
            txtFatherName.Margin = new Padding(4, 3, 4, 3);
            txtFatherName.Name = "txtFatherName";
            txtFatherName.Size = new Size(286, 23);
            txtFatherName.TabIndex = 6;
            // 
            // txtAddress
            // 
            txtAddress.Location = new Point(138, 130);
            txtAddress.Margin = new Padding(4, 3, 4, 3);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(286, 23);
            txtAddress.TabIndex = 7;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(431, 14);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(160, 143);
            pictureBox1.TabIndex = 8;
            pictureBox1.TabStop = false;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // uploadPhoto
            // 
            uploadPhoto.Location = new Point(431, 163);
            uploadPhoto.Name = "uploadPhoto";
            uploadPhoto.Size = new Size(160, 23);
            uploadPhoto.TabIndex = 9;
            uploadPhoto.Text = "Upload Photo";
            uploadPhoto.UseVisualStyleBackColor = true;
            uploadPhoto.Click += uploadPhoto_Click_1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(13, 17);
            label1.Name = "label1";
            label1.Size = new Size(64, 15);
            label1.TabIndex = 10;
            label1.Text = "First Name";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(14, 46);
            label2.Name = "label2";
            label2.Size = new Size(63, 15);
            label2.TabIndex = 11;
            label2.Text = "Last Name";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(14, 75);
            label3.Name = "label3";
            label3.Size = new Size(75, 15);
            label3.TabIndex = 12;
            label3.Text = "Father Name";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(14, 133);
            label4.Name = "label4";
            label4.Size = new Size(49, 15);
            label4.TabIndex = 13;
            label4.Text = "Address";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(14, 104);
            label5.Name = "label5";
            label5.Size = new Size(86, 15);
            label5.TabIndex = 14;
            label5.Text = "Phone number";
            // 
            // loadButton
            // 
            loadButton.Location = new Point(12, 336);
            loadButton.Name = "loadButton";
            loadButton.Size = new Size(139, 28);
            loadButton.TabIndex = 15;
            loadButton.Text = "Load information";
            loadButton.UseVisualStyleBackColor = true;
            loadButton.Click += loadButton_Click;
            // 
            // UpdateBtn
            // 
            UpdateBtn.Location = new Point(137, 192);
            UpdateBtn.Name = "UpdateBtn";
            UpdateBtn.Size = new Size(191, 29);
            UpdateBtn.TabIndex = 16;
            UpdateBtn.Text = "Update";
            UpdateBtn.UseVisualStyleBackColor = true;
            UpdateBtn.Click += UpdateBtn_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(603, 554);
            Controls.Add(UpdateBtn);
            Controls.Add(loadButton);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(uploadPhoto);
            Controls.Add(pictureBox1);
            Controls.Add(txtAddress);
            Controls.Add(txtFatherName);
            Controls.Add(txtLastName);
            Controls.Add(lvContacts);
            Controls.Add(btnDelete);
            Controls.Add(btnAdd);
            Controls.Add(txtPhone);
            Controls.Add(txtFirstName);
            Margin = new Padding(4, 3, 4, 3);
            Name = "Form1";
            Text = "Phone Book";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private TextBox txtLastName;
        private TextBox txtFatherName;
        private TextBox txtAddress;
        private PictureBox pictureBox1;
        private OpenFileDialog openFileDialog1;
        private Button uploadPhoto;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Button loadButton;
        private Button UpdateBtn;
        private ColumnHeader FirstName;
        private ColumnHeader LastName;
        private ColumnHeader FatherName;
        private ColumnHeader Number;
        private ColumnHeader Address;
    }
}
