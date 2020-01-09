using WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Datos;

namespace WebApi.Controllers
{
    public class UsuarioController : ApiController
    {
        private DbapiEntities db = new DbapiEntities();
        
        [HttpGet]
        public IEnumerable<usuario> Get()
        {
            using (DbapiEntities db = new DbapiEntities())
            {
                return db.usuario.ToList();
            }
        }

        [HttpGet]
        public usuario Get(int id)
        {
            using (DbapiEntities db = new DbapiEntities())
            {
                return db.usuario.FirstOrDefault(a => a.id == id);
            }
        }
        [HttpPost]
        public IHttpActionResult agregar([FromBody] usuario usuario)
            {
                if (ModelState.IsValid)
                {
                    db.usuario.Add(usuario);
                    db.SaveChanges();
                    return Ok(usuario);
                }
                else
                {
                    return BadRequest();
                }
            }


        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] usuario usuario)
        {
            var usuarios = db.usuario.Count(u => u.id == id) > 0;
            db.Entry(usuario).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return Ok();
        }
        [HttpDelete]
        public IHttpActionResult borrar(int id)
        {
            var usuario = db.usuario.Find(id);

            db.usuario.Remove(usuario);
            db.SaveChanges();
            return Ok(usuario);
        }

    }
}
