using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace OneGate.Shared.Models.Account
{
    public class OAuthDto
    {
        [Required]
        [MaxLength(30)]
        [FromForm(Name = "username")]
        public string Username { get; set; }
        
        [Required]
        [MaxLength(30)]
        [FromForm(Name = "password")]
        public string Password { get; set; }
    }
}