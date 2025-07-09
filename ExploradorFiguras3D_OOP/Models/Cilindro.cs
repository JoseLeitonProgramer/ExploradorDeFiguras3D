using System;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace ExploradorFiguras3D_OOP.Models
{
    public class Cilindro : Figura3D
    {
        public override GeometryModel3D CrearModelo()
        {
            double radio = 1, altura = 2;
            int slices = 24;
            double h2 = altura / 2;
            var mesh = new MeshGeometry3D();
            // tapas
            for (int cap = 0; cap < 2; cap++)
            {
                double y = cap == 0 ? -h2 : h2;
                var center = new Point3D(0, y, 0);
                for (int i = 0; i < slices; i++)
                {
                    double a1 = 2 * Math.PI * i / slices;
                    double a2 = 2 * Math.PI * (i + 1) / slices;
                    var p1 = new Point3D(radio * Math.Cos(a1), y, radio * Math.Sin(a1));
                    var p2 = new Point3D(radio * Math.Cos(a2), y, radio * Math.Sin(a2));
                    mesh.Positions.Add(center); mesh.Positions.Add(p1); mesh.Positions.Add(p2);
                    int idx = mesh.Positions.Count;
                    if (cap == 0)
                    {
                        // tapa inferior (normal hacia abajo)
                        mesh.TriangleIndices.Add(idx - 3); mesh.TriangleIndices.Add(idx - 2); mesh.TriangleIndices.Add(idx - 1);
                    }
                    else
                    {
                        // tapa superior (normal hacia arriba, invertir orden)
                        mesh.TriangleIndices.Add(idx - 3); mesh.TriangleIndices.Add(idx - 1); mesh.TriangleIndices.Add(idx - 2);
                    }
                }
            }
            // lateral
            for (int i = 0; i < slices; i++)
            {
                double a1 = 2 * Math.PI * i / slices;
                double a2 = 2 * Math.PI * (i + 1) / slices;
                var p1 = new Point3D(radio * Math.Cos(a1), -h2, radio * Math.Sin(a1));
                var p2 = new Point3D(radio * Math.Cos(a1), h2, radio * Math.Sin(a1));
                var p3 = new Point3D(radio * Math.Cos(a2), h2, radio * Math.Sin(a2));
                var p4 = new Point3D(radio * Math.Cos(a2), -h2, radio * Math.Sin(a2));
                // triángulos
                mesh.Positions.Add(p1); mesh.Positions.Add(p2); mesh.Positions.Add(p3);
                mesh.Positions.Add(p1); mesh.Positions.Add(p3); mesh.Positions.Add(p4);
                int idx = mesh.Positions.Count;
                mesh.TriangleIndices.Add(idx - 6); mesh.TriangleIndices.Add(idx - 5); mesh.TriangleIndices.Add(idx - 4);
                mesh.TriangleIndices.Add(idx - 3); mesh.TriangleIndices.Add(idx - 2); mesh.TriangleIndices.Add(idx - 1);
            }
            return new GeometryModel3D(mesh, new DiffuseMaterial(Brushes.Blue));
        }
    }
}