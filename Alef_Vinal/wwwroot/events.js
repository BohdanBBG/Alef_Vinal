"use strict";

var pageData = {};

var eventListenerState = { // to make sure the EventListener was added once
    dataItem: { state: false },
    itemListPutButton: { state: false },
    itemListAddButton: { state: false }
}

var dataListContainer = document.querySelector('.data-list-container');
var dataListEl = document.querySelector('.js-data-value-name-template');

function initDataList() {
    getPageData(function (items) {

        pageData = items;

        // remove all childs
        while (dataListContainer.firstChild) {
            dataListContainer.removeChild(dataListContainer.firstChild);
        }

        items.forEach(function (item, i) {
            addDataItemsBlock(item);
        });
    });
}

function getPageData(callBack) {
    httpGet("/GetAll", function (data) {
        callBack(data);
    });
}


function addDataItemsBlock(item) {

    var clone = dataListEl.cloneNode(true);
    var dataValueEl = clone.querySelector('.data-value-span');
    var dataName = clone.querySelector('.data-name-span');

    dataValueEl.innerText = item.value;
    dataName.innerText = item.name;

    clone.setAttribute('item-id', item.id);
    clone.classList.remove('hidden');
    dataListContainer.appendChild(clone);
}

initDataList();



addBubleEventListener(dataListContainer, ".data-value-name-container", 'click', eventListenerState.dataItem, function (e, actualEl, desiredEl) {
    e.stopPropagation();

    console.log("Item list event. Actual Element: ", actualEl);
    console.log("Item list event. Desired Element: ", desiredEl);


    let codeEntityId = desiredEl.getAttribute('item-id');

    let selectedItemList = pageData.find(x => x.id === codeEntityId);

    let editDataDormEl = document.forms.EditData;

    editDataDormEl.elements.id.value = codeEntityId;
    editDataDormEl.elements.value.value = selectedItemList.value;
    editDataDormEl.elements.name.value = selectedItemList.name;

    if (editDataDormEl.elements.id.classList.contains('input-field-empty-js')) {
        editDataDormEl.elements.id.classList.remove('input-field-empty-js');
    }

    if (editDataDormEl.elements.value.classList.contains('input-field-empty-js')) {
        editDataDormEl.elements.value.classList.remove('input-field-empty-js');
    }

    if (editDataDormEl.elements.name.classList.contains('input-field-empty-js')) {
        editDataDormEl.elements.name.classList.remove('input-field-empty-js');
    }
});

addBubleEventListener(".js-put-button-container", '.js-put-button', 'click', eventListenerState.itemListPutButton, function (e, actualEl, desiredEl) {
    e.stopPropagation();

    var formDataSend = {};


    if (makeRequestBody(formDataSend, document.forms.EditData)) {

        httpRequest("/Update", formDataSend, "PATCH", function (statusCode) {

            if (statusCode == 200) {
                initDataList();
            }
        });

    }

});// event listener on PUT button

addBubleEventListener(".js-add-button-container", '.js-add-button', 'click', eventListenerState.itemListAddButton, function (e, actualEl, desiredEl) {
    e.stopPropagation();

    var formDataSend = {};

    if (makeRequestBody(formDataSend, document.forms.AddNewData)) {

        httpRequest("/Add", formDataSend, "POST", function (statusCode) {

            if (statusCode == 200) {
                initDataList();
            }
        });

    }

});// event listener on ADD button

function makeRequestBody(formDataSend, source) {

    var isSomeoneEmpty = false;

    for (var i = 0; i < source.elements.length - 1; i++) {

        formDataSend[source.elements[i].name] = source.elements[i].value ? source.elements[i].value :
            source.elements[i].classList.add('input-field-empty-js');

        if (formDataSend[source.elements[i].name] === undefined) {
            isSomeoneEmpty = true;
        }

        source.elements[i].onfocus = function (e) {
            if (e.target.classList.contains('input-field-empty-js')) {
                e.target.classList.remove('input-field-empty-js');
            }
        }
    }

    isSomeoneEmpty = validateValue(source) ? false: true ;

    console.log('------', formDataSend);

    return !isSomeoneEmpty;
}

function validateValue(source) {
    
    if (!/^\d{1,3}$/.test(source.elements.value.value)) {
        source.elements.value.classList.add('input-field-empty-js');
        
        return false;
    }
    
    return true;
}


function addBubleEventListener(sourceElSelector, targetElSelector, eventName, checkedHandler, eventHandler) { // for add EventListener to elements

    if (!checkedHandler.state) {
        var sourceEl = (typeof sourceElSelector === "object") ? sourceElSelector : document.querySelector(sourceElSelector);
        checkedHandler.state = true;

        sourceEl.addEventListener(eventName, function (e) {
            var actualEl = e.target; // element event fired on
            var desiredEl = e.target.closest(targetElSelector); // element we excpect event fired on

            var matches = actualEl.matches(targetElSelector);
            var isChildEl = desiredEl !== null; // if this el is parent

            if (matches || isChildEl) {
                eventHandler(e, actualEl, desiredEl || actualEl);
            }
        });
    }
}


function httpRequest(url, data, typeOfrequest, callback) { // for POST PUT DELETE

    var xhr = new XMLHttpRequest();

    xhr.open(typeOfrequest, url, true);

    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.onload = function () {
        if (xhr.status === 200) {

        } else if (xhr.status === 500) {
            console.error('Request failed.  Returned status of ' + xhr.status);
            alert('Request failed.  Returned status of ' + xhr.status);
        } else if (xhr.status === 401 || xhr.status === 403) {
            console.error('Request failed.  Returned status of ' + xhr.status);
            alert('Request failed.  Returned status of ' + xhr.status);
        }
        callback(xhr.status);
    };

    xhr.send(JSON.stringify(data));
}

function httpGet(url, callBack) { // for GET
    var xhr = new XMLHttpRequest();
    xhr.open('GET', url);


    xhr.onload = function () {
        if (xhr.status === 200) {

            if (xhr.responseText !== "") {
                var data = JSON.parse(xhr.responseText);
                console.log("Response data: ", data);
            }

            callBack(data);

        } else if (xhr.status === 500) {
            console.error('Request failed.  Returned status of ' + xhr.status);
            callBack(null);
        } else if (xhr.status === 401 || xhr.status === 403) {
            console.error('Request failed.  Returned status of ' + xhr.status);
            callBack(null);
        }
        else {
            console.error('Request failed.  Returned status of ' + xhr.status);
            callBack(null);
        }
    };
    xhr.send();
}

