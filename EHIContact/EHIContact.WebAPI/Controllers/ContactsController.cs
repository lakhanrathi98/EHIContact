﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using EHIContact.Core.Contracts;
using EHIContact.Core.Models;
using EHIContact.DataAccess.InMemory;

namespace EHIContact.WebAPI.Controllers
{
    public class ContactsController : ApiController
    {
        IContactDataAccessRepository context;
        public ContactsController(IContactDataAccessRepository DataAccessRepository)
        {
            context = DataAccessRepository;
        }

        [HttpGet]
        public IHttpActionResult GetAllContacts()
        {
            try
            {
                return Ok(context.Colleciton().ToList());
            }
            catch(Exception ex)
            {
                return BadRequest("Exception : "+ex.Message);
            }           
        }

        [HttpGet]
        public IHttpActionResult GetSingleContact(int id)
        {
            try
            {
                Contact contact = context.Find(id);
                if (contact != null)
                {
                    return Ok(contact);
                }
                else
                {
                    return Content(HttpStatusCode.NotFound,"Contact with Id = " + id.ToString() + " not found");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Exception : " + ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult AddNewContact([FromBody]Contact contact)
        {
            try
            { 
                if(ModelState.IsValid)
                {
                    context.Insert(contact);
                    context.Commit();
                    return Created(new Uri(Request.RequestUri + contact.Id.ToString()), contact);
                }
                else
                {
                    //IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                    return BadRequest(ModelState);
                }
            }
            catch(Exception ex)
            {
                return BadRequest("Exception : " + ex.Message);
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteContact(int id)
        {
            try
            {
                if (context.Delete(id))
                {
                    context.Commit();
                    return Ok();
                }
                else
                {
                    return Content(HttpStatusCode.NotFound, "Contact with Id = " + id + " not found");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Exception : " + ex.Message);
            }
        }

        [HttpPut]
        public IHttpActionResult UpdateContact([FromBody]Contact contact)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (context.Update(contact))
                    {
                        context.Commit();
                        return Ok();
                    }
                    else
                    {
                        return Content(HttpStatusCode.NotFound, "Contact with Id = " + contact.Id + " not found");
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch(Exception ex)
            {
                return BadRequest("Exception : " + ex.Message);
            }
        }
        /// <summary>
        /// To InActivate contact with given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult InActivateContact(int id)
        {
            try
            {
                if (context.InActivate(id))
                {
                    context.Commit();
                    return Ok();
                }
                else
                {
                    return Content(HttpStatusCode.NotFound, "Contact with Id : " + id + " not found");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Exception : " + ex.Message);
            }
        }
    }
}
