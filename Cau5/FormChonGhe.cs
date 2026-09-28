using System;
using System.Drawing;
using System.Windows.Forms;

namespace Cau5
{
    public class FormChonGhe : Form
    {
        private readonly ListBox lstGhe = new ListBox();
        private readonly Label lblGheDaChon = new Label();
        private readonly Button btnXacNhan = new Button();
        private readonly Button btnBoQua = new Button();

        public string GheChon { get; private set; } = "";

        public FormChonGhe(string gheHienTai)
        {
            Text = "Chọn ghế";
            ClientSize = new Size(300, 300);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;

            lstGhe.Location = new Point(20, 20);
            lstGhe.Size = new Size(120, 200);
            foreach (string hang in new[] { "A", "B", "C" })
            {
                for (int i = 1; i <= 5; i++)
                {
                    lstGhe.Items.Add(hang + i);
                }
            }
            lstGhe.SelectedIndexChanged += lstGhe_SelectedIndexChanged;

            lblGheDaChon.Text = "Đang chọn: (chưa chọn)";
            lblGheDaChon.Location = new Point(160, 20);
            lblGheDaChon.AutoSize = true;

            btnXacNhan.Text = "Xác nhận";
            btnXacNhan.Location = new Point(20, 240);
            btnXacNhan.Size = new Size(120, 32);
            btnXacNhan.Click += btnXacNhan_Click;

            btnBoQua.Text = "Bỏ qua";
            btnBoQua.Location = new Point(160, 240);
            btnBoQua.Size = new Size(120, 32);
            btnBoQua.Click += (s, e) => DialogResult = DialogResult.Cancel;

            Controls.AddRange(new Control[] { lstGhe, lblGheDaChon, btnXacNhan, btnBoQua });

            if (!string.IsNullOrEmpty(gheHienTai))
            {
                lstGhe.SelectedItem = gheHienTai;
            }
        }

        private void lstGhe_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (lstGhe.SelectedItem != null)
            {
                lblGheDaChon.Text = $"Đang chọn: {lstGhe.SelectedItem}";
            }
        }

        private void btnXacNhan_Click(object? sender, EventArgs e)
        {
            if (lstGhe.SelectedItem == null)
            {
                MessageBox.Show("Bạn chưa chọn ghế nào! Vui lòng chọn một ghế.",
                    "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            GheChon = lstGhe.SelectedItem.ToString()!;
            DialogResult = DialogResult.OK;
        }
    }
}
