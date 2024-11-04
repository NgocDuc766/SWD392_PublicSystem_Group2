using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SWD392_PublicService.Models;

namespace SWD392_PublicService.Controllers
{
    public class ResidentController : Controller
    {
        private readonly Swd392PublicSystemContext _context;

        public ResidentController(Swd392PublicSystemContext context)
        {
            _context = context;
        }

        
        public IActionResult ListApplication()
        {
            return View();
        }

        public IActionResult SubmitApplication()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SubmitApplication(string quanHuyen, string coQuanThucHien, string hoVaTen, string gioiTinh, string soGiayTo, string tenDon, string ghiChu, string giaTien)
        {
            // Fix cứng UserId và ServiceId
            int userId = 1;
            int serviceId = 1;

            // Tìm agencyId dựa trên tên cơ quan thực hiện
            var agency = _context.ProcessingAgencies.FirstOrDefault(a => a.Name == coQuanThucHien);
            if (agency == null)
            {
                ModelState.AddModelError("coQuanThucHien", "Cơ quan thực hiện không hợp lệ.");
                return View();
            }

            // Tạo mới Application
            var application = new Application
            {
                UserId = userId,
                ServiceId = serviceId,
                AgencyId = agency.AgencyId,
                Name = tenDon,
                Type = 0,
                PaymentAmount = decimal.Parse(giaTien),
                Note = ghiChu,
                SubmissionDate = DateTime.Now,
                Status = 0
            };

            // Lưu vào database
            _context.Applications.Add(application);
            _context.SaveChanges();
            // Điều hướng tới trang danh sách hoặc trang xác nhận
            ViewBag.Success = "Successful!";
            return View();
        }

    }
}
