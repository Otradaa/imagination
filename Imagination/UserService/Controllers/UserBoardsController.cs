﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserService.Data.Models;
using UserService.Data.Repository;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserBoardsController : ControllerBase
    {
        private readonly IUserRepository _repo;

        public UserBoardsController(IUserRepository repo) => _repo = repo;

        // получение списка досок пользователя
        [HttpGet("{clientId}")]
        public IActionResult GetBoardsByUserId([FromRoute] int userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userBoards = _repo.GetBoardsByUserId(userId);

            if (userBoards == null)
            {
                return NotFound();
            }

            return Ok(userBoards);
        }

        // добавление доски
        // POST: api/UserBoards
        [HttpPost]
        public async Task<IActionResult> PostUserBoard([FromBody] UserBoard userBoard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repo.AddBoard(userBoard);

            return CreatedAtAction("GetUserBoard", new { id = userBoard.Id }, userBoard);
        }

        // удаление доски
        // DELETE: api/UserBoards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserBoard([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userBoard = await _repo.GetBoard(id);
            if (userBoard == null)
            {
                return NotFound();
            }

            await _repo.RemoveBoard(userBoard);

            return Ok(userBoard);
        }

        // добавление изображения в доску
        // POST: api/UserBoards
        [HttpPost]
        [Route("api/UserBoards/images")]
        public async Task<IActionResult> PostImageOnBoard([FromBody] BoardImage image)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repo.AddImage(image);

            return CreatedAtAction("GetUserBoard", new { id = image.Id }, image);
        }

        // удаление изображения из доски
        // DELETE: api/UserBoards/5
        [HttpDelete("/images/{id}")]
        public async Task<IActionResult> DeleteImageFromBoard([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var image = await _repo.GetBoardImage(id);
            if (image == null)
            {
                return NotFound();
            }

            await _repo.RemoveImage(image);

            return Ok(image);
        }

        // получение списка изображений доски
        [HttpGet("images/{id}")]
        public IActionResult GetImagesByBoardId([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var images =  _repo.GetImagesByBoardId(id);

            if (images == null)
            {
                return NotFound();
            }

            return Ok(images);
        }

        // PUT: api/UserBoards/5
        /*[HttpPut("{id}")]
        public async Task<IActionResult> PutUserBoard([FromRoute] int id, [FromBody] UserBoard userBoard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userBoard.Id)
            {
                return BadRequest();
            }

            _context.Entry(userBoard).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserBoardExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        */
    }
       
}