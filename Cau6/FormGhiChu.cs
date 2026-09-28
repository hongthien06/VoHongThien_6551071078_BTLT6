using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Cau6
{
    public class FormGhiChu : Form
    {
        private readonly Label lblTieuDeForm = new Label();
        private readonly TextBox txtTieuDe = new TextBox();
        private readonly TextBox txtNoiDung = new TextBox();
        private readonly ComboBox cboMucDoUuTien = new ComboBox();
        private readonly Button btnLuuGhiChu = new Button();
        private readonly Label lblDemKyTu = new Label();
        private readonly ErrorProvider errorProvider1 = new ErrorProvider();

        private const int GioiHanKyTu = 500;
        private bool _daThayDoi = false;
        private readonly Color _mauNutGoc;

        public FormGhiChu()
        {
            Text = "Ghi chú mới";
            ClientSize = new Size(400, 380);
            KeyPreview = true;

            AutoValidate = AutoValidate.EnableAllowFocusChange;

            errorProvider1.ContainerControl = this;

            lblTieuDeForm.Text = "GHI CHÚ CÔNG VIỆC (double-click để phóng to/thu nhỏ)";
            lblTieuDeForm.Dock = DockStyle.Top;
            lblTieuDeForm.Height = 30;
            lblTieuDeForm.TextAlign = ContentAlignment.MiddleCenter;
            lblTieuDeForm.BackColor = Color.LightSteelBlue;
            lblTieuDeForm.MouseDoubleClick += lblTieuDeForm_MouseDoubleClick;

            var lblTieuDe = new Label { Text = "Tiêu đề:", Location = new Point(15, 45), AutoSize = true };
            txtTieuDe.Location = new Point(110, 41);
            txtTieuDe.Width = 240;
            txtTieuDe.Validating += txtTieuDe_Validating;
            txtTieuDe.Validated += txtTieuDe_Validated;
            txtTieuDe.TextChanged += NoiDung_TextChanged;

            var lblNoiDung = new Label { Text = "Nội dung:", Location = new Point(15, 83), AutoSize = true };
            txtNoiDung.Multiline = true;
            txtNoiDung.ScrollBars = ScrollBars.Vertical;
            txtNoiDung.Location = new Point(110, 79);
            txtNoiDung.Size = new Size(240, 150);
            txtNoiDung.KeyPress += txtNoiDung_KeyPress;
            txtNoiDung.TextChanged += NoiDung_TextChanged;

            lblDemKyTu.Text = $"0/{GioiHanKyTu} ký tự";
            lblDemKyTu.Location = new Point(110, 235);
            lblDemKyTu.AutoSize = true;

            var lblMucDo = new Label { Text = "Mức độ ưu tiên:", Location = new Point(15, 265), AutoSize = true };
            cboMucDoUuTien.Location = new Point(110, 261);
            cboMucDoUuTien.Width = 240;
            cboMucDoUuTien.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMucDoUuTien.Items.AddRange(new object[] { "Thấp", "Trung bình", "Cao" });
            cboMucDoUuTien.SelectedIndex = 0;
            cboMucDoUuTien.SelectedIndexChanged += NoiDung_TextChanged;

            btnLuuGhiChu.Text = "Lưu ghi chú (Ctrl+S)";
            btnLuuGhiChu.Location = new Point(110, 305);
            btnLuuGhiChu.Size = new Size(160, 36);
            _mauNutGoc = btnLuuGhiChu.BackColor;
            btnLuuGhiChu.Click += btnLuuGhiChu_Click;
            btnLuuGhiChu.MouseEnter += (s, e) => btnLuuGhiChu.BackColor = Color.LightGreen;
            btnLuuGhiChu.MouseLeave += (s, e) => btnLuuGhiChu.BackColor = _mauNutGoc;

            Controls.AddRange(new Control[]
            {
                lblTieuDeForm, lblTieuDe, txtTieuDe, lblNoiDung, txtNoiDung,
                lblDemKyTu, lblMucDo, cboMucDoUuTien, btnLuuGhiChu
            });

            KeyDown += FormGhiChu_KeyDown;
        }


        private void FormGhiChu_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                e.SuppressKeyPress = true;
                btnLuuGhiChu.PerformClick();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                if (_daThayDoi)
                {
                    DialogResult ketQua = MessageBox.Show(
                        "Nội dung ghi chú đã thay đổi. Bạn có chắc muốn đóng không?",
                        "Xác nhận đóng", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (ketQua != DialogResult.Yes)
                    {
                        return;
                    }
                }
                _daThayDoi = false;
                Close();
            }
        }

        private void txtNoiDung_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
                && txtNoiDung.TextLength - txtNoiDung.SelectionLength >= GioiHanKyTu)
            {
                e.Handled = true;
            }
        }

        private void NoiDung_TextChanged(object? sender, EventArgs e)
        {
            _daThayDoi = true;
            lblDemKyTu.Text = $"{txtNoiDung.TextLength}/{GioiHanKyTu} ký tự";
        }


        private void lblTieuDeForm_MouseDoubleClick(object? sender, MouseEventArgs e)
        {
            WindowState = WindowState == FormWindowState.Maximized
                ? FormWindowState.Normal
                : FormWindowState.Maximized;
        }


        private void txtTieuDe_Validating(object? sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTieuDe.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTieuDe, "Tiêu đề không được để trống!");
                txtTieuDe.BackColor = Color.MistyRose;
            }
            else if (txtTieuDe.Text.Length > 50)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTieuDe, "Tiêu đề tối đa 50 ký tự!");
                txtTieuDe.BackColor = Color.MistyRose;
            }
        }

        private void txtTieuDe_Validated(object? sender, EventArgs e)
        {
            txtTieuDe.BackColor = Color.White;
            errorProvider1.SetError(txtTieuDe, "");
        }

        private void btnLuuGhiChu_Click(object? sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                return;
            }

            Text = txtTieuDe.Text;
            _daThayDoi = false;
            MessageBox.Show("Đã lưu ghi chú", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
