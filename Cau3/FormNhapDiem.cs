using System;
using System.Drawing;
using System.Windows.Forms;

namespace Cau3
{
    public class FormNhapDiem : Form
    {
        private readonly TextBox txtMaHS = new TextBox();
        private readonly TextBox txtHoTen = new TextBox();
        private readonly TextBox txtToan = new TextBox();
        private readonly TextBox txtVan = new TextBox();
        private readonly TextBox txtAnh = new TextBox();
        private readonly Button btnLuu = new Button();
        private readonly Button btnXoaTrang = new Button();
        private readonly ListBox lstKetQua = new ListBox();
        private readonly ErrorProvider errorProvider1 = new ErrorProvider();

        public FormNhapDiem()
        {
            Text = "Nhập điểm học sinh";
            ClientSize = new Size(560, 320);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;

            errorProvider1.ContainerControl = this;

            string[] nhan = { "Mã học sinh:", "Họ tên:", "Điểm Toán:", "Điểm Văn:", "Điểm Anh:" };
            TextBox[] oNhap = { txtMaHS, txtHoTen, txtToan, txtVan, txtAnh };

            for (int i = 0; i < oNhap.Length; i++)
            {
                var lbl = new Label
                {
                    Text = nhan[i],
                    Location = new Point(20, 22 + i * 38),
                    AutoSize = true,
                    TabStop = false
                };
                oNhap[i].Location = new Point(110, 18 + i * 38);
                oNhap[i].Width = 160;
                oNhap[i].TabIndex = i;
                Controls.Add(lbl);
                Controls.Add(oNhap[i]);
            }

            btnLuu.Text = "Lưu";
            btnLuu.Location = new Point(110, 215);
            btnLuu.Size = new Size(75, 32);
            btnLuu.TabIndex = 5;
            btnLuu.Click += btnLuu_Click;

            btnXoaTrang.Text = "Xóa trắng";
            btnXoaTrang.Location = new Point(195, 215);
            btnXoaTrang.Size = new Size(75, 32);
            btnXoaTrang.TabIndex = 6;
            btnXoaTrang.CausesValidation = false;
            btnXoaTrang.Click += btnXoaTrang_Click;

            lstKetQua.Location = new Point(295, 18);
            lstKetQua.Size = new Size(245, 280);
            lstKetQua.TabStop = false;

            Controls.Add(btnLuu);
            Controls.Add(btnXoaTrang);
            Controls.Add(lstKetQua);

            DangKyEnterChuyenField();

            txtToan.Enter += TextBoxDiem_Enter;
            txtVan.Enter += TextBoxDiem_Enter;
            txtAnh.Enter += TextBoxDiem_Enter;
        }

        private void DangKyEnterChuyenField()
        {
            foreach (Control control in Controls)
            {
                if (control is TextBox txt)
                {
                    txt.KeyPress += (sender, e) =>
                    {
                        if (e.KeyChar == (char)Keys.Enter)
                        {
                            e.Handled = true;
                            if (sender == txtAnh)
                            {
                                btnLuu.PerformClick();
                            }
                            else
                            {
                                SelectNextControl((Control)sender!, true, true, true, true);
                            }
                        }
                    };
                }
            }
        }

        private void TextBoxDiem_Enter(object? sender, EventArgs e)
        {
            ((TextBox)sender!).SelectAll();
        }

        private bool KiemTraDiem(TextBox txt, string tenMon, out decimal diem)
        {
            if (!decimal.TryParse(txt.Text.Trim(), out diem) || diem < 0.0m || diem > 10.0m)
            {
                errorProvider1.SetError(txt, $"Điểm {tenMon} phải là số từ 0.0 đến 10.0!");
                return false;
            }

            errorProvider1.SetError(txt, "");
            return true;
        }

        private void btnLuu_Click(object? sender, EventArgs e)
        {
            bool hopLe = KiemTraDiem(txtToan, "Toán", out decimal toan);
            hopLe &= KiemTraDiem(txtVan, "Văn", out decimal van);
            hopLe &= KiemTraDiem(txtAnh, "Anh", out decimal anh);

            if (!hopLe)
            {
                return;
            }

            lstKetQua.Items.Add($"{txtMaHS.Text} | {txtHoTen.Text} | T:{toan} V:{van} A:{anh}");
            XoaTrangForm();
        }

        private void btnXoaTrang_Click(object? sender, EventArgs e)
        {
            XoaTrangForm();
        }

        private void XoaTrangForm()
        {
            txtMaHS.Clear();
            txtHoTen.Clear();
            txtToan.Clear();
            txtVan.Clear();
            txtAnh.Clear();
            errorProvider1.Clear();
            txtMaHS.Focus();
        }
    }
}
