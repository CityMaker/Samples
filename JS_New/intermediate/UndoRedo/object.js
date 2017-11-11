    var backupSet;
    var backupOC;

    var m_stackUndo = new Array();
    var m_stackRedo = new Array();

    function Command() {
        this.m_cmdType; //delete Insert Update
	    this.m_pSrcDataset;
        this.m_pSrcFCName;
        this.m_pDesObjectClass;
        this.m_idToDel; //代替m_FidVec
        //this.m_cmdId;
        this.m_updateFieldNames = new Array();
    }

    Command.prototype.delRow = function () {
        //1、从源数据库中查出待删除的记录
        var pSrcFeatureClass = this.m_pSrcDataset.openFeatureClass(this.m_pSrcFCName);
        var row = pSrcFeatureClass.getRow(this.m_idToDel);
        //2、删除源数据库中的数据
        var filter = __g.new_QueryFilter;
        filter.whereClause = "oid = " + this.m_idToDel;
        var cursor = pSrcFeatureClass.update(filter);
        cursor.nextRow();
        cursor.deleteRow();
        //3、插入记录到备份数据库
        var cursorInsert = this.m_pDesObjectClass.insert(filter);
        var newRow = this.m_pDesObjectClass.createRowBuffer();
        _InsertFeature(row, newRow, false);
        cursorInsert.insertRow(newRow);
    }

    Command.prototype.updateRow = function (uptRow) {
        //1、获取有哪些字段更新
        this.m_updateFieldNames.length = 0;
        for (var i = 0; i < uptRow.fields.count; ++i) {
            if (uptRow.isChanged) {
                var v = uptRow.fields.get(i);
                this.m_updateFieldNames.push(v.name);
            }
        }
        //2、从源数据库查出这些记录
        var pSrcFeatureClass = this.m_pSrcDataset.openFeatureClass(this.m_pSrcFCName);
        var row = pSrcFeatureClass.getRow(this.m_idToDel);
        //3、插入备份记录到备份数据库
        var pCursor = this.m_pDesObjectClass.insert();
        var newRow = this.m_pDesObjectClass.createRowBuffer();
        _InsertFeature(row, newRow, false);
        pCursor.insertRow(newRow);

        //4、更新源数据库
        this.m_pSrcDataset.dataSource.startEditing();
        var rowcol = __g.new_RowBufferCollection;
        rowcol.add(uptRow);
        pSrcFeatureClass.updateRows(rowcol, true);
        this.m_pSrcDataset.dataSource.stopEditing(true);
    }

    Command.prototype.insertRow = function (newRow) {
        //1、插入数据
        var pSrcFeatureClass = this.m_pSrcDataset.openFeatureClass(this.m_pSrcFCName);
        var cursor = pSrcFeatureClass.insert();
        cursor.insertRow(newRow);
        var oid = cursor.lastInsertId;
        newRow.setValue(0, oid);
        this.m_idToDel = oid;
    }

    Command.prototype.undo = function () {
        __g.pauseRendering(false);
        var me = this;
        if (this.m_cmdType == gvCommandType.gvDeleteCommand) {
            _InsertCallback(me);
        }
        else if (this.m_cmdType == gvCommandType.gvInsertCommand) {
            _DeleteCallback(me);
        }
        else if (this.m_cmdType == gvCommandType.gvUpdateCommand) {
            _UpdateCallback(me);
        }
        __g.resumeRendering();
    }

    Command.prototype.redo = function () {
        __g.pauseRendering(false);
        var me = this;
        if (this.m_cmdType == gvCommandType.gvDeleteCommand) {
            _DeleteCallback(me);
        }
        else if (this.m_cmdType == gvCommandType.gvInsertCommand) {
            _InsertCallback(me);
        }
        else if (this.m_cmdType == gvCommandType.gvUpdateCommand) {
            _UpdateCallback(me);
        }
        __g.resumeRendering();
    }

    function _InsertCallback(me) {
            //1、从备份数据库中查出删除的记录
            var pFilter = __g.new_QueryFilter;
            pFilter.whereClause = "o_id = " + me.m_idToDel;
            var pCur = me.m_pDesObjectClass.search(pFilter, false);
            var pRow = pCur.nextRow();

	        //2、删除备份数据库中的数据
            me.m_pDesObjectClass.del(pFilter);

	        //3、插入记录到源数据库
            var pSrcFeatureClass = me.m_pSrcDataset.openFeatureClass(me.m_pSrcFCName);
            var pCursor=pSrcFeatureClass.insert();
            var pNewRow = pSrcFeatureClass.createRowBuffer();
            _InsertFeature(pRow, pNewRow, true);
            pCursor.insertRow(pNewRow);

            //4、处理三维
            __g.featureManager.createFeature(pSrcFeatureClass, pNewRow);
            __g.featureManager.unhighlightAll();
            __g.featureManager.highlightFeature(pSrcFeatureClass, me.m_idToDel, 0xffff0064);
    }

    function _DeleteCallback(me) {
        //1、从源数据库中查出插入的记录
        var pSrcFeatureClass = me.m_pSrcDataset.openFeatureClass(me.m_pSrcFCName);
        var pFilter = __g.new_QueryFilter;
        pFilter.whereClause = "oid = " + me.m_idToDel;
        var pCur = pSrcFeatureClass.search(pFilter, false);
        var pRow = pCur.nextRow();

        //2、删除源数据库中的数据
        pSrcFeatureClass.del(pFilter);

        //3、插入记录到备份数据库
        var pCursor = me.m_pDesObjectClass.insert();
        var pNewRow = me.m_pDesObjectClass.createRowBuffer();
        _InsertFeature(pRow, pNewRow, false);
        pCursor.insertRow(pNewRow);

        //4、处理三维
        __g.featureManager.deleteFeature(pSrcFeatureClass, me.m_idToDel);
    }

    function _UpdateCallback(me) {
        //1、分别从源和备份中拿到数据
        var pSrcFeatureClass = me.m_pSrcDataset.openFeatureClass(me.m_pSrcFCName);
        var pRow = pSrcFeatureClass.getRow(me.m_idToDel);

        var pFilter2 = __g.new_QueryFilter;
        pFilter2.whereClause = "o_id = " + me.m_idToDel;
        var pCur2 = me.m_pDesObjectClass.search(pFilter2, false);
        var pRow2 = pCur2.nextRow();

        //2、互相交互更新
        //更新到原_UpdateSrcFeatures  传入参数：pRow2
        var rowcol = __g.new_RowBufferCollection;
        var rbf = __g.rowBufferFactory; 
        var rb = rbf.createRowBuffer(pSrcFeatureClass.getFields());
        _UpdateFeature(pRow2, rb, true, me);
        rowcol.add(rb);
        pSrcFeatureClass.updateRows(rowcol, false);

        //更新到备份_UpdateDesFeatures  传入参数：pRow
        var rowcol2 = __g.new_RowBufferCollection;
        _UpdateFeature(pRow, pRow2, false, me); //更新到备份_UpdateDesFeatures
        rowcol2.add(pRow2);
        me.m_pDesObjectClass.updateRows(rowcol2, false);
                
        //4、处理三维
		__g.featureManager.editFeature(pSrcFeatureClass, rb);
        __g.featureManager.unhighlightAll();
        __g.featureManager.highlightFeature(pSrcFeatureClass, me.m_idToDel, 0xffff0064);
    }