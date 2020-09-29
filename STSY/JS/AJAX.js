<script language="javascript">
        var xmlHttp = false;
        try {
            xmlHttp = new ActiveXObject("Msxml2.XMLHTTP");
        } catch (e) {
            try {
                xmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
            } catch (e2) {
                xmlHttp = false;
            }
        }
        if (!xmlHttp && typeof XMLHttpRequest != 'undefined') {
            xmlHttp = new XMLHttpRequest();
        }
        function callServer() {
            var u_name = document.getElementById("txtSDLX").value;
            if ((u_name == null) || (u_name == "")) {
                var url = "AJAX.aspx?txtSDLX=" + escape(u_name);
                xmlHttp.open("GET", url, true);
                xmlHttp.onreadystatechange = updatePage;
                xmlHttp.send(null);
            }
        }
        function updatePage() {
            if (xmlHttp.readyState < 4) {
                test1.innerHTML = "loading...";
            }
            if (xmlHttp.readyState == 4) {
                var response = xmlHttp.responseText;
                test1.innerHTML = response;
//                alert("" + response + "");
            }
            //   if (test1.innerHTML=="已被注册"){
            // document.form1.submit.disabled=true}else{
            // document.form1.submit.disabled=false
            //   }
        }
</script>