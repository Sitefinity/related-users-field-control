Type.registerNamespace("SitefinityWebApp.FieldControls.RelatedUsers");

SitefinityWebApp.FieldControls.RelatedUsers.RelatedUsersField = function (element) {

    SitefinityWebApp.FieldControls.RelatedUsers.RelatedUsersField.initializeBase(this, [element]);
    this._selectButton = null;
    this._doneButton = null;
    this._cancelButton = null;
    this._itemsSelector = null;
    this._selectorWrapper = null;
    this._selectedItemsList = null;
    this._selectDialog = null;
    this._usersServicePath = null;
    this._userType = null;

    /*empty kendo observable array*/
    this._selectedItems = kendo.observable({items:[]});

    this._selectButtonClickedDelegate = null;
    this._getSelectedItemsSuccessDelegate = null;
    this._removeSelectedItemDelegate = null;
    this._doneButtonClickedDelegate = null;
    this._cancelButtonClickedDelegate = null;
	this._binderDataBindingDelegate = null;
    this._onloadDelegate = null;
}

SitefinityWebApp.FieldControls.RelatedUsers.RelatedUsersField.prototype =
{
    initialize: function () {
        SitefinityWebApp.FieldControls.RelatedUsers.RelatedUsersField.callBaseMethod(this, "initialize");

        if (this._selectButton) {
            this._selectButtonClickedDelegate = Function.createDelegate(this, this._selectButtonClicked);
            $addHandler(this._selectButton, "click", this._selectButtonClickedDelegate);
        }

        if (this._doneButton) {
            this._doneButtonClickedDelegate = Function.createDelegate(this, this._doneButtonClicked);
            $addHandler(this._doneButton, "click", this._doneButtonClickedDelegate);
        }

        if (this._cancelButton) {
            this._cancelButtonClickedDelegate = Function.createDelegate(this, this._cancelButtonClicked);
            $addHandler(this._cancelButton, "click", this._cancelButtonClickedDelegate);
        }

        this._getSelectedItemsSuccessDelegate = Function.createDelegate(this, this._getSelectedItemsSuccess);

        this._removeSelectedItemDelegate = Function.createDelegate(this, this._removeSelectedItem);
        jQuery(this.get_element()).find(this.get_selectedItemsList()).delegate('.remove', 'click', this._removeSelectedItemDelegate);
        
		this._binderDataBindingDelegate = Function.createDelegate(this, this._binderDataBindingHandler);
        this._itemsSelector.add_binderDataBinding(this._binderDataBindingDelegate);
		
		this._onloadDelegate = Function.createDelegate(this, this.onload);
		Sys.Application.add_load(this._onloadDelegate);
		
        if (this.get_itemsSelector()) {
            this._selectDialog = jQuery(this.get_selectorWrapper()).dialog({
                autoOpen: false,
                modal: true,
                width: 410,
                height: "auto",
                closeOnEscape: true,
                resizable: false,
                draggable: false,
                zIndex: 5000,
                dialogClass: "sfSelectorDialog"
            });
        }
        /*bind the empty observable array to the list which is going to display it*/
        kendo.bind($(this.get_selectedItemsList()), this._selectedItems);
    },

    dispose: function () {
        SitefinityWebApp.FieldControls.RelatedUsers.RelatedUsersField.callBaseMethod(this, "dispose");
        if (this._selectButton) {
            $removeHandler(this._selectButton, "click", this._selectButtonClickDelegate);
        }
        if (this._selectButtonClickedDelegate) {
            delete this._selectButtonClickedDelegate;
        }
        if (this._getSelectedItemsSuccessDelegate) {
            delete this._getSelectedItemsSuccessDelegate;
        }
        if (this._removeSelectedItemDelegate) {
            jQuery(this.get_element()).find(this.get_selectedItemsList()).undelegate('.remove', 'click', this._removeSelectedItemDelegate);
            delete this._removeSelectedItemDelegate;
        }
        if (this._doneButtonClickedDelegate) {
            delete this._doneButtonClickedDelegate;
        }
        if (this._cancelButtonClickedDelegate) {
            delete this._cancelButtonClickedDelegate;
        }
		if (this._binderDataBindingDelegate) {
            this._itemsSelector.remove_binderDataBinding(this._binderDataBindingDelegate);
            delete this._binderDataBindingDelegate;
        }
        if (this._onloadDelegate) {
            Sys.Application.remove_load(this._onloadDelegate);
            delete this._onloadDelegate;
        }

        this._selectedItems.items.splice(0, this._selectedItems.items.length);
    },

    /* --------------------  public methods ----------- */

    /*Gets the value of the field control.*/
    get_value: function () {
        /*on publish if we have items in the kendo observable array 
        we get their ids in a aray of Guids so that they can be persisted*/
        var selectedKeysArray = new Array();
        var data = this._selectedItems.toJSON();
        for (var i = 0; i < data.items.length; i++) {
            selectedKeysArray.push(data.items[i].UserID);
        }
        if (selectedKeysArray.length > 0)
            return selectedKeysArray;
        else
            return null;
    },

    /*Sets the value of the text field control.*/
    set_value: function (value) {
		/*clears the observable array*/
        this._selectedItems.items.splice(0, this._selectedItems.items.length);        
        
		/*if there are related items get them through the users service*/
		if (value != null && value != "") {
			var filterExpression = "";
			for (var i = 0; i < value.length; i++) {
			    if (i > 0) {
			        filterExpression = filterExpression + ' OR ';
			    }
			    filterExpression = filterExpression + 'Id == ' + value[i].toString();
			}
			var data = {
				"itemType": this.get_userType(), 
				"filter": filterExpression
			};
			$.ajax({
				url: this.get_usersServicePath(),
				type: "GET",
				dataType: "json",
				data: data,
			    //headers: {"SF_UI_CULTURE" : this.get_uiCulture() },
				contentType: "application/json; charset=utf-8",
				/*on success add them to the kendo observable array*/
				success: this._getSelectedItemsSuccessDelegate
			});
		}
		
        this.raisePropertyChanged("value");
        this._valueChangedHandler();
    },


    /* -------------------- events -------------------- */

    /* -------------------- event handlers ------------ */
	
	onload: function () {
        this._itemsSelector.get_binder().set_clearSelectionOnRebind(false);
    },

    _binderDataBindingHandler: function (sender, args) {
        var selectedItems = this._selectedItems.items.toJSON();
        if (selectedItems) {
            var items = args.get_dataItem().Items;
            for (var i = 0, l = selectedItems.length; i < l; i++) {
                var selectedItem = selectedItems[i];
                var index = items.length;
                while (index--) {
                    var item = items[index];
                    if (item.UserID === selectedItem.UserID) {
                        items.splice(index, 1);
                    }
                }
            }
        }
    },
	
    _selectButtonClicked: function (sender, args) {
        this.get_itemsSelector()._selectorSearchBox.get_binderSearch()._multilingual = false;
        this.get_itemsSelector().dataBind();
		this.get_itemsSelector().get_binder().clearSelection();
        this._selectDialog.dialog("open");
        this._selectDialog.dialog().parent().css("min-width", "525px");
        dialogBase.resizeToContent();
			      
        return false;
    },

    _doneButtonClicked: function (keys) {
        if (keys != null) {
            /*push newly selected items in the observable array*/
            var selectedItems = this.get_itemsSelector().getSelectedItems();
            for (var i = 0; i < selectedItems.length; i++) {
                this._selectedItems.items.push(selectedItems[i]);
            }
        }
        this._selectDialog.dialog("close");
        dialogBase.resizeToContent();
    },

    _cancelButtonClicked: function (sender, args) {
        this._selectDialog.dialog("close");
        dialogBase.resizeToContent();
    }, 
	
    _getSelectedItemsSuccess: function (result) {
        /*push existing related items in the kendo observable array*/
        this._selectedItems.items.splice(0, this._selectedItems.items.length);
        for (var i = 0; i < result.Items.length; i++) {
            this._selectedItems.items.push(result.Items[i]);
        }
    },    

    _removeSelectedItem: function (value) {
        var itemToRemove = $(value.target).siblings().first();
        var data = this._selectedItems.toJSON();
        /*find the index of the selected item and delete it*/
        for (var i = 0; i < data.items.length; i++) {
            if (data.items[i].UserID == itemToRemove.data("id")) {
                this._selectedItems.items.splice(i, 1);
                break;
            }
        }
    },

    /* -------------------- private methods ----------- */

    /* -------------------- properties ---------------- */

    get_selectButton: function () {
        return this._selectButton;
    },
    set_selectButton: function (value) {
        this._selectButton = value;
    },

    get_itemsSelector: function () {
        return this._itemsSelector;
    },
    set_itemsSelector: function (value) {
        this._itemsSelector = value;
    },
	
    get_selectorWrapper: function () {
        return this._selectorWrapper;
    },
    set_selectorWrapper: function (value) {
        this._selectorWrapper = value;
    },

    get_selectedItemsList: function () {
        return this._selectedItemsList;
    },
    set_selectedItemsList: function (value) {
        this._selectedItemsList = value;
    },

    get_doneButton: function () {
        return this._doneButton;
    },
    set_doneButton: function (value) {
        this._doneButton = value;
    },

    get_cancelButton: function () {
        return this._cancelButton;
    },
    set_cancelButton: function (value) {
        this._cancelButton = value;
    },

    get_usersServicePath: function () {
        return this._usersServicePath;
    },
    set_usersServicePath: function (value) {
        this._usersServicePath = value;
    },
	
	get_userType: function () {
        return this._userType;
    },
    set_userType: function (value) {
        this._userType = value;
    }
};

SitefinityWebApp.FieldControls.RelatedUsers.RelatedUsersField.registerClass("SitefinityWebApp.FieldControls.RelatedUsers.RelatedUsersField", Telerik.Sitefinity.Web.UI.Fields.FieldControl);
