namespace GoogleMapsApiTester
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnPolygonTest = new System.Windows.Forms.Button();
            this.btnGeocoding1 = new System.Windows.Forms.Button();
            this.btnGeocoding2 = new System.Windows.Forms.Button();
            this.lblZoom = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCenter = new System.Windows.Forms.Label();
            this.btnMarker2 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblLastEvent = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pgPolygon = new System.Windows.Forms.PropertyGrid();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnBoundedGeocoding = new System.Windows.Forms.Button();
            this.btnGeocoding3 = new System.Windows.Forms.Button();
            this.txtGeocoding = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnMarker1 = new System.Windows.Forms.Button();
            this.pgMarker = new System.Windows.Forms.PropertyGrid();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cbShowInfoWindow = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.browser = new System.Windows.Forms.WebBrowser();
            this.pgMapOptions = new System.Windows.Forms.PropertyGrid();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnMapTest = new System.Windows.Forms.Button();
            this.pgStreetViewOptions = new System.Windows.Forms.PropertyGrid();
            this.label4 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.waterReportLng = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.waterReportLat = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.cbLimitMap = new System.Windows.Forms.CheckBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.btnStreetView = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPolygonTest
            // 
            this.btnPolygonTest.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPolygonTest.Location = new System.Drawing.Point(3, 210);
            this.btnPolygonTest.Name = "btnPolygonTest";
            this.btnPolygonTest.Size = new System.Drawing.Size(279, 34);
            this.btnPolygonTest.TabIndex = 1;
            this.btnPolygonTest.Text = "Polygon test";
            this.btnPolygonTest.UseVisualStyleBackColor = true;
            this.btnPolygonTest.Click += new System.EventHandler(this.PolygonTest_Click);
            // 
            // btnGeocoding1
            // 
            this.btnGeocoding1.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnGeocoding1.Location = new System.Drawing.Point(3, 16);
            this.btnGeocoding1.Name = "btnGeocoding1";
            this.btnGeocoding1.Size = new System.Drawing.Size(279, 31);
            this.btnGeocoding1.TabIndex = 2;
            this.btnGeocoding1.Text = "Find Newlands";
            this.btnGeocoding1.UseVisualStyleBackColor = true;
            this.btnGeocoding1.Click += new System.EventHandler(this.GeocodingTest1_Click);
            // 
            // btnGeocoding2
            // 
            this.btnGeocoding2.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnGeocoding2.Location = new System.Drawing.Point(3, 47);
            this.btnGeocoding2.Name = "btnGeocoding2";
            this.btnGeocoding2.Size = new System.Drawing.Size(279, 32);
            this.btnGeocoding2.TabIndex = 3;
            this.btnGeocoding2.Text = "Find Dean Street";
            this.btnGeocoding2.UseVisualStyleBackColor = true;
            this.btnGeocoding2.Click += new System.EventHandler(this.GeocodingTest2_Click);
            // 
            // lblZoom
            // 
            this.lblZoom.AutoSize = true;
            this.lblZoom.Location = new System.Drawing.Point(82, 11);
            this.lblZoom.Name = "lblZoom";
            this.lblZoom.Size = new System.Drawing.Size(44, 13);
            this.lblZoom.TabIndex = 4;
            this.lblZoom.Text = "<zoom>";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(400, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Center:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "ZOOM:";
            // 
            // lblCenter
            // 
            this.lblCenter.AutoSize = true;
            this.lblCenter.Location = new System.Drawing.Point(466, 11);
            this.lblCenter.Name = "lblCenter";
            this.lblCenter.Size = new System.Drawing.Size(49, 13);
            this.lblCenter.TabIndex = 7;
            this.lblCenter.Text = "<center>";
            // 
            // btnMarker2
            // 
            this.btnMarker2.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMarker2.Location = new System.Drawing.Point(3, 185);
            this.btnMarker2.Name = "btnMarker2";
            this.btnMarker2.Size = new System.Drawing.Size(279, 28);
            this.btnMarker2.TabIndex = 8;
            this.btnMarker2.Text = "Custom Icon Marker";
            this.btnMarker2.UseVisualStyleBackColor = true;
            this.btnMarker2.Click += new System.EventHandler(this.MarkerTest2_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblLastEvent);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lblZoom);
            this.panel1.Controls.Add(this.lblCenter);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 765);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1317, 43);
            this.panel1.TabIndex = 9;
            // 
            // lblLastEvent
            // 
            this.lblLastEvent.AutoSize = true;
            this.lblLastEvent.Location = new System.Drawing.Point(969, 10);
            this.lblLastEvent.Name = "lblLastEvent";
            this.lblLastEvent.Size = new System.Drawing.Size(65, 13);
            this.lblLastEvent.TabIndex = 9;
            this.lblLastEvent.Text = "<last event>";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(888, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Last Event:";
            // 
            // pgPolygon
            // 
            this.pgPolygon.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.pgPolygon.Dock = System.Windows.Forms.DockStyle.Top;
            this.pgPolygon.HelpVisible = false;
            this.pgPolygon.LineColor = System.Drawing.SystemColors.ControlDark;
            this.pgPolygon.Location = new System.Drawing.Point(3, 16);
            this.pgPolygon.Name = "pgPolygon";
            this.pgPolygon.Size = new System.Drawing.Size(279, 194);
            this.pgPolygon.TabIndex = 10;
            this.pgPolygon.ToolbarVisible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnPolygonTest);
            this.groupBox1.Controls.Add(this.pgPolygon);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(285, 264);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Polygon";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnBoundedGeocoding);
            this.groupBox2.Controls.Add(this.btnGeocoding3);
            this.groupBox2.Controls.Add(this.txtGeocoding);
            this.groupBox2.Controls.Add(this.btnGeocoding2);
            this.groupBox2.Controls.Add(this.btnGeocoding1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 264);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(285, 197);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Geocoding";
            // 
            // btnBoundedGeocoding
            // 
            this.btnBoundedGeocoding.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnBoundedGeocoding.Location = new System.Drawing.Point(3, 130);
            this.btnBoundedGeocoding.Name = "btnBoundedGeocoding";
            this.btnBoundedGeocoding.Size = new System.Drawing.Size(279, 31);
            this.btnBoundedGeocoding.TabIndex = 6;
            this.btnBoundedGeocoding.Text = "Bounded Geocoding";
            this.btnBoundedGeocoding.UseVisualStyleBackColor = true;
            this.btnBoundedGeocoding.Click += new System.EventHandler(this.GeocodingTest4_Click);
            // 
            // btnGeocoding3
            // 
            this.btnGeocoding3.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnGeocoding3.Location = new System.Drawing.Point(3, 99);
            this.btnGeocoding3.Name = "btnGeocoding3";
            this.btnGeocoding3.Size = new System.Drawing.Size(279, 31);
            this.btnGeocoding3.TabIndex = 5;
            this.btnGeocoding3.Text = "Custom Geocoding";
            this.btnGeocoding3.UseVisualStyleBackColor = true;
            this.btnGeocoding3.Click += new System.EventHandler(this.GeocodingTest3_Click);
            // 
            // txtGeocoding
            // 
            this.txtGeocoding.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtGeocoding.Location = new System.Drawing.Point(3, 79);
            this.txtGeocoding.Name = "txtGeocoding";
            this.txtGeocoding.Size = new System.Drawing.Size(279, 20);
            this.txtGeocoding.TabIndex = 4;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnMarker2);
            this.groupBox3.Controls.Add(this.btnMarker1);
            this.groupBox3.Controls.Add(this.pgMarker);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 461);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(285, 226);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Markers";
            // 
            // btnMarker1
            // 
            this.btnMarker1.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMarker1.Location = new System.Drawing.Point(3, 157);
            this.btnMarker1.Name = "btnMarker1";
            this.btnMarker1.Size = new System.Drawing.Size(279, 28);
            this.btnMarker1.TabIndex = 12;
            this.btnMarker1.Text = "Marker test";
            this.btnMarker1.UseVisualStyleBackColor = true;
            this.btnMarker1.Click += new System.EventHandler(this.MarkerTest1_Click);
            // 
            // pgMarker
            // 
            this.pgMarker.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.pgMarker.Dock = System.Windows.Forms.DockStyle.Top;
            this.pgMarker.HelpVisible = false;
            this.pgMarker.LineColor = System.Drawing.SystemColors.ControlDark;
            this.pgMarker.Location = new System.Drawing.Point(3, 16);
            this.pgMarker.Name = "pgMarker";
            this.pgMarker.Size = new System.Drawing.Size(279, 141);
            this.pgMarker.TabIndex = 11;
            this.pgMarker.ToolbarVisible = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cbShowInfoWindow);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Location = new System.Drawing.Point(0, 687);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(285, 52);
            this.groupBox4.TabIndex = 14;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "InfoWindow";
            // 
            // cbShowInfoWindow
            // 
            this.cbShowInfoWindow.AutoSize = true;
            this.cbShowInfoWindow.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbShowInfoWindow.Location = new System.Drawing.Point(3, 16);
            this.cbShowInfoWindow.Name = "cbShowInfoWindow";
            this.cbShowInfoWindow.Size = new System.Drawing.Size(279, 17);
            this.cbShowInfoWindow.TabIndex = 0;
            this.cbShowInfoWindow.Text = "Show info window on map click";
            this.cbShowInfoWindow.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox4);
            this.panel2.Controls.Add(this.groupBox3);
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(285, 765);
            this.panel2.TabIndex = 15;
            // 
            // browser
            // 
            this.browser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browser.Location = new System.Drawing.Point(564, 0);
            this.browser.MinimumSize = new System.Drawing.Size(20, 20);
            this.browser.Name = "browser";
            this.browser.ScrollBarsEnabled = false;
            this.browser.Size = new System.Drawing.Size(753, 765);
            this.browser.TabIndex = 16;
            // 
            // pgMapOptions
            // 
            this.pgMapOptions.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.pgMapOptions.Dock = System.Windows.Forms.DockStyle.Top;
            this.pgMapOptions.HelpVisible = false;
            this.pgMapOptions.LineColor = System.Drawing.SystemColors.ControlDark;
            this.pgMapOptions.Location = new System.Drawing.Point(3, 16);
            this.pgMapOptions.Name = "pgMapOptions";
            this.pgMapOptions.Size = new System.Drawing.Size(273, 194);
            this.pgMapOptions.TabIndex = 17;
            this.pgMapOptions.ToolbarVisible = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnMapTest);
            this.groupBox5.Controls.Add(this.pgStreetViewOptions);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.pgMapOptions);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox5.Location = new System.Drawing.Point(0, 0);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(279, 461);
            this.groupBox5.TabIndex = 18;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Map Options";
            // 
            // btnMapTest
            // 
            this.btnMapTest.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMapTest.Location = new System.Drawing.Point(3, 417);
            this.btnMapTest.Name = "btnMapTest";
            this.btnMapTest.Size = new System.Drawing.Size(273, 34);
            this.btnMapTest.TabIndex = 19;
            this.btnMapTest.Text = "Map test";
            this.btnMapTest.UseVisualStyleBackColor = true;
            // 
            // pgStreetViewOptions
            // 
            this.pgStreetViewOptions.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.pgStreetViewOptions.Dock = System.Windows.Forms.DockStyle.Top;
            this.pgStreetViewOptions.HelpVisible = false;
            this.pgStreetViewOptions.LineColor = System.Drawing.SystemColors.ControlDark;
            this.pgStreetViewOptions.Location = new System.Drawing.Point(3, 223);
            this.pgStreetViewOptions.Name = "pgStreetViewOptions";
            this.pgStreetViewOptions.Size = new System.Drawing.Size(273, 194);
            this.pgStreetViewOptions.TabIndex = 18;
            this.pgStreetViewOptions.ToolbarVisible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Location = new System.Drawing.Point(3, 210);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "Street View Options";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.groupBox8);
            this.panel3.Controls.Add(this.groupBox6);
            this.panel3.Controls.Add(this.groupBox7);
            this.panel3.Controls.Add(this.groupBox5);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(285, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(279, 765);
            this.panel3.TabIndex = 19;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.button3);
            this.groupBox8.Controls.Add(this.button1);
            this.groupBox8.Controls.Add(this.label6);
            this.groupBox8.Controls.Add(this.waterReportLng);
            this.groupBox8.Controls.Add(this.label5);
            this.groupBox8.Controls.Add(this.waterReportLat);
            this.groupBox8.Location = new System.Drawing.Point(0, 583);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(279, 182);
            this.groupBox8.TabIndex = 22;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Add Water Report";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(9, 66);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(267, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Add Water Report";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(25, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Lng";
            // 
            // waterReportLng
            // 
            this.waterReportLng.Location = new System.Drawing.Point(47, 39);
            this.waterReportLng.Name = "waterReportLng";
            this.waterReportLng.Size = new System.Drawing.Size(229, 20);
            this.waterReportLng.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(22, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Lat";
            // 
            // waterReportLat
            // 
            this.waterReportLat.Location = new System.Drawing.Point(47, 13);
            this.waterReportLat.Name = "waterReportLat";
            this.waterReportLat.Size = new System.Drawing.Size(229, 20);
            this.waterReportLat.TabIndex = 0;
            this.waterReportLat.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.cbLimitMap);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox6.Location = new System.Drawing.Point(0, 519);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(279, 58);
            this.groupBox6.TabIndex = 21;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Limit map region";
            // 
            // cbLimitMap
            // 
            this.cbLimitMap.AutoSize = true;
            this.cbLimitMap.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbLimitMap.Location = new System.Drawing.Point(3, 16);
            this.cbLimitMap.Name = "cbLimitMap";
            this.cbLimitMap.Size = new System.Drawing.Size(273, 17);
            this.cbLimitMap.TabIndex = 0;
            this.cbLimitMap.Text = "Limit map to South Africa and surroundings?";
            this.cbLimitMap.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.btnStreetView);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox7.Location = new System.Drawing.Point(0, 461);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(279, 58);
            this.groupBox7.TabIndex = 20;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Street View";
            // 
            // btnStreetView
            // 
            this.btnStreetView.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnStreetView.Location = new System.Drawing.Point(3, 16);
            this.btnStreetView.Name = "btnStreetView";
            this.btnStreetView.Size = new System.Drawing.Size(273, 34);
            this.btnStreetView.TabIndex = 17;
            this.btnStreetView.Text = "Street View test";
            this.btnStreetView.UseVisualStyleBackColor = true;
            this.btnStreetView.Click += new System.EventHandler(this.btnStreetView_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(9, 95);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(267, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "Get Water Reports From Server";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1317, 808);
            this.Controls.Add(this.browser);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPolygonTest;
        private System.Windows.Forms.Button btnGeocoding1;
        private System.Windows.Forms.Button btnGeocoding2;
        private System.Windows.Forms.Label lblZoom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblCenter;
        private System.Windows.Forms.Button btnMarker2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PropertyGrid pgPolygon;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnGeocoding3;
        private System.Windows.Forms.TextBox txtGeocoding;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.PropertyGrid pgMarker;
        private System.Windows.Forms.Button btnMarker1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox cbShowInfoWindow;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.WebBrowser browser;
        private System.Windows.Forms.PropertyGrid pgMapOptions;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblLastEvent;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBoundedGeocoding;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button btnStreetView;
        private System.Windows.Forms.PropertyGrid pgStreetViewOptions;
        private System.Windows.Forms.Button btnMapTest;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.CheckBox cbLimitMap;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox waterReportLat;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox waterReportLng;
        private System.Windows.Forms.Button button3;
    }
}

