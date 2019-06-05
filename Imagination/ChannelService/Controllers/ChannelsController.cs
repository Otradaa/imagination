using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChannelService.Data.Models;
using ChannelService.Data.Repository;

namespace ChannelService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChannelsController : ControllerBase
    {
        private readonly ChannelContext _context;

        public ChannelsController(ChannelContext context)
        {
            _context = context;
        }

        // GET: api/Channels
        [HttpGet]
        public IEnumerable<Channel> GetChannels([FromQuery] int userid)
        {
            return _context.Channels.Where(c => c.UserId == userid);
        }

        [HttpGet("{id}/images")]
        public ResponseChannel GetChannelImages([FromRoute] int id)
        {
            var cims = _context.ChannelImages.Where(c => c.ChannelId == id);
            var ch = _context.Channels.Find(id);
            var scount = _context.Subscriptions.Where(c => c.ChannelId == id).Count();
            return new ResponseChannel() { channel = ch, images = cims, subsCount = scount };
        }

        [HttpGet("{id}/subs")]
        public int GetChannelSubs([FromRoute] int id)
        {
            return _context.Subscriptions.Where(c => c.ChannelId == id).Count();
        }

        // GET: api/Channels/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetChannel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var channel = await _context.Channels.FindAsync(id);

            if (channel == null)
            {
                return NotFound();
            }

            return Ok(channel);
        }

        // PUT: api/Channels/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChannel([FromRoute] int id, [FromBody] Channel channel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != channel.Id)
            {
                return BadRequest();
            }

            _context.Entry(channel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChannelExists(id))
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

        // POST: api/Channels
        [HttpPost]
        public async Task<IActionResult> PostChannel([FromBody] Channel channel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Channels.Add(channel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChannel", new { id = channel.Id }, channel);
        }

        // DELETE: api/Channels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChannel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var channel = await _context.Channels.FindAsync(id);
            if (channel == null)
            {
                return NotFound();
            }

            _context.Channels.Remove(channel);
            await _context.SaveChangesAsync();

            return Ok(channel);
        }

        [HttpPost("images")]
        public async Task<IActionResult> PostImage([FromBody] ChannelImage image)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ChannelImages.Add(image);
            await _context.SaveChangesAsync();

            return Ok(image);
        }

        private bool ChannelExists(int id)
        {
            return _context.Channels.Any(e => e.Id == id);
        }
    }
}