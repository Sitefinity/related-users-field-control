using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace SitefinityWebApp.FieldControls.RelatedUsers
{
    /// <summary>
    /// A simple field control used to select users.
    /// Use the path to this class when you add the field control in Sitefinity Module Builder
    /// SitefinityWebApp.FieldControls.RelatedUsers.RelatedUsersField
    /// </summary>
    [FieldDefinitionElement(typeof(RelatedUsersFieldElement))]
    public class RelatedUsersField : FieldControl
    {
        #region Properties
        protected override WebControl TitleControl
        {
            get
            {
                return this.TitleLabel;
            }
        }

        protected override WebControl DescriptionControl
        {
            get
            {
                return this.DescriptionLabel;
            }
        }

        protected override WebControl ExampleControl
        {
            get
            {
                return this.ExampleLabel;
            }
        }

        /// <summary>
        /// Obsolete. Use LayoutTemplatePath instead.
        /// </summary>
        protected override string LayoutTemplateName
        {
            get
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// Gets the layout template's relative or virtual path.
        /// </summary>
        public override string LayoutTemplatePath
        {
            get
            {
                if (string.IsNullOrEmpty(base.LayoutTemplatePath))
                    return RelatedUsersField.layoutTemplate;
                return base.LayoutTemplatePath;
            }
            set
            {
                base.LayoutTemplatePath = value;
            }
        }

        /// <summary>
        /// Gets the title label.
        /// </summary>
        /// <value>The title label.</value>
        protected internal virtual Label TitleLabel
        {
            get
            {
                SitefinityLabel titleLabel = this.Container.GetControl<SitefinityLabel>("titleLabel", true);
                return titleLabel;
            }
        }

        /// <summary>
        /// Gets the description label.
        /// </summary>
        /// <value>The description label.</value>
        protected internal virtual Label DescriptionLabel
        {
            get
            {
                SitefinityLabel descriptionLabel = this.Container.GetControl<SitefinityLabel>("descriptionLabel", true);
                return descriptionLabel;
            }
        }

        /// <summary>
        /// Gets the example label.
        /// </summary>
        /// <value>The example label.</value>
        protected internal virtual Label ExampleLabel
        {
            get
            {
                SitefinityLabel exampleLabel = this.Container.GetControl<SitefinityLabel>("exampleLabel", true);
                return exampleLabel;
            }
        }

        /// <summary>
        /// Get a reference to the content selector
        /// </summary>
        protected virtual FlatSelector ItemsSelector
        {
            get
            {
                return this.Container.GetControl<FlatSelector>("itemsSelector", true);
            }
        }

        /// <summary>
        /// Get a reference to the selected items list
        /// </summary>
        protected virtual HtmlGenericControl SelectedItemsList
        {
            get
            {
                return this.Container.GetControl<HtmlGenericControl>("selectedItemsList", true);
            }
        }

        /// <summary>
        /// Get a reference to the selector wrapper
        /// </summary>
        protected virtual HtmlGenericControl SelectorWrapper
        {
            get
            {
                return this.Container.GetControl<HtmlGenericControl>("selectorWrapper", true);
            }
        }

        /// <summary>
        /// The LinkButton for "Done"
        /// </summary>
        protected virtual LinkButton DoneButton
        {
            get
            {
                return this.Container.GetControl<LinkButton>("doneButton", true);
            }
        }

        /// <summary>
        /// The LinkButton for "Cancel"
        /// </summary>
        protected virtual LinkButton CancelButton
        {
            get
            {
                return this.Container.GetControl<LinkButton>("cancelButton", true);
            }
        }

        /// <summary>
        /// The button area control
        /// </summary>
        protected virtual Control ButtonArea
        {
            get
            {
                return this.Container.GetControl<Control>("buttonAreaPanel", false);
            }
        }

        /// <summary>
        /// Get a reference to the link that opens the selector
        /// </summary>
        protected virtual HyperLink SelectButton
        {
            get
            {
                return this.Container.GetControl<HyperLink>("selectButton", true);
            }
        }

        /// <summary>
        /// Get or set the user type 
        /// </summary>
        public string UserType
        {
            get
            {
                return this.userType;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.userType = value;
                }
            }
        }
        #endregion

        #region Overridden Methods
        protected override void InitializeControls(GenericContainer container)
        {
            this.TitleLabel.Text = this.Title;
            this.DescriptionLabel.Text = this.Description;
            this.ExampleLabel.Text = this.Example;
            this.ItemsSelector.ServiceUrl = RelatedUsersField.UsersServicePath;
            this.ItemsSelector.ItemType = this.UserType;
        }

        public override void Configure(IFieldDefinition definition)
        {
            base.Configure(definition);

            IRelatedUsersFieldDefinition fieldDefinition = definition as IRelatedUsersFieldDefinition;

            if (fieldDefinition != null)
            {
                if (!string.IsNullOrEmpty(fieldDefinition.UserType))
                {
                    this.UserType = fieldDefinition.UserType;
                }
            }
        }

        protected override ScriptRef GetRequiredCoreScripts()
        {
            return ScriptRef.JQuery |
                ScriptRef.JQueryUI |
                ScriptRef.KendoAll;
        }
        #endregion

        #region IScriptControl Members
        public override IEnumerable<ScriptReference> GetScriptReferences()
        {
            List<ScriptReference> scripts = new List<ScriptReference>(base.GetScriptReferences());


            //scripts.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.Scripts.IRequiresProvider.js", "Telerik.Sitefinity"));
            //scripts.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.ILocalizableFieldControl.js", "Telerik.Sitefinity"));
            scripts.Add(new ScriptReference(RelatedUsersField.scriptReference));
            return scripts;
        }

        public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
        {
            var descriptors = new List<ScriptDescriptor>(base.GetScriptDescriptors());
            var lastDescriptor = (ScriptControlDescriptor)descriptors.Last();
            lastDescriptor.AddElementProperty("selectButton", this.SelectButton.ClientID);
            lastDescriptor.AddComponentProperty("itemsSelector", this.ItemsSelector.ClientID);
            lastDescriptor.AddElementProperty("selectorWrapper", this.SelectorWrapper.ClientID);
            lastDescriptor.AddElementProperty("selectedItemsList", this.SelectedItemsList.ClientID);
            lastDescriptor.AddElementProperty("doneButton", this.DoneButton.ClientID);
            lastDescriptor.AddElementProperty("cancelButton", this.CancelButton.ClientID);
            lastDescriptor.AddProperty("usersServicePath", RouteHelper.ResolveUrl(RelatedUsersField.UsersServicePath, UrlResolveOptions.Rooted));
            lastDescriptor.AddProperty("userType", this.UserType);

            return descriptors;
        }
        #endregion

        #region Private Fields
        private const string UsersServicePath = "~/Sitefinity/Services/Security/Users.svc/";

        private static readonly string scriptReference = "~/FieldControls/RelatedUsers/RelatedUsersField.js";
        private static readonly string layoutTemplate = "~/FieldControls/RelatedUsers/RelatedUsersField.ascx";

        private string userType = "Telerik.Sitefinity.Security.Model.User";
        #endregion
    }
}