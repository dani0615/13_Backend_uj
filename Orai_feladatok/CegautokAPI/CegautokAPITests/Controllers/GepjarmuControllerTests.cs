using Microsoft.VisualStudio.TestTools.UnitTesting;
using CegautokAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CegautokAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using CegautokAPI.DTOs;

namespace CegautokAPI.Controllers.Tests
{
    [TestClass()]
    public class GepjarmuControllerTests
    {


        private FlottaContext _context;
        private GepjarmuController _controller;

        //Arrange
        [TestInitialize()]
        public void Init()
        {
            var options = new DbContextOptionsBuilder<FlottaContext>().
                UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            _context = new FlottaContext(options);

            //Tesztadatok
            var gepjarmu = new Gepjarmu()
            {
                Id = 1,
                Rendszam = "ABC-123",
                Marka = "Toyota",
                Tipus = "Corolla",
                Ulesek = 5,
                Kikuldottjarmus = null
            };

            var gepjarmu2 = new Gepjarmu()
            {
                Id = 2,
                Rendszam = "BCD-234",
                Marka = "BMW",
                Tipus = "M2",
                Ulesek = 2,
                Kikuldottjarmus = null
            };

            var sofor1 = new User
            {
                Id = 1,
                Name = "Kiss Pista",
                LoginName = "kisspista",
                Active = true,
                Address = "Budapest, Fő utca 1.",
                Email = "kp@email.com",
                Hash = "hash",
                Salt = "salt",
                Phone = "123456789",
                Image = "image.jpg",
                Permission = 1
            };

            var sofor2 = new User
            {
                Id = 2,
                Name = "Nagy Pista",
                LoginName = "nagypista",
                Active = true,
                Address = "Miskolc, Fő utca 1.",
                Email = "np@email.com",
                Hash = "hash1",
                Salt = "salt1",
                Phone = "123456789",
                Image = "nagyP.jpg",
                Permission = 2
            };

            _context.AddRange(gepjarmu, gepjarmu2);
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            _controller = new GepjarmuController(_context);

        }

        [TestMethod()]
        public void GetSoforTest()
        {
            var result = _controller.GetSofor();

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);

            //AZ adatok ellenőrzése
            var data = okResult.Value as List<SoforDTO>;
            Assert.IsNotNull(data);
            Assert.AreEqual(2, data.Count);

            // adattartalom ellenőrzése
            var first = data[0];
            Assert.AreEqual("Kiss Pista", first.SoforNev);
            Assert.AreEqual("ABC-123", first.Rendszam);
            Assert.AreEqual(1, first.Darab);

            var second = data[1];
            Assert.AreEqual("Nagy Pista", second.SoforNev);
            Assert.AreEqual("BCD-234", second.Rendszam);
            Assert.AreEqual(1, second.Darab);
        }


        [TestMethod()]
        public void GetAllTest()
        {
            //Arrange
            //Act
            var result = _controller.GetAll();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.IsInstanceOfType<List<Gepjarmu>>(okResult.Value);
            List<Gepjarmu> eredmenylista = okResult.Value as List<Gepjarmu>;
            Assert.IsNotNull(eredmenylista);
            Assert.AreEqual(2, eredmenylista.Count);
            Assert.AreEqual("ABC-123", eredmenylista[0].Rendszam);
        }

        [TestMethod]
        public void GetbyIdTest()
        {
            // Arrange
            int existingId = 1;
            int nonExistingId = 911;

            // Act
            var existingResult = _controller.GetbyId(existingId);
            var nonExistingResult = _controller.GetbyId(nonExistingId);

            // Assert
            Assert.IsNotNull(existingResult);
            Assert.IsNotNull(nonExistingResult);

            Assert.IsInstanceOfType<OkObjectResult>(existingResult);
            Assert.IsInstanceOfType<NotFoundObjectResult>(nonExistingResult);

            var okResult = (OkObjectResult)existingResult;
            var notFoundResult = (NotFoundObjectResult)nonExistingResult;


            Assert.IsInstanceOfType<Gepjarmu>(okResult.Value);
            var returnedGepjarmu = (Gepjarmu)okResult.Value;
            Assert.AreEqual("ABC-123", returnedGepjarmu.Rendszam);


            Assert.IsInstanceOfType<string>(notFoundResult.Value);

            string expectedMessage;


            expectedMessage = "Nincs ilyen ID-jú gépjármű:";



            var actualMessage = (string)notFoundResult.Value;
            Assert.AreEqual(expectedMessage, actualMessage);
        }

        [TestMethod()]
        public void NewJarmuTest()
        {
            var gepjarmu3 = new Gepjarmu()
            {
                Id = 3,
                Rendszam = "VSOK-01",
                Marka = "Suzuki",
                Tipus = "Ignis",
                Ulesek = 5,
                Kikuldottjarmus = null
            };

            var result = _controller.NewJarmu(gepjarmu3);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<ObjectResult>(result);
            var okResult = result as ObjectResult;
            Assert.IsNotNull(okResult);
        }

        [TestMethod]
        public void UpdateJarmu_LetezoJarmu_AdataiFrissulnekEsOkStringVisszajon()
        {
            // Arrange
            var jarmuToUpdate = new Gepjarmu
            {
                Id = 1,
                Rendszam = "ABC-999",
                Marka = "Toyota Updated",
                Tipus = "Corolla Hybrid",
                Ulesek = 5,
                Kikuldottjarmus = null
            };

            // Act
            var result = _controller.UpdateJarmu(jarmuToUpdate);

            // Assert – válasz típus és tartalom
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<OkObjectResult>(result);

            var okResult = (OkObjectResult)result;
            Assert.IsInstanceOfType<string>(okResult.Value);

            string actualMessage = (string)okResult.Value;
            Assert.AreEqual("Gépjármű sikeresen frissítve.", actualMessage);

            // Ellenőrizzük, hogy az adatbázisban tényleg frissült
            var updatedInDb = _context.Gepjarmus.Find(1);
            Assert.IsNotNull(updatedInDb);
            Assert.AreEqual("ABC-999", updatedInDb.Rendszam);
            Assert.AreEqual("Toyota Updated", updatedInDb.Marka);
            Assert.AreEqual("Corolla Hybrid", updatedInDb.Tipus);
            Assert.AreEqual(5, updatedInDb.Ulesek);
        }

        [TestMethod]
        public void UpdateJarmu_NemLetezoIdval_NotFoundStringVisszajon()
        {
            // Arrange
            var nemLetezoJarmu = new Gepjarmu
            {
                Id = 999,
                Rendszam = "XXX-999",
                Marka = "NemLétező",
                Tipus = "Valami",
                Ulesek = 4,
                Kikuldottjarmus = null
            };

            // Act
            var result = _controller.UpdateJarmu(nemLetezoJarmu);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<NotFoundObjectResult>(result);

            var notFoundResult = (NotFoundObjectResult)result;
            Assert.IsInstanceOfType<string>(notFoundResult.Value);

            string actualMessage = (string)notFoundResult.Value;
            Assert.AreEqual("Jármű nem található", actualMessage);

           
            Assert.IsNull(_context.Gepjarmus.Find(999));
        }

        [TestMethod()]

        public void DeleteJarmuTest() 
        {
            //Arrange
            int existingId = 2;
            int nonExistingId = 999;

            //Act
            var resultExisting = _controller.DeleteJarmu(existingId);
            var resultNonExisting = _controller.DeleteJarmu(nonExistingId);

            //Assert
            Assert.IsNotNull(resultExisting);
            Assert.IsNotNull(resultNonExisting);

            Assert.IsInstanceOfType<OkObjectResult>(resultExisting);
            Assert.IsInstanceOfType<NotFoundObjectResult>(resultNonExisting);

            Assert.AreEqual("Gépjármű sikeresen törölve.", (resultExisting as OkObjectResult).Value);
            Assert.AreEqual("Nincs ilyen ID-jú gépjármű", (resultNonExisting as NotFoundObjectResult).Value);

        }


    }
}
