"use strict";


var eventListenerState = { // to make sure the EventListener was added once
    dataItem: { state: false }
}

var dataListContainer = document.querySelector('.data-list-container');
var dataListEl = document.querySelector('.js-data-value-name-template');

function initDataList() {
    getPageData(function (items) {
        items.forEach(function (item, i) {
            addDataItemsBlock(item, item.CarItemId);
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

addBubleEventListener(dataListEl, dataListContainer,  'click', eventListenerState.dataItem, function (e) {

    console.log("asdasdasdas");
    var editDataDormEl = document.forms.EditData;

    editDataDormEl.elements.id = "1";
    editDataDormEl.elements.value = "1";
    editDataDormEl.elements.name = "1";


});

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

