# related-users-field-control

### This repository is not automatically upgraded to latest Sitefintiy version. The repository is monitored for pull requests and fixes. The latest official version of Sitefinity that supports this sample is 8.2. Be aware that using a higher version could cause unexpected behavior. If you successfully upgrade the example to a greater version, please share your work with the community by submitting your changes via pull request.

If you ever wanted to relate Sitefinity CMS users to one of your Dynamic Content Modules, you probably noticed that there is not an out-of-the-box way to do that. Luckily, Sitefinity CMS is designed for extensibility from the ground up, and this relation is quite easy to achieve.

The solution is based on a field control which lets you select one or many user which are going to be related to your dynamic content items. On the dynamic content side, the users are stored as an array of user identifiers (Guid[]). Both of these functionalities, the custom filed control and the array of GUIDs field have been supported by Sitefinity CMS for a long time. Now we are just using their power to create a useful feature which many of you have been asking for or trying to do.


### Requirements
The only thing you need for this field control to work is a Sitefinity CMS web application.

### Setup
1. Add the source code from this repository to your SitefinityWebApp project.
    * If you used the same folder structure as in this repository, the CLR type of the field control should be 
    ```cs
      SitefinityWebApp.FieldControls.RelatedUsers.RelatedUsersField
    ```
2. Build your solution and run your Sitefinity CMS application
3. Go to the *Module Builder* screen and add a new field to the Dynamic Module you want to extend. Fill in the new field properlies like this:
    * _Name_: by your preference
    * _Type_: Array of GUIDs
    * _This is a hidden field_: unchecked
    * _Type or Virtual path of the custom widget_: SitefinityWebApp.FieldControls.RelatedUsers.RelatedUsersField

Final setup visual:

![alt tag](https://raw.githubusercontent.com/Sitefinity-SDK/related-users-field-control/master/ReadmeResources/related-users-module-builder.PNG)

Now if you go to your content module and add/edit an item, you will see the RelatedUsers field:

![alt tag](https://raw.githubusercontent.com/Sitefinity-SDK/related-users-field-control/master/ReadmeResources/related-users-content-module-1.PNG)

![alt tag](https://raw.githubusercontent.com/Sitefinity-SDK/related-users-field-control/master/ReadmeResources/related-users-content-module-2.PNG)

### Tweaks & Extensibility 
If the current behavior of the user selector does not perfectly fit your needs, you can additionally tweak a lot of it. The user selector is based on the Sitefinity CMS `FlatSelector`, which means that all of the `FlatSelector` options can be changed. The easiest way to do this is through the template file of the user selector field `RelatedUsersField.ascx`, by changing the attributes of the `FlatSelector` control. Popular tweaks for this control are: `AllowPaging`, `PageSize`, `AllowMultipleSelection`, `AllowSearching`, `ShowProvidersList`, etc. See the following screenshot of the current configuration:

![alt tag](https://raw.githubusercontent.com/Sitefinity-SDK/related-users-field-control/master/ReadmeResources/flat-selector-settings.PNG)

Moreover, you can always further extend this field control, through its C# and JavaScript code, to better suit your needs.


