using System;
using System.Drawing;
using System.Windows.Forms;

namespace Cau4
{
    public class FormDanhBa : Form
    {
        private readonly ListBox lstLienHe = new ListBox();
        private readonly TextBox txtTen = new TextBox();
        private readonly TextBox txtSDT = new TextBox();
        private readonly Button btnThem = new Button();
        private readonly Button btnSua = new Button();
        private readonly Button btnXoa = new Button();
        private readonly Button btnThoat = new Button();

        private int _indexDangSua = -1;

        public FormDanhBa()
        {
            Text = "Quản lý danh bạ điện thoại";
            ClientSize = new Size(520, 300);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;

            lstLienHe.Location = new Point(20, 20);
            lstLienHe.Size = new Size(230, 250);

            var lblTen = new Label { Text = "Tên:", Location = new Point(270, 24), AutoSize = true };
            txtTen.Location = new Point(330, 20);
            txtTen.Width = 170;

            var lblSDT = new Label { Text = "SĐT:", Location = new Point(270, 62), AutoSize = true };
            txtSDT.Location = new Point(330, 58);
            txtSDT.Width = 170;

            btnThem.Text = "Thêm";
            btnThem.Location = new Point(270, 110);
            btnThem.Size = new Size(110, 32);
            btnThem.Click += btnThem_Click;

            btnSua.Text = "Sửa";
            btnSua.Location = new Point(390, 110);
            btnSua.Size = new Size(110, 32);
            btnSua.Click += btnSua_Click;

            btnXoa.Text = "Xóa";
            btnXoa.Location = new Point(270, 152);
            btnXoa.Size = new Size(110, 32);
            btnXoa.Click += btnXoa_Click;

            btnThoat.Text = "Thoát";
            btnThoat.Location = new Point(390, 152);
            btnThoat.Size = new Size(110, 32);
            btnThoat.Click += (s, e) => Close();

            Controls.AddRange(new Control[]
            {
                lstLienHe, lblTen, txtTen, lblSDT, txtSDT,
                btnThem, btnSua, btnXoa, btnThoat
            });

            FormClosing += FormDanhBa_FormClosing;
        }

        private void btnThem_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTen.Text) || string.IsNullOrWhiteSpace(txtSDT.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Tên và Số điện thoại!",
                    "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string lienHe = $"{txtTen.Text.Trim()} - {txtSDT.Text.Trim()}";

            if (_indexDangSua >= 0)
            {
                lstLienHe.Items[_indexDangSua] = lienHe;
                _indexDangSua = -1;
                XoaTextBox();
                MessageBox.Show("Cập nhật thành công.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                lstLienHe.Items.Add(lienHe);
                XoaTextBox();
                MessageBox.Show("Thêm thành công.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSua_Click(object? sender, EventArgs e)
        {
            if (lstLienHe.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn một liên hệ để sửa.",
                    "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _indexDangSua = lstLienHe.SelectedIndex;
            string[] phan = lstLienHe.SelectedItem!.ToString()!.Split(" - ", 2);
            txtTen.Text = phan[0];
            txtSDT.Text = phan.Length > 1 ? phan[1] : "";
            txtTen.Focus();
        }

        private void btnXoa_Click(object? sender, EventArgs e)
        {
            if (lstLienHe.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn một liên hệ để xóa",
                    "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string ten = lstLienHe.SelectedItem!.ToString()!.Split(" - ", 2)[0];
            DialogResult ketQua = MessageBox.Show(
                $"Bạn có chắc muốn xóa liên hệ {ten}? Thao tác này không thể hoàn tác!",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (ketQua == DialogResult.Yes)
            {
                lstLienHe.Items.RemoveAt(lstLienHe.SelectedIndex);
                MessageBox.Show("Xóa liên hệ thành công.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void FormDanhBa_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (txtTen.Text.Length > 0 || txtSDT.Text.Length > 0)
            {
                DialogResult ketQua = MessageBox.Show(
                    "Bạn có dữ liệu chưa được lưu. Bạn muốn thoát không?",
                    "Cảnh báo", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

                if (ketQua == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else if (ketQua == DialogResult.No)
                {
                    XoaTextBox();
                }
            }
        }

        private void XoaTextBox()
        {
            txtTen.Clear();
            txtSDT.Clear();
        }
    }
}
