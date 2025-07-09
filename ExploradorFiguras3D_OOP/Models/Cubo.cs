using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace ExploradorFiguras3D_OOP.Models
{
    public class Cubo : Figura3D
    {
        public override GeometryModel3D CrearModelo()
        {
            var mesh = new MeshGeometry3D();

            // Definir los 8 vértices del cubo
            var p = new Point3D[]
            {
                new Point3D(-1, -1, -1), // 0
                new Point3D(1, -1, -1),  // 1
                new Point3D(1, 1, -1),   // 2
                new Point3D(-1, 1, -1),  // 3
                new Point3D(-1, -1, 1),  // 4
                new Point3D(1, -1, 1),   // 5
                new Point3D(1, 1, 1),    // 6
                new Point3D(-1, 1, 1)    // 7
            };

            // Añadir posiciones
            foreach (var point in p)
                mesh.Positions.Add(point);

            // Definir los triángulos de cada cara usando los mismos vértices
            int[] indices = {
                // Cara frontal
                0,1,2, 0,2,3,
                // Cara trasera
                4,7,6, 4,6,5,
                // Cara izquierda
                0,3,7, 0,7,4,
                // Cara derecha
                1,5,6, 1,6,2,
                // Cara superior
                3,2,6, 3,6,7,
                // Cara inferior
                0,4,5, 0,5,1
            };

            foreach (var idx in indices)
                mesh.TriangleIndices.Add(idx);
            return new GeometryModel3D(mesh, new DiffuseMaterial(new SolidColorBrush(Colors.CadetBlue)));
            
        }
    }
}