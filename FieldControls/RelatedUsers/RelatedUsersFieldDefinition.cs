using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Fields.Definitions;

namespace SitefinityWebApp.FieldControls.RelatedUsers
{
    /// <summary>
    /// A control definition for the simple image field
    /// </summary>
    public class RelatedUsersFieldDefinition : FieldControlDefinition, IRelatedUsersFieldDefinition
    {
        #region Constuctors
        /// <summary>
        /// Initializes a new instance of the <see cref="RelatedUsersFieldDefinition"/> class.
        /// </summary>
        public RelatedUsersFieldDefinition()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RelatedUsersFieldDefinition"/> class.
        /// </summary>
        /// <param name="element">The configuration element used to persist the control definition.</param>
        public RelatedUsersFieldDefinition(ConfigElement element)
            : base(element)
        {
        }
        #endregion

        #region IRelatedUsersDefinition members
        public string UserType
        {
            get
            {
                return this.ResolveProperty("UserType", this.userType);
            }
            set
            {
                this.userType = value;
            }
        }
        #endregion

        #region Private members
        private string userType;
        #endregion
    }
}