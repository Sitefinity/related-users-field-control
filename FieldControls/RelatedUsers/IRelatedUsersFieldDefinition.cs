using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace SitefinityWebApp.FieldControls.RelatedUsers
{
    public interface IRelatedUsersFieldDefinition : IFieldControlDefinition
    {
        /// <summary>
        /// Gets or sets the user type.
        /// </summary>
        /// <value>The user type.</value>
        string UserType { get; set; }
    }
}