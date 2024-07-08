using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

//PUT request statuses with totals over $50.00 to REVIEW: api/requests/review/5
        [HttpPut("review/{id}")]
        public async Task<ActionResult<Request>> PutRequestReview(Request request)
        {
        if(request.Total > 50.00m)
        {
            request.Status = "REVIEW";
            _context.Requests.Add(request);
            await _context.SaveChangesAsync();
        }

            return CreatedAtAction("GetOrder", new { id = request.Id }, request);
        }

//PUT request statuses with totals <= $50.00 to APPROVED: api/requests/approve/5
        [HttpPut("approve/{id}")]
        public async Task<ActionResult<Request>> PutRequestApprove(Request request)
        {
        if(request.Total <= 50.00m)
        {
            request.Status = "APPROVED";
            _context.Requests.Add(request);
            await _context.SaveChangesAsync();
        }

            return CreatedAtAction("GetOrder", new { id = request.Id }, request);
        }

//PUT status of provided request Id to REJECTED: api/requests/reject/5
        [HttpPut("reject/{id}")]
        public async Task<ActionResult<Request>> PutRequestReject(Request request, int id)
        {
            var request = await _context.Requests.FindAsync(id);
        
            if(request == null)
            {
            return NotFound();
            }
            else 
            {
            //set status to rejected if id is id and entry is rejected. 
            return CreatedAtAction("GetOrder", new { request = request.Status }, "Rejected");
        }

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

 //POST new requests with NEW status: api/Requests
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            order.Status = "NEW"; //this will make the status column for new orders first.
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.Id }, order); //instance of an order
        }
        
        // POST: api/Requests
        [HttpPost]
        public async Task<ActionResult<Request>> PostRequest(Request request)
        {
            _context.Requests.Add(request);
            await _context.SaveChangesAsync();

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
