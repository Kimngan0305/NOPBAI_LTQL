using QuanLybanHang.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLybanHang.Forms
{
   
    public partial class frmKhachHang : Form
    {
        

        public frmKhachHang()
        {
            InitializeComponent();
        }
        QLBHDbContext context = new QLBHDbContext(); // Khởi tạo biến ngữ cảnh CSDL
        bool xuLyThem = false; // Kiểm tra có nhấn vào nút Thêm hay không?
        int id;
        private void BatTatChucNang(bool giaTri)
        {
            btnluu.Enabled = giaTri;
            btnhuybo.Enabled = giaTri;
            txthoten.Enabled = giaTri;
            txtdienthoai.Enabled = giaTri;
            txtdiachi.Enabled = giaTri;
            btnthem.Enabled = !giaTri;
            btnsua.Enabled = !giaTri;
            btnxoa.Enabled = !giaTri;
            btntimkiem.Enabled = !giaTri;
            btnnhap.Enabled = !giaTri;
            btnxuat.Enabled = !giaTri;
        }

        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            BatTatChucNang(false);
            List<Khachhang> kh = new List<Khachhang>();
            kh = context.KhachHang.ToList();
            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = kh;
            txthoten.DataBindings.Clear();
            txthoten.DataBindings.Add("Text", bindingSource, "HoVaTen", false, DataSourceUpdateMode.Never);
            // Tương tự cho txtDienThoai và txtDiaChi

            txtdienthoai.DataBindings.Clear();
            txtdienthoai.DataBindings.Add("Text", bindingSource, "DienThoai", false, DataSourceUpdateMode.Never);

            txtdiachi.DataBindings.Clear();
            txtdiachi.DataBindings.Add("Text", bindingSource, "DiaChi", false, DataSourceUpdateMode.Never);
           
            dvgdanhsachkhachhang.DataSource = bindingSource;
        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            xuLyThem = true;
            BatTatChucNang(true);
            txthoten.Clear();
            txtdienthoai.Clear();
            txtdiachi.Clear();
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            xuLyThem = false;
            BatTatChucNang(true);
            id = Convert.ToInt32(dvgdanhsachkhachhang.CurrentRow.Cells["ID"].Value.ToString());
        }

        private void btnluu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txthoten.Text))
                MessageBox.Show("Vui lòng nhập họ và tên khách hàng?", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                if (xuLyThem)
                {
                    Khachhang kh = new Khachhang();
                    kh.HoVaTen = txthoten.Text;
                    kh.DienThoai = txtdienthoai.Text;
                    kh.DiaChi = txtdiachi.Text;
                    context.KhachHang.Add(kh);
                    context.SaveChanges();
                }
                else
                {
                    Khachhang kh = context.KhachHang.Find(ID);
                    if (kh != null)
                    {
                        kh.HoVaTen = txthoten.Text;
                        kh.DienThoai = txtdienthoai.Text;
                        kh.DiaChi = txtdiachi.Text;
                        context.KhachHang.Update(kh);
                        context.SaveChanges();
                    }
                }
                frmKhachHang_Load(sender, e);
            }
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Xác nhận xóa khách hàng " + txthoten.Text + "?", "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                id = Convert.ToInt32( dvgdanhsachkhachhang.CurrentRow.Cells["ID"].Value.ToString());
                Khachhang kh = context.KhachHang.Find(ID);
                if (kh != null)
                {
                    context.KhachHang.Remove(kh);
                }
                context.SaveChanges();
                frmKhachHang_Load(sender, e);
            }
        }

        private void btnhuybo_Click(object sender, EventArgs e)
        {
            frmKhachHang_Load(sender, e);
        }
    }
}
