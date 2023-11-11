using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Models.DTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_API.Controllers
{
    [Route("api/VillaAPI")]
    //[Route("api/[controller]")] - Generic way also works
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        private readonly ApplicationDBContext _db;

        public VillaAPIController(ApplicationDBContext db)
        {
            _db = db;
        }

        [HttpGet]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            return Ok(_db.Villas);
        }

        [HttpGet("{id:int}", Name ="GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)] //documenting possible response
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDTO> GetVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();                
            }

            var villa = _db.Villas.FirstOrDefault(x => x.Id == id);

            if (villa == null)
                return NotFound();

            return Ok(villa);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost]
        public ActionResult<VillaDTO> CreateVilla([FromBody]VillaDTO villa)
        {
            //Custom validation for unique villa name
            if(_db.Villas.FirstOrDefault(x => x.Name.ToLower() == villa.Name.ToLower()) != null)
            {
                ModelState.AddModelError("Customerror", "The villa already exists !");
                return BadRequest(ModelState);
            }

            if(villa == null)
                return BadRequest();

            Villa model = new Villa
            {
                Name = villa.Name,
                Id = villa.Id,
                Amenity = villa.Amenity,
                Details = villa.Details,
                ImageUrl = villa.ImageUrl,
                Occupancy = villa.Occupancy,
                Rate = villa.Rate,
                Sqft = villa.Sqft
            };

            _db.Villas.Add(model);
            _db.SaveChanges();

            //return Ok(villa);
            return CreatedAtRoute("GetVilla", new { id = villa.Id }, villa);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:int}", Name ="DeleteVilla")]
        public IActionResult DeleteVilla(int id)
        {
            if (id == 0)
                return BadRequest();

            var villa = _db.Villas.FirstOrDefault(x => x.Id == id);

            if(villa == null)
                return NotFound();

            _db.Villas.Remove(villa);
            _db.SaveChanges();

            return NoContent();
        }

        [HttpPut("{id:int}")]
        public ActionResult UpdateVilla(int id, [FromBody]VillaDTO villa) 
        { 
            if(villa == null || id != villa.Id)
                return BadRequest();

            var villaDTO = VillaStore.villaList.FirstOrDefault(x => x.Id == id);

            Villa model = new Villa
            {
                Name = villa.Name,
                Id = villa.Id,
                Amenity = villa.Amenity,
                Details = villa.Details,
                ImageUrl = villa.ImageUrl,
                Occupancy = villa.Occupancy,
                Rate = villa.Rate,
                Sqft = villa.Sqft
            };

            _db.Update(model);
            _db.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchDTO)
        {
            if(patchDTO == null || id == 0)
            {
                return BadRequest();
            }

            var villa = _db.Villas.AsNoTracking().FirstOrDefault(x => x.Id == id);

            VillaDTO villaDTO = new VillaDTO
            {
                Amenity = villa.Amenity,
                Details = villa.Details,
                ImageUrl = villa.ImageUrl,
                Name = villa.Name,
                Id = villa.Id,
                Occupancy = villa.Occupancy,
                Rate = villa.Rate,
                Sqft = villa.Sqft
            };

            if (villa == null)
            {
                return BadRequest();
            }

            patchDTO.ApplyTo(villaDTO, ModelState);

            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            Villa model = new Villa
            {
                Name = villaDTO.Name,
                Id = villaDTO.Id,
                Amenity = villaDTO.Amenity,
                Details = villaDTO.Details,
                ImageUrl = villaDTO.ImageUrl,
                Occupancy = villaDTO.Occupancy,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.Sqft
            };

            _db.Update(model);
            _db.SaveChanges();

            return NoContent();
        }
    }
}
