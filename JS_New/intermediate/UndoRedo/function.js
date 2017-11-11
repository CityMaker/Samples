// 打开备份数据库
function _OpenBackUpObjectClass(srcOC) {
    var ocName = srcOC.name;
    var fcNames = backupSet.getNamesByType(gviDataSetType.gviDataSetFeatureClassTable);

    var bContain = false;
    var nCount = fcNames.length;
    if (fcNames.length > 0) {
        for (var i = 0; i < nCount; ++i) {
            var strName = fcNames[i];
            if (ocName == strName) {
                bContain = true;
                break;
            }
        }
    }

    if (bContain)
        backupOC = backupSet.openFeatureClass(ocName);
    else {
        var pNames = __g.new_FieldInfoCollection;
        _GetFieldInfos(srcOC, pNames);
        backupOC = backupSet.createFeatureClass(ocName, pNames);
    }
}

//获取所有字段
function _GetFieldInfos(pOC, pNames) {
    for (var i = 0; i < pOC.getFields().count; ++i) {
        var pSrcField = pOC.getFields().get(i);
        if (pSrcField.isSystemField)
            continue;

        var pField = pSrcField.clone();
        pField.domain = null;
        pField.registeredRenderIndex = false;
        pNames.add(pField);
    }

    var pField = __g.new_FieldInfo;
    pField.name = "o_id";
    pField.fieldType = gviFieldType.gviFieldInt32;
    pNames.add(pField);
}

//复制字段
function _InsertFeature(row, newRow, from) {
    var names = row.fields;
    for (var i = 0; i < names.count; ++i) {
        var pField = names.get(i);
        var nPos2 = row.fields.indexOf(pField.name);
        if (nPos2 == -1 || pField.name == "o_id")
            continue;
        if (row.isNull(i) && pField.nullable) {
            newRow.setNull(nPos2);
            continue;
        }
        newRow.setValue(nPos2, row.getValue(i));
    }

    if (from) {
        //备份更新到原
        var nPos = row.fields.indexOf("o_id");
        var v = row.getValue(nPos);
        newRow.setValue(0, v);
    }
    else {
        //源到备份
        var nPos = newRow.fields.indexOf("o_id");
        var v = row.getValue(0);
        newRow.setValue(nPos, v);
        newRow.setNull(0);
    }
}

var nPos = -1;
var nPos2 = -1;
var pField = null;
function _UpdateFeature(pRow, pNewRow, hasCommandId, me) {
    for (var i = 0; i < me.m_updateFieldNames.length; ++i) {
        nPos = pRow.fields.indexOf(me.m_updateFieldNames[i]);
        nPos2 = pNewRow.fields.indexOf(me.m_updateFieldNames[i]);
        if (nPos == -1 || nPos2 == -1)
            continue;
        pField = pRow.fields.get(nPos);
        if (pField.fieldType == gviFieldType.gviFieldFID)
            continue;
        if (pRow.isNull(nPos) && pField.nullable) {
            pNewRow.setNull(nPos2);
            continue;
        }
        pNewRow.setValue(nPos2, pRow.getValue(nPos));
    }

    if (hasCommandId) {   //备份更新到原
        var nPos=pRow.fields.indexOf("o_id");
        var v=pRow.getValue(nPos);
        pNewRow.setValue(0,v);
    }
    else {  //源到备份
        var nPos=pNewRow.fields.indexOf("o_id");
        var v=pRow.getValue(0);
        pNewRow.setValue(nPos,v);
    }
}