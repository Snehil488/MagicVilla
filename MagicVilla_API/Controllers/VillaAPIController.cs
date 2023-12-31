﻿using AutoMapper;
using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Models.DTO;
using MagicVilla_API.Repository.IRepository;
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
        private readonly IVillaRepository _dbVilla;
        private readonly IMapper _mapper;

        public VillaAPIController(IVillaRepository dbVilla, IMapper mapper)
        {
            _dbVilla = dbVilla;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VillaDTO>>> GetVillas()
        {
            IEnumerable<Villa> villaList = await _dbVilla.GetAllAsync();
            return Ok(_mapper.Map<List<VillaDTO>>(villaList));
        }

        [HttpGet("{id:int}", Name ="GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)] //documenting possible response
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VillaDTO>> GetVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();                
            }

            var villa = await _dbVilla.GetAsync(x => x.Id == id);

            if (villa == null)
                return NotFound();

            return Ok(_mapper.Map<VillaDTO>(villa));
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost]
        public async Task<ActionResult<VillaDTO>> CreateVilla([FromBody]VillaCreateDTO villa)
        {
            //Custom validation for unique villa name
            if(await _dbVilla.GetAsync(x => x.Name.ToLower() == villa.Name.ToLower()) != null)
            {
                ModelState.AddModelError("Customerror", "The villa already exists !");
                return BadRequest(ModelState);
            }

            if(villa == null)
                return BadRequest();

            Villa model = _mapper.Map<Villa>(villa);

            await _dbVilla.CreateAsync(model);

            //return Ok(villa);
            return CreatedAtRoute("GetVilla", new { id = model.Id }, model);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:int}", Name ="DeleteVilla")]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            if (id == 0)
                return BadRequest();

            var villa = await _dbVilla.GetAsync(x => x.Id == id);

            if(villa == null)
                return NotFound();

            await _dbVilla.RemoveAsync(villa);

            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateVilla(int id, [FromBody]VillaUpdateDTO villa) 
        { 
            if(villa == null || id != villa.Id)
                return BadRequest();

            Villa model = _mapper.Map<Villa>(villa);

            await _dbVilla.UpdateAsync(model);

            return NoContent();
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDTO> patchDTO)
        {
            if(patchDTO == null || id == 0)
            {
                return BadRequest();
            }

            var villa = await _dbVilla.GetAsync(x => x.Id == id, false);

            VillaUpdateDTO villaDTO = _mapper.Map<VillaUpdateDTO>(villa);

            if (villa == null)
            {
                return BadRequest();
            }

            patchDTO.ApplyTo(villaDTO, ModelState);

            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            Villa model = _mapper.Map<Villa>(villaDTO);

            await _dbVilla.UpdateAsync(model);

            return NoContent();
        }
    }
}
