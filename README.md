# EHIContact
EHI Contact project repository

<b>EHIContact Solution</b>

  <b>1.	EHIContact.WebAPI:</b> This is the startup project with Web API code

        a.	App_Start folder:

          i.	Includes all configuration files 

          ii.	UnityConfig file cretated by Unity.WebAPI framework for Dependency Inversion
          
        b.	Controllers folder:

          i.	ContactsController.cs file (ApiController) : Includes all Contact APIs
          
  <b>2. EHIContact.Core : </b>This project includes required models and contracts(interfaces)

  <b>3. EHIContact.DataAccess.InMemory : </b>Project to create in memory data access layer.This layer is used before directly working with actual database
  
  <b>4. EHIContact.DataAccess.SQL : </b>Prject to perform contact CRUD oprations with SQL database using Entity Framework Code First approach
  
  <b>5. EHIContact.WebAPI.Test : </b>Project to perform Unit testing using MS tests and Moq framework
  
 <b>Steps to Run The Project :</b>
  
  1. Change connection strings as per DB setup in Web.config of WebAPI project and App.config of DataAccess.SQL project
  2. Run the project
  3. Use postman or fiddler to test the APIs
  4. Base Uri : /api/contacts/apiname/{optional parameter}
  5. API to get all contacts (GET) : api/contacts/GetAllContacts
  6. API to get single contact (GET) : api/contacts/GetSingleContact/id
  7. API to add new contact (POST) : api/contacts/AddNewContact
  8. API to delete contact (DELETE) : api/contacts/DeleteContact/id
  9. API to update contact (PUT)  : api/contacts/UpdateContact  
  10. API to InActivate contact (PUT) : api/contacts/InActivateContact/id
  
  
  
  
  <b>Setup Databse :</b>
  
  Option 1. Create database on SQL server mentioned in connection string and then Run SQL migration script "DBMigrationScript.sql" present in DBMigrationScript folder of EHIContact.DataAccess.SQL project
  
  Option 2. Run update-database command in package manager console 

  <b>Single Page Application: <b>
  
  I have created basic SPA to demonstrate calling Web APIs. I have used jQuery, DataTable plugin to develop this SPA.
  HomeController.cs and Index.cshtml are the files related to this application.Just run EHIContact.WebAPI project to check this  application
