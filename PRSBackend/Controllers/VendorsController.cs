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

            var poLines = (from v in _context.Vendors 
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

            var sortedLines = new SortedList<int, Poline>(); //new list with int key and Poline as data.

            foreach(var poLine in poLines) //iterate through each item in the select statement collection once.
            {
                if(!sortedLines.ContainsKey(poLine.Id)) //if the product id from select statement collection is NOT already in the new list...
                {
                    var newPo = new Poline() //...create new data for Poline list and store into variable newPo
                    {
                        Product = poLine.Product, 
                        Quantity = 0, //sets the Quantity to 0.
                        Price = poLine.Price,
                        LineTotal = poLine.LineTotal
                    };
                sortedLines.Add(poLine.Id, newPo); //...and add the new data (newPo) and key (poLine.Id) that will be the productID from the select statement.
                   
                   //then, the new line is in the List with key = poLine.Id. and quantity = 0.
                }

            //items added to the list have Quantity = 0. The below statement accesses the Quantity of the associated key in the new list and increments it by the value of Quantity from the poLine select statment.

            sortedLines[poLine.Id].Quantity += poLine.Quantity;
            }
            //then it moves to the next item in poLine and repeats the process. The process stops once all items have been dealt with. 

                        //why is poLine.Id not named poLine.key? because it points to the primary key of products which will inherently be unique for each item in the select statment. 

                        //why is the quantity not set to 1? because the true quanity will be added at the end (from the quantity value that lives in the select statment) and 0 allows them to match. 

            po.Polines = (sortedLines).Values; //fill Po.Polines property with the sortedLines collection.
            po.PoTotal = po.Polines.Sum(x => x.LineTotal); //Calculate Po total 

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
