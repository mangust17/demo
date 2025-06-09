using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp7
{
    public partial class Edit : Form
    {
        private readonly AppDbContext db = new AppDbContext();
        private Partners_import partnerToEdit;
        private readonly Action reloadCallback;
        public Edit(Action reloadCallback)
        {
            InitializeComponent();
            this.reloadCallback = reloadCallback;
            LoadPartnerTypes();
            this.Text = "Добавление партнера";
            button1.Text = "Добавить";

        }

        public Edit(Action reloadCallback, Partners_import partner) : this(reloadCallback)
        {
            partnerToEdit = partner;
            if (partnerToEdit != null)
            {
                textBox1.Text = partner.partner;
                comboBox1.SelectedValue = partner.partner_type_id;
                textBox3.Text = partner.rating.ToString();
                textBox4.Text = partner.adress;
                textBox5.Text = partner.director;
                textBox6.Text = partner.phone;
                textBox7.Text = partner.email;
                this.Text = $"Обноваление партнера {partner.id}";
                button1.Text = "Изменить";
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(textBox1.Text) || comboBox1.SelectedValue == null || string.IsNullOrWhiteSpace(textBox4.Text) || string.IsNullOrWhiteSpace(textBox5.Text))
                {
                    MessageBox.Show("Заполните все обяхательные поля!","Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return;

                }

                if (byte.TryParse(textBox3.Text, out byte rate) && rate > 11)

                {
                    MessageBox.Show("Рейтинг должен быть целым неотрицательным числом от 0 до 10!", "Ошибка",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (partnerToEdit == null)
                {
                    byte newId = 1;

                    if (db.partners_Imports.Any())
                    {
                        newId = (byte)(db.partners_Imports.Max(p => p.id)+1);
                    }
                    var newPartner = new Partners_import
                    {
                        id = newId,
                        partner = textBox1.Text,
                        partner_type_id = Convert.ToByte(comboBox1.SelectedValue),
                        rating = rate,
                        adress = textBox4.Text,
                        director = textBox5.Text,
                        phone = textBox6.Text,
                        email = textBox7.Text
                    };

                    db.partners_Imports.Add(newPartner);
                }
                else
                {
                    var existingPartner = db.partners_Imports
                        .FirstOrDefault(p => p.id == partnerToEdit.id);

                    if (existingPartner == null)
                    {
                        MessageBox.Show("Партнёр не найден для редактирования.", "Ошибка",
                                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    existingPartner.partner = textBox1.Text;
                    existingPartner.partner_type_id = Convert.ToByte(comboBox1.SelectedValue);
                    existingPartner.rating = rate;
                    existingPartner.adress = textBox4.Text;
                    existingPartner.director = textBox5.Text;
                    existingPartner.phone = textBox6.Text;
                    existingPartner.email = textBox7.Text;
                }
                MessageBox.Show($"Выбранный тип партнёра ID: {comboBox1.SelectedValue}");
                db.SaveChanges();
                MessageBox.Show("Партнёр успешно сохранён!", "Успех",
                               MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                string deepMessage = ex.InnerException?.InnerException?.Message
                                     ?? ex.InnerException?.Message
                                     ?? ex.Message;

                MessageBox.Show($"Ошибка при сохранении:\n{deepMessage}",
                                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void LoadPartnerTypes()
        {
            try
            {
                var partnerTypes = db.partner_Types
                                       .OrderBy(t => t.type)
                                       .ToList();

                comboBox1.DataSource = partnerTypes;
                comboBox1.DisplayMember = "type";
                comboBox1.ValueMember = "id";
                comboBox1.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке типов партнеров: {ex.Message}",
                               "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
