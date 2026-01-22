using Microsoft.AspNetCore.Mvc;
using WSConvertisseur.Models;

namespace WSConvertisseur.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevisesController : ControllerBase
    {
        
        private static readonly List<Devise> devises = new List<Devise>
        {
            new Devise(1, "Dollar", 1.08),
            new Devise(2, "Franc Suisse", 1.07),
            new Devise(3, "Yen", 120)
        };

       
        // GET: api/devises
        [HttpGet]
        public IEnumerable<Devise> GetAll()
        {
            return devises;
        }

        /// <summary>
        /// Récupère une devise spécifique par son identifiant.
        /// </summary>
        /// <param name="id">L'identifiant de la devise</param>
        /// <returns>Un objet Devise</returns>
        /// <response code="200">La devise a été trouvée</response>
        /// <response code="404">La devise n'existe pas</response>
        // GET: api/devises/5
        [HttpGet("{id}", Name = "GetDevise")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<Devise> GetById(int id)
        {
            var devise = devises.FirstOrDefault(d => d.Id == id);
            if (devise == null)
            {
                return NotFound();
            }
            return Ok(devise);
        }

        /// <summary>
        /// Ajoute une nouvelle devise à la liste.
        /// </summary>
        /// <param name="devise">L'objet devise à ajouter</param>
        /// <returns>La devise créée avec son URL d'accès</returns>
        // POST: api/devises
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult<Devise> Post([FromBody] Devise devise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            devises.Add(devise);
            // Renvoie 201 Created et l'en-tête "Location" vers GetById
            return CreatedAtRoute("GetDevise", new { id = devise.Id }, devise);
        }

        /// <summary>
        /// Met à jour une devise existante.
        /// </summary>
        /// <param name="id">L'identifiant de la devise à modifier</param>
        /// <param name="devise">Les nouvelles données de la devise</param>
        // PUT: api/devises/5
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Put(int id, [FromBody] Devise devise)
        {
            if (id != devise.Id)
            {
                return BadRequest();
            }

            var index = devises.FindIndex(d => d.Id == id);
            if (index < 0)
            {
                return NotFound();
            }

            devises[index] = devise;
            return NoContent();
        }

        /// <summary>
        /// Supprime une devise de la liste.
        /// </summary>
        /// <param name="id">L'identifiant de la devise à supprimer</param>
        // DELETE: api/devises/5
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<Devise> Delete(int id)
        {
            var devise = devises.FirstOrDefault(d => d.Id == id);
            if (devise == null)
            {
                return NotFound();
            }
            devises.Remove(devise);
            return Ok(devise);
        }
    }
}