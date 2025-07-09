using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace ExploradorFiguras3D_OOP.Models
{
    public class Piramide : Figura3D
    {
        public override GeometryModel3D CrearModelo()
        {
            double ancho = 2, alto = 2;
            var mesh = new MeshGeometry3D();

            // Definir los 5 v�rtices de la pir�mide
            var v0 = new Point3D(ancho / 2, 0, ancho / 2);   // base esquina 0
            var v1 = new Point3D(-ancho / 2, 0, ancho / 2);  // base esquina 1
            var v2 = new Point3D(-ancho / 2, 0, -ancho / 2); // base esquina 2
            var v3 = new Point3D(ancho / 2, 0, -ancho / 2);  // base esquina 3
            var top = new Point3D(0, alto, 0);               // v�rtice superior

            // A�adir posiciones
            mesh.Positions.Add(v0); // 0
            mesh.Positions.Add(v1); // 1
            mesh.Positions.Add(v2); // 2
            mesh.Positions.Add(v3); // 3
            mesh.Positions.Add(top);// 4

            // Caras de la base (dos tri�ngulos, doble cara)
            mesh.TriangleIndices.Add(0); mesh.TriangleIndices.Add(1); mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(2); mesh.TriangleIndices.Add(1); mesh.TriangleIndices.Add(0); // inverso

            mesh.TriangleIndices.Add(0); mesh.TriangleIndices.Add(2); mesh.TriangleIndices.Add(3);
            mesh.TriangleIndices.Add(3); mesh.TriangleIndices.Add(2); mesh.TriangleIndices.Add(0); // inverso

            // Caras laterales (cuatro tri�ngulos)
            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(4);

            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(4);

            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(3);
            mesh.TriangleIndices.Add(4);

            mesh.TriangleIndices.Add(3);
            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(4);

            return new GeometryModel3D(mesh, new DiffuseMaterial(Brushes.Gold));
        }
    }
}