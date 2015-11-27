<%@ Control Language="C#" AutoEventWireup="true" %>
<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Web.UI" TagPrefix="sf" %>
<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="designers" Namespace="Telerik.Sitefinity.Web.UI.ControlDesign" %>
<sf:ResourceLinks ID="resourcesLinks" runat="server">
    <sf:ResourceFile JavaScriptLibrary="JQuery" />
    <sf:ResourceFile Name="Styles/jQuery/jquery.ui.core.css" />
    <sf:ResourceFile Name="Styles/jQuery/jquery.ui.dialog.css" />
    <sf:ResourceFile Name="Styles/jQuery/jquery.ui.theme.sitefinity.css" />
</sf:ResourceLinks>

<sf:SitefinityLabel ID="titleLabel" runat="server" CssClass="sfTxtLbl" />
<div id="selectorWrapper" runat="server">
    <sf:FlatSelector ID="itemsSelector" runat="server" 
        ItemType="Telerik.Sitefinity.Security.Model.User"
        DataKeyNames="UserID"
        AllowPaging="true"
        PageSize="10"
        AllowMultipleSelection="true"
        AllowSearching="true"
        ShowProvidersList="true"
        InclueAllProvidersOption="true"
        SearchBoxTitleText="Filter by Username"
        ShowHeader="true">
        <DataMembers>
            <sf:DataMemberInfo runat="server" Name="Username" IsExtendedSearchField="true" HeaderText='Username'>
                <strong>{{UserName}}</strong>
            </sf:DataMemberInfo>
            <sf:DataMemberInfo runat="server" Name="DisplayName" HeaderText='Name'>
                <span>{{DisplayName}}</span>
            </sf:DataMemberInfo>
        </DataMembers>
    </sf:FlatSelector>
    <asp:Panel runat="server" ID="buttonAreaPanel" class="sfButtonArea sfSelectorBtns">
        <asp:LinkButton ID="doneButton" runat="server" OnClientClick="return false;" CssClass="sfLinkBtn sfSave">
            <strong class="sfLinkBtnIn">
                <asp:Literal runat="server" Text="<%$Resources:Labels, Done %>" />
            </strong>
        </asp:LinkButton>
        <asp:Literal ID="literalOr" runat="server" Text="<%$Resources:Labels, or%>" />
            <asp:LinkButton ID="cancelButton" runat="server" CssClass="sfCancel" OnClientClick="return false;">
                <asp:Literal runat="server" Text="<%$Resources:Labels, Cancel %>" />
            </asp:LinkButton>
    </asp:Panel>
</div>

<ul id="selectedItemsList" data-template="ul-template-RelatedUsers" data-bind="source: items" runat="server" class="sfCategoriesList"></ul>
<script id="ul-template-RelatedUsers" type="text/x-kendo-template">
    <li>
        <span data-bind="text: UserName, attr: {data-id: UserID}"> </span>
        <a class="remove sfRemoveBtn">Remove</a>
    </li>
</script>
<asp:HyperLink ID="selectButton" runat="server" NavigateUrl="javascript:void(0);" CssClass="sfLinkBtn sfChange">
    <strong class="sfLinkBtnIn">Select users...</strong>
</asp:HyperLink>
<sf:SitefinityLabel ID="descriptionLabel" runat="server" WrapperTagName="div" HideIfNoText="true" CssClass="sfDescription" />
<sf:SitefinityLabel ID="exampleLabel" runat="server" WrapperTagName="div" HideIfNoText="true" CssClass="sfExample" />
