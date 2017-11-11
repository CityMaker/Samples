 /*
  *	遍历逻辑图层树，设置featurelayer内的所有逻辑组可见
  * 同时，在第一遍遍历时，将逻辑树的内容显示在页面上
  */
var str=["<ul>"];
var pid=0;
var tempid=0;
var oldpid=0;

function TravelXML(pNode, fLayer) { 
    if (pNode == null)
        return;

    var nodeName = pNode.nodeName;
    if (nodeName.toLowerCase() == "pgroup" || nodeName.toLowerCase() == "agroup") {
        var att = pNode.getAttribute("ID");
        if (att != null) {
            var grpId = parseInt(att);

            /************************************************************************/
            /* 界面显示                                                             */
            /************************************************************************/
            if (needShowOnHtml) {
                pid = pNode.getAttribute("ParentID");
                if (tempid != pid) {
                    if (pid == oldpid) {
                        str.push("</ul><li><input type='checkbox' onclick='setgpVisble(this)'value='" + grpId + "' checked=" + pNode.getAttribute("CheckState") + ">" + "<a href='#'>" + pNode.getAttribute("Name") + "</a></li>");
                    }
                    else {
                        str.push("<ul><li><input type='checkbox' onclick='setgpVisble(this)' value='" + grpId + "' checked=" + pNode.getAttribute("CheckState") + ">" + "<a href='#'>" + pNode.getAttribute("Name") + "</a></li>");
                    }
                    tempid = pid;
                }
                else {
                    str.push("<li><input type='checkbox' onclick='setgpVisble(this)' value='" + grpId + "' checked=" + pNode.getAttribute("CheckState") + ">" + "<a href='#'>" + pNode.getAttribute("Name") + "</a></li>");
                }
            }

            if (grpId != null && fLayer != null) {
                fLayer.setGroupVisibleMask(grpId, gviViewportMask.gviViewAllNormalView);
            }
        }
    }
    else if (nodeName.toLowerCase() == "pgroups" || nodeName.toLowerCase() == "agroups") {
        /************************************************************************/
        /* 界面显示                                                             */
        /************************************************************************/
        if (pNode.getAttribute("Name") == "回收站") {  // 不显示回收站及其子节点
            return;
        }

        if (needShowOnHtml) {

            pid = pNode.getAttribute("ParentID");
            if (tempid != pid) {
				if (pid == oldpid) {
					str.push("</ul><li><input type='checkbox'checked=" + pNode.getAttribute("CheckState") + ">" + pNode.getAttribute("Name") + "</li>")
				}
				else{
					str.push("<ul><li><input type='checkbox'checked=" + pNode.getAttribute("CheckState") + ">" + pNode.getAttribute("Name") + "</li>")
				}
            }
            else {
                str.push("<li><input type='checkbox'checked=" + pNode.getAttribute("CheckState") + ">" + pNode.getAttribute("Name") + "</a></li>")
            }
            tempid = pid;
			oldpid = pid;
        }
               
        for (var i = 0; i < pNode.childNodes.length; i++) {
            var node = pNode.childNodes[i];
            TravelXML(node, fLayer);
        }
    }
}
