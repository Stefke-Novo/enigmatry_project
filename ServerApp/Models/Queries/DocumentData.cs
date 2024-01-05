using System.Collections.ObjectModel;

namespace ServerApp.Models.Queries
{
    public class DocumentData
    {
        public int account_number { get; set; } = 0;
        public int balance { get; set; } = 0;
        public string currency { get; set; } = "";
        public ICollection<Transaction> transactions = new Collection<Transaction>();
    }
}
