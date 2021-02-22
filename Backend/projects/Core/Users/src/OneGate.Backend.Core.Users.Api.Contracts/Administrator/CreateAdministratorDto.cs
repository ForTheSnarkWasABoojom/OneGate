using Newtonsoft.Json;

namespace OneGate.Backend.Core.Users.Api.Contracts.Administrator
{
    public class CreateAdministratorDto
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}