using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStoreApp.Models
{
    public class GioHang
    {
        BookStoreDb db = new BookStoreDb();
        public int iMasach {  get; set; }
        public string sTensach { get; set; }
        public string sAnhbia { get; set; }
        public Double dDongia { get; set; }
        public int iSoluong { get; set; }
        public Double dThanhtoan
        {
            get { return iSoluong * dDongia; }
        }
        public GioHang(int Masach)
        {
            iMasach = Masach;
            SACH sach = db.SACHes.Single(n => n.Masach == iMasach);
            sTensach = sach.Tensach;
            sAnhbia = sach.Anhbia;
            dDongia = Double.Parse(sach.Giaban.ToString());
            iSoluong = 1;
        }
    }
}