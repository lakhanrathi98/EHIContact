using System;
using System.Collections.Generic;
using System.Web.Http.Results;
using System.Net.Http;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using EHIContact.Core.Contracts;
using EHIContact.WebAPI.Controllers;
using EHIContact.Core.Models;

namespace EHIContact.WebAPI.Tests.Controllers
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class ContactsControllerTest
    {        
        [TestMethod]
        public void GetAllContactsOkResponse()
        {
            //Arrange
            var mockContactDataAccessRepoObject = new Mock<IContactDataAccessRepository>();
            ContactsController contactsController = new ContactsController(mockContactDataAccessRepoObject.Object);

            //Act
            var result = contactsController.GetAllContacts();

            //Assert
            Assert.IsTrue(result is OkNegotiatedContentResult<List<Contact>>);
        }

        [TestMethod]
        public void GetAllContactsZeroCount()
        {
            //Arrange
            var mockContactDataAccessRepoObject = new Mock<IContactDataAccessRepository>();
            ContactsController contactsController = new ContactsController(mockContactDataAccessRepoObject.Object);

            //Act
            var result = contactsController.GetAllContacts() as OkNegotiatedContentResult<List<Contact>>;

            //Assert
            Assert.AreEqual(0, result.Content.Count);
        }

        [TestMethod]
        public void AddNewContact()
        {
            //Arrange
            var mockContactDataAccessRepoObject = new Mock<IContactDataAccessRepository>();
            ContactsController contactsController = new ContactsController(mockContactDataAccessRepoObject.Object);
            contactsController.Request = new HttpRequestMessage();
            contactsController.Configuration = new HttpConfiguration();            
            contactsController.Request.RequestUri = new UriBuilder("http://localhost/").Uri;
            
            //Act
            var contact = new Contact { Id = 1, FirstName = "Jon", LastName = "Smith", Email = "test@gmail.com", PhoneNumber = "9876543210", ActiveStatus = false };
            var result = contactsController.AddNewContact(contact) as CreatedNegotiatedContentResult<Contact>;

            //Assert
            Assert.AreEqual("Jon", result.Content.FirstName);
        }

        [TestMethod]
        public void DeleteInvalidContact()
        {
            //Arrange
            var mockContactDataAccessRepoObject = new Mock<IContactDataAccessRepository>();
            ContactsController contactsController = new ContactsController(mockContactDataAccessRepoObject.Object);

            //Act
            var result = contactsController.DeleteContact(1) as NegotiatedContentResult<string>;

            //Assert
            Assert.AreEqual("Contact with Id = 1 not found", result.Content);
        }
    }
}
