namespace KTS_Reporting.Reports
{
    partial class Paper
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Paper));
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter2 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            this.detail = new Telerik.Reporting.DetailSection();
            this.table1 = new Telerik.Reporting.Table();
            this.panel1 = new Telerik.Reporting.Panel();
            this.panel5 = new Telerik.Reporting.Panel();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox12 = new Telerik.Reporting.TextBox();
            this.panel8 = new Telerik.Reporting.Panel();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.panel2 = new Telerik.Reporting.Panel();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox16 = new Telerik.Reporting.TextBox();
            this.textBox18 = new Telerik.Reporting.TextBox();
            this.panel3 = new Telerik.Reporting.Panel();
            this.textBox17 = new Telerik.Reporting.TextBox();
            this.pageHeaderSection1 = new Telerik.Reporting.PageHeaderSection();
            this.textBox15 = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.reportHeaderSection1 = new Telerik.Reporting.ReportHeaderSection();
            this.textBox19 = new Telerik.Reporting.TextBox();
            this.shape1 = new Telerik.Reporting.Shape();
            this.textBox20 = new Telerik.Reporting.TextBox();
            this.textBox21 = new Telerik.Reporting.TextBox();
            this.textBox22 = new Telerik.Reporting.TextBox();
            this.textBox23 = new Telerik.Reporting.TextBox();
            this.shape2 = new Telerik.Reporting.Shape();
            this.textBox24 = new Telerik.Reporting.TextBox();
            this.textBox25 = new Telerik.Reporting.TextBox();
            this.shape3 = new Telerik.Reporting.Shape();
            this.textBox26 = new Telerik.Reporting.TextBox();
            this.textBox27 = new Telerik.Reporting.TextBox();
            this.textBox28 = new Telerik.Reporting.TextBox();
            this.objectDataSource1 = new Telerik.Reporting.ObjectDataSource();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(0.72000002861022949D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.table1,
            this.panel3});
            this.detail.Name = "detail";
            this.detail.PageBreak = Telerik.Reporting.PageBreak.None;
            this.detail.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.detail.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(3D);
            // 
            // table1
            // 
            this.table1.Bindings.Add(new Telerik.Reporting.Binding("DataSource", "=ReportItem.DataObject.listQuestions"));
            this.table1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(0.25000002980232239D)));
            this.table1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(6.0199995040893555D)));
            this.table1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(0.22999997437000275D)));
            this.table1.Body.Rows.Add(new Telerik.Reporting.TableBodyRow(Telerik.Reporting.Drawing.Unit.Pixel(50.8800048828125D)));
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
            this.table1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9365557313431054E-05D), Telerik.Reporting.Drawing.Unit.Inch(0.18000000715255737D));
            this.table1.Name = "table1";
            tableGroup5.Name = "group1";
            tableGroup4.ChildGroups.Add(tableGroup5);
            tableGroup4.Groupings.Add(new Telerik.Reporting.Grouping(null));
            tableGroup4.Name = "detailTableGroup";
            this.table1.RowGroups.Add(tableGroup4);
            this.table1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.4999995231628418D), Telerik.Reporting.Drawing.Unit.Inch(0.53000003099441528D));
            // 
            // panel1
            // 
            this.panel1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.panel5,
            this.panel8,
            this.panel2});
            this.panel1.Name = "panel1";
            this.panel1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.0199999809265137D), Telerik.Reporting.Drawing.Unit.Pixel(50.880001068115234D));
            this.panel1.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.panel1.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.panel1.StyleName = "";
            // 
            // panel5
            // 
            this.panel5.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox5,
            this.textBox12,
            this.textBox6,
            this.textBox13,
            this.textBox7,
            this.textBox14});
            this.panel5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0.36000001430511475D));
            this.panel5.Name = "panel5";
            this.panel5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.0199999809265137D), Telerik.Reporting.Drawing.Unit.Inch(0.17000000178813934D));
            this.panel5.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.panel5.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.panel5.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(0D);
            // 
            // textBox5
            // 
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9630467654205859E-05D), Telerik.Reporting.Drawing.Unit.Inch(3.9312573790084571E-05D));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.18000000715255737D), Telerik.Reporting.Drawing.Unit.Inch(0.125960573554039D));
            this.textBox5.Value = "=Fields.objAnswers.option4alphabet";
            // 
            // textBox12
            // 
            this.textBox12.Anchoring = ((Telerik.Reporting.AnchoringStyles)((Telerik.Reporting.AnchoringStyles.Top | Telerik.Reporting.AnchoringStyles.Bottom)));
            this.textBox12.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.18011829257011414D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.8200000524520874D), Telerik.Reporting.Drawing.Unit.Inch(0.17000001668930054D));
            this.textBox12.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox12.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox12.Style.BorderWidth.Left = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox12.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox12.Value = "=Fields.objAnswers.option4";
            // 
            // panel8
            // 
            this.panel8.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox3,
            this.textBox9,
            this.textBox4,
            this.textBox10,
            this.textBox8,
            this.textBox11});
            this.panel8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0.18000000715255737D));
            this.panel8.Name = "panel8";
            this.panel8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.0199999809265137D), Telerik.Reporting.Drawing.Unit.Inch(0.17000000178813934D));
            this.panel8.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            // 
            // textBox9
            // 
            this.textBox9.Anchoring = ((Telerik.Reporting.AnchoringStyles)((Telerik.Reporting.AnchoringStyles.Top | Telerik.Reporting.AnchoringStyles.Bottom)));
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.18000000715255737D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.8200000524520874D), Telerik.Reporting.Drawing.Unit.Inch(0.17000001668930054D));
            this.textBox9.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox9.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox9.Style.BorderWidth.Left = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox9.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox9.Value = "=Fields.objAnswers.option1";
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
            this.textBox16.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.24999998509883881D), Telerik.Reporting.Drawing.Unit.Pixel(50.880001068115234D));
            this.textBox16.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox16.Style.BorderWidth.Right = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox16.StyleName = "";
            this.textBox16.Value = "= Fields.serial_no";
            // 
            // textBox18
            // 
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.23000000417232513D), Telerik.Reporting.Drawing.Unit.Pixel(50.880001068115234D));
            this.textBox18.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox18.StyleName = "";
            // 
            // panel3
            // 
            this.panel3.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox17});
            this.panel3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.8731114626862109E-05D), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05D));
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
            this.textBox17.Value = "= Fields.test_subject";
            // 
            // pageHeaderSection1
            // 
            this.pageHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.29999995231628418D);
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
            this.textBox15.Value = "= Fields.version_character";
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.7000001668930054D), Telerik.Reporting.Drawing.Unit.Inch(0.60000002384185791D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.0999999046325684D), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985D));
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox1.Value = "= Fields.test_description";
            // 
            // reportHeaderSection1
            // 
            this.reportHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(2.2100000381469727D);
            this.reportHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox1,
            this.textBox19,
            this.shape1,
            this.textBox20,
            this.textBox21,
            this.textBox22,
            this.textBox23,
            this.shape2,
            this.textBox24,
            this.textBox25,
            this.shape3,
            this.textBox26,
            this.textBox27,
            this.textBox28});
            this.reportHeaderSection1.Name = "reportHeaderSection1";
            // 
            // textBox19
            // 
            this.textBox19.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.8600001335144043D), Telerik.Reporting.Drawing.Unit.Inch(0.1000000610947609D));
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.699999988079071D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.textBox19.Style.Font.Bold = true;
            this.textBox19.Value = "Class No: ";
            // 
            // shape1
            // 
            this.shape1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.559999942779541D), Telerik.Reporting.Drawing.Unit.Inch(0.18999999761581421D));
            this.shape1.Name = "shape1";
            this.shape1.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.64999997615814209D), Telerik.Reporting.Drawing.Unit.Inch(0.1111111119389534D));
            // 
            // textBox20
            // 
            this.textBox20.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.7999999523162842D), Telerik.Reporting.Drawing.Unit.Inch(0.30000001192092896D));
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.9000000953674316D), Telerik.Reporting.Drawing.Unit.Inch(0.15000000596046448D));
            this.textBox20.Style.Font.Bold = true;
            this.textBox20.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox20.Value = "KHYBER MEDICAL COLLEGE PESHAWAR";
            // 
            // textBox21
            // 
            this.textBox21.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.8000000715255737D), Telerik.Reporting.Drawing.Unit.Inch(0.44999998807907104D));
            this.textBox21.Name = "textBox21";
            this.textBox21.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.9000000953674316D), Telerik.Reporting.Drawing.Unit.Inch(0.15000000596046448D));
            this.textBox21.Style.Font.Bold = true;
            this.textBox21.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox21.Value = "(EXAMINATION SECTION)";
            // 
            // textBox22
            // 
            this.textBox22.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.29999998211860657D), Telerik.Reporting.Drawing.Unit.Inch(0.89999997615814209D));
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.89999997615814209D), Telerik.Reporting.Drawing.Unit.Inch(0.15000000596046448D));
            this.textBox22.Style.Font.Bold = true;
            this.textBox22.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.textBox22.Value = "Time Allowed:";
            // 
            // textBox23
            // 
            this.textBox23.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.1000003814697266D), Telerik.Reporting.Drawing.Unit.Inch(0.89999997615814209D));
            this.textBox23.Name = "textBox23";
            this.textBox23.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.74000000953674316D), Telerik.Reporting.Drawing.Unit.Inch(0.15000000596046448D));
            this.textBox23.Style.Font.Bold = true;
            this.textBox23.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.textBox23.Value = "Max Marks:";
            // 
            // shape2
            // 
            this.shape2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.29999998211860657D), Telerik.Reporting.Drawing.Unit.Inch(1.0499999523162842D));
            this.shape2.Name = "shape2";
            this.shape2.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.9000000953674316D), Telerik.Reporting.Drawing.Unit.Inch(0.070000000298023224D));
            this.shape2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            // 
            // textBox24
            // 
            this.textBox24.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.30000001192092896D), Telerik.Reporting.Drawing.Unit.Inch(1.1200000047683716D));
            this.textBox24.Name = "textBox24";
            this.textBox24.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D), Telerik.Reporting.Drawing.Unit.Inch(0.11999999731779099D));
            this.textBox24.Style.Font.Bold = true;
            this.textBox24.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.textBox24.Value = "Note:";
            // 
            // textBox25
            // 
            this.textBox25.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.30000001192092896D), Telerik.Reporting.Drawing.Unit.Inch(1.2400000095367432D));
            this.textBox25.Name = "textBox25";
            this.textBox25.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.0999999046325684D), Telerik.Reporting.Drawing.Unit.Inch(0.43999999761581421D));
            this.textBox25.Style.Font.Bold = true;
            this.textBox25.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.textBox25.Value = resources.GetString("textBox25.Value");
            // 
            // shape3
            // 
            this.shape3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.30000004172325134D), Telerik.Reporting.Drawing.Unit.Inch(1.6799999475479126D));
            this.shape3.Name = "shape3";
            this.shape3.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.9000000953674316D), Telerik.Reporting.Drawing.Unit.Inch(0.070000000298023224D));
            this.shape3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            // 
            // textBox26
            // 
            this.textBox26.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.30000004172325134D), Telerik.Reporting.Drawing.Unit.Inch(1.75D));
            this.textBox26.Name = "textBox26";
            this.textBox26.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.9000000953674316D), Telerik.Reporting.Drawing.Unit.Inch(0.46000000834465027D));
            this.textBox26.Style.Font.Bold = true;
            this.textBox26.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.textBox26.Value = resources.GetString("textBox26.Value");
            // 
            // textBox27
            // 
            this.textBox27.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.2000788450241089D), Telerik.Reporting.Drawing.Unit.Inch(0.89999997615814209D));
            this.textBox27.Name = "textBox27";
            this.textBox27.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.23996102809906D), Telerik.Reporting.Drawing.Unit.Inch(0.15000000596046448D));
            this.textBox27.Style.Font.Bold = true;
            this.textBox27.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.textBox27.Value = "= Fields.total_time_mins";
            // 
            // textBox28
            // 
            this.textBox28.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.8400783538818359D), Telerik.Reporting.Drawing.Unit.Inch(0.89999997615814209D));
            this.textBox28.Name = "textBox28";
            this.textBox28.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.50208371877670288D), Telerik.Reporting.Drawing.Unit.Inch(0.15000000596046448D));
            this.textBox28.Style.Font.Bold = true;
            this.textBox28.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.textBox28.Value = "= Fields.total_marks";
            // 
            // objectDataSource1
            // 
            this.objectDataSource1.DataMember = "GetPaper";
            this.objectDataSource1.DataSource = typeof(KTS_Reporting.ReportEngine);
            this.objectDataSource1.Name = "objectDataSource1";
            this.objectDataSource1.Parameters.AddRange(new Telerik.Reporting.ObjectDataSourceParameter[] {
            new Telerik.Reporting.ObjectDataSourceParameter("test_id", typeof(decimal), "= Parameters.testid.Value"),
            new Telerik.Reporting.ObjectDataSourceParameter("version", typeof(int), "= Parameters.version.Value")});
            // 
            // textBox6
            // 
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.0099999904632568D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.18000000715255737D), Telerik.Reporting.Drawing.Unit.Inch(0.125960573554039D));
            this.textBox6.Value = "=Fields.objAnswers.option5alphabet";
            // 
            // textBox13
            // 
            this.textBox13.Anchoring = ((Telerik.Reporting.AnchoringStyles)((Telerik.Reporting.AnchoringStyles.Top | Telerik.Reporting.AnchoringStyles.Bottom)));
            this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.2000000476837158D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.8200000524520874D), Telerik.Reporting.Drawing.Unit.Inch(0.17000001668930054D));
            this.textBox13.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox13.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox13.Style.BorderWidth.Left = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox13.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox13.Value = "=Fields.objAnswers.option5";
            // 
            // textBox7
            // 
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.0199999809265137D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.18000000715255737D), Telerik.Reporting.Drawing.Unit.Inch(0.125960573554039D));
            this.textBox7.Value = "=Fields.objAnswers.option6alphabet";
            // 
            // textBox14
            // 
            this.textBox14.Anchoring = ((Telerik.Reporting.AnchoringStyles)((Telerik.Reporting.AnchoringStyles.Top | Telerik.Reporting.AnchoringStyles.Bottom)));
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.1999998092651367D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.8200000524520874D), Telerik.Reporting.Drawing.Unit.Inch(0.17000001668930054D));
            this.textBox14.Value = "=Fields.objAnswers.option6";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9365557313431054E-05D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.18000000715255737D), Telerik.Reporting.Drawing.Unit.Inch(0.125960573554039D));
            this.textBox3.Value = "=Fields.objAnswers.option1alphabet";
            // 
            // textBox4
            // 
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.0099999904632568D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.18000000715255737D), Telerik.Reporting.Drawing.Unit.Inch(0.125960573554039D));
            this.textBox4.Value = "=Fields.objAnswers.option2alphabet";
            // 
            // textBox8
            // 
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.0199999809265137D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.18000000715255737D), Telerik.Reporting.Drawing.Unit.Inch(0.125960573554039D));
            this.textBox8.Value = "=Fields.objAnswers.option3alphabet";
            // 
            // textBox10
            // 
            this.textBox10.Anchoring = ((Telerik.Reporting.AnchoringStyles)((Telerik.Reporting.AnchoringStyles.Top | Telerik.Reporting.AnchoringStyles.Bottom)));
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.2000000476837158D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.8200000524520874D), Telerik.Reporting.Drawing.Unit.Inch(0.17000001668930054D));
            this.textBox10.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox10.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox10.Style.BorderWidth.Left = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox10.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox10.Value = "=Fields.objAnswers.option2";
            // 
            // textBox11
            // 
            this.textBox11.Anchoring = ((Telerik.Reporting.AnchoringStyles)((Telerik.Reporting.AnchoringStyles.Top | Telerik.Reporting.AnchoringStyles.Bottom)));
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.1999998092651367D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.8200000524520874D), Telerik.Reporting.Drawing.Unit.Inch(0.17000001668930054D));
            this.textBox11.Value = "=Fields.objAnswers.option3";
            // 
            // Paper
            // 
            this.DataSource = this.objectDataSource1;
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeaderSection1,
            this.detail,
            this.reportHeaderSection1});
            this.Name = "Paper";
            this.PageSettings.ContinuousPaper = false;
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Legal;
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
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(6.4999604225158691D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.ObjectDataSource objectDataSource1;
        private Telerik.Reporting.Table table1;
        private Telerik.Reporting.PageHeaderSection pageHeaderSection1;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.ReportHeaderSection reportHeaderSection1;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox12;
        private Telerik.Reporting.Panel panel1;
        private Telerik.Reporting.Panel panel5;
        private Telerik.Reporting.Panel panel8;
        private Telerik.Reporting.TextBox textBox16;
        private Telerik.Reporting.Panel panel2;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox15;
        private Telerik.Reporting.Panel panel3;
        private Telerik.Reporting.TextBox textBox17;
        private Telerik.Reporting.TextBox textBox18;
        private Telerik.Reporting.TextBox textBox19;
        private Telerik.Reporting.Shape shape1;
        private Telerik.Reporting.TextBox textBox20;
        private Telerik.Reporting.TextBox textBox21;
        private Telerik.Reporting.TextBox textBox22;
        private Telerik.Reporting.TextBox textBox23;
        private Telerik.Reporting.Shape shape2;
        private Telerik.Reporting.TextBox textBox24;
        private Telerik.Reporting.TextBox textBox25;
        private Telerik.Reporting.Shape shape3;
        private Telerik.Reporting.TextBox textBox26;
        private Telerik.Reporting.TextBox textBox27;
        private Telerik.Reporting.TextBox textBox28;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox11;
    }
}