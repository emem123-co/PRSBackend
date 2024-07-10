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
    public class VendorsController : ControllerBase
    {
        private readonly PRSBackendContext _context;

        public VendorsController(PRSBackendContext context)
        {
            _context = context;
        }

        //CREATE PO
        [HttpGet("po/{vendorId}")]
        public async Task<ActionResult<Po>> CreatePo(int vendorId)
        {
            Po po = new Po(); //instance of PO class. includes all properties. 
            
            var newVendor = await GetVendor(vendorId);
            
            newVendor = po.Vendor;

            var polines = (from v in _context.Vendors 
                          join p in _context.Products
                            on v.Id equals p.VendorId
                          join rql in _context.RequestLines 
                            on p.Id equals rql.ProductID
                          join r in _context.Requests
                          on rql.RequestID equals r.Id
                          where r.Status == "APPROVED"
                          select new
                          {
                            p.Id,
                            Product = p.Name,
                            rql.Quantity,
                            p.Price,
                            LineTotal = (p.Price * rql.Quantity)
                          });

            var sortedLines = new SortedList<int, Poline>(); //new collection with int as key and Poline as data.

            //foreach(var line in sortedLines)
            //{
            //    if(!sortedLines.ContainsKey(line.Key))
            //    {
            //        var poline = new Poline()
            //        {
            //            Product = l.Product
            //            Quantity = 0
            //            Price = l.Price
            //            LineTotal = 

            //        };
            //    }
            //}
            

            

            
            
        return Ok();   
        }

        // GET: api/Vendors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vendor>>> GetVendor()
        {
            return await _context.Vendors.ToListAsync();
        }

        // GET: api/Vendors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vendor>> GetVendor(int id)
        {
            var vendor = await _context.Vendors.FindAsync(id);

            if (vendor == null)
            {
                return NotFound();
            }
            return vendor;
        }

        // PUT: api/Vendors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVendor(int id, Vendor vendor)
        {
            if (id != vendor.Id)
            {
                return BadRequest();
            }

            _context.Entry(vendor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendorExists(id))
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

        // POST: api/Vendors
        [HttpPost]
        public async Task<ActionResult<Vendor>> PostVendor(Vendor vendor)
        {
            _context.Vendors.Add(vendor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVendor", new { id = vendor.Id }, vendor);
        }

        // DELETE: api/Vendors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVendor(int id)
        {
            var vendor = await _context.Vendors.FindAsync(id);
            if (vendor == null)
            {
                return NotFound();
            }

            _context.Vendors.Remove(vendor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VendorExists(int id)
        {
            return _context.Vendors.Any(e => e.Id == id);
        }
    }
}
