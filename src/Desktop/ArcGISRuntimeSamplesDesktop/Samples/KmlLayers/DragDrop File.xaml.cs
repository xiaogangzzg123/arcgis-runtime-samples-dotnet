﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Esri.ArcGISRuntime.Layers;

namespace ArcGISRuntime.Samples.Desktop.Samples.KmlLayers
{
    /// <summary>
    /// This sample show you how you can add KML or KMZ file from your machine to the map using Drag/Drop. 
    /// </summary>
    /// <title>DragDrop</title>
    /// <category>Layers</category>
    /// <subcategory>Kml Layers</subcategory>
    public partial class DragDrop_File : UserControl
    {
        /// <summary>Construct KML DragDrop sample control</summary>
        public DragDrop_File()
        {
            InitializeComponent();
        }

        private async void MyMapView_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Note that you can have more than one file.
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                KmlLayer kmlLayer = new KmlLayer(new Uri(files[0]));
                await kmlLayer.InitializeAsync();

                //Add the kml layer
                MyMapView.Map.Layers.Add(kmlLayer);

                //Zoom to the kml layer if available
                if (kmlLayer.RootFeature.Viewpoint != null)
                    await MyMapView.SetViewAsync(kmlLayer.RootFeature.Viewpoint);
            }
        }

        private void ResetMapButton_Click(object sender, RoutedEventArgs e)
        {
            MyMapView.Map.Layers.Clear();
            MyMapView.Map.Layers.Add(new ArcGISTiledMapServiceLayer(new Uri("http://services.arcgisonline.com/ArcGIS/rest/services/World_Topo_Map/MapServer")));
        }
    }
}
