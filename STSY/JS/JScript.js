// JScript 文件
var isIe = (document.all) ? true : false;
//设置select的可见状态 
function setSelectState(state) {
    var objl = document.getElementsByTagName('select');
    for (var i = 0; i < objl.length; i++) {
        objl[i].style.visibility = state;
    }
}
function mousePosition(ev) {
    if (ev.pageX || ev.pageY) {
        return { x: ev.pageX, y: ev.pageY };
    }
    return {
        x: ev.clientX + document.body.scrollLeft - document.body.clientLeft, y: ev.clientY + document.body.scrollTop - document.body.clientTop
    };
}
//弹出方法 
function showMessageBox(wTitle, content, pos, wWidth) {
    closeWindow();
    var bWidth = parseInt(document.documentElement.scrollWidth);
    var bHeight = parseInt(document.documentElement.scrollHeight);
    if (isIe) {
        setSelectState('hidden');
    }
    var back = document.createElement("div");
    back.id = "back";
    var styleStr = "top:0px;left:0px;position:absolute;background:#666;width:" + bWidth + "px;height:" + bHeight + "px;";
    styleStr += (isIe) ? "filter:alpha(opacity=0);" : "opacity:0;";
    back.style.cssText = styleStr;
    document.body.appendChild(back);
    showBackground(back, 50);
    var mesW = document.createElement("div");
    mesW.id = "mesWindow";
    mesW.className = "mesWindow";
    mesW.innerHTML = "<div class='mesWindowTop'><table width='100%' height='100%'><tr><td>" + wTitle + "</td><td style='width:1px;'><input type='button' onclick='closeWindow();' title='关闭窗口' class='close' value='关闭' /></td></tr></table></div><div class='mesWindowContent' id='mesWindowContent'>" + content + "</div><div class='mesWindowBottom'></div>";

    var left = (screen.width / 2) - 350 / 2;
    var top = (screen.height / 2) - 235;

    styleStr = "left:" + left + "px;top:" + top + "px;position:absolute;width:" + wWidth + "px;";
    mesW.style.cssText = styleStr;
    document.body.appendChild(mesW);
}
//让背景渐渐变暗 
function showBackground(obj, endInt) {
    if (isIe) {
        obj.filters.alpha.opacity += 1;
        if (obj.filters.alpha.opacity < endInt) {
            setTimeout(function () { showBackground(obj, endInt) }, 5);
        }
    } else {
        var al = parseFloat(obj.style.opacity); al += 0.01;
        obj.style.opacity = al;
        if (al < (endInt / 100))
        { setTimeout(function () { showBackground(obj, endInt) }, 5); }
    }
}
//关闭窗口 
function closeWindow() {
    if (document.getElementById('back') != null) {
        document.getElementById('back').parentNode.removeChild(document.getElementById('back'));
    }
    if (document.getElementById('mesWindow') != null) {
        document.getElementById('mesWindow').parentNode.removeChild(document.getElementById('mesWindow'));
    }

    if (isIe) {
        setSelectState('');
    }
}
//测试弹出 
function testMessageBox(ev) {
    var objPos = mousePosition(ev);
    messContent = "<div style='padding:20px 0 20px 0;text-align:center'><table width='70%' border='0' cellspacing='0' cellpadding='0'><tr><td height='36'>输入账号：</td><td><input ID='txtUserName' type='text' CssClass='c2' Width='120px' /></td></tr><tr><td height='36'>输入密码：</td><td><input ID='txtPassWord' type='password' CssClass='c2' Width='120px' /></td></tr></table></div>";
    showMessageBox('用户登录', messContent, objPos, 350);
}
function validatelogon() {
    var loginname = document.getElementById("txtUserName").value;
    var password = document.getElementById("txtPassWord").value;
//    var num = document.getElementById("txtNum").value;

    if (loginname == "") {
        alert("请输入用户名！");
        return false;
    }
    if (password == "") {
        alert("请输入密码！");
        return false;
    }
//    if (num == "") {
//        alert("请输入验证码！");
//        return false;
//    }

    xmlHttp = new ActiveXObject("Microsoft.XMLHTTP");

    if (xmlHttp != null) {
        xmlHttp.onreadystatechange = getdata;
        xmlHttp.Open("GET", "ValidateLogon.aspx?loginname=" + loginname + "&password=" + password + "&num=" + num, true);
        xmlHttp.Send();
    }
    else {
        alert("你的浏览器不支持此登录方式，点击确定后跳转到登录页面！");
        window.location.href = "Logon.aspx";
    }
}
function getdata() {
    if (xmlHttp.readystate == 4) {
        if (xmlHttp.status == 200) {
            var text = xmlHttp.responseText;
            if (text == "0") {
                alert("验证码输入错误！");
                return false;
            }
            else if (text == "1") {
                alert("登录失败，请重新登录！");
                return false;
            }
            else if (text == "2") {
                window.location.reload();
            }
        }
    }
}
function showimg(im) { im.src = "ImgValidate.aspx?" + new Date; }

function gotoHeadUrl(url) {
    //window.parent.close();
    window.top.location = url + 'Login.aspx'; 
    //LoadLogin(url + 'Login.aspx');
}