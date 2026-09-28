using System;
using System.Drawing;
using System.Windows.Forms;

namespace Cau5
{
    public class FormBanVe : Form
    {
        private readonly TextBox txtTenKhach = new TextBox();
        private readonly ComboBox cboPhim = new ComboBox();
        private readonly ComboBox cboSuatChieu = new ComboBox();
        private readonly TextBox txtGheDaChon = new TextBox();
        private readonly Button btnChonGhe = new Button();
        private readonly Button btnDatVe = new Button();
        private readonly Button btnHuy = new Button();

        private const decimal GiaVe = 75000m;

        public FormBanVe()
        {
            Text = "Bán vé xem phim";
            ClientSize = new Size(420, 280);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;

            var lblTen = new Label { Text = "Tên khách:", Location = new Point(20, 24), AutoSize = true };
            txtTenKhach.Location = new Point(130, 20);
            txtTenKhach.Width = 250;

            var lblPhim = new Label { Text = "Phim:", Location = new Point(20, 62), AutoSize = true };
            cboPhim.Location = new Point(130, 58);
            cboPhim.Width = 250;
            cboPhim.DropDownStyle = ComboBoxStyle.DropDownList;
            cboPhim.Items.AddRange(new object[] { "Mai", "Đào, Phở và Piano", "Lật Mặt 7", "Godzilla x Kong" });

            var lblSuat = new Label { Text = "Suất chiếu:", Location = new Point(20, 100), AutoSize = true };
            cboSuatChieu.Location = new Point(130, 96);
            cboSuatChieu.Width = 250;
            cboSuatChieu.DropDownStyle = ComboBoxStyle.DropDownList;
            cboSuatChieu.Items.AddRange(new object[] { "10:00", "14:00", "18:00", "21:00" });

            var lblGhe = new Label { Text = "Ghế đã chọn:", Location = new Point(20, 138), AutoSize = true };
            txtGheDaChon.Location = new Point(130, 134);
            txtGheDaChon.Width = 140;
            txtGheDaChon.ReadOnly = true;

            btnChonGhe.Text = "Chọn ghế...";
            btnChonGhe.Location = new Point(280, 132);
            btnChonGhe.Size = new Size(100, 28);
            btnChonGhe.Click += btnChonGhe_Click;

            btnDatVe.Text = "Đặt vé";
            btnDatVe.Location = new Point(130, 200);
            btnDatVe.Size = new Size(110, 34);
            btnDatVe.Click += btnDatVe_Click;

            btnHuy.Text = "Hủy";
            btnHuy.Location = new Point(270, 200);
            btnHuy.Size = new Size(110, 34);
            btnHuy.Click += btnHuy_Click;

            Controls.AddRange(new Control[]
            {
                lblTen, txtTenKhach, lblPhim, cboPhim, lblSuat, cboSuatChieu,
                lblGhe, txtGheDaChon, btnChonGhe, btnDatVe, btnHuy
            });
        }

        private void btnChonGhe_Click(object? sender, EventArgs e)
        {
            using (var dlg = new FormChonGhe(txtGheDaChon.Text))
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    txtGheDaChon.Text = dlg.GheChon;
                }
            }
        }

        private void btnDatVe_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenKhach.Text) ||
                cboPhim.SelectedIndex < 0 ||
                cboSuatChieu.SelectedIndex < 0 ||
                string.IsNullOrEmpty(txtGheDaChon.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ: tên khách, phim, suất chiếu và chọn ghế!",
                    "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show(
                "Đặt vé thành công!\n" +
                $"Khách: {txtTenKhach.Text}\n" +
                $"Phim: {cboPhim.SelectedItem}\n" +
                $"Suất chiếu: {cboSuatChieu.SelectedItem}\n" +
                $"Ghế: {txtGheDaChon.Text}\n" +
                $"Giá vé: {GiaVe:N0}đ",
                "Xác nhận đặt vé", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnHuy_Click(object? sender, EventArgs e)
        {
            txtTenKhach.Clear();
            cboPhim.SelectedIndex = -1;
            cboSuatChieu.SelectedIndex = -1;
            txtGheDaChon.Clear();
            txtTenKhach.Focus();
        }
    }
}
