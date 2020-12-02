# MusicManager
## Project Overview 
The aim of this project is to create a 3 tier application that will allow the user to easily access and search through music tablature. Users with accounts will be able to upload tabs, rate tabs and add tabs to their favourites.

## Project Goals
The final product will have:
A WPF front end
An SQL database backend with at least two linked tables.
The relationship between the backend object model and database managed by entity framework.
A Business Layer with some logic - not just a simple CRUD application
Unit tests, which exercise the normal functionality, boundary and error conditions.

## Sprint Breakdowns
### Sprint 1
#### Sprint Review
By the end of Sprint 1 I had aimed to have my database completed and populated with faux data, some nUnit tests created and their methods extracted and a very simplistic GUI. I underestimated the length of time it would take to structure the database so by the end of the sprint I hadn't manage to populate the database, however the database had been created, some CRUD functions had been extracted from the nUnit tests and the GUI would possibly list the tab list if the tab list was populated.

#### Sprint Retrospective
Heading forward, I will base the Sprint backlog on the time I estimate it to take instead of what tasks I want to complete.
#### Goals
- [ ] User Story 0.1
- [x] User Story 0.2
- [ ] User Story 0.3
- [x] Update readme.md - project goals
- [x] Git init commit

#### Sprint Start
![](Sprint1Start.png)

##### Sprint End
![](Sprint1End.png)

### Sprint 2
#### Sprint Review
At the end of Sprint 2 I had successfully populated my database with faux data. The ability for a user to create an account was added, with their data being inserted into the database by the back end. Conditions were added to prevent users with identical usernames from being added. Additional nUnit tests were created to test the identical username prevention conditions. The feature that unlocks the dead-buttons favourites, account and upload wasn't added as I realised it is heavily dependent on another feature I have yet to add, the ability to login! 

#### Sprint Retrospective
This Sprint went a lot smoother than Sprint 1, however it came apparent that some of my user stories were extremely interlinked and dependent on eachother, so from now on I should be more cautious in regards to task dependencies when planning the Spring Backlog. In terms of time management I believe I estimated the length of my tasks well and so will not have much runover into the next day.
####
- [x] User Story 0.1
- [x] User Story 0.1.1
- [x] User Story 0.3
- [x] User Story 1.1
- [x] User Story 1.2
- [ ] User Story 0.4
- [x] Update readme.md - project goals
- [x] Git commit

#### Sprint Start
![](Sprint2Start.png)


#### Sprint End
![](Sprint2End.png)
