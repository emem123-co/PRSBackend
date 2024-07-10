using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PRSBackend.Data;
using PRSBackend.Models;

namespace PRSBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly PRSBackendContext _context;

        public RequestsController(PRSBackendContext context)
        {
            _context = context;
        }

        // GET: api/Requests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequest()
        {
            return await _context.Requests.ToListAsync();
        }

        // GET: api/Requests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Request>> GetRequest(int id)
        {
            var request = await _context.Requests.FindAsync(id);

            if (request == null)
            {
                return NotFound();
            }

            return request;
        }

//GET REVIEW by USER ID (not belonging to current user): api/requests/reviews/{userId}
        [HttpGet("Requests/Reviews/{userId}")]
        //public async Task<IActionResult> GetReviews(Request requests, User users, int userId)
        
        //if user.Id == userId
        //do not show items with request.Status = "REVIEW"

            //UserId is automatically set to the Id of the logged in user?
            //var currentuserId = _context.Requests.UserId;
            
           //var inReview = from r in _context.Requests 
            //               where r.Status == "REVIEW"
            //               and r.UserId != userId
            //               select requests;
                                           
                    
        //    }
        //    //list of requests that are in review that do NOT have the userId of current user.

        //    var inReview = await _context.Requests.UserId.FindAsync(userId);


        //                return requests.ToList
        //    return inReview;
        //}


//GET requests by STATUS: api/requests/status/{status}
        [HttpGet("Requests/Status/{status}")]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequestByStatus(Request request, string status)
        {
            if(request.Status != status)
            {
            return NotFound();
            }
            
            var statusList = from r in _context.Requests
                             where r.Status == status
                             select r;

            await _context.SaveChangesAsync();

            return statusList.ToList();
        }
        
//PUT REVIEW status if >= $50.00m: api/requests/review/5
        [HttpPut("Requests/Review/{id}")]
        public async Task<IActionResult> PutReview(int id, Request request)
        {
            if(request.Total > 50.00m)
            {
                request.Status = "REVIEW";
            }
        await _context.SaveChangesAsync();
        return await PutRequest(id, request);
        }

//PUT APPROVED status if < $50.00: api/requests/approve/5
        [HttpPut("Requests/Approve/{id}")]
        public async Task<IActionResult> PutApprove(int id, Request request)
        {
            if (request.Total <= 50.00m)  
            {
                request.Status = "APPROVED";
            }
        await _context.SaveChangesAsync();
        return await PutRequest(id, request);
        }

//PUT REJECTED status: api/requests/reject/5
        [HttpPut("Requests/Reject/{id}")]
        public async Task<IActionResult> PutReject(int id, Request request)
        {
            if(id == request.Id)
            {
               request.Status = "REJECTED";
            }
            await _context.SaveChangesAsync();
            return await PutRequest(id, request);
        }

//PUT userId as the current user:






        // PUT: api/Requests/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequest(int id, Request request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            _context.Entry(request).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(id))
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

 
// POST: api/Requests
        [HttpPost]
        public async Task<ActionResult<Request>> PostRequest(Request request, User user)
        {
            request.Status = "NEW"; //this will make the status column for new orders first.
                             
            _context.Requests.Add(request);
            await _context.SaveChangesAsync();
            await PutApprove(request.Id, request);
            await PutReview(request.Id, request);
            await PutReject(request.Id, request);

            return CreatedAtAction("GetRequest", new { id = request.Id }, request);
        }

        // DELETE: api/Requests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequest(int id)
        {
            var request = await _context.Requests.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            _context.Requests.Remove(request);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RequestExists(int id)
        {
            return _context.Requests.Any(e => e.Id == id);
        }
    }
}
