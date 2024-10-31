using SWD392_PublicService.Models;

namespace SWD392_PublicService.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly Swd392PublicSystemContext _context;
        // message variable for return
        public string message = null;

        public ApplicationService(Swd392PublicSystemContext context)
        {
            _context = context;
        }

        public void SaveApplication(string applicationName, int applicationType, string note, decimal paymentAmount)
        {
            if (ValidateApplication(applicationName))
            {
                try
                {
                    // Tạo đối tượng Application để lưu vào database
                    var application = new Application
                    {
                        Name = applicationName,
                        Type = applicationType,
                        Status = 0,
                        Note = note,
                        PaymentAmount = paymentAmount,
                        SubmissionDate = DateTime.Now
                    };

                    // Thêm vào context và lưu thay đổi
                    _context.Applications.Add(application);
                    _context.SaveChanges();

                    // Cập nhật thông báo thành công
                    message = "Đơn từ đã được lưu thành công.";
                }
                catch (Exception ex)
                {
                    // Cập nhật thông báo lỗi nếu xảy ra lỗi khi lưu
                    message = $"Có lỗi xảy ra khi lưu đơn từ: {ex.Message}";
                }
            }
            else
            {
                // Nếu ValidateApplication trả về false, message đã được cập nhật trong hàm đó
                message = "Tên đơn từ không hợp lệ.";
            }
        }

        public bool ValidateApplication(string applicationName)
        {
            
            if(string.IsNullOrEmpty(applicationName))
            {
                return false;
            }
            return true;
        }
    }
}
