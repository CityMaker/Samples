    var backupSet;
    var backupOC;

    var m_stackUndo = new Array();
    var m_stackRedo = new Array();

    function Command() {
        this.m_cmdType; //delete Insert Update
	    this.m_pSrcDataset;
        this.m_pSrcFCName;
        this.m_pDesObjectClass;
        this.m_idToDel; //����m_FidVec
        //this.m_cmdId;
        this.m_updateFieldNames = new Array();
    }

    Command.prototype.delRow = function () {
        //1����Դ���ݿ��в����ɾ���ļ�¼
        var pSrcFeatureClass = this.m_pSrcDataset.openFeatureClass(this.m_pSrcFCName);
        var row = pSrcFeatureClass.getRow(this.m_idToDel);
        //2��ɾ��Դ���ݿ��е�����
        var filter = __g.new_QueryFilter;
        filter.whereClause = "oid = " + this.m_idToDel;
        var cursor = pSrcFeatureClass.update(filter);
        cursor.nextRow();
        cursor.deleteRow();
        //3�������¼���������ݿ�
        var cursorInsert = this.m_pDesObjectClass.insert(filter);
        var newRow = this.m_pDesObjectClass.createRowBuffer();
        _InsertFeature(row, newRow, false);
        cursorInsert.insertRow(newRow);
    }

    Command.prototype.updateRow = function (uptRow) {
        //1����ȡ����Щ�ֶθ���
        this.m_updateFieldNames.length = 0;
        for (var i = 0; i < uptRow.fields.count; ++i) {
            if (uptRow.isChanged) {
                var v = uptRow.fields.get(i);
                this.m_updateFieldNames.push(v.name);
            }
        }
        //2����Դ���ݿ�����Щ��¼
        var pSrcFeatureClass = this.m_pSrcDataset.openFeatureClass(this.m_pSrcFCName);
        var row = pSrcFeatureClass.getRow(this.m_idToDel);
        //3�����뱸�ݼ�¼���������ݿ�
        var pCursor = this.m_pDesObjectClass.insert();
        var newRow = this.m_pDesObjectClass.createRowBuffer();
        _InsertFeature(row, newRow, false);
        pCursor.insertRow(newRow);

        //4������Դ���ݿ�
        this.m_pSrcDataset.dataSource.startEditing();
        var rowcol = __g.new_RowBufferCollection;
        rowcol.add(uptRow);
        pSrcFeatureClass.updateRows(rowcol, true);
        this.m_pSrcDataset.dataSource.stopEditing(true);
    }

    Command.prototype.insertRow = function (newRow) {
        //1����������
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
            //1���ӱ������ݿ��в��ɾ���ļ�¼
            var pFilter = __g.new_QueryFilter;
            pFilter.whereClause = "o_id = " + me.m_idToDel;
            var pCur = me.m_pDesObjectClass.search(pFilter, false);
            var pRow = pCur.nextRow();

	        //2��ɾ���������ݿ��е�����
            me.m_pDesObjectClass.del(pFilter);

	        //3�������¼��Դ���ݿ�
            var pSrcFeatureClass = me.m_pSrcDataset.openFeatureClass(me.m_pSrcFCName);
            var pCursor=pSrcFeatureClass.insert();
            var pNewRow = pSrcFeatureClass.createRowBuffer();
            _InsertFeature(pRow, pNewRow, true);
            pCursor.insertRow(pNewRow);

            //4��������ά
            __g.featureManager.createFeature(pSrcFeatureClass, pNewRow);
            __g.featureManager.unhighlightAll();
            __g.featureManager.highlightFeature(pSrcFeatureClass, me.m_idToDel, 0xffff0064);
    }

    function _DeleteCallback(me) {
        //1����Դ���ݿ��в������ļ�¼
        var pSrcFeatureClass = me.m_pSrcDataset.openFeatureClass(me.m_pSrcFCName);
        var pFilter = __g.new_QueryFilter;
        pFilter.whereClause = "oid = " + me.m_idToDel;
        var pCur = pSrcFeatureClass.search(pFilter, false);
        var pRow = pCur.nextRow();

        //2��ɾ��Դ���ݿ��е�����
        pSrcFeatureClass.del(pFilter);

        //3�������¼���������ݿ�
        var pCursor = me.m_pDesObjectClass.insert();
        var pNewRow = me.m_pDesObjectClass.createRowBuffer();
        _InsertFeature(pRow, pNewRow, false);
        pCursor.insertRow(pNewRow);

        //4��������ά
        __g.featureManager.deleteFeature(pSrcFeatureClass, me.m_idToDel);
    }

    function _UpdateCallback(me) {
        //1���ֱ��Դ�ͱ������õ�����
        var pSrcFeatureClass = me.m_pSrcDataset.openFeatureClass(me.m_pSrcFCName);
        var pRow = pSrcFeatureClass.getRow(me.m_idToDel);

        var pFilter2 = __g.new_QueryFilter;
        pFilter2.whereClause = "o_id = " + me.m_idToDel;
        var pCur2 = me.m_pDesObjectClass.search(pFilter2, false);
        var pRow2 = pCur2.nextRow();

        //2�����ཻ������
        //���µ�ԭ_UpdateSrcFeatures  ���������pRow2
        var rowcol = __g.new_RowBufferCollection;
        var rbf = __g.rowBufferFactory; 
        var rb = rbf.createRowBuffer(pSrcFeatureClass.getFields());
        _UpdateFeature(pRow2, rb, true, me);
        rowcol.add(rb);
        pSrcFeatureClass.updateRows(rowcol, false);

        //���µ�����_UpdateDesFeatures  ���������pRow
        var rowcol2 = __g.new_RowBufferCollection;
        _UpdateFeature(pRow, pRow2, false, me); //���µ�����_UpdateDesFeatures
        rowcol2.add(pRow2);
        me.m_pDesObjectClass.updateRows(rowcol2, false);
                
        //4��������ά
		__g.featureManager.editFeature(pSrcFeatureClass, rb);
        __g.featureManager.unhighlightAll();
        __g.featureManager.highlightFeature(pSrcFeatureClass, me.m_idToDel, 0xffff0064);
    }