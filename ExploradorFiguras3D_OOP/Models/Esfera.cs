using System;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace ExploradorFiguras3D_OOP.Models
{
    public class Esfera : Figura3D
    {
        public override GeometryModel3D CrearModelo()
        {
            double radio = 1.5;
            int merid = 24, par = 16;
            var mesh = new MeshGeometry3D();
            for (int i = 0; i <= par; i++)
            {
                double lat = Math.PI * i / par;
                for (int j = 0; j <= merid; j++)
                {
                    double lon = 2 * Math.PI * j / merid;
                    double x = radio * Math.Sin(lat) * Math.Cos(lon);
                    double y = radio * Math.Cos(lat);
                    double z = radio * Math.Sin(lat) * Math.Sin(lon);
                    mesh.Positions.Add(new Point3D(x, y, z));
                }
            }
            for (int i = 0; i < par; i++)
            {
                for (int j = 0; j < merid; j++)
                {
                    int a = i*(merid+1)+j, b = a + merid+1;
                    mesh.TriangleIndices.Add(a); mesh.TriangleIndices.Add(b); mesh.TriangleIndices.Add(a+1);
                    mesh.TriangleIndices.Add(b); mesh.TriangleIndices.Add(b+1); mesh.TriangleIndices.Add(a+1);
                }
            }
            return new GeometryModel3D(mesh, new DiffuseMaterial(Brushes.LightGreen));
        }
    }
}