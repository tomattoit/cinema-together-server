# Technical Specification for the Movie Watching Group App

## Main Features

### Registration and Authentication
1. New user registration:
   - Using email, unique username and password.
   - Social media login (Google, Facebook, etc.).
3. Login for registered users + 2FA using a code on email.
4. Password recovery.

### User Profile
1. Personal information:
   - Name.
   - Username.
   - Age.
   - Gender.
   - Location. (Country and city selector)
   - Profile picture.
2. Setting preferences:
   - Favorite movie genres.
   - Favorite directors.
   - Preferred days and times for watching movies.
   - Preferred cinemas.
3. History of watched movies and left reviews.
4. Settings: 
   - Enable/disable 2FA. Enabled by default.
   - Toggle displaying the history of watched movies and reviews to other users in the profile.
   - Choose who can send DMs: any users, friends only.
   - Make the account private (only friends can view your account).
   - Delete the account.
   - Disable all notifications for different time periods

### User functionality
1. Searching for users:
   - By username.
   - By name.
2. Viewing another user profile.
3. Adding to the friends list.
4. Sending a message (see the "Messaging" section).
5. Editing blacklist


### Groups
1. Searching for groups:
   - By group name.
   - By movie genres.
   - By date and time of sessions.
   - By location.
3. Creating a group:
   - Explicitly specifying the group location (city).
   - Specifying the group name.
   - Specifying the group picture.
   - Specifying a description (optionally).
   - Selecting the group type (public/private):
        - A user can find a public group via search and send a join request.
        - A user can join a private group only if they receive an invitation.
   - Specifying limitations on the number of participants.
   - Specifying group preferred genres (optionally).
   - Specifying group preferred times of sessions (optionally).
4. Managing a group:
   - The group owner can change any information about the group that was specified on creation.
   - The group owner can change time when notifications about upcoming events are sent.
   - The group owner can ban a user for a specific period. This removes the user from the group and prevents them from joining the group until unbanned.
   - The group owner can delete a group.
   - The group owner can edit users' permissions for:
      - Inviting other users (for private groups). Default: no.
      - Accepting join requests (for public groups). Default: no.
      - Cancelling selected choices in polls. Default: no.
      - Creating polls. Default: yes.
      - Banning users. Default: no.
      - Deleting other users' messages. Default: no.
      - Editing the group information (except for the group type). Default: no.
   - The owner can make another user an owner with the ability to take this privilege back.
4. Creating an event in a group:
   - Selecting a movie.
   - Setting the date and time of the event.
   - Specifying the meetup place (on maps).
5. Group chat (see the "Messaging" section).
6. Joining a group:
   - Accepting/declining a private group invitation.
7. Leaving a group.
8. Muting a specific group for different periods of time.
9. Rating another users in a group
     
### Organizing Meetups
1. Users can start a poll to vote for a movie to watch next.
2. Users can start a poll to vote for a date, time, and place of a meetup.

#### Votings
1. Polls are created by owner.
2. Polls can be closed at any time by owner.
3. When poll is closed, if there is an option with majority of votes it is selected automatically, otherwise the final choice is up
to owner.
4. Only one poll of each type at a time is allowed.
5. After poll is closed notifications to group members are sent.

### Messaging
1. There are two types of chats: group and personal
2. User can send a message to a chat
3. Pictures can be sent
4. Links can be sent
5. Links can be opened by clicking on them
6. Message can be edited, deleted (for sender or for everybody), copied, pinned
7. Owner can delete any message for all members, others can delete only their messages

### Movie Recomendations
1. Recommendations are based on the user's and friends' preferences and do not include movies from the user's watch history. 
2. Information about movies:
   - Title.
   - Year.
   - Description.
   - Director.
   - Genres.
   - Rating.
   - Trailers.
   - Cast.
   - Reviews from other users.
3. On a movie page a user can leave a review and rate the movie. The rating is in range 1..10.
4. On a movie page a user can edit their review. 

### Administration features
1. Managing movies
2. Managing reviews

### Documentation
1. User documentation:
   - FAQ.
   - Support: a chat with admins on a specified topic.
2. Technical documentation:
   - API documentation.
   - Installation and deployment guide.
