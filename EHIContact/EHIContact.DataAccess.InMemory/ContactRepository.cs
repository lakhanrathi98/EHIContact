using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using EHIContact.Core.Models;

namespace EHIContact.DataAccess.InMemory
{
    public class ContactRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Contact> contacts;
        public ContactRepository()
        {
            contacts = cache["Contacts"] as List<Contact>;
            if (contacts == null)
                contacts = new List<Contact>();
        }

        public void Commit()
        {
            cache["Contacts"] = contacts;
        }

        public Contact Find(int Id)
        {
            Contact contact = contacts.Find(c => c.Id == Id);
            if (contact != null)
                return contact;
            throw new Exception("Contact not found");
        }

        public IQueryable<Contact> Colleciton()
        {
            return contacts.AsQueryable();
        }

        public void Insert(Contact contact)
        {
            contacts.Add(contact);
        }

        public void Update(Contact contact)
        {
            Contact contactToUpdate = contacts.Find(c => c.Id == contact.Id);
            if (contactToUpdate != null)
                contactToUpdate = contact;
            else
                throw new Exception("Contact not found");
        }

        public void Delete(int Id)
        {
            Contact contactToDelete = contacts.Find(c => c.Id == Id);
            if (contactToDelete != null)
                contacts.Remove(contactToDelete);
            else
                throw new Exception("Contact not found");
        }

        public void InActivate(int Id)
        {
            Contact contactToInActivate = contacts.Find(c => c.Id == Id);
            if (contactToInActivate != null)
                contactToInActivate.ActiveStatus = false;
            else
                throw new Exception("Contact not found");
        }
    }
}
