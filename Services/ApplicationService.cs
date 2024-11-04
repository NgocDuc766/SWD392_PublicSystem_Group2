using SWD392_PublicService.Models;
using System;

namespace SWD392_PublicService.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly Swd392PublicSystemContext _context;

        public string Message { get; private set; } = null;

        public ApplicationService(Swd392PublicSystemContext context)
        {
            _context = context;
        }

        public bool SaveApplication(string coQuanThucHien, string tenDon, string ghiChu, decimal giaTien)
        {
            try
            {
                var agency = _context.ProcessingAgencies.FirstOrDefault(a => a.Name == coQuanThucHien);
                if (agency == null)
                {
                    Message = "Cơ quan thực hiện không hợp lệ.";
                    return false;
                }

                var application = new Application
                {
                    UserId = 1,
                    ServiceId = 1,
                    AgencyId = agency.AgencyId,
                    Name = tenDon,
                    Type = 0,
                    PaymentAmount = giaTien,
                    Note = ghiChu,
                    SubmissionDate = DateTime.Now,
                    Status = 0
                };

                _context.Applications.Add(application);
                _context.SaveChanges();
                Message = "Đơn từ đã được nộp thành công.";
                return true;
            }
            catch (Exception ex)
            {
                Message = $"Có lỗi xảy ra khi nộp đơn từ: {ex.Message}";
                return false;
            }
        }
    }
}
