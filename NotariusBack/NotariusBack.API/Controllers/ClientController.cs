using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotariusBack.Repository.Entity;
using NotariusBack.Service;
using NotariusBack.Service.ModelDto;
using NotariusBack.Repository.Entity.Enums;

namespace NotariusBack.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        ClientService clientService;
        UserService userService;

        public ClientController()
        {
            clientService = new ClientService();
            userService = new UserService();
        }

        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult<string>> Get(string name)
        {
            if (await userService.IsAccess(Request, new UserRoleEnum?[] { UserRoleEnum.Notarius, UserRoleEnum.Administrator }))
            {
                Client dto;
                try
                {
                    dto = await clientService.Get(name);
                }
                catch (ArgumentException e)
                {
                    return UnprocessableEntity(e.Message);
                }
                catch (Exception)
                {
                    return BadRequest();
                }
                if (dto != null)
                {
                    return Ok($"{dto.Id}%{dto.Name}%{dto.Adress}%{dto.Phone}%{(int)(dto.Type)}");
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        [Route("Add")]
        public async Task<ActionResult<string>> Add(ClientDto client)
        {
            if (await userService.IsAccess(Request, new UserRoleEnum?[] { UserRoleEnum.Administrator }))
            {
                Client dto;
                try
                {
                    dto = await clientService.Add(client);
                }
                catch (ArgumentException e)
                {
                    return UnprocessableEntity(e.Message);
                }
                catch (Exception)
                {
                    return BadRequest();
                }
                return Ok($"{dto.Id}%{dto.Name}%{dto.Adress}%{dto.Phone}%{(int)(dto.Type)}");
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPut]
        [Route("Update")]
        public async Task<ActionResult> Update(int id, string? adress, string? phone)
        {
            if (await userService.IsAccess(Request, new UserRoleEnum?[] { UserRoleEnum.Administrator }))
            {
                try
                {
                    await clientService.Update(new ClientDto() { Adress = adress, Phone = phone }, id);
                }
                catch (ArgumentException e)
                {
                    return UnprocessableEntity(e.Message);
                }
                catch (Exception)
                {
                    return BadRequest();
                }
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
