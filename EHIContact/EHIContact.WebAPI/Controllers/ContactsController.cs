using EHIContact.Core.Models;
using EHIContact.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace EHIContact.WebAPI.Controllers
{
    public class ContactsController : ApiController
    {
        ContactRepository context;
        public ContactsController()
        {
            context = new ContactRepository();
        }

        [HttpGet]
        public HttpResponseMessage GetAllContacts()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, context.Colleciton().ToList());
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }           
        }

        [HttpGet]
        public HttpResponseMessage GetSingleContact(int id)
        {
            try
            {
                Contact contact = context.Find(id);
                if (contact != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, contact);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Contact with Id = " + id.ToString() + " not found");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage AddNewContact([FromBody]Contact contact)
        {
            try
            { 
                context.Insert(contact);
                context.Commit();
                var message = Request.CreateResponse(HttpStatusCode.Created, contact);
                message.Headers.Location = new Uri(Request.RequestUri + contact.Id.ToString());
                return message;
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpDelete]
        public HttpResponseMessage DeleteContact(int id)
        {
            try
            {
                if (context.Delete(id))
                {
                    context.Commit();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Contact with Id = " + id + " not found");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPut]
        public HttpResponseMessage UpdateContact([FromBody]Contact contact)
        {
            try
            {
                if(context.Update(contact))
                {
                    context.Commit();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Contact with Id = " + contact.Id + " not found");
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        /// <summary>
        /// To InActivate contact with given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        public HttpResponseMessage InActivateContact(int id)
        {
            try
            {
                if (context.InActivate(id))
                {
                    context.Commit();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Contact with Id : " + id + " not found");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
