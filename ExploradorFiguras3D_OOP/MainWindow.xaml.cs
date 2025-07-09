// MainWindow.xaml.cs
using ExploradorFiguras3D_OOP.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Media3D;

namespace ExploradorFiguras3D_OOP
{
    public partial class MainWindow : Window
    {
        private ModelVisual3D modeloActual;
        private Transform3DGroup transformGroup;
        private bool isRotating = false;
        private double angularSpeed = 30; // grados por segundo
        private System.Windows.Threading.DispatcherTimer rotateTimer;
        private ModelVisual3D previewModel;

        // En el constructor, inicializa el timer:
        public MainWindow()
        {
            InitializeComponent();
            rotateTimer = new System.Windows.Threading.DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(33) // ~30fps
            };
            rotateTimer.Tick += RotateTimer_Tick;
        }

        // Manejador botón Rotar
        private void BtnRotar_Click(object sender, RoutedEventArgs e)
        {
            isRotating = !isRotating;
            if (isRotating)
            {
                rotateTimer.Start();
                (sender as Button).Content = "Detener";
            }
            else
            {
                rotateTimer.Stop();
                (sender as Button).Content = "Rotar";
            }
        }

        // Cada tick, actualiza rotación Z:
        private void RotateTimer_Tick(object sender, EventArgs e)
        {
            if (modeloActual == null) return;
            // Incrementa sliderRotZ
            sliderRotZ.Value = (sliderRotZ.Value + angularSpeed * rotateTimer.Interval.TotalSeconds) % 360;
        }


        private void ComboFiguras_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // Quitar modelo anterior
            if (modeloActual != null)
                viewport.Children.Remove(modeloActual);

            var item = comboFiguras.SelectedItem as System.Windows.Controls.ComboBoxItem;
            if (item == null) return;

            // Instanciar la figura OOP
            string tipo = item.Content.ToString();
            ExploradorFiguras3D_OOP.Models.Figura3D figura;
            switch (tipo)
            {
                case "Cubo":
                    figura = new ExploradorFiguras3D_OOP.Models.Cubo();
                    break;
                case "Esfera":
                    figura = new ExploradorFiguras3D_OOP.Models.Esfera();
                    break;
                case "Cono":
                    figura = new ExploradorFiguras3D_OOP.Models.Cono();
                    break;
                case "Cilindro":
                    figura = new ExploradorFiguras3D_OOP.Models.Cilindro();
                    break;
                case "Piramide":
                    figura = new ExploradorFiguras3D_OOP.Models.Piramide();
                    break;
                default:
                    figura = null;
                    break;
            }
            if (figura == null) return;

            // Crear modelo y asignar transformación
            GeometryModel3D geo = figura.CrearModelo();
            modeloActual = new ModelVisual3D { Content = geo };
            transformGroup = new Transform3DGroup();
            modeloActual.Transform = transformGroup;
            viewport.Children.Add(modeloActual);

            // Mostrar en la vista previa
            MostrarVistaPrevia(tipo);
        }

        private void TransformChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (modeloActual == null) return;

            // Limpiar anteriores
            transformGroup.Children.Clear();

            // 1) Traslación
            double tx = sliderPosX.Value;
            double ty = sliderPosY.Value;
            double tz = sliderPosZ.Value;
            transformGroup.Children.Add(new TranslateTransform3D(tx, ty, tz));

            // 2) Escala
            double escala = sliderEscala.Value;
            transformGroup.Children.Add(new ScaleTransform3D(escala, escala, escala));

            // 3) Rotaciones
            transformGroup.Children.Add(new RotateTransform3D(
                new AxisAngleRotation3D(new Vector3D(1, 0, 0), sliderRotX.Value)));
            transformGroup.Children.Add(new RotateTransform3D(
                new AxisAngleRotation3D(new Vector3D(0, 1, 0), sliderRotY.Value)));
            transformGroup.Children.Add(new RotateTransform3D(
                new AxisAngleRotation3D(new Vector3D(0, 0, 1), sliderRotZ.Value)));
        }

        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            if (modeloActual != null)
                viewport.Children.Remove(modeloActual);

            modeloActual = null;
            // Resetear todos los sliders
            sliderEscala.Value = 1;
            sliderRotX.Value = sliderRotY.Value = sliderRotZ.Value = 0;
            sliderPosX.Value = sliderPosY.Value = sliderPosZ.Value = 0;
        }

        private void MostrarVistaPrevia(string tipoFigura)
        {
            // Quitar modelo anterior de la vista previa
            if (previewModel != null)
                previewViewport.Children.Remove(previewModel);

            ExploradorFiguras3D_OOP.Models.Figura3D figura;
            switch (tipoFigura)
            {
                case "Cubo":
                    figura = new ExploradorFiguras3D_OOP.Models.Cubo();
                    break;
                case "Esfera":
                    figura = new ExploradorFiguras3D_OOP.Models.Esfera();
                    break;
                case "Cono":
                    figura = new ExploradorFiguras3D_OOP.Models.Cono();
                    break;
                case "Cilindro":
                    figura = new ExploradorFiguras3D_OOP.Models.Cilindro();
                    break;
                case "Piramide":
                    figura = new ExploradorFiguras3D_OOP.Models.Piramide();
                    break;
                default:
                    figura = null;
                    break;
            }
            if (figura == null) return;

            GeometryModel3D geo = figura.CrearModelo();
            previewModel = new ModelVisual3D { Content = geo };

            // Escala y centra la figura en la vista previa
            var group = new Transform3DGroup();
            group.Children.Add(new ScaleTransform3D(0.7, 0.7, 0.7));
            previewModel.Transform = group;

            previewViewport.Children.Add(previewModel);
        }
    }
}
