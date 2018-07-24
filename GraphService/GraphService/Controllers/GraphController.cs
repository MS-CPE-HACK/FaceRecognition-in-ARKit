using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json.Linq;

namespace GraphService.Controllers
{
    [Route("api/[controller]")]
    public class GraphController : Controller
    {
        private readonly AuthenticationOptions authenticationOptions;

        public GraphController(IOptions<AuthenticationOptions> authenticationOptionsAccessor)
        {
            this.authenticationOptions = authenticationOptionsAccessor.Value;
        }

        [HttpGet("{alias}")]
        public async Task<IActionResult> Get(string alias)
        {
            if (!PerformaAuthentication())
            {
                return Unauthorized();
            }

            var client = new HttpClient();
            var token = await GetGraphAccessAuthenticationTokenAsync();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var userGraphUrl = $"https://graph.windows.net/microsoft.com/users/{alias}@microsoft.com?api-version=1.6";
            var response = await client.GetAsync(userGraphUrl);
            if(response.StatusCode != HttpStatusCode.OK)
            {
                return BadRequest();
            }
            var jsonResult = JObject.Parse(await response.Content.ReadAsStringAsync());
            var userInfo = jsonResult.ToObject<GraphObjectInfo>();
            return Ok(userInfo);
        }

        private async Task<string> GetGraphAccessAuthenticationTokenAsync()
        {
            var authority = string.Format("https://login.microsoftonline.com/{0}", this.authenticationOptions.TenantId);
            var authContext = new AuthenticationContext(authority);

            var credential = new ClientCredential(this.authenticationOptions.ClientId, this.authenticationOptions.ClientSecret);
            var result = await authContext.AcquireTokenAsync("https://graph.windows.net/", credential);
            return result.AccessToken;
        }

        private bool PerformaAuthentication()
        {
            try
            {
                var headerValue = Request.Headers["Authorization"];
                return Encoding.UTF8.GetString(Convert.FromBase64String(headerValue)) == this.authenticationOptions.SecretWord;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
