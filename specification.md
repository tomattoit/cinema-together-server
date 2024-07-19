# Technical Specification for the Movie Watching Group App

## Main Features

### Registration and Authentication
1. New user registration:
   - Using email, unique username and password.
   - Social media login (Google, Facebook, etc.).
3. Login for registered users.
4. Password recovery.

### User Profile
1. Personal information:
   - Name.
   - Username.
   - Age.
   - Gender.
   - Location. **?**
2. Setting preferences:
   - Favorite movie genres.
   - Favorite directors.
   - Preferred days and times for watching movies.
   - Preferred cinemas or meetup locations.
3. History of watched movies and left reviews.
4. Settings:
   - Toggle displaying the history of watched movies and reviews to other users in the profile.
   - Choose who can send DMs: any users, friends only.
   - Make the account private.
   - Delete the account.
5. Searching for users:
   - By username.
6. Viewing another user profile.
7. Adding to the friends list.
8. Sending a message (see the "Messaging" section).

### Searching and Creating Groups
1. Searching for groups:
   - By group name.
   - By movie genres.
   - By date and time of sessions.
   - By location.
3. Creating a group:
   - Specifying the group name.
   - Specifying a description (optionally).
   - Selecting the group type (public/private):
        - A user can find a public group via search and send a join request.
        - A user can join a private group only if they receive an invitation. 
   - Specifying limitations on the number of participants.
   - Specifying group preferred genres (optionally).
   - Specifying group preferred times of sessions (optionally).
4. Managing a group:
   - The group owner can change any information about the group that was specified on creation.
   - The group owner can ban a user. This removes the user from the group and prevents them from joining the group until unbanned. The ban period is specified by the owner.
   - The group owner can delete a group.
4. Creating an event in a group:
   - Selecting a movie.
   - Setting the date and time of the event.
   - Specifying the meetup place (on maps).
5. Group chat (see the "Messaging" section):
   - General chat.
   - Specific themed chats **(like a chat for each watched movie in the group AFAIU)**
6. Joining a group:
   - Accepting/declining a private group invitation.
7. Leaving a group.
     
### Organizing Meetups
1. Users can start a poll to vote for a movie to watch next.
2. Users can start a poll to vote for a date, time, and place of a meetup.

**How will date, time and place be selected if they all vote differently?** 

3. Notifications to group members about upcoming meetups.

### Messaging
1. users can send messages...
2. ???
3. 

### Movie Recomendations
1. Recommendations are based on the user's and friends' preferences and do not include movies for the user's watch history. 
2. Information about movies:
   - Title.
   - Year.
   - Description.
   - Director.
   - Genres.
   - Rating.
   - Trailers.
   - Reviews from other users.
3. On a movie page a user can leave a review and rate the movie in general and on specific parameters (plot, acting, special effects, etc.). The rating is in range 1..10.
4. On a movie page a user can edit their review. 


### Additional Features
1. Calendar integration for adding events. **wdym**
2. Reminders about upcoming events.
3. Ability to share meetups and movies on social media **?**.

### Security
1. Encryption of user data.
2. Protection against XSS and CSRF attacks.
3. Regular updates of dependencies and components.

### Documentation
1. User documentation:
   - User guide.
   - Frequently Asked Questions (FAQ).
2. Technical documentation:
   - API documentation.
   - System architecture.
   - Installation and deployment guide.
