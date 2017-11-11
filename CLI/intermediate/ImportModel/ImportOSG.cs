using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Data;
using System.IO;
using System.Windows.Forms;

using Gvitech.CityMaker.Common;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Resource;
using System.Collections;

namespace ImportModel
{
    class ImportOSG : Import
    {
        public override bool ImportModel(IFeatureClass fc, string file, int groupId, int nCount)
        {
            CommonEntity.RenderEntity.FeatureManager.UnhighlightAll();
            CommonEntity.RenderEntity.PauseRendering(false);
            int beginOid = GetMaxOID(fc);
            fc.SetRenderIndexEnabled("Geometry", false);
            // 核心代码
            ImportModelOsg(fc, file, groupId);
            fc.SetRenderIndexEnabled("Geometry", true);
            fc.RebuildRenderIndex("Geometry", gviRenderIndexRebuildType.gviRenderIndexRebuildWithData);
            CommonEntity.RenderEntity.FeatureManager.RefreshFeatureClass(fc);
            CommonEntity.RenderEntity.ResumeRendering();
            SetCamera(fc, beginOid);

            return true;
        }

        public void ImportModelOsg(IFeatureClass fc, string osgFile, int groupId)
        {
            try
            {
                FileInfo fInfo = new FileInfo(osgFile);
                string strFilePath = fInfo.DirectoryName;
                IResourceManager rm = fc.FeatureDataSet as IResourceManager;
                IGeometryFactory geoFactory = new GeometryFactory();
                IResourceFactory symbolFac = new ResourceFactory();
                IRowBuffer fcRow = null;

                //fc
                string modelName = fInfo.Name.Split('.')[0];
                fcRow = fc.CreateRowBuffer();
                int nPose = fcRow.FieldIndex("name");
                if (nPose != -1)
                {
                    fcRow.SetValue(nPose, modelName);
                }
                nPose = fcRow.FieldIndex("groupId");
                if (nPose != -1)
                {
                    fcRow.SetValue(nPose, groupId);
                }                

                //mc
                IPropertySet Images = null;
                IModel simpleModel = null;
                string osgFilePath = strFilePath + "\\" + modelName + ".osg";
                IModel fineModel = null;
                IMatrix matrix = null;
                symbolFac.CreateModelAndImageFromFileEx(osgFilePath,
                                             out Images, out simpleModel, out fineModel, out matrix);
                if (fineModel == null || fineModel.GroupCount == 0)
                    return;  
                rm.AddModel(modelName, fineModel, simpleModel);
                //Marshal.ReleaseComObject(simpleModel);
                //Marshal.ReleaseComObject(fineModel);

                //tc
                int nCount = Images.Count;
                if (Images != null && nCount > 0)
                {
                    Hashtable htImages = Images.AsHashtable();
                    foreach (DictionaryEntry item in htImages)
                    {
                        String imgName = item.Key.ToString();
                        IImage img = item.Value as IImage;
                        rm.AddImage(imgName, img);
                    }
                }

                //modelpoint
                IModelPoint modePoint = null;
                modePoint = (IModelPoint)geoFactory.CreateGeometry(
                                gviGeometryType.gviGeometryModelPoint,
                                gviVertexAttribute.gviVertexAttributeZ);
                modePoint.FromMatrix(matrix);
                modePoint.ModelName = modelName;
                double dModePointX = 0.0;
                double dModePointY = 0.0;
                double dModePointZ = 0.0;
                modePoint.X += dModePointX;
                modePoint.Y += dModePointY;
                modePoint.Z += dModePointZ;
                nPose = fcRow.FieldIndex("Geometry");
                fcRow.SetValue(nPose, modePoint);

                if (fc.HasTemporal())
                {
                    ITemporalManager mgr = fc.TemporalManager;
                    mgr.Insert(DateTime.Now, fcRow);                   
                    //Marshal.ReleaseComObject(mgr);
                }
                else
                {
                    IRowBufferCollection fcRows = new RowBufferCollection();
                    fcRows.Add(fcRow);
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