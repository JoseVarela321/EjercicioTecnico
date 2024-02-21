using EjercicioTecnico_02.CS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EjercicioTecnico_02
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }



        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string LoadData()
        {
            try
            {
                string result = "";

                using (var httpClient = new HttpClient())
                {
                    try
                    {
                        // HACEMOS UNA PETICION A NUESTRA API PARA OBETENER TODOS LOS ITEMS EXISTENTES
                        HttpResponseMessage response = httpClient.GetAsync("http://www.apistest.somee.com/Inventario").Result;

                        //SI EL RESPONSE ES EXITOSO GUARAMOS LSO DATOS EN UNA VARIABLE PARA REGRESARLA
                        if (response.IsSuccessStatusCode)
                        {
                            //GUARAMOS LA RESPUESTA EN UNA VARIABLE Y LA REGRESAMOS AL AJAX PARA QUE CARGUE LOS DATOS.
                            result = response.Content.ReadAsStringAsync().Result;


                        }
                        else
                        {
                            return result = "Error en la petición";
                        }

                        return result;
                    }
                    catch (Exception ex)
                    {
                        return $"Error: {ex.Message}";
                    }
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

        public static string SearchItem(int idItem)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    try
                    {
                        string result = "";
                        // HACEMOS UNA PETICION A NUESTRA API PARA OBETENER TODOS LOS ITEMS EXISTENTES
                        HttpResponseMessage response = httpClient.GetAsync($"http://www.apistest.somee.com/BuscarProducto/{idItem}").Result;

                        //SI EL RESPONSE ES EXITOSO GUARAMOS LSO DATOS EN UNA VARIABLE PARA REGRESARLA
                        if (response.IsSuccessStatusCode)
                        {
                            //GUARAMOS LA RESPUESTA EN UNA VARIABLE Y LA REGRESAMOS AL AJAX PARA QUE CARGUE LOS DATOS.
                            result = response.Content.ReadAsStringAsync().Result;


                        }
                        else
                        {
                            return result = "Error en la petición";
                        }

                        return result;
                    }
                    catch (Exception ex)
                    {
                        return $"Error: {ex.Message}";
                    }
                }


                return "";
            }
            catch(Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

        public static string SaveChanges( int idItem,string codBarras,string descripcion,string marca,int categoria,double precio)
        {
            try
            {
                string result = "";
                Producto item = new Producto{
                
                    IDProducto = idItem,
                    Codigobarra = codBarras,
                    Descripcion = descripcion,
                    Marca = marca,
                    IDCategoria = categoria,
                    Precio= precio
                };


                string json = JsonConvert.SerializeObject(item);

                //CONVERTIMOS LA CADENA JSON A UN OBJETO CONTENT
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                using (var httpClient = new HttpClient())
                {
                    try
                    {
                        // Hacer una solicitud PUT a la URL de la API con el objeto como JSON
                        HttpResponseMessage response =  httpClient.PutAsync("http://www.apistest.somee.com/EditarProducto", content).Result;

                        // Verificar si la solicitud fue exitosa (código de estado 200)
                        if (!response.IsSuccessStatusCode)
                        {
                            result = "Error en la petición";
                            
                        }

                        return result;
                    }
                    catch (HttpRequestException ex)
                    {
                        return $"Error: {ex.Message}";
                    }
                }


            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string DeleteItem(int idItem)
        {
            try
            {
                 string result = "";

                using (var httpClient = new HttpClient())
                {
                    try
                    {
                        // Hacer una solicitud PUT a la URL de la API con el objeto como JSON
                        HttpResponseMessage response = httpClient.DeleteAsync($"http://www.apistest.somee.com/EliminarProducto/{idItem}").Result;

                        // Verificar si la solicitud fue exitosa (código de estado 200)
                        if (!response.IsSuccessStatusCode)
                        {
                            result = "Error en la petición";
                        }
                    }
                    catch (HttpRequestException ex)
                    {
                        return $"Error: {ex.Message}";
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

        public static string SaveNewItem(string codBarras, string descripcion, string marca, int categoria, double precio)
        {

            try
            {
                string result = "";

                Producto item = new Producto
                {

                    Codigobarra = codBarras,
                    Descripcion = descripcion,
                    Marca = marca,
                    IDCategoria = categoria,
                    Precio = precio
                };


                string json = JsonConvert.SerializeObject(item);

                //CONVERTIMOS LA CADENA JSON A UN OBJETO CONTENT
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                using (var httpClient = new HttpClient())
                {
                    try
                    {
                        // Hacer una solicitud PUT a la URL de la API con el objeto como JSON
                        HttpResponseMessage response = httpClient.PostAsync("http://www.apistest.somee.com/GuardarProducto", content).Result;

                        // Verificar si la solicitud fue exitosa (código de estado 200)
                        if (response.IsSuccessStatusCode)
                        {
                            result= "Producto Guardado Correctamente";
                        }
                        else
                        {
                            result = "Error en la petición";
                        }
                    }
                    catch (HttpRequestException e)
                    {
                        Console.WriteLine($"Error de solicitud: {e.Message}");
                    }
                }

                return result;

            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string LoadCategories()
        {
            try
            {
                List<Categoria> ListCategorias = new List<Categoria>();
                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
                {
                    cnn.Open();

                    using(SqlCommand cmd = new SqlCommand("SELECT IdCategoria,Descripcion FROM CATEGORIA",cnn))
                    {
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if(dr.HasRows)
                            {
                                while(dr.Read())
                                {
                                    Categoria cat = new Categoria
                                    {
                                        CategoriaId = Convert.ToInt32(dr["IdCategoria"].ToString()),
                                        DescripcionCategoria = dr["Descripcion"].ToString()
                                    };
                                    ListCategorias.Add(cat);
                                }
                            }
                        }
                    }
                }
                return JsonConvert.SerializeObject(ListCategorias);
            }
            catch(Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string GetItemsTypeHead(string Query)
        {
            try
            {
                List<Producto> productos = new List<Producto>();
                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
                {
                    cnn.Open();

                    using (SqlCommand cmd = new SqlCommand($"SELECT CodigoBarra,Descripcion,Marca,IdCategoria FROM PRODUCTO where CodigoBarra like '%{Query}%'", cnn))
                    {
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    Producto item = new Producto
                                    {
                                       Codigobarra = dr["CodigoBarra"].ToString(),
                                       Descripcion = dr["Descripcion"].ToString(),
                                       Marca = dr["Marca"].ToString()
                                    };
                                    productos.Add(item);
                                }
                            }
                        }
                    }
                }
                return JsonConvert.SerializeObject(productos);
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string LoadItems()
        {
            try
            {
                List<Producto> productos = new List<Producto>();
                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
                {
                    cnn.Open();

                    using (SqlCommand cmd = new SqlCommand($"SELECT CodigoBarra,Descripcion,Marca,IdCategoria FROM PRODUCTO", cnn))
                    {
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    Producto item = new Producto
                                    {
                                        Codigobarra = dr["CodigoBarra"].ToString(),
                                        Descripcion = dr["Descripcion"].ToString(),
                                        Marca = dr["Marca"].ToString()
                                    };
                                    productos.Add(item);
                                }
                            }
                        }
                    }
                }
                return JsonConvert.SerializeObject(productos);
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }
}