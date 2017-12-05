using Newtonsoft.Json;
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
using System.Web.Script.Serialization;
using ZeccaWebApplication;
using ZeccaWebApplication.Models;

namespace ZeccaWebApplication.Controllers
{
    [RoutePrefix("Devices")]
    public class DevicesController : ApiController
    {
        private asdEntities3 db = new asdEntities3();

        // GET: Devices
        [Route("")]
        public IHttpActionResult GetDispositivi()
        {
            return Json(db.Dispositivi);
        }

        // GET: Devices/{id}
        [Route("{id}")]
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

        // GET: Devices/{id}/Functions
        [Route("{id}/Functions")]
        public HttpResponseMessage GetDeviceFunctions(string id)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            DBConnection db = new DBConnection();
            response.Content = new StringContent(JsonConvert.SerializeObject(db.SelectDeviceFunctions(id)));
            return response;
        }

        // GET: Devices/{id}/Request/{idFunc}
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [Route("{id}/RequestInfos/{idFunc}")]
        public HttpResponseMessage RequestInfos (string id, [FromUri]int[] idFunc)
        {
            int sum = 0;
            HttpResponseMessage response = new HttpResponseMessage();
            DBConnection db = new DBConnection();
            foreach(int idf in idFunc)
            {
                sum = sum + idf;
            }
            response.Content = new StringContent(sum.ToString()); //new StringContent(JsonConvert.SerializeObject(db.SelectDeviceFunctions(id)));
            return response;
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