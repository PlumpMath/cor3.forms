/* User: oIo * Date: 5/18/2010 * Time: 3:20 PM */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace System.Cor3.Forms
{
	public class MemoPanel2 : UserControl
	{
		int _lim = 255;
		public int MaxLength { get { return _lim; } set { _lim = value; } }
		
		public bool ShowNavigator { get { return this.bindingNavigator1.Visible; } set { this.bindingNavigator1.Visible = value; } }
		public bool ShowToolStrip { get { return this.toolStrip1.Visible; } set { this.toolStrip1.Visible = value; } }
		public bool ShowCategoryButton { get { return this.btn_category_dropdown.Visible; } set { this.btn_category_dropdown.Visible = value; } }
		public bool ShowNewButton { get { return this.btn_new.Visible; } set { this.btn_new.Visible = value; } }
		public bool ShowNavButton { get { return this.btn_nav.Visible; } set { this.btn_nav.Visible = value; } }
//		public bool ShowProperties { get { return this.btn_nav.Visible; } set { this.btn_nav.Visible = value; } }

		void e_apply_clicked(object sender, EventArgs e)
		{
			
		}
//		public event EventHandler Validity;
		static public Color ColorInvalidInput = Color.FromArgb(240,80,80);
	
		public MemoPanel2() : base()
		{
			InitializeComponent();
		}
	
		#region Design
		void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Cor3.Forms.ToolStripGradientSetting gradientSetting1 = new System.Cor3.Forms.ToolStripGradientSetting();
			System.Cor3.Forms.ToolStripGradientSetting gradientSetting2 = new System.Cor3.Forms.ToolStripGradientSetting();
			System.Cor3.Forms.ToolStripGradientSetting gradientSetting3 = new System.Cor3.Forms.ToolStripGradientSetting();
			this.tMemo = new System.Cor3.Forms.rtf.RichEditorUser();
			this.toolStrip1 = new System.Cor3.Forms.ToolStripGradientControl();
			this.btn_new = new System.Windows.Forms.ToolStripDropDownButton();
			this.weDidntAssertCategoriesJustYetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.btn_category_dropdown = new System.Windows.Forms.ToolStripDropDownButton();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.btn_nav = new System.Windows.Forms.ToolStripSplitButton();
			this.tCaption = new System.Windows.Forms.RichTextBox();
			this.bindingNavigator1 = new System.Windows.Forms.BindingNavigator(this.components);
			this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
			this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
			this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
			this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
			this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
			this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
			this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
			this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
			this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).BeginInit();
			this.bindingNavigator1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tMemo
			// 
			this.tMemo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tMemo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tMemo.Location = new System.Drawing.Point(0, 45);
			this.tMemo.Name = "tMemo";
			this.tMemo.ShowProperties = true;
			this.tMemo.Size = new System.Drawing.Size(443, 198);
			this.tMemo.TabIndex = 3;
			// 
			// toolStrip1
			// 
			this.toolStrip1.AutoSize = false;
			this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
			this.toolStrip1.ForeGroundShadow = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(178)))), ((int)(((byte)(178)))), ((int)(((byte)(178)))));
			gradientSetting1.Colour = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			gradientSetting1.Offset = 0F;
			gradientSetting2.Colour = System.Drawing.Color.White;
			gradientSetting2.Offset = 0.25F;
			gradientSetting3.Colour = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			gradientSetting3.Offset = 1F;
			this.toolStrip1.Gradients.Add(gradientSetting1);
			this.toolStrip1.Gradients.Add(gradientSetting2);
			this.toolStrip1.Gradients.Add(gradientSetting3);
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.btn_new,
									this.btn_category_dropdown,
									this.toolStripButton1,
									this.btn_nav});
			this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip1.Size = new System.Drawing.Size(443, 24);
			this.toolStrip1.TabIndex = 5;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// btn_new
			// 
			this.btn_new.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.weDidntAssertCategoriesJustYetToolStripMenuItem});
			this.btn_new.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btn_new.Margin = new System.Windows.Forms.Padding(0);
			this.btn_new.Name = "btn_new";
			this.btn_new.Size = new System.Drawing.Size(66, 24);
			this.btn_new.Text = "New Memo";
			// 
			// weDidntAssertCategoriesJustYetToolStripMenuItem
			// 
			this.weDidntAssertCategoriesJustYetToolStripMenuItem.Name = "weDidntAssertCategoriesJustYetToolStripMenuItem";
			this.weDidntAssertCategoriesJustYetToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
			this.weDidntAssertCategoriesJustYetToolStripMenuItem.Text = "(we didn\'t assert categories just yet)";
			// 
			// btn_category_dropdown
			// 
			this.btn_category_dropdown.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btn_category_dropdown.Margin = new System.Windows.Forms.Padding(0);
			this.btn_category_dropdown.Name = "btn_category_dropdown";
			this.btn_category_dropdown.Size = new System.Drawing.Size(56, 24);
			this.btn_category_dropdown.Text = "Category";
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(29, 21);
			this.toolStripButton1.Text = "Save";
			this.toolStripButton1.Click += new System.EventHandler(this.e_apply_clicked);
			// 
			// btn_nav
			// 
			this.btn_nav.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.btn_nav.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btn_nav.Margin = new System.Windows.Forms.Padding(0);
			this.btn_nav.Name = "btn_nav";
			this.btn_nav.Size = new System.Drawing.Size(57, 24);
			this.btn_nav.Text = "Navigate";
			// 
			// tCaption
			// 
			this.tCaption.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.tCaption.DetectUrls = false;
			this.tCaption.Dock = System.Windows.Forms.DockStyle.Top;
			this.tCaption.EnableAutoDragDrop = true;
			this.tCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tCaption.Location = new System.Drawing.Point(0, 24);
			this.tCaption.Multiline = false;
			this.tCaption.Name = "tCaption";
			this.tCaption.ShowSelectionMargin = true;
			this.tCaption.Size = new System.Drawing.Size(443, 21);
			this.tCaption.TabIndex = 6;
			this.tCaption.Text = "Some Demo";
			this.tCaption.WordWrap = false;
			// 
			// bindingNavigator1
			// 
			this.bindingNavigator1.AddNewItem = this.bindingNavigatorAddNewItem;
			this.bindingNavigator1.BackColor = System.Drawing.Color.Transparent;
			this.bindingNavigator1.CountItem = this.bindingNavigatorCountItem;
			this.bindingNavigator1.DeleteItem = this.bindingNavigatorDeleteItem;
			this.bindingNavigator1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.bindingNavigator1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.bindingNavigator1.ImageScalingSize = new System.Drawing.Size(12, 12);
			this.bindingNavigator1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.bindingNavigatorMoveFirstItem,
									this.bindingNavigatorMovePreviousItem,
									this.bindingNavigatorSeparator,
									this.bindingNavigatorPositionItem,
									this.bindingNavigatorCountItem,
									this.bindingNavigatorSeparator1,
									this.bindingNavigatorMoveNextItem,
									this.bindingNavigatorMoveLastItem,
									this.bindingNavigatorSeparator2,
									this.bindingNavigatorAddNewItem,
									this.bindingNavigatorDeleteItem});
			this.bindingNavigator1.Location = new System.Drawing.Point(0, 243);
			this.bindingNavigator1.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
			this.bindingNavigator1.MoveLastItem = this.bindingNavigatorMoveLastItem;
			this.bindingNavigator1.MoveNextItem = this.bindingNavigatorMoveNextItem;
			this.bindingNavigator1.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
			this.bindingNavigator1.Name = "bindingNavigator1";
			this.bindingNavigator1.PositionItem = this.bindingNavigatorPositionItem;
			this.bindingNavigator1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.bindingNavigator1.Size = new System.Drawing.Size(443, 25);
			this.bindingNavigator1.TabIndex = 7;
			this.bindingNavigator1.Text = "bindingNavigator1";
			// 
			// bindingNavigatorAddNewItem
			// 
			this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
			this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
			this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
			this.bindingNavigatorAddNewItem.Text = "Add new";
			// 
			// bindingNavigatorCountItem
			// 
			this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
			this.bindingNavigatorCountItem.Size = new System.Drawing.Size(31, 22);
			this.bindingNavigatorCountItem.Text = "of {0}";
			this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
			// 
			// bindingNavigatorDeleteItem
			// 
			this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
			this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
			this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 22);
			this.bindingNavigatorDeleteItem.Text = "Delete";
			// 
			// bindingNavigatorMoveFirstItem
			// 
			this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
			this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
			this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
			this.bindingNavigatorMoveFirstItem.Text = "Move first";
			// 
			// bindingNavigatorMovePreviousItem
			// 
			this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
			this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
			this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
			this.bindingNavigatorMovePreviousItem.Text = "Move previous";
			// 
			// bindingNavigatorSeparator
			// 
			this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
			this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
			// 
			// bindingNavigatorPositionItem
			// 
			this.bindingNavigatorPositionItem.AccessibleName = "Position";
			this.bindingNavigatorPositionItem.AutoSize = false;
			this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
			this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 21);
			this.bindingNavigatorPositionItem.Text = "0";
			this.bindingNavigatorPositionItem.ToolTipText = "Current position";
			// 
			// bindingNavigatorSeparator1
			// 
			this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
			this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// bindingNavigatorMoveNextItem
			// 
			this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
			this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
			this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
			this.bindingNavigatorMoveNextItem.Text = "Move next";
			// 
			// bindingNavigatorMoveLastItem
			// 
			this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
			this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
			this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
			this.bindingNavigatorMoveLastItem.Text = "Move last";
			// 
			// bindingNavigatorSeparator2
			// 
			this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
			this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// MemoPanel2
			// 
			this.Controls.Add(this.tMemo);
			this.Controls.Add(this.bindingNavigator1);
			this.Controls.Add(this.tCaption);
			this.Controls.Add(this.toolStrip1);
			this.Name = "MemoPanel2";
			this.Size = new System.Drawing.Size(443, 268);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).EndInit();
			this.bindingNavigator1.ResumeLayout(false);
			this.bindingNavigator1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
		private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
		private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
		private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
		private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
		private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
		private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
		private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
		private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
		private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
		private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.BindingNavigator bindingNavigator1;
		private System.Windows.Forms.ToolStripDropDownButton btn_category_dropdown;
		public System.Cor3.Forms.rtf.RichEditorUser tMemo;
		public System.Windows.Forms.RichTextBox tCaption;
		private System.Windows.Forms.ToolStripMenuItem weDidntAssertCategoriesJustYetToolStripMenuItem;
		private System.Windows.Forms.ToolStripDropDownButton btn_new;
		private System.Windows.Forms.ToolStripSplitButton btn_nav;
		private System.Cor3.Forms.ToolStripGradientControl toolStrip1;
		#endregion
		
		
	}
}
