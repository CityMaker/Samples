var __g;
var __fcMap = {};     //key: guid, value: fc
var __fcGeoMap = {};  //key: guid, value: geoNames[]
var __fds;
var __rootId;
var __fc;
var __fl;
var __dataspatialCRS;

function getSamplesPath() {
    var path = __g.internalTool.getRuntimeTempPath() + "Gvitech\\SampleMedia";
    return path;
}

function getSamplesRelatePath(relPath){
    return (getSamplesPath() + relPath).replace(/\//g, "\\");
}


/************************************************************************/
/* 初始化三维控件，并设置天空
/************************************************************************/
function initAxControl() {
    __g = $("renderControl");  // 兼容Firefox

    var lic = __g.new_LicenseServer;
	if(lic.enableTrial) //7.1的ComServer无此属性
	    lic.enableTrial();

    // 初始化RenderControl控件
    if(!__g.project.create(null)){
        alert("create project failed!");
        return false;
    }

    __g.camera.flyTime = 0;
    __rootId = __g.objectManager.getProjectTree().rootID; //也可直接用字符串"11111111-1111-1111-1111-111111111111"

    // 设置天空盒
    var skyboxPath = getSamplesRelatePath("/skybox");
    var skyboxObj = __g.objectManager.getSkyBox(0);
    skyboxObj.setImagePath(gviSkyboxImageIndex.gviSkyboxImageBack, skyboxPath + "/1_BK.jpg");
    skyboxObj.setImagePath(gviSkyboxImageIndex.gviSkyboxImageBottom, skyboxPath + "/1_DN.jpg");
    skyboxObj.setImagePath(gviSkyboxImageIndex.gviSkyboxImageFront, skyboxPath + "/1_FR.jpg");
    skyboxObj.setImagePath(gviSkyboxImageIndex.gviSkyboxImageLeft, skyboxPath + "/1_LF.jpg");
    skyboxObj.setImagePath(gviSkyboxImageIndex.gviSkyboxImageRight, skyboxPath + "/1_RT.jpg");
    skyboxObj.setImagePath(gviSkyboxImageIndex.gviSkyboxImageTop, skyboxPath + "/1_UP.jpg");

    // 绑定RenderControl事件
    initControlEvents(__g);

    return true;
}

/************************************************************************/
/* 加载FDB场景
/************************************************************************/
function loadFdb(fileName, textRender, geoRender) { 

    // 加载FDB场景
    var c = __g.new_ConnectionInfo;
    c.connectionType = gviConnectionType.gviConnectionFireBird2x;
    c.database = getSamplesRelatePath("/") + fileName;
    try {
        var ds = __g.dataSourceFactory.openDataSource(c);
        var fdsNames = ds.getFeatureDatasetNames();
        if (fdsNames.length == 0)
            return false;
        __fds = ds.openFeatureDataset(fdsNames[0]);
        __dataspatialCRS = __fds.spatialReference;
        var fcNames = __fds.getNamesByType(gviDataSetType.gviDataSetFeatureClassTable);
        if (fcNames.length == 0)
            return false;

        for (var i = 0; i < fcNames.length; i++) {
            var fc = __fds.openFeatureClass(fcNames[i]);
            __fc = fc;
            // 找到FC里面的所有空间列字段
            var geoNames = [];
            var fieldinfos = fc.getFields();
            for (var j = 0; j < fieldinfos.count; j++) {
                var fi = fieldinfos.get(j);
                if (fi && fi.geometryDef)
                    geoNames.push(fi.name);
            }
            __fcMap[fc.guid] = fc;
            __fcGeoMap[fc.guid] = geoNames;
        }
    }
    catch (e) {
        exceptionHandler(e);
    }

    // CreateFeautureLayer
    var hasfly = false;
    for (var fcId in __fcGeoMap) {
        var fc = __fcMap[fcId];
        var geoNames = __fcGeoMap[fcId];
        for (var k = 0; k < geoNames.length; k++) {
            var geoName = geoNames[k];
            var fl = __g.objectManager.createFeatureLayer(fc, geoName, textRender, geoRender, __rootId);
			__fl=fl;
            if (!hasfly) {
                var fieldinfos = fc.getFields();
                var fi = fieldinfos.get(fieldinfos.indexOf(geoName));
                var env = fi.geometryDef.envelope;
                if (env.maxX == 0.0 && env.maxY == 0.0 && env.maxZ == 0.0 &&
                        env.minX == 0.0 && env.minY == 0.0 && env.minZ == 0.0)
                    continue;

                var pos = __g.new_Vector3;
                var ang = __g.new_EulerAngle;
                pos.set((env.maxX + env.minX) / 2, (env.maxY + env.minY) / 2, (env.maxZ + env.minZ) / 2);
                ang.heading = 0;
                ang.tilt = -20;
                __g.camera.lookAt(pos, 600, ang);

                hasfly = true;
            }
        }
    }
}

/************************************************************************/
/* 加载server场景
/************************************************************************/
function loadServer(c, textRender, geoRender) {
    try {
        var ds = __g.dataSourceFactory.openDataSource(c);
        var fdsNames = ds.getFeatureDatasetNames();
        if (fdsNames.length == 0)
            return false;
        __fds = ds.openFeatureDataset(fdsNames[0]);

        var fcNames = __fds.getNamesByType(gviDataSetType.gviDataSetFeatureClassTable);
        if (fcNames.length == 0)
            return false;

        for (var i = 0; i < fcNames.length; i++) {
            var fc = __fds.openFeatureClass(fcNames[i]);

            // 找到FC里面的所有空间列字段
            var geoNames = [];
            var fieldinfos = fc.getFields();
            for (var j = 0; j < fieldinfos.count; j++) {
                var fi = fieldinfos.get(j);
                if (fi && fi.geometryDef)
                    geoNames.push(fi.name);
            }
            __fcMap[fc.guid] = fc;
            __fcGeoMap[fc.guid] = geoNames;
        }
    }
    catch (e) {
        exceptionHandler(e);
    }

    // CreateFeautureLayer
    var hasfly = false;
    for (var fcId in __fcGeoMap) {
        var fc = __fcMap[fcId];
        var geoNames = __fcGeoMap[fcId];
        for (var k = 0; k < geoNames.length; k++) {
            var geoName = geoNames[k];
            var fl = __g.objectManager.createFeatureLayer(fc, geoName, textRender, geoRender, __rootId);

            if (!hasfly) {
                var fieldinfos = fc.getFields();
                var fi = fieldinfos.get(fieldinfos.indexOf(geoName));
                var env = fi.geometryDef.envelope;
                if (env.maxX == 0.0 && env.maxY == 0.0 && env.maxZ == 0.0 &&
                        env.minX == 0.0 && env.minY == 0.0 && env.minZ == 0.0)
                    continue;

                var pos = __g.new_Vector3;
                var ang = __g.new_EulerAngle;
                pos.set((env.maxX + env.minX) / 2, (env.maxY + env.minY) / 2, (env.maxZ + env.minZ) / 2);
                ang.heading = 0;
                ang.tilt = -20;
                __g.camera.lookAt(pos, 600, ang);

                hasfly = true;
            }
        }
    }
}