using System.Collections.Generic;
using System.Runtime.InteropServices;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.FdeGeometry;

namespace ImportModel
{
    class Import
    {

        public virtual bool ImportModel(IFeatureClass fc, string file, int groupId, int nCount)
        {
            return false;
        }

        public int GetMaxOID(IFeatureClass fc)
        {
            int maxOID = -1;
            if (fc == null)
                return maxOID;
            QueryFilter filter = new QueryFilter();
            filter.AddSubField("max(oid) as MaxID");
            IFdeCursor cur = fc.Search(filter, true);
            IRowBuffer row = cur.NextRow();
            //Marshal.ReleaseComObject(cur);
            if (row == null)
                return maxOID;

            if (row.IsNull(0))
                return maxOID;

            maxOID = int.Parse(row.GetValue(0).ToString());

            return maxOID;
        }

        public IEnvelope Clone(IEnvelope envelop)
        {
            if (envelop == null)
                return null;

            IEnvelope ev = new Envelope();
            ev.MinX = envelop.MinX;
            ev.MaxX = envelop.MaxX;
            ev.MinY = envelop.MinY;
            ev.MaxY = envelop.MaxY;
            ev.MinZ = envelop.MinZ;
            ev.MaxZ = envelop.MaxZ;

            return ev;
        }

        public void InsertFeatures(IObjectClass oc, IRowBufferCollection rows)
        {
            if (oc == null || rows == null || rows.Count == 0)
                return;
            IFdeCursor cursor = null;
            try
            {
                oc.FeatureDataSet.DataSource.StartEditing();
                cursor = oc.Insert();
                for (int i = 0; i < rows.Count; ++i)
                {
                    IRowBuffer row = rows.Get(i);
                    cursor.InsertRow(row);
                    int oid = cursor.LastInsertId;
                    row.SetValue(0, oid);
                }
            }
            catch (COMException ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
            finally
            {
                if (cursor != null)
                    //Marshal.ReleaseComObject(cursor);
                oc.FeatureDataSet.DataSource.StopEditing(true);
                //Marshal.ReleaseComObject(cursor);
            }
        }

        public IRowBufferCollection GetRows(Dictionary<string, IRowBuffer> rows)
        {
            if (rows == null || rows.Count == 0)
                return null;
            IRowBufferCollection rowCollection = new RowBufferCollection();
            foreach (string key in rows.Keys)
            {
                rowCollection.Add(rows[key]);
            }
            return rowCollection;
        }

        public void SetCamera(IFeatureClass fc, int beginOid)
        {
            // 获取外包框进行定位
            double[] extent = new double[7];
            int i = 0;
            int endOid = GetMaxOID(fc);
            QueryFilter filter = new QueryFilter();
            filter.WhereClause = string.Format("oid > {0} and oid <= {1}", beginOid, endOid);
            IRowBuffer fdeRow = null;
            IFdeCursor cursor = fc.Search(filter, true);
            while ((fdeRow = cursor.NextRow()) != null)
            {
                int nPos = fdeRow.FieldIndex("Geometry");
                if (nPos == -1 || fdeRow.IsNull(nPos))
                    continue;
                IGeometry geo = fdeRow.GetValue(nPos) as IGeometry;
                if (geo != null)
                {
                    double[] ev = new double[7];
                    ev[0] = geo.Envelope.MinX;
                    ev[1] = geo.Envelope.MaxX;
                    ev[2] = geo.Envelope.MinY;
                    ev[3] = geo.Envelope.MaxY;
                    ev[4] = geo.Envelope.MinZ;
                    ev[5] = geo.Envelope.MaxZ;
                    double dis = (geo.Envelope.Width > geo.Envelope.Height) ? geo.Envelope.Width : geo.Envelope.Height;
                    dis = (dis > geo.Envelope.Depth) ? dis : geo.Envelope.Depth;
                    ev[6] = dis * 2;

                    if (i == 0)
                    {
                        extent[0] = ev[0];
                        extent[1] = ev[1];
                        extent[2] = ev[2];
                        extent[3] = ev[3];
                        extent[4] = ev[4];
                        extent[5] = ev[5];
                        extent[6] = ev[6];
                    }
                    else
                    {
                        extent[0] = (ev[0] < extent[0]) ? ev[0] : extent[0];
                        extent[1] = (ev[1] > extent[1]) ? ev[1] : extent[1];
                        extent[2] = (ev[2] < extent[2]) ? ev[2] : extent[2];
                        extent[3] = (ev[3] > extent[3]) ? ev[3] : extent[3];
                        extent[4] = (ev[4] < extent[4]) ? ev[4] : extent[4];
                        extent[5] = (ev[5] > extent[5]) ? ev[5] : extent[5];
                        extent[6] = (ev[6] > extent[6]) ? ev[6] : extent[6];
                    }
                    ++i;
                }
                nPos = fdeRow.FieldIndex("oid");
                if (nPos == -1 || fdeRow.IsNull(nPos))
                    continue;
                int nFid = int.Parse(fdeRow.GetValue(0).ToString());
                CommonEntity.RenderEntity.FeatureManager.HighlightFeature(fc, nFid, System.Drawing.Color.Yellow);
            }
            //Marshal.ReleaseComObject(cursor);

            if (extent != null)
            {
                double x = (extent[0] + extent[1]) / 2.0;
                double y = (extent[2] + extent[3]) / 2.0;
                double z = (extent[4] + extent[5]) / 2.0;
                double distance = 300;
                if (extent.Length == 7)
                {
                    distance = extent[6];
                }                
                IVector3 center = new Vector3();
                center.Set(x, y, z);
                IEulerAngle angle = new EulerAngle();
                angle.Set(0, -50, 0);
                CommonEntity.RenderEntity.Camera.LookAt(center, distance, angle);
            }
        }

    }
}
