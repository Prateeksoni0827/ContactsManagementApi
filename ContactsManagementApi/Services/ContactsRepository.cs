using ContactsManagementApi.Models;
using System.Text.Json;

namespace ContactsManagementApi.Services
{
    public class ContactsRepository
    {
        private readonly string _filePath = "Data/contacts.json";

        public List<Contact> GetAllContacts()
        {
            var jsonData = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Contact>>(jsonData) ?? new List<Contact>();
        }

        public Contact AddContact(Contact contact)
        {
            var contacts = GetAllContacts();
            contact.Id = contacts.Any() ? contacts.Max(c => c.Id) + 1 : 1;
            contacts.Add(contact);
            File.WriteAllText(_filePath, JsonSerializer.Serialize(contacts));
            return contact;
        }

        public Contact UpdateContact(Contact contact)
        {
            var contacts = GetAllContacts();
            var existingContact = contacts.FirstOrDefault(c => c.Id == contact.Id);
            if (existingContact == null) throw new Exception("Contact not found.");

            existingContact.FirstName = contact.FirstName;
            existingContact.LastName = contact.LastName;
            existingContact.Email = contact.Email;
            File.WriteAllText(_filePath, JsonSerializer.Serialize(contacts));
            return existingContact;
        }

        public void DeleteContact(int id)
        {
            var contacts = GetAllContacts();
            var contact = contacts.FirstOrDefault(c => c.Id == id);
            if (contact == null) throw new Exception("Contact not found.");

            contacts.Remove(contact);
            File.WriteAllText(_filePath, JsonSerializer.Serialize(contacts));
        }
    }
}
