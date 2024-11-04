using Microsoft.AspNetCore.Mvc;
using SWD392_PublicService.Services;
using SWD392_PublicService.Models;

namespace SWD392_PublicService.Controllers
{
    public class ResidentController : Controller
    {
        private readonly ICaptchaService _captchaService;
        private readonly IApplicationService _applicationService;

        public ResidentController(ICaptchaService captchaService, IApplicationService applicationService)
        {
            _captchaService = captchaService;
            _applicationService = applicationService;
        }

        public IActionResult SubmitApplication()
        {
            ViewBag.CaptchaCode = _captchaService.GenerateCaptchaCode();
            TempData["CaptchaCode"] = ViewBag.CaptchaCode;
            return View();
        }

        [HttpPost]
        public IActionResult SubmitApplication(string quanHuyen, string coQuanThucHien, string hoVaTen, string gioiTinh, string soGiayTo, string tenDon, string ghiChu, string giaTien, string captchaCode)
        {
            // CAPTCHA verification
            var storedCaptcha = TempData["CaptchaCode"] as string;
            if (captchaCode != storedCaptcha)
            {
                ViewBag.Error = "Mã CAPTCHA không hợp lệ.";
                ViewBag.CaptchaCode = _captchaService.GenerateCaptchaCode();
                TempData["CaptchaCode"] = ViewBag.CaptchaCode;
                return View();
            }

            if (_applicationService.SaveApplication(coQuanThucHien, tenDon, ghiChu, decimal.Parse(giaTien)))
            {
                ViewBag.Success = _applicationService.Message;
            }
            else
            {
                ModelState.AddModelError("coQuanThucHien", _applicationService.Message);
                ViewBag.CaptchaCode = _captchaService.GenerateCaptchaCode();
                TempData["CaptchaCode"] = ViewBag.CaptchaCode;
            }

            return View();
        }
    }
}
