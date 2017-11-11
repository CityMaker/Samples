using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Resource;
using Gvitech.CityMaker.Common;
using System.Collections;

namespace ImportModel
{
    class ImportMDB : Import
    {
        private string _ConnStr = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source='";
        private string _QueryModelStr = "SELECT `Block Name`,`Layer`, " +
                                    "`X insertion point` ,`Y insertion point` ,`Z insertion point` ," +
                                    "`Rotation`,`Rotation Direction X`,`Rotation Direction Y`,`Rotation Direction Z` ," +
                                    "`X scale`,`Y scale`,`Z scale` ," +
                                    "`UTrans angle`,`UTrans Direction X`,`UTrans Direction Y`,`UTrans Direction Z` " +
                                    " FROM Attribute_Table_CityManager";

        public bool CheckMDBFile(string strFileName, out int nTotalCount)
        {
            string strConn = _ConnStr + strFileName + "\'";
            bool bContain = false;
            int nCount = -1;
            try
            {
                using (OleDbConnection oleConn = new OleDbConnection(strConn))
                {
                    oleConn.Open();
                    DataTable dt = oleConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables,
                                        new object[] { null, null, null, "TABLE" });
                    if (dt != null)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            string strTableName = row[2].ToString();
                            if (strTableName == "Attribute_Table_CityManager")
                            {
                                string strSql = "select count(*)  from Attribute_Table_CityManager";
                                OleDbCommand countCmd = new OleDbCommand(strSql, oleConn);
                                OleDbDataReader countReader = countCmd.ExecuteReader();
                                countReader.Read();
                                nCount = countReader.GetInt32(0);

                                bContain = true;
                                break;
                            }
                        }
                    }
                    oleConn.Close();
                }
                if (!bContain)
                {
                    MessageBox.Show(strFileName + "格式不符合要求!");
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            nTotalCount = nCount;

            return bContain;
        }

        public override bool ImportModel(IFeatureClass fc, string file, int groupId, int nCount)
        {
            CommonEntity.RenderEntity.FeatureManager.UnhighlightAll();
            CommonEntity.RenderEntity.PauseRendering(false);
            int beginOid = GetMaxOID(fc);
            fc.SetRenderIndexEnabled("Geometry", false);
            // 核心代码
            ImportModelMdb(fc, file, groupId);
            fc.SetRenderIndexEnabled("Geometry", true);
            fc.RebuildRenderIndex("Geometry", gviRenderIndexRebuildType.gviRenderIndexRebuildWithData);
            CommonEntity.RenderEntity.FeatureManager.RefreshFeatureClass(fc);
            CommonEntity.RenderEntity.ResumeRendering();
            SetCamera(fc, beginOid);

            return true;
        }

        public void ImportModelMdb(IFeatureClass fc, string mdbFile, int groupId)
        {
            try
            {
                //2、获取基本信息
                FileInfo fInfo = new FileInfo(mdbFile);
                string strFilePath = fInfo.DirectoryName;

                Gvitech.CityMaker.Resource.IResourceFactory rf = new Gvitech.CityMaker.Resource.ResourceFactory();
                IResourceManager rm = fc.FeatureDataSet as IResourceManager;
                Dictionary<string, IEnvelope> cacheModeInfos = new Dictionary<string, IEnvelope>();
                List<string> cacheImageNames = new List<string>();

                IGeometryFactory geoFactory = new GeometryFactory();
                IResourceFactory symbolFac = new ResourceFactory();

                IRowBufferCollection fcRows = new RowBufferCollection();
                IRowBuffer fcRow = null;

                //4、解析mdb
                string strConn = _ConnStr + mdbFile + "\'";
                int index = 0;
                int totalCount = 0;
                using (OleDbConnection oleConn = new OleDbConnection(strConn))
                {
                    oleConn.Open();
                    string strSql = "select count(*)  from Attribute_Table_CityManager";
                    OleDbCommand countCmd = new OleDbCommand(strSql, oleConn);
                    OleDbDataReader countReader = countCmd.ExecuteReader();
                    countReader.Read();
                    totalCount = countReader.GetInt32(0);
                    countReader.Close();

                    OleDbCommand command = new OleDbCommand(_QueryModelStr, oleConn);
                    OleDbDataReader reader = command.ExecuteReader();
                    IModelPoint modePoint = null;
                    while (reader.Read())
                    {
                        int percent = ++index * 100 / totalCount;
                        string toolTip = string.Format("已完成{0}条/总共{1}条 {2}%", index, totalCount, percent);
                        CommonEntity.FormEntity.Text = toolTip;

                        double dScaleX = 0.0;
                        double dScaleY = 0.0;
                        double dScaleZ = 0.0;

                        double.TryParse(reader.GetString(9), out dScaleX);
                        double.TryParse(reader.GetString(10), out dScaleY);
                        double.TryParse(reader.GetString(11), out dScaleZ);

                        //if (dScaleX < 0 || dScaleY < 0 || dScaleZ < 0)
                        //    continue;

                        //fc
                        fcRow = fc.CreateRowBuffer();
                        int nPose = fcRow.FieldIndex("name");
                        if (nPose == -1)
                            continue;
                        string modelName = reader.GetString(0);
                        fcRow.SetValue(nPose, modelName);

                        nPose = fcRow.FieldIndex("groupId");
                        if (nPose == -1)
                            continue;
                        fcRow.SetValue(nPose, groupId);

                        modePoint = (IModelPoint)geoFactory.CreateGeometry(
                                        gviGeometryType.gviGeometryModelPoint,
                                        gviVertexAttribute.gviVertexAttributeZ);
                        modePoint.ModelName = modelName;

                        double dModePointX = double.Parse(reader.GetString(2)) + 0;  // CoorX = 0
                        double dModePointY = double.Parse(reader.GetString(3)) + 0;  // CoorY = 0
                        double dModePointZ = double.Parse(reader.GetString(4)) + 0;  // CoorZ = 0

                        double dRotationAngle = double.Parse(reader.GetString(5));
                        double dAxisX = double.Parse(reader.GetString(6));
                        double dAxisY = double.Parse(reader.GetString(7));
                        double dAxisZ = double.Parse(reader.GetString(8));

                        double dUTransAngel = double.Parse(reader.GetString(12));
                        double dUTransX = double.Parse(reader.GetString(13));
                        double dUTransY = double.Parse(reader.GetString(14));
                        double dUTransZ = double.Parse(reader.GetString(15));

                        modePoint.X = dModePointX;
                        modePoint.Y = dModePointY;
                        modePoint.Z = dModePointZ;

                        IMatrix matrix = new Matrix();

                        IVector3 pointVec = new Vector3();
                        pointVec.X = dModePointX;
                        pointVec.Y = dModePointY;
                        pointVec.Z = dModePointZ;

                        IVector3 Scale = new Vector3();
                        Scale.X = dScaleX;
                        Scale.Y = dScaleY;
                        Scale.Z = dScaleZ;

                        IVector3 Rotation = new Vector3();
                        Rotation.X = dAxisX;
                        Rotation.Y = dAxisY;
                        Rotation.Z = dAxisZ;

                        IVector3 Shear = new Vector3();
                        Shear.X = dUTransX;
                        Shear.Y = dUTransY;
                        Shear.Z = dUTransZ;

                        matrix.Compose2(pointVec, Scale, dRotationAngle, Rotation, dUTransAngel, Shear);
                        modePoint.FromMatrix(matrix);

                        //mc
                        IPropertySet Images = null;

                        //如果内存中不包含
                        if (!cacheModeInfos.Keys.Contains(modelName))
                        {
                            //数据库中包含
                            if (rm.ModelExist(modelName))
                            {
                                //重名即跳过
                                IEnvelope ev = rm.GetModel(modelName).Envelope;
                                modePoint.ModelEnvelope = ev;
                            }
                            else
                            {
                                IModel simpleModel = null;
                                string osgFilePath = strFilePath + "\\" + modelName + ".osg";
                                IModel fineModel = null;
                                IMatrix m = null;
                                symbolFac.CreateModelAndImageFromFileEx(osgFilePath,
                                                             out Images, out simpleModel, out fineModel, out m);
                                if (fineModel == null || fineModel.GroupCount == 0)
                                    continue;

                                cacheModeInfos[modelName] = modePoint.ModelEnvelope = Clone(fineModel.Envelope);

                                rm.AddModel(modelName, fineModel, simpleModel);
      
                            }
                        }
                        else
                        {
                            modePoint.ModelEnvelope = cacheModeInfos[modelName];
                        }

                        nPose = fcRow.FieldIndex("Geometry");
                        fcRow.SetValue(nPose, modePoint);
                        fcRows.Add(fcRow);

                        //tc
                        if (Images != null)
                        {
                            int nCount = Images.Count;
                            if (nCount > 0)
                            {
                                Hashtable htImages = Images.AsHashtable();
                                foreach (DictionaryEntry item in htImages)
                                {
                                    string imgName = item.Key.ToString();
                                    IImage img = item.Value as IImage;
                                    if (img == null)
                                        continue;
                                    if (string.IsNullOrEmpty(imgName))
                                        continue;

                                    //如果内存中不包含
                                    if (!cacheImageNames.Contains(imgName))
                                    {
                                        //数据库中包含
                                        if (rm.ImageExist(imgName))
                                        {
                                            //重名即跳过                                    
                                        }
                                    }

                                    //如果内存中不包含，数据库中也不包含
                                    if (!rm.ImageExist(imgName) &&
                                        !cacheImageNames.Contains(imgName))
                                    {
                                        cacheImageNames.Add(imgName);
                                        rm.AddImage(imgName, img);
                                    }
                                }
                            }
                        }
                        //end

                        if (fcRows.Count >= 10)
                        {
                            InsertFeatures(fc as IObjectClass, fcRows);
                            fcRows.Clear();
                        }
                    }
                    reader.Close();
                    oleConn.Close();
                }

                if (fcRows.Count > 0)
                {
                    InsertFeatures(fc as IObjectClass, fcRows);
                    fcRows.Clear();
                }
            }
            catch (System.Exception ex)
            {
                if (ex.GetType().Name.Equals("UnauthorizedAccessException"))
                    MessageBox.Show("需要标准runtime授权");
                else
                    MessageBox.Show(ex.Message);
            }
        }
    }
}
