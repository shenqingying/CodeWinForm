var onSelect = false;
var selectValue = '';
var tempValueList;
var keyDownObject;
var canSelect;
var plist;

function show(_txtbox, _list, _valueList, reload) {
    tempValueList = _valueList;
    var txtbox = document.getElementById(_txtbox);
    var txtboxValue = txtbox.value;
    var valueList = document.getElementById(_valueList);
    var list = document.getElementById(_list);

    plist = list;

    if (reload == 1) {
        GetClientValue();
    }
    var myheight = txtbox.clientHeight + 3;
    var t = txtbox.offsetTop; var l = txtbox.offsetLeft;
    while (txtbox = txtbox.offsetParent)
    { t += txtbox.offsetTop; l += txtbox.offsetLeft; }

    var top = parseInt(t, 10) + myheight;
    var left = l;
    list.style.display = '';
    list.style.position = 'absolute';
    list.style.top = top;
    list.style.left = left;

    if (txtboxValue != '') {
        valueList.value = txtboxValue;
    }
    else {
        valueList.selectedIndex = 0;
    }


    keyDownObject = document.onkeydown;
    document.onkeydown = null;

}
//在回调方法中注册的接收返回结果的函数
function ReceiveControlServerData(result) {
    var valueList = document.getElementById(tempValueList);
    if (result != "") {
        var ss = result.split('^')
        for (i = 0; i < ss.length - 1; i++) {
            var itemValue = ss[i];
            var ss1 = itemValue.split('-');
            var oOption2 = document.createElement("OPTION");
            oOption2.text = ss1[1] + "-" + ss1[0];
            oOption2.value = ss1[1] + "-" + ss1[0];
            valueList.add(oOption2);
        }
    }
}


function unshow(_txtbox, _list, _valueList, outControlList) {

    var valueList = document.getElementById(_valueList);
    var list = document.getElementById(_list);
    var txtbox = document.getElementById(_txtbox);
    var isFound = false;

    if (document.activeElement == valueList) return;

    if (txtbox.value != '') {
        for (i = 0; i < valueList.length; i++) {
            var tempValue = valueList.options[i].value;
            var ss = tempValue.split('-')
            for (j = 0; j < ss.length; j++) {
                var s = ss[j].toUpperCase().toUpperCase().indexOf(txtbox.value.toUpperCase());
                var s1 = valueList.options[i].value.toUpperCase().indexOf(txtbox.value.toUpperCase())
                if (s != '-1' || s1 != '-1') {
                    txtbox.value = valueList.options[i].value;
                    isFound = true;
                    selectValue = txtbox.value;
                    break;
                }
            }
        }
    }
    /*
    if (isFound == false) {
        txtbox.value = '';
    }
    */
    list.style.display = 'none';
    onSelect = true;
    document.onkeydown = keyDownObject;
    /*
    //如果外部控件不为空
    if (outControlList != '') {
        var controlName = outControlList.split(',')

        for (k = 0; k < controlName.length; k++) {
            var valueObject = document.getElementById(controlName[k]);

            if (txtbox.value != 'TWN-中国台湾') {
                valueObject.style.display = "none";
            }
            else {
                valueObject.style.display = "";
            }
        }

    }
    */
}

function unshowapply(_txtbox, _list, _valueList, outControlList) {
    var valueList = document.getElementById(_valueList);
    var list = document.getElementById(_list);
    var txtbox = document.getElementById(_txtbox);
    var isFound = false;

    if (document.activeElement == valueList) return;

    if (txtbox.value != '') {
        for (i = 0; i < valueList.length; i++) {
            var tempValue = valueList.options[i].value;
            var ss = tempValue.split('-')
            for (j = 0; j < ss.length; j++) {
                var s = ss[j].toUpperCase().toUpperCase().indexOf(txtbox.value.toUpperCase());
                var s1 = valueList.options[i].value.toUpperCase().indexOf(txtbox.value.toUpperCase())
                if (s != '-1' || s1 != '-1') {
                    txtbox.value = valueList.options[i].value;
                    isFound = true;
                    selectValue = txtbox.value;
                    break;
                }
            }
        }
    }

    list.style.display = 'none';
    onSelect = true;
    document.onkeydown = keyDownObject;
}


function getVaule(_txtbox, _list, _valueList) {
    onSelect = true;
    while (onSelect) {
        var valueList = document.getElementById(_valueList);
        var list = document.getElementById(_list);
        var txtbox = document.getElementById(_txtbox);

        txtbox.value = valueList.value;
        selectValue = txtbox.value;
        list.style.display = 'none';
        focuscontrol = "";
        onSelect = false;
        document.onkeydown = keyDownObject;
    }
}

function acceptKey(_txtbox, _valueList) {
    var valueList = document.getElementById(_valueList);
    var txtbox = document.getElementById(_txtbox);
    var myIndex = valueList.selectedIndex;
    canSelect = true;

    //向上按键
    if (window.event.keyCode == 38 && myIndex > 0) {
        canSelect = false;
        valueList.options[parseInt(myIndex - 1)].selected = true;
        myIndex = myIndex - 1;
        return;
    }

    //向下按键
    if (window.event.keyCode == 40 && myIndex < valueList.length - 1) {
        canSelect = false;
        valueList.options[parseInt(myIndex + 1)].selected = true;
        myIndex = myIndex + 1;
        return;
    }

    if (window.event.keyCode == 13) {
        //if(txtbox.value != '')
        if (valueList.selectedIndex != -1)
            txtbox.value = valueList.value;
        //plist.style.display='none';
        document.onkeydown = keyDownObject;
        return;
    }

    if (window.event.keyCode == 9) {
        document.onkeydown = keyDownObject;
        return;
    }
}

function acceptKeyapply(_txtbox, _valueList) {
    var valueList = document.getElementById(_valueList);
    var txtbox = document.getElementById(_txtbox);
    var myIndex = valueList.selectedIndex;
    canSelect = true;

    //向上按键
    if (window.event.keyCode == 38 && myIndex > 0) {
        canSelect = false;
        valueList.options[parseInt(myIndex - 1)].selected = true;
        myIndex = myIndex - 1;
        return;
    }

    //向下按键
    if (window.event.keyCode == 40 && myIndex < valueList.length - 1) {
        canSelect = false;
        valueList.options[parseInt(myIndex + 1)].selected = true;
        myIndex = myIndex + 1;
        return;
    }

    if (window.event.keyCode == 13) {
        if (valueList.selectedIndex != -1)
            txtbox.value = valueList.value;
        //plist.style.display='none';
        document.onkeydown = keyDownObject;
        return;
    }

    if (window.event.keyCode == 9) {
        document.onkeydown = keyDownObject;
        return;
    }
}

function acceptKeyDown(_txtbox, _valueList) {

    if (canSelect == false)
        return;

    var minIndex;
    var minK;
    var valueList = document.getElementById(_valueList);
    var txtbox = document.getElementById(_txtbox);
    var myIndex = valueList.selectedIndex;
    var serchValue = txtbox.value.toUpperCase();

    for (k = 0; k < valueList.length; k++) {
        var s = valueList.options[k].value.toUpperCase().indexOf(serchValue)
        if (s != '-1') {
            if (minIndex == null)
                minIndex = 999999;

            if (parseInt(s) < parseInt(minIndex)) {
                minIndex = s;
                minK = k;
            }
        }
    }

    if (minIndex != null) {
        valueList.options[parseInt(minK)].selected = true;
    }
}

function acceptKeyDownapply(_txtbox, _valueList) {

    if (canSelect == false)
        return;

    var minIndex;
    var minK;
    var valueList = document.getElementById(_valueList);
    var txtbox = document.getElementById(_txtbox);
    var myIndex = valueList.selectedIndex;
    var serchValue = txtbox.value.toUpperCase();

    for (k = 0; k < valueList.length; k++) {
        var s = valueList.options[k].value.toUpperCase().indexOf(serchValue)
        if (s != '-1') {
            if (minIndex == null)
                minIndex = 999999;

            if (parseInt(s) < parseInt(minIndex)) {
                minIndex = s;
                minK = k;
            }
        }
    }

    if (minIndex != null) {
        valueList.options[parseInt(minK)].selected = true;
    }
    else
        valueList.selectedIndex = -1;
}
