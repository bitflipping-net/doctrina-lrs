using Doctrina.ExperienceApi.Data;
using Doctrina.WebUI.ExperienceApi.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Doctrina.WebUI.ExperienceApi.Models
{
    [ModelBinder(BinderType = typeof(PostStatementsModelBinder))]
    public class PostStatementContent
    {
        [Required]
        public StatementCollection Statements { get; set; }
    }
}
