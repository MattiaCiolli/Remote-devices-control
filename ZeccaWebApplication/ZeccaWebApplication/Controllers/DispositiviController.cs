using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZeccaWebApplication.Models;

namespace ZeccaWebApplication.Controllers
{
    public class DispositiviController : ApiController
        {
            Dispositivo[] dispositivi = new Dispositivo[]{
         new Dispositivo { ID = "1", Descrizione = "Mark", Tipo =
            1, Indirizzo = "Mark" },
         new Dispositivo { ID = "2", Descrizione = "Allan", Tipo =
            2, Indirizzo = "Allan" },
         new Dispositivo { ID = "3", Descrizione = "Johny", Tipo =
            3, Indirizzo = "Johny" }
      };

            public IHttpActionResult GetAllDispositivi()
            {
                return Json(dispositivi);
            }

            public IHttpActionResult GetDispositivo(string id)
            {
                var dispositivo = dispositivi.FirstOrDefault((p) => p.ID == id);
                if (dispositivo == null)
                {
                    return NotFound();
                }
                return Ok(dispositivo);
            }
        }
    }

