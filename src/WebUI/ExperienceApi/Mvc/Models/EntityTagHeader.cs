using Doctrina.ExperienceApi.Client.Http;

namespace Doctrina.WebUI.ExperienceApi.Mvc.Models
{
    public class EntityTagHeader
    {
        public string Tag { get; set; }
        public ETagMatch Match { get; set; }
    }
}
