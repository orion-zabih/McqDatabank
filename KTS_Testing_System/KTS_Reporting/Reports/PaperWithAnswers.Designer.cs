namespace KTS_Reporting.Reports
{
    partial class PaperWithAnswers
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.TableGroup tableGroup1 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup2 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup3 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup4 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup5 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter2 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            this.pageHeaderSection1 = new Telerik.Reporting.PageHeaderSection();
            this.textBox15 = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.panel3 = new Telerik.Reporting.Panel();
            this.textBox17 = new Telerik.Reporting.TextBox();
            this.table1 = new Telerik.Reporting.Table();
            this.panel1 = new Telerik.Reporting.Panel();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox12 = new Telerik.Reporting.TextBox();
            this.panel8 = new Telerik.Reporting.Panel();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.panel2 = new Telerik.Reporting.Panel();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox16 = new Telerik.Reporting.TextBox();
            this.textBox18 = new Telerik.Reporting.TextBox();
            this.textBox21 = new Telerik.Reporting.TextBox();
            this.textBox20 = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.reportHeaderSection1 = new Telerik.Reporting.ReportHeaderSection();
            this.objectDataSource1 = new Telerik.Reporting.ObjectDataSource();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeaderSection1
            // 
            this.pageHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.30000001192092896D);
            this.pageHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox15});
            this.pageHeaderSection1.Name = "pageHeaderSection1";
            // 
            // textBox15
            // 
            this.textBox15.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.179999828338623D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.25D), Telerik.Reporting.Drawing.Unit.Inch(0.30000001192092896D));
            this.textBox15.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox15.Style.Font.Name = "Engravers MT";
            this.textBox15.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(16D);
            this.textBox15.Value = "=Fields.version_character";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(0.52999997138977051D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.panel3,
            this.table1});
            this.detail.Name = "detail";
            // 
            // panel3
            // 
            this.panel3.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox17});
            this.panel3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9365557313431054E-05D), Telerik.Reporting.Drawing.Unit.Inch(7.8731114626862109E-05D));
            this.panel3.Name = "panel3";
            this.panel3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.5D), Telerik.Reporting.Drawing.Unit.Inch(0.17000000178813934D));
            this.panel3.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.panel3.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Point(1D);
            // 
            // textBox17
            // 
            this.textBox17.Docking = Telerik.Reporting.DockingStyle.Fill;
            this.textBox17.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.5D), Telerik.Reporting.Drawing.Unit.Inch(0.17000000178813934D));
            this.textBox17.Style.Font.Bold = true;
            this.textBox17.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox17.Value = "=Fields.test_subject";
            // 
            // table1
            // 
            this.table1.Bindings.Add(new Telerik.Reporting.Binding("DataSource", "=ReportItem.DataObject.listQuestions"));
            this.table1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(0.24999998509883881D)));
            this.table1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(6.0199995040893555D)));
            this.table1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(0.22999997437000275D)));
            this.table1.Body.Rows.Add(new Telerik.Reporting.TableBodyRow(Telerik.Reporting.Drawing.Unit.Pixel(33.599998474121094D)));
            this.table1.Body.SetCellContent(0, 1, this.panel1);
            this.table1.Body.SetCellContent(0, 0, this.textBox16);
            this.table1.Body.SetCellContent(0, 2, this.textBox18);
            tableGroup1.Name = "group2";
            tableGroup2.Name = "tableGroup";
            tableGroup3.Name = "group";
            this.table1.ColumnGroups.Add(tableGroup1);
            this.table1.ColumnGroups.Add(tableGroup2);
            this.table1.ColumnGroups.Add(tableGroup3);
            this.table1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox16,
            this.panel1,
            this.textBox18});
            this.table1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0.18000000715255737D));
            this.table1.Name = "table1";
            tableGroup5.Name = "group1";
            tableGroup4.ChildGroups.Add(tableGroup5);
            tableGroup4.Groupings.Add(new Telerik.Reporting.Grouping(null));
            tableGroup4.Name = "detailTableGroup";
            this.table1.RowGroups.Add(tableGroup4);
            this.table1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.4999995231628418D), Telerik.Reporting.Drawing.Unit.Inch(0.34999999403953552D));
            // 
            // panel1
            // 
            this.panel1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.panel8,
            this.panel2});
            this.panel1.Name = "panel1";
            this.panel1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.0199995040893555D), Telerik.Reporting.Drawing.Unit.Pixel(33.599998474121094D));
            this.panel1.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.panel1.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.panel1.StyleName = "";
            // 
            // textBox5
            // 
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.25D), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05D));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.25D), Telerik.Reporting.Drawing.Unit.Inch(0.125960573554039D));
            this.textBox5.Value = "=Fields.objAnswers.option1alphabet";
            // 
            // textBox12
            // 
            this.textBox12.Anchoring = ((Telerik.Reporting.AnchoringStyles)((Telerik.Reporting.AnchoringStyles.Top | Telerik.Reporting.AnchoringStyles.Bottom)));
            this.textBox12.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.5145841836929321D), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05D));
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.49375057220459D), Telerik.Reporting.Drawing.Unit.Inch(0.17000001668930054D));
            this.textBox12.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox12.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox12.Style.BorderWidth.Left = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox12.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox12.Value = "=Fields.objAnswers.option1";
            // 
            // panel8
            // 
            this.panel8.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox9,
            this.textBox5,
            this.textBox12});
            this.panel8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0.18000000715255737D));
            this.panel8.Name = "panel8";
            this.panel8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.0199999809265137D), Telerik.Reporting.Drawing.Unit.Inch(0.17000000178813934D));
            this.panel8.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            // 
            // textBox9
            // 
            this.textBox9.Anchoring = ((Telerik.Reporting.AnchoringStyles)((Telerik.Reporting.AnchoringStyles.Top | Telerik.Reporting.AnchoringStyles.Bottom)));
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2499998807907105D), Telerik.Reporting.Drawing.Unit.Inch(0.17000001668930054D));
            this.textBox9.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox9.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox9.Style.BorderWidth.Left = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox9.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox9.Style.Font.Bold = true;
            this.textBox9.Value = "Correct Answer :- ";
            // 
            // panel2
            // 
            this.panel2.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox2});
            this.panel2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05D), Telerik.Reporting.Drawing.Unit.Inch(0.0010000000474974513D));
            this.panel2.Name = "panel2";
            this.panel2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.0199999809265137D), Telerik.Reporting.Drawing.Unit.Inch(0.17000000178813934D));
            this.panel2.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.panel2.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Point(0D);
            // 
            // textBox2
            // 
            this.textBox2.Docking = Telerik.Reporting.DockingStyle.Fill;
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.0199999809265137D), Telerik.Reporting.Drawing.Unit.Inch(0.17000000178813934D));
            this.textBox2.Value = "=Fields.question_desc";
            // 
            // textBox16
            // 
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.24999998509883881D), Telerik.Reporting.Drawing.Unit.Pixel(33.599998474121094D));
            this.textBox16.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox16.Style.BorderWidth.Right = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox16.StyleName = "";
            this.textBox16.Value = "=Fields.serial_no";
            // 
            // textBox18
            // 
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.22999997437000275D), Telerik.Reporting.Drawing.Unit.Pixel(33.599998474121094D));
            this.textBox18.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox18.StyleName = "";
            // 
            // textBox21
            // 
            this.textBox21.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.8000000715255737D), Telerik.Reporting.Drawing.Unit.Inch(0.24999989569187164D));
            this.textBox21.Name = "textBox21";
            this.textBox21.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.9000000953674316D), Telerik.Reporting.Drawing.Unit.Inch(0.15000000596046448D));
            this.textBox21.Style.Font.Bold = true;
            this.textBox21.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox21.Value = "(EXAMINATION SECTION)";
            // 
            // textBox20
            // 
            this.textBox20.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.8000000715255737D), Telerik.Reporting.Drawing.Unit.Inch(0.1000000610947609D));
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.9000000953674316D), Telerik.Reporting.Drawing.Unit.Inch(0.15000000596046448D));
            this.textBox20.Style.Font.Bold = true;
            this.textBox20.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox20.Value = "KHYBER MEDICAL COLLEGE PESHAWAR";
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.7645841836929321D), Telerik.Reporting.Drawing.Unit.Inch(0.3999999463558197D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.0999999046325684D), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985D));
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox1.Value = "=Fields.test_description";
            // 
            // reportHeaderSection1
            // 
            this.reportHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.67000001668930054D);
            this.reportHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox20,
            this.textBox21,
            this.textBox1});
            this.reportHeaderSection1.Name = "reportHeaderSection1";
            // 
            // objectDataSource1
            // 
            this.objectDataSource1.DataMember = "GetPaperWithAnswers";
            this.objectDataSource1.DataSource = typeof(KTS_Reporting.ReportEngine);
            this.objectDataSource1.Name = "objectDataSource1";
            this.objectDataSource1.Parameters.AddRange(new Telerik.Reporting.ObjectDataSourceParameter[] {
            new Telerik.Reporting.ObjectDataSourceParameter("test_id", typeof(decimal), "= Parameters.testid.Value"),
            new Telerik.Reporting.ObjectDataSourceParameter("version", typeof(int), "= Parameters.version.Value")});
            // 
            // PaperWithAnswers
            // 
            this.DataSource = this.objectDataSource1;
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeaderSection1,
            this.detail,
            this.reportHeaderSection1});
            this.Name = "PaperWithAnswers";
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            reportParameter1.Name = "testid";
            reportParameter1.Text = "testid";
            reportParameter1.Type = Telerik.Reporting.ReportParameterType.Float;
            reportParameter2.Name = "version";
            reportParameter2.Text = "version";
            reportParameter2.Type = Telerik.Reporting.ReportParameterType.Integer;
            this.ReportParameters.Add(reportParameter1);
            this.ReportParameters.Add(reportParameter2);
            styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.TextItemBase)),
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.HtmlTextBox))});
            styleRule1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2D);
            styleRule1.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(2D);
            this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            styleRule1});
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(6.5D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeaderSection1;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.ObjectDataSource objectDataSource1;
        private Telerik.Reporting.TextBox textBox15;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox20;
        private Telerik.Reporting.TextBox textBox21;
        private Telerik.Reporting.Panel panel3;
        private Telerik.Reporting.TextBox textBox17;
        private Telerik.Reporting.ReportHeaderSection reportHeaderSection1;
        private Telerik.Reporting.Table table1;
        private Telerik.Reporting.Panel panel1;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox12;
        private Telerik.Reporting.Panel panel8;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.Panel panel2;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox16;
        private Telerik.Reporting.TextBox textBox18;
    }
}