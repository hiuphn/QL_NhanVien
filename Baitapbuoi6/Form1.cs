using Baitapbuoi6.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Baitapbuoi6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void FillFalcultyCombobox(List<Phongban> listFaculties)
        {
            this.cbPB.DataSource = listFaculties;
            this.cbPB.DisplayMember = "TenPB";
            this.cbPB.ValueMember = "MaPB";
        }
        private void BindGrid(List<Nhanvien> listNhanvien)
        {
            dgvNhanvien.Rows.Clear();
            foreach(var items in listNhanvien)
            {
                int index = dgvNhanvien.Rows.Add();
                dgvNhanvien.Rows[index].Cells[0].Value = items.MaNV;
                dgvNhanvien.Rows[index].Cells[1].Value = items.TenNV;
                dgvNhanvien.Rows[index].Cells[2].Value = items.Ngaysinh.ToString("dd/MM/yyyy");
                dgvNhanvien.Rows[index].Cells[3].Value = items.Phongban.TenPB;            
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            NhanvienContextDB context = new NhanvienContextDB();
            List<Nhanvien> listNhanvien = context.Nhanvien.ToList();
            List<Phongban> listFaculties = context.Phongban.ToList();
            FillFalcultyCombobox(listFaculties);
            BindGrid(listNhanvien);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtMaNV.Text == "" || txtTenNV.Text == "" || cbPB.Text == "")
                {
                    throw new Exception("Hãy điền đủ thông tin !!!");
                }
                NhanvienContextDB context = new NhanvienContextDB();
                List<Nhanvien> listNhanvien = context.Nhanvien.ToList();
                Nhanvien checkID = context.Nhanvien.FirstOrDefault(nv => nv.MaNV == txtMaNV.Text);
                if (txtMaNV.Text.Length != 6)
                {
                    txtMaNV.Focus();
                    throw new Exception("Hãy nhập mã nhân viên có 6 kí tự");
                }
                if (checkID != null)
                {
                    throw new Exception("Mã số nhân viên đã tồn tại!!!");
                }
                
                string selectedPhongBan = cbPB.Text;
                Phongban selectedPhongBanObj = context.Phongban.FirstOrDefault(NV => NV.TenPB == selectedPhongBan);
                string maPB = selectedPhongBanObj.MaPB;
                
                Nhanvien NhanVien = new Nhanvien() { MaNV = txtMaNV.Text, TenNV = txtTenNV.Text, Ngaysinh = dtpNgaysinh.Value, MaPB = maPB };
                context.Nhanvien.Add(NhanVien);
                context.SaveChanges();
                
                List<Nhanvien> listNewNhanVien = context.Nhanvien.ToList();
                dgvNhanvien.DataSource = null;
                BindGrid(listNewNhanVien);
                txtMaNV.Text = "";
                txtTenNV.Text = "";
                dtpNgaysinh.Value = DateTime.Now;
                throw new Exception("Thêm nhân viên thành công !!!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult dl = MessageBox.Show("Bạn có muốn thoát chương trình", "Thông báo", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (dl == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                NhanvienContextDB context = new NhanvienContextDB();
                List<Nhanvien> listNhanvien = context.Nhanvien.ToList();

                string selectedPhongBan = cbPB.Text;
                Phongban selectedPhonBanObj = context.Phongban.FirstOrDefault(nv => nv.TenPB == selectedPhongBan);
                string maPB = selectedPhonBanObj.MaPB;

                Nhanvien dbUpdate = context.Nhanvien.FirstOrDefault(NV => NV.MaNV == txtMaNV.Text);
                if(dbUpdate != null)
                {
                    dbUpdate.TenNV = txtTenNV.Text;
                    dbUpdate.MaPB = maPB;
                    dbUpdate.Ngaysinh = dtpNgaysinh.Value;
                    context.SaveChanges();
                    List<Nhanvien> listNewNhanVien = context.Nhanvien.ToList();
                    dgvNhanvien.DataSource = null;
                    BindGrid(listNewNhanVien);
                    txtMaNV.Text = "";
                    txtTenNV.Text = "";
                    dtpNgaysinh.Value = DateTime.Now;
                    throw new Exception("Cập nhật thành công !!!");
                }
                else
                {
                    if(dbUpdate == null)
                    {
                        throw new Exception("Không thể thay đổi mã số NV!!!");
                    }
                    throw new Exception("Cập nhật không thành công");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thong bao", MessageBoxButtons.OK);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                NhanvienContextDB context = new NhanvienContextDB();
                List<Nhanvien> listNhanvien = context.Nhanvien.ToList();
                DialogResult dl = MessageBox.Show("Bạn có muốn xóa nhân viên","",MessageBoxButtons.YesNo);
                if(dl == DialogResult.Yes)
                {
                    Nhanvien dbXoa = context.Nhanvien.FirstOrDefault(nv => nv.MaNV == txtMaNV.Text);
                    if(dbXoa != null)
                    {
                        context.Nhanvien.Remove(dbXoa);
                        context.SaveChanges();

                        List<Nhanvien> listNewNhanvien = context.Nhanvien.ToList();
                        dgvNhanvien.DataSource = null;
                        BindGrid(listNewNhanvien);
                        txtMaNV.Text = "";
                        txtTenNV.Text = "";
                        throw new Exception("Xóa nhân viên thành công");
                    }
                    else
                    {
                        throw new Exception("Vui lòng chọn nhân viên cần xóa");
                    }
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvNhanvien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            try
            {
                if(dgvNhanvien.SelectedRows.Count > 0 || dgvNhanvien.SelectedRows.Count < dgvNhanvien.RowCount)
                {
                    txtMaNV.Text = dgvNhanvien.Rows[index].Cells[0].Value.ToString();
                    txtTenNV.Text = dgvNhanvien.Rows[index].Cells[1].Value.ToString();

                    string StringNgaysinh = dgvNhanvien.Rows[index].Cells[2].Value.ToString();
                    DateTime ngaySinh = DateTime.ParseExact(StringNgaysinh, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    dtpNgaysinh.Value = ngaySinh;
                    cbPB.Text = dgvNhanvien.Rows[index].Cells[3].Value.ToString();

                }
                else
                {
                    throw new Exception("Vui lòng chọn lại hàng");
                }
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
