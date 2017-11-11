using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Data;
using System.IO;
using System.Windows.Forms;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Resource;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.Common;
using System.Collections;

namespace ImportModel
{
    class ImportXML : Import
    {
        private DataTable _dataTable;
        private string _mpSchema =
                                    "<?xml version='1.0' standalone='yes'?>"
                                    + "<NewDataSet>"
                                    + "<xs:schema id='ModelPointClass' xmlns='' xmlns:xs='http://www.w3.org/2001/XMLSchema' xmlns:msdata='urn:schemas-microsoft-com:xml-msdata'>"
                                    + "<xs:element name='ModelPointClass' msdata:IsDataSet='true' msdata:MainDataTable='ModelPoint' msdata:UseCurrentLocale='true'>"
                                      + "<xs:complexType>"
                                      + "<xs:choice minOccurs='0' maxOccurs='unbounded'>"
                                          + "<xs:element name='ModelPoint'>"
                                            + "<xs:complexType>"
                                              + "<xs:sequence>"
                                                + "<xs:element name='ModelName' type='xs:string' minOccurs='0' />"
                                                + "<xs:element name='LocationX' type='xs:double' minOccurs='0' /> "
                                                + "<xs:element name='LocationY' type='xs:double' minOccurs='0' />"
                                                + "<xs:element name='LocationZ' type='xs:double' minOccurs='0' />"
                                                + "<xs:element name='Matrix3'    type='xs:string' minOccurs='0' />"
                                              + "</xs:sequence>"
                                            + "</xs:complexType>"
                                          + "</xs:element>"
                                        + "</xs:choice>"
                                      + "</xs:complexType>"
                                    + "</xs:element>"
                                  + "</xs:schema>"
                                  + "</NewDataSet>";

        public int GetCount(string strFileName)
        {
            int nCount = -1;
            try
            {
                _dataTable = new DataTable();
                _dataTable.ReadXmlSchema(new StringReader(_mpSchema));
                _dataTable.ReadXml(strFileName);
                nCount = _dataTable.Rows.Count;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return nCount;
        }

        public override bool ImportModel(IFeatureClass fc, string file, int groupId, int nCount)
        {
            CommonEntity.RenderEntity.FeatureManager.UnhighlightAll();
            CommonEntity.RenderEntity.PauseRendering(false);
            int beginOid = GetMaxOID(fc);
            fc.SetRenderIndexEnabled("Geometry", false);
            // 核心代码
            ImportModelXml(fc, file, groupId, nCount);  
            fc.SetRenderIndexEnabled("Geometry", true);
            fc.RebuildRenderIndex("Geometry", gviRenderIndexRebuildType.gviRenderIndexRebuildWithData);
            CommonEntity.RenderEntity.FeatureManager.RefreshFeatureClass(fc);
            CommonEntity.RenderEntity.ResumeRendering();
            // 定位相机
            SetCamera(fc, beginOid);

            return true;
        }

        public void ImportModelXml(IFeatureClass fc, string mdbFile, int groupId, int _nCount)
        {
            try
            {
                //2、获取基本信息
                FileInfo fInfo = new FileInfo(mdbFile);
                string strFilePath = fInfo.DirectoryName;
                IResourceManager rm = fc.FeatureDataSet as IResourceManager;
                Dictionary<string, IEnvelope> cacheModeInfos = new Dictionary<string, IEnvelope>();
                List<string> cacheImageNames = new List<string>();

                IGeometryFactory geoFactory = new GeometryFactory();
                IResourceFactory symbolFac = new ResourceFactory();

                IRowBufferCollection fcRows = new RowBufferCollection();
                IRowBuffer fcRow = null;

                //4、解析xml
                string strConn = "" + mdbFile + "\'";
                int index = 0;
                int totalCount = _nCount;
                IModelPoint modePoint = null;
                for (int i = 0; i < _nCount; ++i)
                {
                    int percent = ++index * 100 / totalCount;
                    string toolTip = string.Format("已完成{0}条/总共{1}条 {2}%", index, totalCount, percent);
                    CommonEntity.FormEntity.Text = toolTip;

                    //fc
                    fcRow = fc.CreateRowBuffer();
                    int nPose = fcRow.FieldIndex("name");
                    if (nPose == -1)
                        continue;
                    string modelName = _dataTable.Rows[i]["ModelName"].ToString();
                    fcRow.SetValue(nPose, modelName);

                    nPose = fcRow.FieldIndex("groupId");
                    if (nPose == -1)
                        continue;
                    fcRow.SetValue(nPose, groupId);

                    modePoint = (IModelPoint)geoFactory.CreateGeometry(
                                    gviGeometryType.gviGeometryModelPoint,
                                    gviVertexAttribute.gviVertexAttributeZ);
                    modePoint.ModelName = modelName;

                    double dModePointX = double.Parse(_dataTable.Rows[i]["LocationX"].ToString()) + 0;  // CoorX = 0
                    double dModePointY = double.Parse(_dataTable.Rows[i]["LocationY"].ToString()) + 0;  // CoorY = 0
                    double dModePointZ = double.Parse(_dataTable.Rows[i]["LocationZ"].ToString()) + 0;  // CoorZ = 0

                    string strMatrix = _dataTable.Rows[i]["Matrix3"].ToString();
                    float[] Matrix = null;
                    if (!string.IsNullOrEmpty(strMatrix))
                    {
                        string[] strArray = strMatrix.Split(',');
                        if (strArray.Length == 9)
                        {
                            Matrix = new float[9];
                            Matrix[0] = float.Parse(strArray[0]);
                            Matrix[1] = float.Parse(strArray[1]);
                            Matrix[2] = float.Parse(strArray[2]);
                            Matrix[3] = float.Parse(strArray[3]);
                            Matrix[4] = float.Parse(strArray[4]);
                            Matrix[5] = float.Parse(strArray[5]);
                            Matrix[6] = float.Parse(strArray[6]);
                            Matrix[7] = float.Parse(strArray[7]);
                            Matrix[8] = float.Parse(strArray[8]);
                        }
                    }
                    modePoint.X = dModePointX;
                    modePoint.Y = dModePointY;
                    modePoint.Z = dModePointZ;
                    modePoint.Matrix33 = Matrix;


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
                            IMatrix matrix = null;
                            symbolFac.CreateModelAndImageFromFileEx(osgFilePath,
                                                         out Images, out simpleModel, out fineModel, out matrix);
                            if (fineModel == null || fineModel.GroupCount == 0)
                                continue;

                            cacheModeInfos[modelName] = modePoint.ModelEnvelope = Clone(fineModel.Envelope);

                            rm.AddModel(modelName, fineModel, simpleModel);
                            //Marshal.ReleaseComObject(simpleModel);
                            //Marshal.ReleaseComObject(fineModel);
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
                    int nCount = Images.Count;
                    if (Images != null && nCount > 0)
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
                    //end

                    if (fcRows.Count >= 10)
                    {
                        InsertFeatures(fc as IObjectClass, fcRows);
                        fcRows.Clear();
                    }
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
