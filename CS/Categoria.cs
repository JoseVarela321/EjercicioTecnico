using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EjercicioTecnico_02.CS
{
    public class Categoria
    {
        private int _categoriaid;
        private string _descripcionCategoria;


        public int CategoriaId
        {
            get { return _categoriaid; }
            set { _categoriaid = value; }
        }

        public string DescripcionCategoria
        {
            get { return _descripcionCategoria; }
            set { _descripcionCategoria = value; }
        }
    }
}