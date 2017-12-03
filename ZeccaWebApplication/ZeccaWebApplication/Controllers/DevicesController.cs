using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ZeccaWebApplication.Models;

namespace ZeccaWebApplication.Controllers
{
    public class DevicesController : ApiController
    {
        private asdEntities3 db = new asdEntities3();

        // GET: api/Devices
        public IHttpActionResult GetDispositivi()
        {
            return Json(db.Dispositivi);
        }

        // GET: api/Devices/5
        [ResponseType(typeof(Dispositivi))]
        public async Task<IHttpActionResult> GetDispositivi(string id)
        {
            Dispositivi dispositivi = await db.Dispositivi.FindAsync(id);
            if (dispositivi == null)
            {
                return NotFound();
            }

            return Ok(dispositivi);
        }

        // PUT: api/Devices/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDispositivi(string id, Dispositivi dispositivi)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dispositivi.id)
            {
                return BadRequest();
            }

            db.Entry(dispositivi).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DispositiviExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Devices
        [ResponseType(typeof(Dispositivi))]
        public async Task<IHttpActionResult> PostDispositivi(Dispositivi dispositivi)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Dispositivi.Add(dispositivi);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DispositiviExists(dispositivi.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = dispositivi.id }, dispositivi);
        }

        // DELETE: api/Devices/5
        [ResponseType(typeof(Dispositivi))]
        public async Task<IHttpActionResult> DeleteDispositivi(string id)
        {
            Dispositivi dispositivi = await db.Dispositivi.FindAsync(id);
            if (dispositivi == null)
            {
                return NotFound();
            }

            db.Dispositivi.Remove(dispositivi);
            await db.SaveChangesAsync();

            return Ok(dispositivi);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DispositiviExists(string id)
        {
            return db.Dispositivi.Count(e => e.id == id) > 0;
        }
    }
}