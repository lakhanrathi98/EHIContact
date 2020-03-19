using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using EHIContact.Core.Models;
using EHIContact.Core.Contracts;

namespace EHIContact.DataAccess.InMemory
{
    public class ContactInMemoryRepository : IContactDataAccessRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Contact> contacts;
        public ContactInMemoryRepository()
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
            return contacts.Find(c => c.Id == Id);
        }

        public IQueryable<Contact> Colleciton()
        {
            return contacts.AsQueryable();
        }

        public void Insert(Contact contact)
        {
            contacts.Add(contact);
        }

        public bool Update(Contact contact)
        {
            Contact contactToUpdate = contacts.Find(c => c.Id == contact.Id);
            if (contactToUpdate != null)
            {
                contactToUpdate = contact;
                return true;
            }
            else
                return false;
        }

        public bool Delete(int Id)
        {
            Contact contactToDelete = contacts.Find(c => c.Id == Id);
            if (contactToDelete != null)
            {
                contacts.Remove(contactToDelete);
                return true;
            }
            else
                return false;
        }

        public bool InActivate(int Id)
        {
            Contact contactToInActivate = contacts.Find(c => c.Id == Id);
            if (contactToInActivate != null)
            {
                contactToInActivate.ActiveStatus = false;
                return true;
            }
            else
                return false;
        }
    }
}
