# related-users-field-control
If you ever wanted to relate Sitefinity users to one of your Dynamic Content Modules, you probably noticed that there is not an out-of-the-box way to do that. Luckily, Sitefinity is designed for extensibility, from the ground up, and this relation is quite easy to achieve.

The solution is based on a field control which lets you select one or many user which are going to be related to your dynamic content items. On the dynamic content side, the users are stored as an array of user identifiers (Guid[]). Both of these functionalities, the custom filed control and the array of GUIDs field have been supported by Sitefinity for a long time. Now we are just using their power to create a useful feature which many of you have been asking for or trying to do.
