using System;
using System.Windows.Forms;

namespace WindowsFormsApp7
{
    public partial class UserControl1 : UserControl
    {
        public int PartnerId { get; private set; }
        public event Action<int> OnEditRequested;
        public UserControl1()
        {
            InitializeComponent();
            this.Click += UserControlClick;
            foreach (Control control in this.Controls)
                control.Click += UserControlClick;
        }

        public void DataSet(int partnerId, string type, string name, string director, string phone, byte rating, int discount)
        {
            this.PartnerId = partnerId;
            label1.Text = type + " | " + name + "\n\n\n" + director + "\n\n" + "+7" + phone + "\n\n" + "Рейтинг :" + rating;
            label2.Text = discount + "%";
        }

        public void UserControlClick(object sender, EventArgs e)
        {
            OnEditRequested(PartnerId);

        }
    }
}
