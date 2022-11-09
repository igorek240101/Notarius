using Microsoft.AspNetCore.Mvc;
using NotariusBack.Repository.Entity.Enums;
using NotariusBack.Repository.Entity;
using NotariusBack.Service;
using NotariusBack.Service.ModelDto;

namespace NotariusBack.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DealController : ControllerBase
    {
        DealService dealService;
        UserService userService;

        public DealController()
        {
            dealService = new DealService();
            userService = new UserService();
        }

        [HttpPost]
        [Route("Add")]
        public async Task<ActionResult<string>> Add(DealDto deal)
        {
            if (await userService.IsAccess(Request, new UserRoleEnum?[] { UserRoleEnum.Administrator }))
            {
                try
                {
                    await dealService.Add(deal);
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

        [HttpGet]
        [Route("GetOpen")]
        public async Task<ActionResult<List<Deal>>> GetOpen()
        {
            if (await userService.IsAccess(Request, new UserRoleEnum?[] { UserRoleEnum.Notarius}))
            {
                List<Deal> deal;
                try
                {
                    deal = await dealService.GetOpen();
                }
                catch (ArgumentException e)
                {
                    return UnprocessableEntity(e.Message);
                }
                catch (Exception)
                {
                    return BadRequest();
                }
                if (deal != null)
                {
                    string s = string.Empty;
                    for (int i = 0; i < deal.Count; i++)
                    {
                        s += $"{deal[i].Id}" +
                            $"%{deal[i].Description}" +
                            $"{((i + 1 < deal.Count) ? "~" : "")}";
                    }
                    return Ok(s);
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

        [HttpGet]
        [Route("GetDone")]
        public async Task<ActionResult<List<Deal>>> GetDone()
        {
            if (await userService.IsAccess(Request, new UserRoleEnum?[] { UserRoleEnum.Administrator }))
            {
                List<Deal> deal;
                try
                {
                    deal = await dealService.GetDone();
                }
                catch (ArgumentException e)
                {
                    return UnprocessableEntity(e.Message);
                }
                catch (Exception)
                {
                    return BadRequest();
                }
                if (deal != null)
                {
                    string s = string.Empty;
                    for (int i = 0; i < deal.Count; i++)
                    {
                        s += $"{deal[i].Id}" +
                            $"%{deal[i].Description}" +
                            $"{((i + 1 < deal.Count) ? "~" : "")}";
                    }
                    return Ok(s);
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

        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult<Deal>> Get()
        {
            if (await userService.IsAccess(Request, new UserRoleEnum?[] { UserRoleEnum.Notarius }))
            {
                Deal deal;
                try
                {
                    deal = await dealService.Get();
                }
                catch (ArgumentException e)
                {
                    return UnprocessableEntity(e.Message);
                }
                catch (Exception)
                {
                    return BadRequest();
                }
                string s;
                if (deal != null)
                {
                    s = $"{deal.Id}" +
                        $"%{deal.Description}" +
                        $"%{deal.Date}" +
                        $"%{deal.Client.Id}" +
                        $"~{deal.Client.Name}" +
                        $"~{deal.Client.Phone}" +
                        $"~{deal.Client.Adress}" +
                        $"~{deal.Client.Type}" +
                        $"%{deal.Service.Id}" +
                        $"~{deal.Service.Name}" +
                        $"~{deal.Service.Description}" +
                        $"~{deal.Service.Price}" +
                        $"~{deal.Service.Commission}";
                }
                else s = "null";
                return Ok(s);
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<List<Deal>>> GetAll()
        {
            if (await userService.IsAccess(Request, new UserRoleEnum?[] { UserRoleEnum.Administrator }))
            {
                List<Deal> deal;
                try
                {
                    deal = await dealService.GetAll();
                }
                catch (ArgumentException e)
                {
                    return UnprocessableEntity(e.Message);
                }
                catch (Exception)
                {
                    return BadRequest();
                }
                if (deal != null)
                {
                    return Ok(deal);
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


        [HttpPut]
        [Route("ToInProgress")]
        public async Task<ActionResult> ToInProgress(int id)
        {
            if (await userService.IsAccess(Request, new UserRoleEnum?[] { UserRoleEnum.Notarius }))
            {
                try
                {
                    await dealService.ToInProgress(id);
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

        [HttpPut]
        [Route("ToDone")]
        public async Task<ActionResult> ToDone(int id, int transactionAmount)
        {
            if (await userService.IsAccess(Request, new UserRoleEnum?[] { UserRoleEnum.Notarius }))
            {
                try
                {
                    await dealService.ToDone(id, transactionAmount);
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

        [HttpPut]
        [Route("ToCanceld")]
        public async Task<ActionResult> ToCanceld(int id)
        {
            if (await userService.IsAccess(Request, new UserRoleEnum?[] { UserRoleEnum.Notarius }))
            {
                try
                {
                    await dealService.ToCanceld(id);
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

        [HttpPut]
        [Route("ToClouse")]
        public async Task<ActionResult> ToClouse(int id)
        {
            if (await userService.IsAccess(Request, new UserRoleEnum?[] { UserRoleEnum.Administrator }))
            {
                try
                {
                    await dealService.ToClouse(id);
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


        [HttpGet]
        [Route("GetSum")]
        public async Task<ActionResult<double>> GetSum(DateTime start, DateTime end)
        {
            if (await userService.IsAccess(Request, new UserRoleEnum?[] { UserRoleEnum.Financer }))
            {
                double? sum;
                try
                {
                    sum = await dealService.GetSum(start, end);
                }
                catch (ArgumentException e)
                {
                    return UnprocessableEntity(e.Message);
                }
                catch (Exception)
                {
                    return BadRequest();
                }
                if (sum != null)
                {
                    return Ok(sum);
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
    }
}
