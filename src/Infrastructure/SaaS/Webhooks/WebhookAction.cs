using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctrina.Infrastructure.SaaS.Webhooks
{
    public enum WebhookAction
    {
        [Description("when the resource has been deleted")]
        Unsubscribe,

        [Description("when the change plan operation has completed")]
        ChangePlan,

        [Description("when the change quantity operation has completed")]
        ChangeQuantity,

        [Description("when resource has been suspended")]
        Suspend,

        [Description("when resource has been reinstated after suspension")]
        Reinstate
    }
}
