using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AirSnitch.Api.Controllers
{
    [ApiController]
    [Route("apiUser")]
    public class ApiUserController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            //Page<ApiUser> page = _apiUserRepository.GetAll();

            return new SuccessRestApiResult();
        }
        
        [HttpGet]
        [Route("{clientUserId}")]
        public SuccessRestApiResult GetById(string clientUserId)
        {
            //ApiUser user = _apiUserRepository.GetById(clientUserId);

            return new SuccessRestApiResult();
        }
    }
}