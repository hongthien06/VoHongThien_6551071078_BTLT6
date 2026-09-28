using System;
using System.Windows.Forms;

namespace Cau6
{
    public class FormChinh : Form
    {
        private readonly MenuStrip menuStrip1 = new MenuStrip();
        private readonly StatusStrip statusStrip1 = new StatusStrip();
        private readonly ToolStripStatusLabel lblSoGhiChu = new ToolStripStatusLabel();

        public FormChinh()
        {
            Text = "Note Manager - Quản lý ghi chú công việc";
            IsMdiContainer = true;
            WindowState = FormWindowState.Maximized;

            var mnuTep = new ToolStripMenuItem("Tệp");
            var mnuMoGhiChu = new ToolStripMenuItem("Mở ghi chú mới", null, MnuMoGhiChu_Click, Keys.Control | Keys.N);
            var mnuSapXep = new ToolStripMenuItem("Sắp xếp cửa sổ", null, (s, e) => LayoutMdi(MdiLayout.Cascade));
            var mnuThoat = new ToolStripMenuItem("Thoát", null, (s, e) => Close());
            mnuTep.DropDownItems.Add(mnuMoGhiChu);
            mnuTep.DropDownItems.Add(mnuSapXep);
            mnuTep.DropDownItems.Add(new ToolStripSeparator());
            mnuTep.DropDownItems.Add(mnuThoat);

            var mnuCuaSo = new ToolStripMenuItem("Cửa sổ");
            var mnuXepTang = new ToolStripMenuItem("Xếp tầng", null, (s, e) => LayoutMdi(MdiLayout.Cascade));
            var mnuXepNgang = new ToolStripMenuItem("Xếp ngang", null, (s, e) => LayoutMdi(MdiLayout.TileHorizontal));
            var mnuXepDoc = new ToolStripMenuItem("Xếp dọc", null, (s, e) => LayoutMdi(MdiLayout.TileVertical));
            mnuCuaSo.DropDownItems.AddRange(new ToolStripItem[] { mnuXepTang, mnuXepNgang, mnuXepDoc });

            menuStrip1.Items.Add(mnuTep);
            menuStrip1.Items.Add(mnuCuaSo);
            MainMenuStrip = menuStrip1;

            statusStrip1.Items.Add(lblSoGhiChu);

            Controls.Add(menuStrip1);
            Controls.Add(statusStrip1);

            CapNhatStatusStrip();
        }

        private void MnuMoGhiChu_Click(object? sender, EventArgs e)
        {
            var frm = new FormGhiChu();
            frm.MdiParent = this;
            frm.FormClosed += (s, ev) => BeginInvoke(new Action(CapNhatStatusStrip));
            frm.Show();
            CapNhatStatusStrip();
        }

        private void CapNhatStatusStrip()
        {
            lblSoGhiChu.Text = $"Số ghi chú đang mở: {MdiChildren.Length}";
        }
    }
}
