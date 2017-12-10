using Newtonsoft.Json;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ZeccaWebAPI.Models;
using ZeccaWebAPI.Services;

namespace ZeccaWebAPI.Controllers
{
    [RoutePrefix("Devices")]
    public class DevicesController : ApiController
    {
        private QueueThreadService qts = new QueueThreadService();
        private DBConnection db = new DBConnection();
        private static readonly object sharedResultLock = new object();
        private static string sharedResult;

        public static string SharedResult
        {
            get
            {
                return sharedResult;
            }

            set
            {
                sharedResult = value;
            }
        }

        public static object SharedResultLock
        {
            get
            {
                return sharedResultLock;
            }
        }

        // GET: Devices
        [Route("")]
        public IHttpActionResult GetDispositivi()
        {
            return Json(db.GetAllDevices());
        }

        // GET: Devices/{id}
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [Route("{id}")]
        [ResponseType(typeof(Dispositivi))]
        public IHttpActionResult FindDeviceById(string id)
        {
            Dispositivi dispositivo = db.FindDeviceById(id);
            if (dispositivo == null)
            {
                return NotFound();
            }

            return Json(dispositivo);
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

        // GET: Devices/{id}/Request/?idFunc=1&idFunc=2}
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [Route("{id}/RequestInfos/")]
        public HttpResponseMessage RequestInfos(string id, [FromUri]int[] idFunc)
        {
            string result = null;
            
            HttpResponseMessage response = new HttpResponseMessage();
            foreach (int idf in idFunc)
            {
               Request_ThreadCollection waitThread = qts.handleRequest(id, idf);
               waitThread.Thread.Join();
                lock(sharedResultLock)
                {
                    result = sharedResult;
                }
            }

        response.Content = new StringContent(result);
            return response;
        }
    /*
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
            }*/
}
}