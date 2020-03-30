using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using adService.Models;
using Microsoft.EntityFrameworkCore;

namespace adService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public AdController(ApplicationDbContext context)
        {
            this.context = context;
        }

        //Get api/ad
        //[Route("")]
        [HttpGet]
        public IEnumerable<Ad> GetAll()
        {
            return context.Ads.ToList();
        }

        //[Route("getbyname")]
        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            name = name.ToLower();
            var ad = context.Ads.Where(
                x => x.Description.ToLower().Contains(name)).FirstOrDefault();

            if(ad == null)
            {
                //Si no encuentra publicidad con ese texto, regresa una random
                Random rnd = new Random();
                int pos = rnd.Next(1, context.Ads.ToList().Count);
                var adRnd = context.Ads.FirstOrDefault(x => x.Id == pos);
                return Ok(adRnd);
            }

            return Ok(ad);
        }


        /*[HttpPost]
        public IActionResult Post([FromBody] Ad ad)
        {
            if (ModelState.IsValid)
            {
                context.Ads.Add(ad);
                context.SaveChanges();

                return new CreatedAtRouteResult("adCreated", new { id = ad.Id }, ad);
            }

            return BadRequest(ModelState);
        }*/


        /*[HttpPut("{id}")]
        public IActionResult Put([FromBody] Ad ad, int id)
        {
            if(ad.Id != id)
            {
                return BadRequest();
            }

            context.Entry(ad).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }*/


        /*[HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var ad = context.Ads.FirstOrDefault(x => x.Id == id);
            if(ad == null)
            {
                return NotFound();
            }

            context.Ads.Remove(ad);
            context.SaveChanges();
            return Ok(ad);
        }*/
    }
}