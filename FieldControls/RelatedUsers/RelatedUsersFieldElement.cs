using System;
using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields.Config;

namespace SitefinityWebApp.FieldControls.RelatedUsers
{
    /// <summary>
    /// A configuration element used to persist the properties of <see cref="RelatedUsersFieldDefinition"/>
    /// </summary>
    public class RelatedUsersFieldElement : FieldControlDefinitionElement, IRelatedUsersFieldDefinition
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="RelatedUsersFieldElement"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public RelatedUsersFieldElement(ConfigElement parent)
            : base(parent)
        {
        }
        #endregion

        #region FieldControlDefinitionElement Members
        /// <summary>
        /// Gets an instance of the <see cref="RelatedUsersFieldDefinition"/> class.
        /// </summary>
        public override DefinitionBase GetDefinition()
        {
            return new RelatedUsersFieldDefinition(this);
        }
        #endregion

        #region IFieldDefinition members
        public override Type DefaultFieldType
        {
            get
            {
                return typeof(RelatedUsersField);
            }
        }
        #endregion

        #region IRelatedUsersDefinition Members
        /// <summary>
        /// Gets or sets the user type
        /// </summary>
        [ConfigurationProperty("UserType")]
        public string UserType
        {
            get
            {
                return (string)this["UserType"];
            }
            set
            {
                this["UserType"] = value;
            }
        }
        #endregion
    }
}