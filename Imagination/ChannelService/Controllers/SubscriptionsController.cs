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
    public class SubscriptionsController : ControllerBase
    {
        private readonly ChannelContext _context;

        public SubscriptionsController(ChannelContext context)
        {
            _context = context;
        }

        // GET: api/Subscriptions
        [HttpGet]
        public IEnumerable<SubsResponse> GetUserSubscriptions([FromQuery] int userid)
        {
            var subs = _context.Subscriptions.Where(s => s.UserId == userid);
            var resultSubs = new List<SubsResponse>();
            foreach (var sub in subs)
            {
                var channel = _context.Channels.Find(sub.ChannelId);
                resultSubs.Add(new SubsResponse()
                {
                    Id = sub.Id,
                    UserId = userid,
                    ChannelId = channel.Id,
                    ChannelName = channel.Name,
                    Description = channel.Description
                });
            }
            return resultSubs;
        }

        // GET: api/Subscriptions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubscription([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var subscription = await _context.Subscriptions.FindAsync(id);

            if (subscription == null)
            {
                return NotFound();
            }

            return Ok(subscription);
        }

        // PUT: api/Subscriptions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubscription([FromRoute] int id, [FromBody] Subscription subscription)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != subscription.Id)
            {
                return BadRequest();
            }

            _context.Entry(subscription).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubscriptionExists(id))
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

        [HttpGet("{cid}/issubed/{uid}")]
        public bool IsSubed([FromRoute] int cid, [FromRoute] int uid)
        {
            var issubed = _context.Subscriptions.Any(s => s.ChannelId == cid && s.UserId == uid);
            return issubed;
        }

        // POST: api/Subscriptions
        [HttpPost]
        public async Task<IActionResult> PostSubscription([FromBody] Subscription subscription)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Subscriptions.Add(subscription);
            await _context.SaveChangesAsync();

            return Ok(subscription);
        }

        // DELETE: api/Subscriptions/5
        [HttpDelete("{cid}")]
        public async Task<IActionResult> DeleteSubscription([FromRoute] int cid, [FromQuery]int userid)
        {
            var subscription = await _context.Subscriptions.FirstAsync(s=> s.ChannelId==cid && s.UserId==userid);
            if (subscription == null)
            {
                return NotFound();
            }

            _context.Subscriptions.Remove(subscription);
            await _context.SaveChangesAsync();

            return Ok(subscription);
        }

        private bool SubscriptionExists(int id)
        {
            return _context.Subscriptions.Any(e => e.Id == id);
        }
    }
}