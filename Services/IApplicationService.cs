namespace SWD392_PublicService.Services
{
    public interface IApplicationService
    {
        void SaveApplication(string applicationName, int applicationType, string note, decimal paymentAmount);
    }
}
