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
using ZeccaWebAPI.Models;
using ZeccaWebApplication.Models;

namespace ZeccaWebApplication.Controllers
{
    [RoutePrefix("Auth")]
    public class AuthController : ApiController
    {
        private asdEntities5 db = new asdEntities5();

        // GET: api/Auth
        [Route("")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        public IQueryable<Utenti> GetUtentis()
        {
            return db.Utenti;
        }

        // GET: api/Auth/5
        [Route("{id}")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [ResponseType(typeof(Utenti))]
        public async Task<IHttpActionResult> GetUtenti(string id)
        {
            Utenti utenti = await db.Utenti.FindAsync(id);
            if (utenti == null)
            {
                return NotFound();
            }

            return Ok(utenti);
        }

        // GET: api/Auth/login/{id}/{pwd}
        [Route("login/{id}/{pwd}")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [ResponseType(typeof(Utenti))]
        public async Task<HttpResponseMessage> Login(string id, string pwd)
        {
            Utenti utente = await db.Utenti.FindAsync(id);
            if (utente == null)
            {
                utente = new Utenti();
                utente.Id = "Username errato";              
            }
            else
            {
                if (!utente.Password.Equals(pwd))
                {
                    utente = new Utenti();
                    utente.Id = "Password errata";
                }
            }

            HttpResponseMessage response = new HttpResponseMessage();
            response.Content = new StringContent(JsonConvert.SerializeObject(utente));
            return response;
        }
        /*
        // PUT: api/Auth/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUtenti(string id, Utenti utenti)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != utenti.Id)
            {
                return BadRequest();
            }

            db.Entry(utenti).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UtentiExists(id))
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

        // POST: api/Auth
        [ResponseType(typeof(Utenti))]
        public async Task<IHttpActionResult> PostUtenti(Utenti utenti)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Utentis.Add(utenti);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UtentiExists(utenti.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = utenti.Id }, utenti);
        }

        // DELETE: api/Auth/5
        [ResponseType(typeof(Utenti))]
        public async Task<IHttpActionResult> DeleteUtenti(string id)
        {
            Utenti utenti = await db.Utentis.FindAsync(id);
            if (utenti == null)
            {
                return NotFound();
            }

            db.Utentis.Remove(utenti);
            await db.SaveChangesAsync();

            return Ok(utenti);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UtentiExists(string id)
        {
            return db.Utentis.Count(e => e.Id == id) > 0;
        }*/
    }
    
}