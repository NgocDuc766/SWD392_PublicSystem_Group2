namespace SWD392_PublicService.Services
{
    public interface IApplicationService
    {
        bool ValidateApplication(string applicationName);
        void SaveApplication(string applicationName, int applicationType, string note, decimal paymentAmount);
    }
}
