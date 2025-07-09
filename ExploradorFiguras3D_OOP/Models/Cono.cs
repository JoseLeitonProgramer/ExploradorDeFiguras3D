using System;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace ExploradorFiguras3D_OOP.Models
{
    public class Cono : Figura3D
    {
        public override GeometryModel3D CrearModelo()
        {
            double radio = 1, altura = 2;
            int slices = 24;
            var mesh = new MeshGeometry3D();
            var apex = new Point3D(0, altura/2, 0);
            var baseCenter = new Point3D(0, -altura/2, 0);
            for (int i = 0; i < slices; i++)
            {
                double a1 = 2 * Math.PI * i / slices;
                double a2 = 2 * Math.PI * (i+1) / slices;
                var p1 = new Point3D(radio*Math.Cos(a1), -altura/2, radio*Math.Sin(a1));
                var p2 = new Point3D(radio*Math.Cos(a2), -altura/2, radio*Math.Sin(a2));
                // base
                mesh.Positions.Add(baseCenter); mesh.Positions.Add(p2); mesh.Positions.Add(p1);
                int idx = mesh.Positions.Count;
                mesh.TriangleIndices.Add(idx-3); mesh.TriangleIndices.Add(idx-2); mesh.TriangleIndices.Add(idx-1);
                // lateral
                mesh.Positions.Add(p1); mesh.Positions.Add(p2); mesh.Positions.Add(apex);
                idx = mesh.Positions.Count;
                mesh.TriangleIndices.Add(idx-3); mesh.TriangleIndices.Add(idx-2); mesh.TriangleIndices.Add(idx-1);
            }
            return new GeometryModel3D(mesh, new DiffuseMaterial(Brushes.Coral));
        }
    }
}