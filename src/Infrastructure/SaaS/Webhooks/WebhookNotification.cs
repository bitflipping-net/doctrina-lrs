using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctrina.Infrastructure.SaaS.Webhooks
{
    public class WebhookNotification
    {
        public string Id { get; set; }
        public string ActivityId { get; set; }
        public string SubscriptionId { get; set; }
        public string PublisherId { get; set; }
        public string OfferId { get; set; }
        public string PlanId { get; set; }
        public string Quantity { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
        public string Action { get; set; }
        public string Status { get; set; }
    }
}
