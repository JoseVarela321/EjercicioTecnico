using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EjercicioTecnico_02.CS
{
    public class Producto
    {
        #region PROPIEDADES PRIVADAS
        private int _idProducto;
        private string _codigobarras;
        private string _descripcion;
        private string _marca;
        private int _idcategoria;
        private double _precio;
        #endregion


        #region PROPIEDADES PUBLICAS
        //PROPIEDADES PUBLICAS

        public int IDProducto
        {
            get { return _idProducto; }
            set { _idProducto = value; }
        }

        public string Codigobarra
        {
            get { return _codigobarras; }
            set { _codigobarras = value; }
        }

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }
        public string Marca
        {
            get { return _marca; }
            set{   _marca = value;}
        }
        public int IDCategoria
        {
            get { return _idcategoria; }
            set { _idcategoria = value;}
        }

        public double Precio
        {
            get { return _precio; }
            set {_precio = value;}
        }
        #endregion
    }
}