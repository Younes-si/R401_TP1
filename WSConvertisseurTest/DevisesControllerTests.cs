using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WSConvertisseur.Controllers;
using WSConvertisseur.Models;

namespace WSConvertisseurTests
{
    [TestClass]
    public class DevisesControllerTests
    {
        private DevisesController _controller;

        
        [TestInitialize]
        public void InitialisationDesTests()
        {
            _controller = new DevisesController();
        }

        // GetById

        [TestMethod]
        public void GetById_ExistingIdPassed_ReturnsRightItem()
        {
            // Arrange
            DevisesController controller = new DevisesController();
            // Act
            var result = controller.GetById(1);
            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Devise>), "Pas un ActionResult"); // Test du type de retour
            Assert.IsNull(result.Result, "Erreur est pas null"); //Test de l'erreur
            Assert.IsInstanceOfType(result.Value, typeof(Devise), "Pas une Devise"); // Test du type du contenu (valeur) du retour
            Assert.AreEqual(new Devise(1, "Dollar", 1.08), (Devise?)result.Value, "Devises pas identiques"); //Test de la devise récupérée

        }

        [TestMethod]
        public void GetById_UnknownIdPassed_ReturnsNotFoundResult()
        {
            // Act
            var result = _controller.GetById(100);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Devise>), "Pas un ActionResult");
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult), "Pas un NotFoundResult");
            Assert.IsNull(result.Value, "Value n'est pas null");
            Assert.AreEqual(((NotFoundResult)result.Result).StatusCode, StatusCodes.Status404NotFound, "Pas 404");
        }

        // GetAll

        [TestMethod]
        public void GetAll_ReturnsAllItems()
        {
            // Act
            var result = _controller.GetAll();

            // Assert 
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Devise>), "Pas un IEnumerable");
            List<Devise> maListe = result.ToList();
            Assert.AreEqual(3, maListe.Count, "Pas 3 devises");
        }

        // Post

        [TestMethod]
        public void Post_ValidDevise_ReturnsCreatedAtRoute()
        {
            // Arrange
            Devise d = new Devise(4, "Bitcoin", 0.00002);

            // Act
            var result = _controller.Post(d);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Devise>), "Pas un ActionResult");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtRouteResult), "Pas un CreatedAtRouteResult");

            CreatedAtRouteResult routeResult = (CreatedAtRouteResult)result.Result;
            Assert.AreEqual(StatusCodes.Status201Created, routeResult.StatusCode, "Pas 201");
            Assert.AreEqual(d, (Devise)routeResult.Value, "Devise créée différente");
        }

        // Put

        [TestMethod]
        public void Put_IdMismatch_ReturnsBadRequest()
        {
            var d = new Devise(2, "Euro", 1.0);
            var result = _controller.Put(1, d); 
            Assert.IsInstanceOfType(result, typeof(BadRequestResult), "Pas un BadRequest");
        }

        [TestMethod]
        public void Put_UnknownId_ReturnsNotFound()
        {
            var d = new Devise(100, "Inconnue", 1.0);
            var result = _controller.Put(100, d);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult), "Pas un NotFound");
        }

        [TestMethod]
        public void Put_ValidDevise_ReturnsNoContent()
        {
            var d = new Devise(1, "Dollar", 1.10);
            var result = _controller.Put(1, d);
            Assert.IsInstanceOfType(result, typeof(NoContentResult), "Pas un NoContent");
        }

        // Delete

        [TestMethod]
        public void Delete_UnknownId_ReturnsNotFound()
        {
            //identifiant qui n'existe pas.
            var result = _controller.Delete(100);
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult), "Pas un NotFound");
        }

        [TestMethod]
        public void Delete_ExistingId_ReturnsOkAndDeletedDevise()
        {

            
            var result = _controller.Delete(3);
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "Pas un OkObjectResult");


            //cast pour lire l'info
            var okResult = (OkObjectResult)result.Result;
            Assert.AreEqual(3, ((Devise)okResult.Value).Id, "Id supprimé incorrect");
        }
    }
}