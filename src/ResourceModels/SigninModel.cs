using System.ComponentModel.DataAnnotations;

namespace Plex.Web.Api.ResourceModels
{
    public class SigninModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}