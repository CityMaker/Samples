var boxname = null;
var pmodel = null;

   var dataSource = null;
    var featureDataSet = null;
    var featureClass = null;
    var featureLayer = null;

function randomChar(len) {
    len = len || 32;
    var $chars = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz2345678';
    var maxPos = $chars.length;
    var pwd = '';
    for(i=0; i<len; i++)
    {
        pwd+=$chars.charAt(Math.floor(Math.random()*maxPos));
    }
    return pwd;
}

function DrawAssisPolygon(intersectPoint, length, width, height) {

    var gfactory = __g.geometryFactory;
    var fde_polygon = gfactory.createGeometry(gviGeometryType.gviGeometryPolygon,
                            gviVertexAttribute.gviVertexAttributeZ);
    var fde_point = gfactory.createGeometry(gviGeometryType.gviGeometryPoint,
                            gviVertexAttribute.gviVertexAttributeZ);
    fde_point.setCoords(intersectPoint.x, intersectPoint.y, intersectPoint.z, 0, 0);
    fde_polygon.exteriorRing.appendPoint(fde_point);
    fde_point.setCoords(intersectPoint.x + width, intersectPoint.y, intersectPoint.z, 0, 0);
    fde_polygon.exteriorRing.appendPoint(fde_point);
    fde_point.setCoords(intersectPoint.x + width, intersectPoint.y + length, intersectPoint.z, 0, 0);
    fde_polygon.exteriorRing.appendPoint(fde_point);
    fde_point.setCoords(intersectPoint.x, intersectPoint.y + length, intersectPoint.z, 0, 0);
    fde_polygon.exteriorRing.appendPoint(fde_point);

    boxname =  randomChar(5);
    var model;
    var modelpoint;
    var b;
    
    pmodel = __g.geometryConvertor.extrudePolygonToModel(fde_polygon,1,height,0,gviRoofType.gviRoofFlat,"","");
    pmodel.modelPoint.modelName = boxname;
    pmodel.modelPoint.spatialCRS = __fds.spatialReference;
    
    __g.objectManager.addModel(boxname, pmodel.model);
    __g.objectManager.createRenderModelPoint(pmodel.modelPoint, null, null);
    SaveRecordToFDB();
}


var firstCreate = false;
function SaveRecordToFDB() {
    var mapObject = __g;
    var ci = mapObject.new_ConnectionInfo;
    ci.connectionType = gviConnectionType.gviConnectionSQLite3;
    ci.database = "";
   //var b =  mapObject.dataSourceFactory.hasDataSource(ci);

   if(firstCreate)
   {
    dataSource = mapObject.dataSourceFactory.openDataSource(ci);
   }else{
     firstCreate = true;
     dataSource = mapObject.dataSourceFactory.createDataSource(ci, "");
   }

    var hasFds = false;   
    var fdsNames = dataSource.getFeatureDatasetNames();
    if (fdsNames.length > 0) {
        for (var i = 0; i < fdsNames.length; i++) {
            var fdsName = fdsNames[i];
            if (fdsName == "态势专题图") {
                hasFds = true;
                break;
            }
        }
    }

    if (hasFds) {
        featureDataSet = dataSource.openFeatureDataset("态势专题图");
    } 
     else {
         var coorSys = mapObject.crsFactory.createFromWKT("UNKNOWNCS[\"unnamed\"]");
         featureDataSet = dataSource.createFeatureDataset("态势专题图", __fds.spatialReference);
     }
    if (featureDataSet) {
        var fcNames = featureDataSet.getNamesByType(gviDataSetType.gviDataSetFeatureClassTable);
        var hasFc = false;
        for (var j = 0; j < fcNames.length; j++) {
            var fcName = fcNames[j];
            if (fcName == "态势专题图") {
                hasFc = true;
                break;
            }
        }
        if (hasFc) {
            if(firstCreate)
            {
                  InsertRecord(featureClass,null,"1");
            }
            else
            {
               featureClass = featureDataSet.openFeatureClass("态势专题图");
            }
        } else {
            var fields = mapObject.new_FieldInfoCollection;
            var field = mapObject.new_FieldInfo;
            field.name = "Geometry";
            field.fieldType = gviFieldType.gviFieldGeometry;
            field.geometryDef = mapObject.new_GeometryDef;
            field.geometryDef.geometryColumnType = gviGeometryColumnType.gviGeometryColumnModelPoint;
            field.registeredRenderIndex = true;
            fields.add(field);
            var field = mapObject.new_FieldInfo;
            field.name = "Type";
            field.fieldType = gviFieldType.gviFieldString;
            fields.add(field);
            featureClass = featureDataSet.createFeatureClass("态势专题图", fields);
        }

        if (featureClass) {
            InsertRecord(featureClass,null,"1");
            
            featureLayer = __g.objectManager.createFeatureLayer(featureClass, "Geometry", null, null);
            __g.camera.flyToObject(featureLayer.guid, 0);   
        }
    }
}

function InsertRecord(featureClass, geometry, type) {
    var cursor = featureClass.insert();
    var row = featureClass.createRowBuffer();
    var polygonType = row.fieldIndex("Type");
    var polygonGeometry = row.fieldIndex("Geometry");
    if ( polygonType >= 0 && polygonGeometry >= 0) {
        row.setValue(polygonType, type);
        row.setValue(polygonGeometry, pmodel.modelPoint.clone2(1));
        featureClass.featureDataSet.addModel(boxname, pmodel.model, null);
       // featureClass.featureDataSet.updateModel("xxxx", pmodel.Model);
    }
    cursor.insertRow(row);
    var id = cursor.lastInsertId;
    cursor.close();
    row.setValue(0, id);
}