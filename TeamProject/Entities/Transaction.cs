using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject.Entities
{

    public class Transaction
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public decimal UserId { get; set; } // додав привязку до акаунта котрий додає транзакції
        public string Note { get; set; }

        public string type { get; set; }
        public string Category { get; set; }
    }
}
