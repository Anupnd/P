using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P.data;
using P.Models;

namespace P.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeoplesController : Controller
    {
        private readonly PeoplesAPIDbContext dbContext;

        public PeoplesController(PeoplesAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet]
        public async Task<IActionResult> GetPeoples()
        {
            return Ok(await dbContext.Peoples.ToListAsync());
        }
        [HttpGet]
        [Route("{id:guid}")]

        public async Task<IActionResult> GetPeople([FromRoute] Guid id)
        {
            var people = await dbContext.Peoples.FindAsync(id);

            if (people == null)
            {
                return NotFound("people not found");
            }
            return Ok(people);  
        }


        [HttpPost]
        public async Task<IActionResult> AddPeople(AddPeopleRequest addPeopleRequest) //creating a new peoples object
        {
            var people = new People()
            {

                Id = Guid.NewGuid(),
                FirstName = addPeopleRequest.FirstName,
                LastName = addPeopleRequest.LastName,
                Address = addPeopleRequest.Address,
                DOB = addPeopleRequest.DOB
            };

            await dbContext.Peoples.AddAsync(people);
            await dbContext.SaveChangesAsync();

            return Ok(people);
        }


        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdatePeople([FromRoute] Guid id, UpdatePeopleRequest updatePeopleRequest)
        {
            var people = await dbContext.Peoples.FindAsync(id);

            if (people != null) 
            {
                people.FirstName = updatePeopleRequest.FirstName;
                people.LastName = updatePeopleRequest.LastName;
                people.Address = updatePeopleRequest.Address;
                people.DOB = updatePeopleRequest.DOB;

                await dbContext.SaveChangesAsync();

                return Ok(people);
            
            }
            return NotFound();
        }


        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> DeletePeople([FromRoute] Guid id)
        {
            var people = await dbContext.Peoples.FindAsync(id);

            if (people != null)
            {
                dbContext.Remove(people);
                await dbContext.SaveChangesAsync();
                return Ok(people);
            }
            return NotFound(nameof(people));
        }

    }
}
