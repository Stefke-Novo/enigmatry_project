namespace ServerApp.Models.Request
{
    public class DocumentRequest
    {
        public string ProductCode { get; set; } = string.Empty;
        public string TenantId { get; set; } = string.Empty;
        public string DocumentId { get; set; } = string.Empty;
    }
}
