using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _13_ORM_Geenid.Data;
using _13_ORM_Geenid.Models;
using _13_ORM_Geenid.DTOs;

namespace _13_ORM_Geenid.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeeniController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GeeniController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Geeni
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<GeeniResponse>>> GetGeenid()
        {
            var geenid = await _context.Geenid
                .Include(g => g.Alleel1)
                .Include(g => g.Alleel2)
                .ToListAsync();

            var response = geenid.Select(g => new GeeniResponse
            {
                Id = g.Id,
                Alleel1 = new AlleeliResponse
                {
                    Id = g.Alleel1.Id,
                    Nimetus = g.Alleel1.Nimetus,
                    Positiivne = g.Alleel1.Positiivne
                },
                Alleel2 = new AlleeliResponse
                {
                    Id = g.Alleel2.Id,
                    Nimetus = g.Alleel2.Nimetus,
                    Positiivne = g.Alleel2.Positiivne
                },
                OnPositiivne = g.OnPositiivne
            });

            return Ok(response);
        }

        // GET: api/Geeni/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GeeniResponse>> GetGeeni(int id)
        {
            var geeni = await _context.Geenid
                .Include(g => g.Alleel1)
                .Include(g => g.Alleel2)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (geeni == null)
            {
                return NotFound($"Geeni ID-ga {id} ei leitud");
            }

            var response = new GeeniResponse
            {
                Id = geeni.Id,
                Alleel1 = new AlleeliResponse
                {
                    Id = geeni.Alleel1.Id,
                    Nimetus = geeni.Alleel1.Nimetus,
                    Positiivne = geeni.Alleel1.Positiivne
                },
                Alleel2 = new AlleeliResponse
                {
                    Id = geeni.Alleel2.Id,
                    Nimetus = geeni.Alleel2.Nimetus,
                    Positiivne = geeni.Alleel2.Positiivne
                },
                OnPositiivne = geeni.OnPositiivne
            };

            return Ok(response);
        }

        // GET: api/Geeni/5/juhuslikalleel
        [HttpGet("{id}/juhuslikalleel")]
        public async Task<ActionResult<AlleeliResponse>> GetJuhuslikAlleel(int id)
        {
            var geeni = await _context.Geenid
                .Include(g => g.Alleel1)
                .Include(g => g.Alleel2)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (geeni == null)
            {
                return NotFound($"Geeni ID-ga {id} ei leitud");
            }

            var juhuslikAlleel = geeni.VotaJuhuslikAlleel();

            var response = new AlleeliResponse
            {
                Id = juhuslikAlleel.Id,
                Nimetus = juhuslikAlleel.Nimetus,
                Positiivne = juhuslikAlleel.Positiivne
            };

            return Ok(response);
        }

        // GET: api/Geeni/nimetus/{nimetus}
        [HttpGet("nimetus/{nimetus}")]
        public async Task<ActionResult<IEnumerable<GeeniResponse>>> GetGeenidByNimetus(string nimetus)
        {
            var geenid = await _context.Geenid
                .Include(g => g.Alleel1)
                .Include(g => g.Alleel2)
                .Where(g => g.Alleel1.Nimetus == nimetus)
                .ToListAsync();

            if (!geenid.Any())
            {
                return NotFound($"Geene alleeli nimetusega '{nimetus}' ei leitud");
            }

            var response = geenid.Select(g => new GeeniResponse
            {
                Id = g.Id,
                Alleel1 = new AlleeliResponse
                {
                    Id = g.Alleel1.Id,
                    Nimetus = g.Alleel1.Nimetus,
                    Positiivne = g.Alleel1.Positiivne
                },
                Alleel2 = new AlleeliResponse
                {
                    Id = g.Alleel2.Id,
                    Nimetus = g.Alleel2.Nimetus,
                    Positiivne = g.Alleel2.Positiivne
                },
                OnPositiivne = g.OnPositiivne
            });

            return Ok(response);
        }

        // POST: api/Geeni/loo
        [HttpPost("loo")]
        public async Task<ActionResult<GeeniResponse>> LooGeen([FromBody] LooGeenRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var alleel1 = new Alleeli(request.AlleeliNimetus, request.Vanem1Positiivne);
            var alleel2 = new Alleeli(request.AlleeliNimetus, request.Vanem2Positiivne);

            _context.Alleelid.Add(alleel1);
            _context.Alleelid.Add(alleel2);
            await _context.SaveChangesAsync();

            var geeni = new Geeni
            {
                Alleel1Id = alleel1.Id,
                Alleel2Id = alleel2.Id
            };

            _context.Geenid.Add(geeni);
            await _context.SaveChangesAsync();

            await _context.Entry(geeni).Reference(g => g.Alleel1).LoadAsync();
            await _context.Entry(geeni).Reference(g => g.Alleel2).LoadAsync();

            var response = new GeeniResponse
            {
                Id = geeni.Id,
                Alleel1 = new AlleeliResponse
                {
                    Id = geeni.Alleel1.Id,
                    Nimetus = geeni.Alleel1.Nimetus,
                    Positiivne = geeni.Alleel1.Positiivne
                },
                Alleel2 = new AlleeliResponse
                {
                    Id = geeni.Alleel2.Id,
                    Nimetus = geeni.Alleel2.Nimetus,
                    Positiivne = geeni.Alleel2.Positiivne
                },
                OnPositiivne = geeni.OnPositiivne
            };

            return CreatedAtAction(nameof(GetGeenidByNimetus), new { nimetus = request.AlleeliNimetus }, response);
        }

        // POST: api/Geeni/yhenda
        [HttpPost("yhenda")]
        public async Task<ActionResult<GeeniResponse>> YhendaGeenid([FromBody] YhendaRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vanem1 = await _context.Geenid
                .Include(g => g.Alleel1)
                .Include(g => g.Alleel2)
                .FirstOrDefaultAsync(g => g.Id == request.Vanem1Id);

            var vanem2 = await _context.Geenid
                .Include(g => g.Alleel1)
                .Include(g => g.Alleel2)
                .FirstOrDefaultAsync(g => g.Id == request.Vanem2Id);

            if (vanem1 == null || vanem2 == null)
            {
                return BadRequest("Mõlemad vanemad peavad eksisteerima");
            }

            if (vanem1.Alleel1.Nimetus != vanem2.Alleel1.Nimetus)
            {
                return BadRequest("Geenid peavad olema sama tüüpi (sama nimetusega alleelid)");
            }

            var laps = Geeni.Yhenda(vanem1, vanem2);

            _context.Alleelid.Add(laps.Alleel1);
            _context.Alleelid.Add(laps.Alleel2);
            await _context.SaveChangesAsync();

            laps.Alleel1Id = laps.Alleel1.Id;
            laps.Alleel2Id = laps.Alleel2.Id;

            _context.Geenid.Add(laps);
            await _context.SaveChangesAsync();

            var response = new GeeniResponse
            {
                Id = laps.Id,
                Alleel1 = new AlleeliResponse
                {
                    Id = laps.Alleel1.Id,
                    Nimetus = laps.Alleel1.Nimetus,
                    Positiivne = laps.Alleel1.Positiivne
                },
                Alleel2 = new AlleeliResponse
                {
                    Id = laps.Alleel2.Id,
                    Nimetus = laps.Alleel2.Nimetus,
                    Positiivne = laps.Alleel2.Positiivne
                },
                OnPositiivne = laps.OnPositiivne
            };

            return CreatedAtAction(nameof(GetGeenidByNimetus), new { nimetus = laps.Alleel1.Nimetus }, response);
        }

        // PUT: api/Geeni/5
        [HttpPut("{id}")]
        public async Task<ActionResult<GeeniResponse>> UpdateGeeni(int id, [FromBody] UpdateGeeniRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var geeni = await _context.Geenid
                .Include(g => g.Alleel1)
                .Include(g => g.Alleel2)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (geeni == null)
            {
                return NotFound($"Geeni ID-ga {id} ei leitud");
            }

            geeni.Alleel1.Nimetus = request.AlleeliNimetus;
            geeni.Alleel1.Positiivne = request.Alleel1Positiivne;

            geeni.Alleel2.Nimetus = request.AlleeliNimetus;
            geeni.Alleel2.Positiivne = request.Alleel2Positiivne;

            _context.Entry(geeni.Alleel1).State = EntityState.Modified;
            _context.Entry(geeni.Alleel2).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            var response = new GeeniResponse
            {
                Id = geeni.Id,
                Alleel1 = new AlleeliResponse
                {
                    Id = geeni.Alleel1.Id,
                    Nimetus = geeni.Alleel1.Nimetus,
                    Positiivne = geeni.Alleel1.Positiivne
                },
                Alleel2 = new AlleeliResponse
                {
                    Id = geeni.Alleel2.Id,
                    Nimetus = geeni.Alleel2.Nimetus,
                    Positiivne = geeni.Alleel2.Positiivne
                },
                OnPositiivne = geeni.OnPositiivne
            };

            return Ok(response);
        }

        // DELETE: api/Geeni/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGeeni(int id)
        {
            var geeni = await _context.Geenid
                .Include(g => g.Alleel1)
                .Include(g => g.Alleel2)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (geeni == null)
            {
                return NotFound($"Geeni ID-ga {id} ei leitud");
            }

            var alleel1Id = geeni.Alleel1Id;
            var alleel2Id = geeni.Alleel2Id;

            _context.Geenid.Remove(geeni);
            await _context.SaveChangesAsync();

            var alleel1 = await _context.Alleelid.FindAsync(alleel1Id);
            var alleel2 = await _context.Alleelid.FindAsync(alleel2Id);

            if (alleel1 != null)
                _context.Alleelid.Remove(alleel1);
            if (alleel2 != null)
                _context.Alleelid.Remove(alleel2);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
