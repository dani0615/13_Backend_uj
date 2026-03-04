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

namespace CegautokAPI.Controllers.Tests
{
    [TestClass()]
    public class LoginControllerTests
    {
        private FlottaContext _context;
        private LoginController _controller;
        private Jwtsettings _jwtSettings;

        [TestInitialize()]
        public void Init()
        {
            var options = new DbContextOptionsBuilder<FlottaContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
                .Options;

            _context = new FlottaContext(options);

            _jwtSettings = new Jwtsettings
            {
                SecretKey = "token_titkos_kulcs_teszteleshez_legalabb_32_karakter",
                Issuer = "test_issuer",
                Audience = "test_audience",
                ExpiryMinutes = 60
            };

           
            var user = new User
            {
                Id = 1,
                LoginName = "testuser",
                Salt = "teszt_salt_ertek",
                Name = "Teszt Elek",
                Active = true,
                Address = "Teszt utca 1.",
                Email = "teszt@teszt.hu",
                Hash = "teszt_hash",
                Permission = 1,
                Image = "teszt.png"
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            _controller = new LoginController(_jwtSettings, _context);
        }

        [TestMethod()]
        public void GetSalt_LetezoFelhasznalo()
        {
            // Act
            var result = _controller.GetSalt("testuser");

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult, "OkObjectResult-ot vártunk");
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual("teszt_salt_ertek", okResult.Value);
        }

        [TestMethod()]
        public void GetSalt_NemLetezoFelhasznalo()
        {
            // Act
            var result = _controller.GetSalt("nincsenilyenuser");

            // Assert
            var notFoundResult = result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult, "NotFoundObjectResult-ot vártunk");
            Assert.AreEqual(404, notFoundResult.StatusCode);
            Assert.AreEqual("Nincs ilyen felhasználó.", notFoundResult.Value);
        }
    }
}