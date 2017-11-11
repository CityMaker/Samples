using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Gvitech.CityMaker.Common;
using Gvitech.CityMaker.Controls;
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Resource;
using Gvitech.CityMaker.Math;
using System.Collections;

namespace DynamicModelPoint
{
    public partial class MainForm : Form
    {
        AxRenderControl RenderControl = null;
        public MainForm()
        {
            RenderControl = new AxRenderControl();
            RenderControl.Dock = DockStyle.Fill;
            this.Controls.Add(RenderControl);
            InitializeComponent();
        }


        Timer t = new Timer();
        List<IRenderModelPoint> list = new List<IRenderModelPoint>();
        List<IMatrix> MatrixList = new List<IMatrix>();
        

        private void MainForm_Load(object sender, EventArgs e)
        {
            RenderControl.Initialize(true, null);
            t.Tick += new EventHandler(t_Tick);
        }

        void t_Tick(object sender, EventArgs e)
        {
            Random r = new Random();
            int i = 0;
            foreach (IRenderModelPoint rmp in list)
            {
                //System.Threading.Thread.Sleep(2);
                int n = r.Next(100);
                if (n > 80)
                {
                    rmp.Highlight(System.Drawing.Color.Red);
                }
                else if (n > 40 && n <= 80)
                {
                    rmp.Highlight(System.Drawing.Color.Yellow);
                }
                else
                {
                    rmp.Highlight(System.Drawing.Color.Blue);
                }
                IModelPoint modelPoint = rmp.GetFdeGeometry() as IModelPoint;
                modelPoint.FromMatrix(MatrixList[i]);
                i++;
                ITransform tf = modelPoint as ITransform;
                tf.Scale3D(1, 1, n, modelPoint.Envelope.Center.X, modelPoint.Envelope.Center.Y, modelPoint.Envelope.MinZ);
                IModelPoint newModelPoint = tf as IModelPoint;
                rmp.SetFdeGeometry(newModelPoint);
            }
        }



        private void toolStripButton_CreateRenderModelPoint_Click(object sender, EventArgs e)
        {
            IModel model = CreateModel(1, 1, 1, 0xffffffff);
            RenderControl.ObjectManager.AddModel("model", model);
            t.Stop();
            foreach (IRenderModelPoint m in list)
            {
                RenderControl.ObjectManager.DeleteObject(m.Guid);
            }
            list.Clear();
            MatrixList.Clear();
            int x = Convert.ToInt32(toolStripTextBox_x.Text.Trim());
            int y = Convert.ToInt32(toolStripTextBox_y.Text.Trim());
            int n = Convert.ToInt32(toolStripTextBox_n.Text.Trim());
            double x1, y1;
            IGeometryFactory gf = new GeometryFactory();
            for (int j = 0; j < x; j++)
            {
                for (int k = 0; k < y; k++)
                {
                    IModelPoint mp = gf.CreateGeometry(gviGeometryType.gviGeometryModelPoint, gviVertexAttribute.gviVertexAttributeZ) as IModelPoint;

                    x1 = n * j;
                    y1 = n * k;
                    mp.X = x1;
                    mp.Y = y1;
                    mp.Z = 0;
                    mp.ModelName = "model";
                    mp.ModelEnvelope = model.Envelope;
                    IRenderModelPoint rmp = RenderControl.ObjectManager.CreateRenderModelPoint(mp, null, RenderControl.ProjectTree.RootID);
                    rmp.MaxVisibleDistance = 99999999999999;
                    rmp.MinVisiblePixels = 0;
                    list.Add(rmp);
                    MatrixList.Add(mp.AsMatrix());
                }
            }
        }
        private IModel CreateModel(float lenght, float width, float height, uint color)
        {
            IModel model = new ResourceFactory().CreateModel();
            IDrawGroup group = new DrawGroup();
            IDrawPrimitive primitive = new DrawPrimitive();
            IFloatArray vertexArray = new FloatArray();
            IFloatArray textureArray = new FloatArray();
            IUInt32Array colorArray = new UInt32Array();

            #region
            //顶点数组3个为一组(三角面1)
            vertexArray.Append(0.0f); vertexArray.Append(0.0f); vertexArray.Append(0.0f);
            vertexArray.Append(lenght); vertexArray.Append(lenght); vertexArray.Append(0.0f);
            vertexArray.Append(lenght); vertexArray.Append(0.0f); vertexArray.Append(0.0f);
            //顶点数组3个为一组(三角面2)
            vertexArray.Append(0.0f); vertexArray.Append(0.0f); vertexArray.Append(0.0f);
            vertexArray.Append(0.0f); vertexArray.Append(lenght); vertexArray.Append(0.0f);
            vertexArray.Append(lenght); vertexArray.Append(lenght); vertexArray.Append(0.0f);
            //顶点数组3个为一组(三角面3)
            vertexArray.Append(lenght); vertexArray.Append(0.0f); vertexArray.Append(0.0f);
            vertexArray.Append(lenght); vertexArray.Append(lenght); vertexArray.Append(0.0f);
            vertexArray.Append(lenght); vertexArray.Append(0.0f); vertexArray.Append(lenght);
            //顶点数组3个为一组(三角面4)
            vertexArray.Append(lenght); vertexArray.Append(lenght); vertexArray.Append(0.0f);
            vertexArray.Append(lenght); vertexArray.Append(lenght); vertexArray.Append(lenght);
            vertexArray.Append(lenght); vertexArray.Append(0.0f); vertexArray.Append(lenght);
            //顶点数组3个为一组(三角面5)
            vertexArray.Append(lenght); vertexArray.Append(lenght); vertexArray.Append(0.0f);
            vertexArray.Append(0.0f); vertexArray.Append(lenght); vertexArray.Append(0.0f);
            vertexArray.Append(lenght); vertexArray.Append(lenght); vertexArray.Append(lenght);
            //顶点数组3个为一组(三角面6)
            vertexArray.Append(0.0f); vertexArray.Append(lenght); vertexArray.Append(0.0f);
            vertexArray.Append(0.0f); vertexArray.Append(lenght); vertexArray.Append(lenght);
            vertexArray.Append(lenght); vertexArray.Append(lenght); vertexArray.Append(lenght);
            //顶点数组3个为一组(三角面7)
            vertexArray.Append(0.0f); vertexArray.Append(0.0f); vertexArray.Append(0.0f);
            vertexArray.Append(0.0f); vertexArray.Append(lenght); vertexArray.Append(lenght);
            vertexArray.Append(0.0f); vertexArray.Append(lenght); vertexArray.Append(0.0f);
            //顶点数组3个为一组(三角面8)
            vertexArray.Append(0.0f); vertexArray.Append(0.0f); vertexArray.Append(0.0f);
            vertexArray.Append(0.0f); vertexArray.Append(0.0f); vertexArray.Append(lenght);
            vertexArray.Append(0.0f); vertexArray.Append(lenght); vertexArray.Append(lenght);
            //顶点数组3个为一组(三角面9)
            vertexArray.Append(0.0f); vertexArray.Append(0.0f); vertexArray.Append(lenght);
            vertexArray.Append(lenght); vertexArray.Append(0.0f); vertexArray.Append(lenght);
            vertexArray.Append(0.0f); vertexArray.Append(lenght); vertexArray.Append(lenght);
            //顶点数组3个为一组(三角面10)
            vertexArray.Append(lenght); vertexArray.Append(0.0f); vertexArray.Append(lenght);
            vertexArray.Append(lenght); vertexArray.Append(lenght); vertexArray.Append(lenght);
            vertexArray.Append(0.0f); vertexArray.Append(lenght); vertexArray.Append(lenght);
            //顶点数组3个为一组(三角面11)
            vertexArray.Append(0.0f); vertexArray.Append(0.0f); vertexArray.Append(0.0f);
            vertexArray.Append(lenght); vertexArray.Append(0.0f); vertexArray.Append(0.0f);
            vertexArray.Append(lenght); vertexArray.Append(0.0f); vertexArray.Append(lenght);
            //顶点数组3个为一组(三角面12)
            vertexArray.Append(0.0f); vertexArray.Append(0.0f); vertexArray.Append(0.0f);
            vertexArray.Append(lenght); vertexArray.Append(0.0f); vertexArray.Append(lenght);
            vertexArray.Append(0.0f); vertexArray.Append(0.0f); vertexArray.Append(lenght);

            #endregion

            #region
            //颜色数组1个为一组
            colorArray.Append(color);
            colorArray.Append(color);
            colorArray.Append(color);
            colorArray.Append(color);
            colorArray.Append(color);
            colorArray.Append(color);
            colorArray.Append(color);
            colorArray.Append(color);
            colorArray.Append(color);
            colorArray.Append(color);
            colorArray.Append(color);
            colorArray.Append(color);
            colorArray.Append(color);
            colorArray.Append(color);
            colorArray.Append(color);
            colorArray.Append(color);
            colorArray.Append(color);
            colorArray.Append(color);
            colorArray.Append(color);
            colorArray.Append(color);
            colorArray.Append(color);
            colorArray.Append(color);
            colorArray.Append(color);
            colorArray.Append(color);
            colorArray.Append(color);
            colorArray.Append(color);
            colorArray.Append(color);
            colorArray.Append(color);
            colorArray.Append(color);
            colorArray.Append(color);
            colorArray.Append(color);
            colorArray.Append(color);
            colorArray.Append(color);
            colorArray.Append(color);
            colorArray.Append(color);
            colorArray.Append(color);
            #endregion

            #region
            //纹理数组2个为一组
            textureArray.Append(0.0f);
            textureArray.Append(0.0f);

            textureArray.Append(1.0f);
            textureArray.Append(0.0f);

            textureArray.Append(1.0f);
            textureArray.Append(1.0f);

            textureArray.Append(0.0f);
            textureArray.Append(0.0f);

            textureArray.Append(1.0f);
            textureArray.Append(1.0f);

            textureArray.Append(0.0f);
            textureArray.Append(1.0f);

            textureArray.Append(0.0f);
            textureArray.Append(0.0f);

            textureArray.Append(1.0f);
            textureArray.Append(0.0f);

            textureArray.Append(1.0f);
            textureArray.Append(1.0f);

            textureArray.Append(0.0f);
            textureArray.Append(0.0f);

            textureArray.Append(1.0f);
            textureArray.Append(1.0f);

            textureArray.Append(0.0f);
            textureArray.Append(1.0f);

            textureArray.Append(0.0f);
            textureArray.Append(0.0f);

            textureArray.Append(1.0f);
            textureArray.Append(0.0f);

            textureArray.Append(1.0f);
            textureArray.Append(1.0f);

            textureArray.Append(0.0f);
            textureArray.Append(0.0f);

            textureArray.Append(1.0f);
            textureArray.Append(1.0f);

            textureArray.Append(0.0f);
            textureArray.Append(1.0f);

            textureArray.Append(0.0f);
            textureArray.Append(0.0f);

            textureArray.Append(1.0f);
            textureArray.Append(0.0f);

            textureArray.Append(1.0f);
            textureArray.Append(1.0f);

            textureArray.Append(0.0f);
            textureArray.Append(0.0f);

            textureArray.Append(1.0f);
            textureArray.Append(1.0f);

            textureArray.Append(0.0f);
            textureArray.Append(1.0f);

            textureArray.Append(0.0f);
            textureArray.Append(0.0f);

            textureArray.Append(1.0f);
            textureArray.Append(0.0f);

            textureArray.Append(1.0f);
            textureArray.Append(1.0f);

            textureArray.Append(0.0f);
            textureArray.Append(0.0f);

            textureArray.Append(1.0f);
            textureArray.Append(0.0f);

            textureArray.Append(0.0f);
            textureArray.Append(1.0f);

            textureArray.Append(0.0f);
            textureArray.Append(0.0f);

            textureArray.Append(1.0f);
            textureArray.Append(0.0f);

            textureArray.Append(1.0f);
            textureArray.Append(1.0f);

            textureArray.Append(0.0f);
            textureArray.Append(0.0f);

            textureArray.Append(1.0f);
            textureArray.Append(1.0f);

            textureArray.Append(0.0f);
            textureArray.Append(1.0f);
            #endregion

            primitive.ColorArray = colorArray;
            primitive.TexcoordArray = textureArray;
            primitive.VertexArray = vertexArray;

            IDrawMaterial material = new DrawMaterial();
            material.EnableLight = true;
            primitive.Material = material;
            group.AddPrimitive(primitive);
            //光照效果，跟法向有关
            group.ComputeNormal();
            model.AddGroup(group);
            return model;
        }

        private void toolStripButton_DynamicModelPoint_Click(object sender, EventArgs e)
        {
            t.Stop();
            int i = Convert.ToInt32(toolStripTextBox_i.Text.Trim());
            t.Interval = i * 1000;
            t.Start();
        }
    }
}
