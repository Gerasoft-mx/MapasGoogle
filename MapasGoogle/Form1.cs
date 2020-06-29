using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

namespace MapasGoogle
{
    public partial class FrmMapas : Form
    {
        GMarkerGoogle marker;
        GMapOverlay markerOverlay;
        DataTable dt;

        int filaSeleccionada = 0;
        double LatInicial = 19.040319;
        double LngInicial = -98.3330542;
        


        public FrmMapas()
        {
            InitializeComponent();
        }

        private void FrmMapas_Load(object sender, EventArgs e)
        {

            dt = new DataTable();
            dt.Columns.Add(new DataColumn("Descripición", typeof(string)));
            dt.Columns.Add(new DataColumn("Latitud", typeof(double)));
            dt.Columns.Add(new DataColumn("Longitud", typeof(double)));

            dt.Rows.Add("Ubicación 1", LatInicial, LngInicial);
            DGridView.DataSource = dt;

            DGridView.Columns[1].Visible = false;
            DGridView.Columns[2].Visible = false;

            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.CanDragMap = true;
            gMapControl1.MapProvider = GMapProviders.GoogleMap;
            gMapControl1.Position = new PointLatLng(LatInicial, LngInicial);
            gMapControl1.MinZoom = 0;
            gMapControl1.MaxZoom = 24;
            gMapControl1.Zoom = 10;
            gMapControl1.AutoScroll = true;

            //Marcador
            markerOverlay = new GMapOverlay("Marcador");
            marker = new GMarkerGoogle(new PointLatLng(LatInicial, LngInicial), GMarkerGoogleType.green);
            markerOverlay.Markers.Add(marker);

            //Agregamos un tooltips de texto a los marcadores
            marker.ToolTipMode = MarkerTooltipMode.Always;
            marker.ToolTipText = string.Format("Ubicacion: \n Latitud: {0} \n Longitud {1}", LatInicial, LngInicial);

            //Agregamosel en el mapa y el marcaodr al map control
            gMapControl1.Overlays.Add(markerOverlay);
        }

        private void SeleccionarRegistro(object sender, DataGridViewCellMouseEventArgs e)
        {
            filaSeleccionada = e.RowIndex;
            TxtDescripcion.Text = DGridView.Rows[filaSeleccionada].Cells[0].Value.ToString();
            TxtLat.Text = DGridView.Rows[filaSeleccionada].Cells[1].Value.ToString();
            TxtLong.Text = DGridView.Rows[filaSeleccionada].Cells[2].Value.ToString();
        }
    }
}
