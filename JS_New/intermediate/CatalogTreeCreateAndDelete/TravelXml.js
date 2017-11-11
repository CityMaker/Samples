/************************************************************************/
/* XMl
/************************************************************************/
 /*
  *	遍历逻辑图层树，设置featurelayer内的所有逻辑组可见
  */
function  TravelXML(pNode, fLayer)
{
    if (pNode == null)
        return;
    var nodeName = pNode.nodeName;
    if (nodeName.toLowerCase()== "pgroup" || nodeName.toLowerCase()== "agroup")
    {
        var att =pNode.getAttribute("ID");
        if (att != null)
        {
            var grpId = parseInt(att);
            if (grpId!=null&& fLayer != null)
            {
                fLayer.setGroupVisibleMask(grpId, gviViewportMask.gviViewAllNormalView);
            }
        }
    }
    else if (nodeName.toLowerCase()== "pgroups" || nodeName.toLowerCase() == "agroups")
    {
        for (var i=0;i< pNode.childNodes.length;i++)
        {
            var node=pNode.childNodes[i];
            TravelXML(node, fLayer);
        }
    }
}