using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLybanHang.Data
{
    public class Hangsanxuat
    {
        public int ID { get; set; }
        public string TenHangSanXuat { get; set; }
        public string Tenhangsanxuat { get; internal set; }
        public virtual ObservableCollectionListSource<sanpham> sanpham { get; } = new();
     
    }
}
