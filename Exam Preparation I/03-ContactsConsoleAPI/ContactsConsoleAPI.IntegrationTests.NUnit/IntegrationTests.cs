using ContactsConsoleAPI.Business;
using ContactsConsoleAPI.Business.Contracts;
using ContactsConsoleAPI.Data.Models;
using ContactsConsoleAPI.DataAccess;
using ContactsConsoleAPI.DataAccess.Contrackts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsConsoleAPI.IntegrationTests.NUnit
{
    public class IntegrationTests
    {
        private TestContactDbContext dbContext;
        private IContactManager contactManager;

        [SetUp]
        public void SetUp()
        {
            this.dbContext = new TestContactDbContext();
            this.contactManager = new ContactManager(new ContactRepository(this.dbContext));
        }


        [TearDown]
        public void TearDown()
        {
            this.dbContext.Database.EnsureDeleted();
            this.dbContext.Dispose();
        }
        private Contact CreateValidNewContact()
        {
            var random = new Random();
            var contact = new Contact()
            {
                FirstName = $"Test{random.Next(1,1000)}",
                LastName = $"Testa{random.Next(1,1000)}",
                Address = $"HomeToYou{random.Next(1,1000)}",
                Phone = "123456789012345",
                Email = $"test{DateTime.Now.Ticks}@gmail.com",
                Gender = "Female",
                Contact_ULID = $"{DateTime.Now.Ticks}"
            };
            return contact;
        }
        private Contact CreateInvalidNewContact()
        {
            var contact = new Contact()
            {
                FirstName = "Test",
                LastName = "Testa",
                Address = "HomeToYou",
                Phone = "123456789010415789",
                Email = "test@gmail.com",
                Gender = "NewModernGender",
                Contact_ULID = "133377331007"
            };
            return contact;
        }

        //positive test
        [Test]
        public async Task AddContactAsync_ShouldAddNewContact()
        {
            // Arrange
            var contact = CreateValidNewContact();

            // Act
            await contactManager.AddAsync(contact);
            var dbContact = await dbContext.Contacts.FirstOrDefaultAsync(s => s.Contact_ULID == contact.Contact_ULID);

            // Assert
            Assert.NotNull(dbContact);
            Assert.AreEqual(contact, dbContact);
        }

        //Negative test
        [Test]
        public async Task AddContactAsync_TryToAddContactWithInvalidCredentials_ShouldThrowException()
        {
            // Arrange
            var contact = CreateInvalidNewContact();
            // Act 
            var ex = Assert.ThrowsAsync<ValidationException>( async()=> await contactManager.AddAsync(contact));
            var actual = await dbContext.Contacts.FirstOrDefaultAsync(s => s.Contact_ULID == contact.Contact_ULID);
            // Assert
            Assert.IsNull(actual);
            Assert.AreEqual(ex.Message, "Invalid contact!");
        }

        [Test]
        public async Task DeleteContactAsync_WithValidULID_ShouldRemoveContactFromDb()
        {
            // Arrange
            var contact = CreateValidNewContact();
            await contactManager.AddAsync(contact);

            // Act
            await contactManager.DeleteAsync(contact.Contact_ULID);
            var dbContact = await dbContext.Contacts.FirstOrDefaultAsync(s=> s.Contact_ULID == contact.Contact_ULID);

            //Assert
            Assert.IsNull(dbContact);
        }

        [Test]
        [TestCase(" ")]
        [TestCase(null)]
        public async Task DeleteContactAsync_TryToDeleteWithNullOrWhiteSpaceULID_ShouldThrowException(string invalidULID)
        {
            // Arrange
            var contact = CreateValidNewContact();
            await contactManager.AddAsync(contact);

            // Act
            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await contactManager.DeleteAsync(invalidULID));

            //Assert
            Assert.AreEqual(ex.Message, "ULID cannot be empty.");
        }

        [Test]
        public async Task GetAllAsync_WhenContactsExist_ShouldReturnAllContacts()
        {
            // Arrange
            var contact = CreateValidNewContact();
            var contact1 = CreateValidNewContact();
            await contactManager.AddAsync(contact);
            await contactManager.AddAsync(contact1);

            // Act
            var contacts = await contactManager.GetAllAsync();

            // Assert
            Assert.AreEqual(contacts.Count(),2);
            CollectionAssert.Contains(contacts, contact);
            CollectionAssert.Contains(contacts, contact1);
        }

        [Test]
        public async Task GetAllAsync_WhenNoContactsExist_ShouldThrowKeyNotFoundException()
        {
            // Act
            var ex = Assert.ThrowsAsync<KeyNotFoundException>(async () => await contactManager.GetAllAsync());

            // Assert
            Assert.AreEqual(ex.Message, "No contact found.");
        }

        [Test]
        public async Task SearchByFirstNameAsync_WithExistingFirstName_ShouldReturnMatchingContacts()
        {
            // Arrange
            var contact = CreateValidNewContact();
            await contactManager.AddAsync(contact);
            // Act
            var contacts = await contactManager.SearchByFirstNameAsync(contact.FirstName);
            //Assert
            Assert.AreEqual(contact.FirstName,contacts.First().FirstName);
        }

        [Test]
        public async Task SearchByFirstNameAsync_WithNonExistingFirstName_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            string invalidFirstName = "Goshee";
            var contact = CreateValidNewContact();
            await contactManager.AddAsync(contact);
            // Act
           var ex = Assert.ThrowsAsync<KeyNotFoundException>(async() => await contactManager.SearchByFirstNameAsync(invalidFirstName));

            // Assert
            Assert.AreEqual(ex.Message, "No contact found with the given first name.");
        }

        [Test]
        public async Task SearchByLastNameAsync_WithExistingLastName_ShouldReturnMatchingContacts()
        {
            // Arrange
            var contact = CreateValidNewContact();
            await contactManager.AddAsync(contact);
            // Act
            var contacts = await contactManager.SearchByLastNameAsync(contact.LastName);
            //Assert
            Assert.AreEqual(contact.LastName, contacts.First().LastName);
        }

        [Test]
        public async Task SearchByLastNameAsync_WithNonExistingLastName_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            string invalidLastName = "Ivanoveca";
            var contact = CreateValidNewContact();
            await contactManager.AddAsync(contact);
            // Act
            var ex = Assert.ThrowsAsync<KeyNotFoundException>(async () => await contactManager.SearchByLastNameAsync(invalidLastName));

            // Assert
            Assert.AreEqual(ex.Message, "No contact found with the given last name.");
        }

        [Test]
        public async Task GetSpecificAsync_WithValidULID_ShouldReturnContact()
        {
            // Arrange
            var contact = CreateValidNewContact();
            await contactManager.AddAsync(contact);
            // Act
            var dbContact = await contactManager.GetSpecificAsync(contact.Contact_ULID);
            //Assert
            Assert.AreEqual(contact.Contact_ULID, dbContact.Contact_ULID);
        }

        [Test]
        public async Task GetSpecificAsync_WithInvalidULID_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            string invalidULID = "159753654852";
            var contact = CreateValidNewContact();
            await contactManager.AddAsync(contact);
            // Act
            var ex = Assert.ThrowsAsync<KeyNotFoundException>(async () => await contactManager.GetSpecificAsync(invalidULID));

            // Assert
            Assert.AreEqual(ex.Message, $"No contact found with ULID: {invalidULID}");
        }

        [Test]
        public async Task UpdateAsync_WithValidContact_ShouldUpdateContact()
        {
            // Arrange
            var contact = CreateValidNewContact();
            await contactManager.AddAsync(contact);
            var contact1 = CreateValidNewContact();
            // Act
            await contactManager.UpdateAsync(contact1);
            var dbContact = await dbContext.Contacts.FirstOrDefaultAsync( s=> s.Contact_ULID == contact1.Contact_ULID );
            // Assert
            Assert.AreEqual(dbContact, contact1);
        }

        [Test]
        public async Task UpdateAsync_WithInvalidContact_ShouldThrowValidationException()
        {
            // Arrange
            var contact = CreateValidNewContact();
            await contactManager.AddAsync(contact);
            // Act
            var ex = Assert.ThrowsAsync<ValidationException>(async () => await contactManager.UpdateAsync(new Contact()));

            // Assert
            Assert.AreEqual(ex.Message, "Invalid contact!");
        }
    }
}
