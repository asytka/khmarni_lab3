using Azure;
using System.Xml.Linq;

namespace khmarni_lab3
{
    public partial class Form1 : Form
    {
        private AzureTableStorageService _storageService;
        private AzureBlobStorageService _blobStorageService;
        private string currentPhotoUrl; // Store the current photo URL for the contact being added/edited.

        public Form1()
        {
            InitializeComponent();

            // Table Storage initialization
            string tableStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=lab3bohush;AccountKey=nrDw0Nqi17udXisxxqE81l0+cXWfy3ZU3xneOxNC76fp13wLur1wCJBca1HOerN0NjlkidQGmolp+AStAycSWA==;EndpointSuffix=core.windows.net";
            string tableName = "Contacts";
            _storageService = new AzureTableStorageService(tableStorageConnectionString, tableName);

            // Blob Storage initialization
                    string blobStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=lab3bohush;AccountKey=nrDw0Nqi17udXisxxqE81l0+cXWfy3ZU3xneOxNC76fp13wLur1wCJBca1HOerN0NjlkidQGmolp+AStAycSWA==;EndpointSuffix=core.windows.net";
            string blobContainerName = "contactphotos";
            _blobStorageService = new AzureBlobStorageService(blobStorageConnectionString, blobContainerName);

            lvContacts.SelectedIndexChanged += lvContacts_SelectedIndexChanged;

        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            lvContacts.Items.Clear();

            // Fetch all contacts from Azure Table Storage
            var contacts = await _storageService.GetAllContactsAsync();
            foreach (var contact in contacts)
            {
                var listItem = new ListViewItem(contact.FirstName); // Display First Name in ListView
                listItem.SubItems.Add(contact.Phone); // Add Phone as a sub-item
                listItem.Tag = contact; // Store the contact entity in the Tag property
                lvContacts.Items.Add(listItem);
            }
        }

        private void lvContacts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvContacts.SelectedItems.Count == 0) return;

            var selectedItem = lvContacts.SelectedItems[0];
            var contact = (ContactEntity)selectedItem.Tag; // Retrieve the contact entity from Tag

            // Display contact details in the text fields
            txtFirstName.Text = contact.FirstName;
            txtLastName.Text = contact.LastName;
            txtFatherName.Text = contact.FatherName;
            txtAddress.Text = contact.Address;
            txtPhone.Text = contact.Phone;

            // Load photo into PictureBox
            if (!string.IsNullOrEmpty(contact.PhotoUrl))
            {
                try
                {
                    // Download and display photo from URL
                    using (var webClient = new System.Net.WebClient())
                    {
                        var imageBytes = webClient.DownloadData(contact.PhotoUrl);
                        using (var ms = new MemoryStream(imageBytes))
                        {
                            pictureBox1.Image = Image.FromStream(ms);
                            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom; // Ensure proportional scaling
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to load photo: {ex.Message}", "Error");
                }
            }
            else
            {
                pictureBox1.Image = null; // Clear photo if none exists
            }
        }


        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) || string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Please enter both Name and Phone Number.", "Validation Error");
                return;
            }

            // Create a new contact entity
            var contact = new ContactEntity
            {
                PartitionKey = "PhoneBook", // Use a fixed PartitionKey for grouping
                RowKey = Guid.NewGuid().ToString(), // Unique identifier
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                FatherName = txtFatherName.Text,
                Address = txtAddress.Text,
                PhotoUrl = currentPhotoUrl, // Set the uploaded photo URL
                Phone = txtPhone.Text
            };

            // Add to Azure Table Storage
            await _storageService.AddContactAsync(contact);

            // Refresh the UI
            var listItem = new ListViewItem(new[] {
                contact.FirstName,
                contact.LastName,
                contact.FatherName,
                contact.Address,
                contact.Phone
            });
            listItem.Tag = contact; // Attach the contact object to the list item
            lvContacts.Items.Add(listItem);

            // Clear fields
            txtFirstName.Clear();
            txtAddress.Clear();
            txtFatherName.Clear();
            txtLastName.Clear();
            txtPhone.Clear();
            pictureBox1.Image = null; // Clear the photo preview
            currentPhotoUrl = null; // Reset the photo URL

            MessageBox.Show("Successfully added a contact!", "Success!");
        }

        private async void uploadPhoto_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp",
                Title = "Select a Photo"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                string photoName = Guid.NewGuid().ToString() + Path.GetExtension(filePath);

                try
                {
                    // Upload photo to Blob Storage
                    currentPhotoUrl = await _blobStorageService.UploadFileAsync(filePath, photoName);

                    // Display the uploaded photo in the PictureBox
                    using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {
                        pictureBox1.Image = Image.FromStream(stream);
                        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to upload photo: {ex.Message}", "Error");
                }
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (lvContacts.SelectedItems.Count > 0)
            {
                var selectedItem = lvContacts.SelectedItems[0];
                var contact = (ContactEntity)selectedItem.Tag;

                try
                {
                    // Delete photo from Blob Storage (if exists)
                    if (!string.IsNullOrEmpty(contact.PhotoUrl))
                    {
                        string blobName = Path.GetFileName(new Uri(contact.PhotoUrl).LocalPath);
                        await _blobStorageService.DeleteBlobAsync(blobName);
                    }

                    // Delete contact from Table Storage
                    await _storageService.DeleteContactAsync(contact.PartitionKey, contact.RowKey);

                    // Remove from ListView
                    lvContacts.Items.Remove(selectedItem);

                    MessageBox.Show("Contact deleted successfully.", "Success!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to delete contact: {ex.Message}", "Error");
                }
            }
            else
            {
                MessageBox.Show("Please select a contact to delete.", "Error");
            }
        }

        private async void loadButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Clear existing items in the ListView
                lvContacts.Items.Clear();

                // Fetch all contacts from Azure Table Storage
                var contacts = await _storageService.GetAllContactsAsync();

                foreach (var contact in contacts)
                {
                    // Create a new ListView item for each contact
                    var listItem = new ListViewItem(contact.FirstName);
                    listItem.SubItems.Add(contact.LastName);
                    listItem.SubItems.Add(contact.FatherName);
                    listItem.SubItems.Add(contact.Address);
                    listItem.SubItems.Add(contact.Phone);
                    listItem.Tag = contact; // Store the ContactEntity for quick access

                    lvContacts.Items.Add(listItem); // Add item to ListView
                }

                MessageBox.Show("Contacts successfully loaded!", "Success");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading contacts: {ex.Message}", "Error");
            }
        }

        private async void lvContacts_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (lvContacts.SelectedItems.Count == 0) return;

            try
            {
                // Get the selected contact entity from the ListView's Tag property
                var selectedItem = lvContacts.SelectedItems[0];
                var contact = (ContactEntity)selectedItem.Tag;

                // Display contact details in the text fields
                txtFirstName.Text = contact.FirstName;
                txtLastName.Text = contact.LastName;
                txtFatherName.Text = contact.FatherName;
                txtAddress.Text = contact.Address;
                txtPhone.Text = contact.Phone;

                // If the contact has an associated photo, download and display it
                if (!string.IsNullOrEmpty(contact.PhotoUrl))
                {
                    using (var stream = await _blobStorageService.DownloadBlobAsync(contact.PhotoUrl))
                    {
                        pictureBox1.Image = Image.FromStream(stream);
                        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                }
                else
                {
                    pictureBox1.Image = null; // Clear the PictureBox if no photo exists
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error displaying contact details: {ex.Message}", "Error");
            }
        }

        private async void UpdateBtn_Click(object sender, EventArgs e)
        {
            if (lvContacts.SelectedItems.Count > 0)
            {
                var selectedItem = lvContacts.SelectedItems[0];
                var contact = (ContactEntity)selectedItem.Tag;

                try
                {
                    // Delete photo from Blob Storage (if exists)
                    if (!string.IsNullOrEmpty(contact.PhotoUrl))
                    {
                        string blobName = Path.GetFileName(new Uri(contact.PhotoUrl).LocalPath);
                        await _blobStorageService.DeleteBlobAsync(blobName);
                    }

                    // Delete contact from Table Storage
                    await _storageService.DeleteContactAsync(contact.PartitionKey, contact.RowKey);

                    // Remove from ListView
                    lvContacts.Items.Remove(selectedItem);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to update contact: {ex.Message}", "Error");
                }
            }
            else
            {
                MessageBox.Show("Please select a contact to update.", "Error");
            }

            if (string.IsNullOrWhiteSpace(txtFirstName.Text) || string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                return;
            }

            // Create a new contact entity
            var updatedContact = new ContactEntity
            {
                PartitionKey = "PhoneBook", // Use a fixed PartitionKey for grouping
                RowKey = Guid.NewGuid().ToString(), // Unique identifier
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                FatherName = txtFatherName.Text,
                Address = txtAddress.Text,
                PhotoUrl = currentPhotoUrl, // Set the uploaded photo URL
                Phone = txtPhone.Text
            };

            // Add to Azure Table Storage
            await _storageService.AddContactAsync(updatedContact);

            // Refresh the UI
            var listItem = new ListViewItem(new[] {
                updatedContact.FirstName,
                updatedContact.LastName,
                updatedContact.FatherName,
                updatedContact.Address,
                updatedContact.Phone
            });
            listItem.Tag = updatedContact; // Attach the contact object to the list item
            lvContacts.Items.Add(listItem);

            // Clear fields
            txtFirstName.Clear();
            txtAddress.Clear();
            txtFatherName.Clear();
            txtLastName.Clear();
            txtPhone.Clear();
            pictureBox1.Image = null; // Clear the photo preview
            currentPhotoUrl = null; // Reset the photo URL

            MessageBox.Show("Successfully updated a contact!", "Success!");
        }
    }
}
