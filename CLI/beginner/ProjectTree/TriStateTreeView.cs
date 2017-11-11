using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace ProjectTree
{
    public partial class TriStateTreeView : TreeView
    {
        public const int STATE_UNCHECKED = 0; //unchecked state
        public const int STATE_CHECKED = 1; //checked state
        public const int STATE_MIXED = 2; //mixed state (indeterminate)

        //create a new ThreeStateTreeView
        public TriStateTreeView()
            : base()
        {

        }

        public void UpdateView()
        {
            this.SetNodesState(this.Nodes);
        }

        public void SelectAll()
        {
            this.SetNodesState(this.Nodes, STATE_CHECKED);
        }

        public void UnselectAll()
        {
            this.SetNodesState(this.Nodes, STATE_UNCHECKED);
        }

        public void AntiSelect()
        {
            this.SetNodesToAntiState(this.Nodes);
        }

        //returns a value indicating if all children are checked
        public static bool IsAllChildrenChecked(TreeNode parent)
        {
            return IsAllChildrenSame(parent, STATE_CHECKED);
        }

        //returns a value indicating if all children are unchecked
        public static bool IsAllChildrenUnchecked(TreeNode parent)
        {
            return IsAllChildrenSame(parent, STATE_UNCHECKED);
        }

        //initialize all nodes state image
        private void SetNodesState(TreeNodeCollection nodes, int state)
        {
            foreach (TreeNode node in nodes)
            {
                node.StateImageIndex = state;
                if (state == STATE_CHECKED)
                {
                    node.Checked = true;
                }
                else
                {
                    node.Checked = false;
                }

                if (node.Nodes.Count != 0)
                {
                    SetNodesState(node.Nodes, state);
                }
            }
        }

        private void SetNodesState(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Checked)
                {
                    node.StateImageIndex = STATE_CHECKED;
                }
                else
                {
                    node.StateImageIndex = STATE_UNCHECKED;
                }
                UpdateParent(node);

                if (node.Nodes.Count != 0)
                {
                    SetNodesState(node.Nodes);
                }
            }
        }

        private void SetNodesToAntiState(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Checked)
                {
                    node.StateImageIndex = STATE_UNCHECKED;
                    node.Checked = false;
                }
                else
                {
                    node.StateImageIndex = STATE_CHECKED;
                    node.Checked = true;
                }
                UpdateParent(node);

                if (node.Nodes.Count != 0)
                {
                    SetNodesToAntiState(node.Nodes);
                }
            }
        }

        //update children state image with the parent value
        private void UpdateChildren(TreeNode parent)
        {
            int state = parent.StateImageIndex;
            foreach (TreeNode node in parent.Nodes)
            {
                node.StateImageIndex = state;
                if (node.Nodes.Count != 0)
                {
                    UpdateChildren(node);
                }
            }
        }

        //update parent state image base on the children state
        private void UpdateParent(TreeNode child)
        {
            TreeNode parent = child.Parent;
            if (parent == null)
            {
                return;
            }

            if (child.StateImageIndex == STATE_MIXED)
            {
                parent.StateImageIndex = STATE_MIXED;
            }
            else if (IsAllChildrenChecked(parent))
            {
                parent.StateImageIndex = STATE_CHECKED;
            }
            else if (IsAllChildrenUnchecked(parent))
            {
                parent.StateImageIndex = STATE_UNCHECKED;
            }
            else
            {
                parent.StateImageIndex = STATE_MIXED;
            }
            UpdateParent(parent);
        }

        //returns a value indicating if all children are in the same state
        private static bool IsAllChildrenSame(TreeNode parent, int state)
        {
            foreach (TreeNode node in parent.Nodes)
            {
                if (node.StateImageIndex != state)
                {
                    return false;
                }
                if (node.Nodes.Count != 0 && !IsAllChildrenSame(node, state))
                {
                    return false;
                }
            }
            return true;
        }

        //build the checked, unchecked and indeterminate images
        private static Image GetStateImage(CheckBoxState state, Size imageSize)
        {
            Bitmap bmp = new Bitmap(16, 16);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                Point pt = new Point((16 - imageSize.Width) / 2, (16 - imageSize.Height) / 2);
                CheckBoxRenderer.DrawCheckBox(g, pt, state);
            }
            return bmp;
        }


        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            this.StateImageList = new ImageList();
            using (Graphics g = base.CreateGraphics())
            {
                Size glyphSize = CheckBoxRenderer.GetGlyphSize(g, CheckBoxState.UncheckedNormal);
                this.StateImageList.Images.Add(GetStateImage(CheckBoxState.UncheckedNormal, glyphSize));
                this.StateImageList.Images.Add(GetStateImage(CheckBoxState.CheckedNormal, glyphSize));
                this.StateImageList.Images.Add(GetStateImage(CheckBoxState.MixedNormal, glyphSize));
                this.StateImageList.Images.Add(GetStateImage(CheckBoxState.UncheckedDisabled, glyphSize));
                this.StateImageList.Images.Add(GetStateImage(CheckBoxState.CheckedDisabled, glyphSize));
                this.StateImageList.Images.Add(GetStateImage(CheckBoxState.MixedDisabled, glyphSize));
            }
        }

        //check if user click on the state image
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (e.Button == MouseButtons.Left)
            {
                TreeViewHitTestInfo info = base.HitTest(e.Location);
                if (info.Node != null && info.Location == TreeViewHitTestLocations.StateImage)
                {
                    TreeNode node = info.Node;
                    switch (node.StateImageIndex)
                    {
                        case STATE_UNCHECKED:
                        case STATE_MIXED:
                            node.StateImageIndex = STATE_CHECKED;
                            break;
                        case STATE_CHECKED:
                            node.StateImageIndex = STATE_UNCHECKED;
                            break;
                    }
                    UpdateChildren(node);
                    UpdateParent(node);
                }
            }
        }

        //check if user press the space key
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Space)
            {
                if (base.SelectedNode != null)
                {
                    TreeNode node = base.SelectedNode;
                    switch (node.StateImageIndex)
                    {
                        case STATE_UNCHECKED:
                        case STATE_MIXED:
                            node.StateImageIndex = STATE_CHECKED;
                            break;
                        case STATE_CHECKED:
                            node.StateImageIndex = STATE_UNCHECKED;
                            break;
                    }
                    UpdateChildren(node);
                    UpdateParent(node);
                }
            }
        }

        //swap between enabled and disabled images
        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);

            for (int i = 0; i < 3; i++)
            {
                Image img = this.StateImageList.Images[0];
                this.StateImageList.Images.RemoveAt(0);
                this.StateImageList.Images.Add(img);
            }
        }
    }
}
