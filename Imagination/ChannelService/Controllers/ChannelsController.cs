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

        [HttpGet("top")]
        public IEnumerable<Channel> GetTopChannels()
        {
            int top = 20;
            int count = _context.Channels.Count();
            return _context.Channels.OrderBy(r => Guid.NewGuid()).Take((count>top)?top:count);
        }

        [HttpGet("{id}/images")]
        public ResponseChannel GetChannelImages([FromRoute]int id, [FromQuery]int userid)
        {
            var cims = _context.ChannelImages.Where(c => c.ChannelId == id);
            var ch = _context.Channels.Find(id);
            var scount = _context.Subscriptions.Where(c => c.ChannelId == id).Count();
            //var sub = _context.Subscriptions.Where(s => s.ChannelId == id && s.UserId == userid);
            //var issubed = (sub.Count() != 0) ? true : false;
            var issubed = _context.Subscriptions.Any(s => s.ChannelId == id && s.UserId == userid);
            return new ResponseChannel() { channel = ch, images = cims, subsCount = scount, isSubscribed = issubed };
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

            var chimages = _context.ChannelImages.Where(c => c.ChannelId == id);
            if (chimages != null && chimages.Count() > 0)
            {
                _context.ChannelImages.RemoveRange(chimages);
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

        [HttpDelete("{id}/images/{imageid}")]
        public async Task<IActionResult> DeleteChannelImage(int id, int imageid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var channelImage = _context.ChannelImages.First(c => c.Id == imageid); 
            if (channelImage == null)
            {
                return NotFound();
            }

            _context.ChannelImages.Remove(channelImage);
            await _context.SaveChangesAsync();

            return Ok(channelImage);
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