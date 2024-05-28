using BookStoreApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStoreApp.Controllers
{
    public class GiohangController : Controller
    {
        // GET: GioHang
        BookStoreDb db = new BookStoreDb();

        //Lấy giỏ hàng
        public List<GioHang> Laygiohang()
        {
            List<GioHang> lstGiohang = Session["Giohang"] as List<GioHang>;
            if(lstGiohang == null)
            {
                //Nếu giỏ hàng chưa tồn tại thì khởi tạo listGiohang
                lstGiohang  = new List<GioHang>();
                Session["Giohang"] = lstGiohang;
            }
            return lstGiohang;
        }

        //Thêm hàng vào giỏ
        public ActionResult ThemGiohang(int iMasach, string strURL)
        {
            // Lấy ra Session giỏ hàng
            List<GioHang> lstGiohang = Laygiohang();

            GioHang sanpham = lstGiohang.Find(n => n.iMasach == iMasach);
            if(sanpham == null)
            {
                sanpham = new GioHang(iMasach);
                lstGiohang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSoluong++;
                return Redirect(strURL);
            }
        }

        //Tính tổng số lượng
        private double Tongsoluong()
        {
            double iTongsoluong = 0;
            List<GioHang> lstGiohang = Session["Giohang"] as List<GioHang>;
            if (lstGiohang != null)
            {
                iTongsoluong = lstGiohang.Sum(n => n.iSoluong);
            }
            return iTongsoluong;
        }
        //Tính tổng tiền
        private double Tongtien()
        {
            double iTongtien = 0;
            List<GioHang> lstGiohang = Session["Giohang"] as List<GioHang>;
            if(lstGiohang != null)
            {
                iTongtien = lstGiohang.Sum(n => n.dThanhtoan);
            }
            return iTongtien;
        }
        public ActionResult Giohang()
        {
            List<GioHang> lstGiohang = Laygiohang();
            if(lstGiohang.Count == 0)
            {
                return RedirectToAction("Index", "BookStore");
            }
            ViewBag.Tongsoluong = Tongsoluong();
            ViewBag.Tongtien = Tongtien();
            return View(lstGiohang);
        }

        //Xóa giỏ hàng
        public ActionResult XoaGiohang(int iMaSP)
        {
            List<GioHang> lstGiohang = Laygiohang();
            GioHang sanpham = lstGiohang.SingleOrDefault(n => n.iMasach == iMaSP);
            if(sanpham != null)
            {
                lstGiohang.RemoveAll(n => n.iMasach == iMaSP);
                return RedirectToAction("GioHang");
            }
            if(lstGiohang.Count == 0)
            {
                return RedirectToAction("Index", "BookStore");
            }
            return RedirectToAction("GioHang");
        }
        
        //Cập nhật giỏ hàng
        public ActionResult CapnhatGiohang(int iMaSP, FormCollection f)
        {
            List<GioHang> lstGiohang = Laygiohang();
            GioHang sanpham = lstGiohang.SingleOrDefault(n => n.iMasach == iMaSP);
            if(sanpham != null)
            {
                sanpham.iSoluong = int.Parse(f["txtSoluong"].ToString());
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult XoaTatcaGiohang()
        {
            List<GioHang> lstGiohang = Laygiohang();
            lstGiohang.Clear();
            return RedirectToAction("Index", "BookStore");
        }
    }
}