using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Cau1
{
    public class FormDangKy : Form
    {
        private readonly TextBox txtHoTen = new TextBox();
        private readonly TextBox txtSDT = new TextBox();
        private readonly TextBox txtEmail = new TextBox();
        private readonly TextBox txtMatKhau = new TextBox();
        private readonly TextBox txtXacNhanMK = new TextBox();
        private readonly Button btnDangKy = new Button();
        private readonly Button btnHuy = new Button();
        private readonly ErrorProvider errorProvider1 = new ErrorProvider();

        public FormDangKy()
        {
            Text = "Đăng ký tài khoản - Giao đồ ăn";
            ClientSize = new Size(420, 280);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;

            errorProvider1.ContainerControl = this;
            errorProvider1.BlinkStyle = ErrorBlinkStyle.BlinkIfDifferentError;

            string[] nhan = { "Họ tên:", "Số điện thoại:", "Email:", "Mật khẩu:", "Xác nhận MK:" };
            TextBox[] oNhap = { txtHoTen, txtSDT, txtEmail, txtMatKhau, txtXacNhanMK };

            for (int i = 0; i < oNhap.Length; i++)
            {
                var lbl = new Label
                {
                    Text = nhan[i],
                    Location = new Point(20, 22 + i * 38),
                    AutoSize = true
                };
                oNhap[i].Location = new Point(130, 18 + i * 38);
                oNhap[i].Width = 230;
                Controls.Add(lbl);
                Controls.Add(oNhap[i]);
            }

            txtMatKhau.PasswordChar = '*';
            txtXacNhanMK.PasswordChar = '*';

            btnDangKy.Text = "Đăng ký";
            btnDangKy.Location = new Point(130, 220);
            btnDangKy.Size = new Size(100, 32);
            btnDangKy.Click += btnDangKy_Click;

            btnHuy.Text = "Hủy";
            btnHuy.Location = new Point(250, 220);
            btnHuy.Size = new Size(100, 32);
            btnHuy.CausesValidation = false;
            btnHuy.Click += btnHuy_Click;

            Controls.Add(btnDangKy);
            Controls.Add(btnHuy);
        }

        private bool KiemTraHopLe()
        {
            bool hopLe = true;

            if (string.IsNullOrWhiteSpace(txtHoTen.Text) || txtHoTen.Text.Trim().Length < 3)
            {
                errorProvider1.SetError(txtHoTen, "Họ tên không được để trống và phải có tối thiểu 3 ký tự!");
                hopLe = false;
            }
            else
            {
                errorProvider1.SetError(txtHoTen, "");
            }

            string sdt = txtSDT.Text.Trim();
            if (sdt.Length != 10 || !sdt.All(char.IsDigit) || !sdt.StartsWith("0"))
            {
                errorProvider1.SetError(txtSDT, "Số điện thoại phải đúng 10 chữ số và bắt đầu bằng số 0!");
                hopLe = false;
            }
            else
            {
                errorProvider1.SetError(txtSDT, "");
            }

            string email = txtEmail.Text.Trim();
            int viTriA = email.IndexOf('@');
            if (viTriA <= 0 || email.IndexOf('.', viTriA) <= viTriA + 1)
            {
                errorProvider1.SetError(txtEmail, "Email phải chứa \"@\" và dấu \".\" phía sau \"@\"!");
                hopLe = false;
            }
            else
            {
                errorProvider1.SetError(txtEmail, "");
            }

            if (txtMatKhau.Text.Length < 6)
            {
                errorProvider1.SetError(txtMatKhau, "Mật khẩu phải có tối thiểu 6 ký tự!");
                hopLe = false;
            }
            else
            {
                errorProvider1.SetError(txtMatKhau, "");
            }

            if (txtXacNhanMK.Text != txtMatKhau.Text)
            {
                errorProvider1.SetError(txtXacNhanMK, "Xác nhận mật khẩu không khớp với mật khẩu!");
                hopLe = false;
            }
            else
            {
                errorProvider1.SetError(txtXacNhanMK, "");
            }

            return hopLe;
        }

        private void btnDangKy_Click(object? sender, EventArgs e)
        {
            if (!KiemTraHopLe())
            {
                return;
            }

            MessageBox.Show("Đăng ký thành công! Chào mừng " + txtHoTen.Text,
                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnHuy_Click(object? sender, EventArgs e)
        {
            errorProvider1.Clear();
            Close();
        }
    }
}
