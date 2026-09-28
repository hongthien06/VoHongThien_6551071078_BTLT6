using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Cau2
{
    public class FormDatPhong : Form
    {
        private readonly TextBox txtHoTen = new TextBox();
        private readonly TextBox txtCCCD = new TextBox();
        private readonly TextBox txtNgayNhan = new TextBox();
        private readonly TextBox txtNgayTra = new TextBox();
        private readonly TextBox txtSoNguoiLon = new TextBox();
        private readonly TextBox txtSoTreEm = new TextBox();
        private readonly Button btnDatPhong = new Button();
        private readonly ErrorProvider errorProvider1 = new ErrorProvider();

        public FormDatPhong()
        {
            Text = "Đặt phòng khách sạn";
            ClientSize = new Size(430, 320);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;

            errorProvider1.ContainerControl = this;

            string[] nhan =
            {
                "Họ tên khách:", "Số CCCD:", "Ngày nhận (dd/MM/yyyy):",
                "Ngày trả (dd/MM/yyyy):", "Số người lớn (1-4):", "Số trẻ em (0-3):"
            };
            TextBox[] oNhap = { txtHoTen, txtCCCD, txtNgayNhan, txtNgayTra, txtSoNguoiLon, txtSoTreEm };

            for (int i = 0; i < oNhap.Length; i++)
            {
                var lbl = new Label
                {
                    Text = nhan[i],
                    Location = new Point(20, 22 + i * 38),
                    AutoSize = true
                };
                oNhap[i].Location = new Point(185, 18 + i * 38);
                oNhap[i].Width = 200;
                Controls.Add(lbl);
                Controls.Add(oNhap[i]);
            }

            txtHoTen.Validating += txtHoTen_Validating;
            txtCCCD.Validating += txtCCCD_Validating;
            txtNgayNhan.Validating += txtNgayNhan_Validating;
            txtNgayTra.Validating += txtNgayTra_Validating;
            txtSoNguoiLon.Validating += txtSoNguoiLon_Validating;
            txtSoTreEm.Validating += txtSoTreEm_Validating;

            foreach (var txt in oNhap)
            {
                txt.Validated += TextBox_Validated;
            }

            btnDatPhong.Text = "Đặt phòng";
            btnDatPhong.Location = new Point(185, 260);
            btnDatPhong.Size = new Size(120, 34);
            btnDatPhong.Click += btnDatPhong_Click;
            Controls.Add(btnDatPhong);

            FormClosing += (s, e) => e.Cancel = false;
        }

        private void BaoLoi(TextBox txt, string thongBao, CancelEventArgs e)
        {
            e.Cancel = true;
            errorProvider1.SetError(txt, thongBao);
            txt.BackColor = Color.MistyRose;
        }

        private void XoaLoi(TextBox txt)
        {
            errorProvider1.SetError(txt, "");
            txt.BackColor = Color.Honeydew;
        }

        private void txtHoTen_Validating(object? sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
                BaoLoi(txtHoTen, "Họ tên khách không được để trống!", e);
            else
                XoaLoi(txtHoTen);
        }

        private void txtCCCD_Validating(object? sender, CancelEventArgs e)
        {
            string cccd = txtCCCD.Text.Trim();
            if (cccd.Length != 12 || !cccd.All(char.IsDigit))
                BaoLoi(txtCCCD, "Số CCCD phải gồm đúng 12 chữ số!", e);
            else
                XoaLoi(txtCCCD);
        }

        private bool ParseNgay(string chuoi, out DateTime ngay)
        {
            return DateTime.TryParseExact(chuoi.Trim(), "dd/MM/yyyy",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out ngay);
        }

        private void txtNgayNhan_Validating(object? sender, CancelEventArgs e)
        {
            if (!ParseNgay(txtNgayNhan.Text, out DateTime ngayNhan))
                BaoLoi(txtNgayNhan, "Ngày nhận phòng phải đúng định dạng dd/MM/yyyy!", e);
            else if (ngayNhan.Date < DateTime.Today)
                BaoLoi(txtNgayNhan, "Ngày nhận phòng phải từ hôm nay trở đi!", e);
            else
                XoaLoi(txtNgayNhan);
        }

        private void txtNgayTra_Validating(object? sender, CancelEventArgs e)
        {
            if (!ParseNgay(txtNgayTra.Text, out DateTime ngayTra))
                BaoLoi(txtNgayTra, "Ngày trả phòng phải đúng định dạng dd/MM/yyyy!", e);
            else if (ParseNgay(txtNgayNhan.Text, out DateTime ngayNhan) && ngayTra <= ngayNhan)
                BaoLoi(txtNgayTra, "Ngày trả phòng phải lớn hơn ngày nhận phòng!", e);
            else
                XoaLoi(txtNgayTra);
        }

        private void txtSoNguoiLon_Validating(object? sender, CancelEventArgs e)
        {
            if (!int.TryParse(txtSoNguoiLon.Text.Trim(), out int soNguoi) || soNguoi < 1 || soNguoi > 4)
                BaoLoi(txtSoNguoiLon, "Số người lớn phải là số nguyên từ 1 đến 4!", e);
            else
                XoaLoi(txtSoNguoiLon);
        }

        private void txtSoTreEm_Validating(object? sender, CancelEventArgs e)
        {
            if (!int.TryParse(txtSoTreEm.Text.Trim(), out int soTreEm) || soTreEm < 0 || soTreEm > 3)
                BaoLoi(txtSoTreEm, "Số trẻ em phải là số nguyên từ 0 đến 3!", e);
            else
                XoaLoi(txtSoTreEm);
        }

        private void TextBox_Validated(object? sender, EventArgs e)
        {
            if (sender is TextBox txt)
            {
                txt.BackColor = Color.Honeydew;
            }
        }

        private void btnDatPhong_Click(object? sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                return;
            }

            ParseNgay(txtNgayNhan.Text, out DateTime ngayNhan);
            ParseNgay(txtNgayTra.Text, out DateTime ngayTra);
            int soDem = (ngayTra - ngayNhan).Days;

            MessageBox.Show(
                "Đặt phòng thành công!\n" +
                $"Khách: {txtHoTen.Text}\n" +
                $"Số đêm: {soDem}\n" +
                $"Người lớn: {txtSoNguoiLon.Text} - Trẻ em: {txtSoTreEm.Text}",
                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
