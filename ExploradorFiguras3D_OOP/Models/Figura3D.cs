using System.Windows.Media.Media3D;

namespace ExploradorFiguras3D_OOP.Models
{
    public abstract class Figura3D
    {
        public abstract GeometryModel3D CrearModelo();
    }
}