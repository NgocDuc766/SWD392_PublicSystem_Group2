namespace SWD392_PublicService.Services
{
    public interface IApplicationService
    {
        bool SaveApplication(string coQuanThucHien, string tenDon, string ghiChu, decimal giaTien);
        string Message { get; }
    }
}
