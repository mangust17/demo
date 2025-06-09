using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp7
{
    public partial class Form1 : Form
    {
        private AppDbContext db = new AppDbContext();
        public Form1()
        {

            InitializeComponent();
            LoadPartners();
        }

        public void LoadPartners()
        {
            using (var newDb = new AppDbContext()) {
                flowLayoutPanel1.Controls.Clear();
                var partners = newDb.partners_Imports.ToList();
                foreach (var partner in partners)
                {
                    var total = newDb.partner_Products_Imports.Where(r=>r.partner_id == partner.id).Sum(r=> (int?)r.prod_ammount)??0;
                    var discount = total >= 30000 ? 15 : total >= 50000 ? 10 : total >= 100000 ? 5 : 0;
                    var type = newDb.partner_Types.FirstOrDefault(t => t.id == partner.partner_type_id)?.type ?? "Неизвестный тип";
                    var uc = new UserControl1();
                    uc.DataSet(partner.id, type, partner.partner, partner.director, partner.phone, partner.rating, discount);
                    uc.OnEditRequested += OpenEditForm;
                    flowLayoutPanel1.Controls.Add(uc);
                }
            } 
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var edit = new Edit(() => LoadPartners());
            if (edit.ShowDialog() == DialogResult.OK)
            {
                LoadPartners();
            }
        }
        public void OpenEditForm(int partner_id)
        {
            var partner = db.partners_Imports.FirstOrDefault(p => p.id == partner_id);
            if (partner != null) {
                var edit = new Edit(() => LoadPartners(), partner);
                if (edit.ShowDialog() == DialogResult.OK)
                {
                    LoadPartners();
                }
               
            }
        }
        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
