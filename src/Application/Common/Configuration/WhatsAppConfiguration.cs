namespace Application.Common.Configuration;

public class WhatsAppConfiguration
{
    public string BaseUrl { get; set; } = null!;

    public string CloudApiversion { get; set; } = null!;

    public string LeaveAppliedTemplate { get; set; } = null!;

    public string LeaveApprovedTemplate { get; set; } = null!;

    public string LeaveRejectedTemplate { get; set; } = null!;

    public string AccessToken { get; set; } = null!;

    public string WhatsAppPhoneNumberId { get; set; } = null!;

    public string TargetPhoneNumber { get; set; } = null!;
}
