# CodeBase
This source is code base for all app I will develop in the future 

# Structure
## 1: CodeBase.Core
   This is project use to define entities, this project use [code first](https://learn.microsoft.com/en-us/ef/ef6/modeling/code-first/workflows/new-database) so when define a class synonymous define a table in database 
## 2: CodeBase.EntityFrameworkCore
  
 This is project use to manage Context. It's represent for methods in database. [EntityFrameworkCore](https://learn.microsoft.com/en-us/aspnet/entity-framework) is framework to do that.

   Each times change entity need update migration file.
   Use: `add-migration [description_change]` and `update-database` to create migration and update new instance on database
## 3: CodeBase.Service 
  This is project use to handle logic for application
## 4: Codebase.Web.Host
  This is project contain APIs to commuticate with FrontEnd
