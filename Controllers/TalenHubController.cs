using BibliotecaGenerica;
using L1_TalentHub.Models;
using L1_TalentHub.Models.Datos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace L1_TalentHub.Controllers
{
    public class TalenHubController : Controller
    {
        // GET: TalenHub
        public ActionResult Index()
        {
            return View(Singleton.Instance.ListaRegistros);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LeeArchivo(IFormFile ArchivoCargado)
        {
            try
            {
                byte[] fileBytes;
                using (var memoryStream = new MemoryStream())
                {
                    ArchivoCargado.CopyTo(memoryStream);
                    fileBytes = memoryStream.ToArray();
                }

                string fileContent = Encoding.UTF8.GetString(fileBytes);
                /*if(!(ArchivoCargado == null))
                {

                }*/
                // var Archivo = new StreamReader(ArchivoCargado.OpenReadStream());
                //{
                string info = fileContent;
                    foreach (string Fila in info.Split("\n"))
                    {
                        if (!(string.IsNullOrEmpty(Fila)))
                        {
                            string jsonAccion = Fila.Split(";")[0];
                            string jsonString = Fila.Split(";")[1];

                            Registros NuevoRegistro = JsonConvert.DeserializeObject<Registros>(jsonString);

                            if (jsonAccion == "INSERT")
                            {
                                for (int i = 0; i < NuevoRegistro.Empresas.Length; i++)
                                {
                                    List<string> MensajeCodificado = new List<string>();
                                    List<string> ID = new List<string>();
                                    char[] auxdpi = NuevoRegistro.Dpi.ToCharArray();

                                    ObtenerTabla(NuevoRegistro.Empresas[i], MensajeCodificado);

                                    Singleton.Instance.ListaHuffman = Singleton.Instance.ListaHuffman.OrderBy(x => x.Frecuencia).ToList();

                                    CrearArbol(Singleton.Instance.ListaHuffman);
                                    CrearCodigo("", Singleton.Instance.ListaHuffman[0], MensajeCodificado);

                                    if (auxdpi.Length > MensajeCodificado.Count)
                                    {
                                        for (int j = 0; j < MensajeCodificado.Count; j++)
                                        {
                                            int aux = Convert.ToInt32(auxdpi[j].ToString());
                                            int auxId = Convert.ToInt32(MensajeCodificado[j]);
                                            string auxNum = "";
                                            if (auxId == 0 || MensajeCodificado[j].Length != Convert.ToString(auxId).Length)
                                            {
                                                for (int z = 0; z < MensajeCodificado[j].Length; z++)
                                                {
                                                    auxNum += "1";
                                                    auxId = Convert.ToInt32(auxNum);
                                                }
                                            }
                                            ID.Add(Convert.ToString(aux + auxId));
                                        }
                                        for (int j = 0; j < (auxdpi.Length - MensajeCodificado.Count); j++)
                                        {
                                            string aux = auxdpi[MensajeCodificado.Count + j].ToString();
                                            ID.Add(aux);
                                        }
                                    }
                                    else
                                    {
                                        for (int j = 0; j < auxdpi.Length; j++)
                                        {
                                            int aux = Convert.ToInt32(auxdpi[j].ToString());
                                            int auxId = Convert.ToInt32(MensajeCodificado[j]);
                                            string auxNum = "";
                                            if (auxId == 0 || MensajeCodificado[j].Length != Convert.ToString(auxId).Length)
                                            {
                                                for (int z = 0; z < MensajeCodificado[j].Length; z++)
                                                {
                                                    auxNum += "1";
                                                    auxId = Convert.ToInt32(auxNum);
                                                }
                                            }
                                            ID.Add(Convert.ToString(aux + auxId));
                                        }
                                    }
                                    string DPICodificado = string.Join("", ID);

                                    NuevoRegistro.Empresas[i] += ":" + DPICodificado;
                                }
                                Singleton.Instance.AVLNombres.Agregar(NuevoRegistro, NuevoRegistro.InsertarPorNombre);
                                Singleton.Instance.AVLDPI.Agregar(NuevoRegistro, NuevoRegistro.InsertarPorDPI);
                            }
                            else if (jsonAccion == "PATCH")
                            {
                                for (int i = 0; i < NuevoRegistro.Empresas.Length; i++)
                                {
                                    List<string> MensajeCodificado = new List<string>();
                                    List<string> ID = new List<string>();
                                    char[] auxdpi = NuevoRegistro.Dpi.ToCharArray();

                                    ObtenerTabla(NuevoRegistro.Empresas[i], MensajeCodificado);

                                    Singleton.Instance.ListaHuffman = Singleton.Instance.ListaHuffman.OrderBy(x => x.Frecuencia).ToList();

                                    CrearArbol(Singleton.Instance.ListaHuffman);
                                    CrearCodigo("", Singleton.Instance.ListaHuffman[0], MensajeCodificado);

                                    if (auxdpi.Length > MensajeCodificado.Count)
                                    {
                                        for (int j = 0; j < MensajeCodificado.Count; j++)
                                        {
                                            int aux = Convert.ToInt32(auxdpi[j].ToString());
                                            int auxId = Convert.ToInt32(MensajeCodificado[j]);
                                            string auxNum = "";
                                            if (auxId == 0 || MensajeCodificado[j].Length != Convert.ToString(auxId).Length)
                                            {
                                                for (int z = 0; z < MensajeCodificado[j].Length; z++)
                                                {
                                                    auxNum += "1";
                                                    auxId = Convert.ToInt32(auxNum);
                                                }
                                            }
                                            ID.Add(Convert.ToString(aux + auxId));
                                        }
                                        for (int j = 0; j < (auxdpi.Length - MensajeCodificado.Count); j++)
                                        {
                                            string aux = auxdpi[MensajeCodificado.Count + j].ToString();
                                            ID.Add(aux);
                                        }
                                    }
                                    else
                                    {
                                        for (int j = 0; j < auxdpi.Length; j++)
                                        {
                                            int aux = Convert.ToInt32(auxdpi[j].ToString());
                                            int auxId = Convert.ToInt32(MensajeCodificado[j]);
                                            string auxNum = "";
                                            if (auxId == 0 || MensajeCodificado[j].Length != Convert.ToString(auxId).Length)
                                            {
                                                for (int z = 0; z < MensajeCodificado[j].Length; z++)
                                                {
                                                    auxNum += "1";
                                                    auxId = Convert.ToInt32(auxNum);
                                                }
                                            }
                                            ID.Add(Convert.ToString(aux + auxId));
                                        }
                                    }
                                    string DPICodificado = string.Join("", ID);

                                    NuevoRegistro.Empresas[i] += ":" + DPICodificado;
                                }
                                Singleton.Instance.AVLNombres.Actualizar(NuevoRegistro, NuevoRegistro.InsertarPorNombre, NuevoRegistro.InsertarPorDPI);
                                Singleton.Instance.AVLDPI.Borrar(NuevoRegistro, NuevoRegistro.InsertarPorDPI);
                                Singleton.Instance.AVLDPI.Agregar(NuevoRegistro, NuevoRegistro.InsertarPorDPI);
                            }
                            else
                            {
                                Singleton.Instance.AVLNombres.Borrar(NuevoRegistro, NuevoRegistro.InsertarPorNombre, NuevoRegistro.InsertarPorDPI);
                                Singleton.Instance.AVLDPI.Borrar(NuevoRegistro, NuevoRegistro.InsertarPorDPI);

                            }
                        }
                    }
              //  }
                TempData["Mensaje"] = "Los Datos han sido cargados correctamente";
                Singleton.Instance.ListaRegistros = Singleton.Instance.AVLDPI.Recorrido();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View("Index");
            }
        }

        public ActionResult Buscar(string id)
        {
            try
            {
                Registros NuevoRegistro = new Registros
                {
                    Nombre = id
                };

                Singleton.Instance.RegistrosEncontrados = Singleton.Instance.AVLNombres.Busqueda(NuevoRegistro, NuevoRegistro.InsertarPorNombre);

                if (Singleton.Instance.RegistrosEncontrados == null)
                {
                    TempData["Mensaje"] = "La Persona No Existe o Coloco Mal Los Datos";
                    return RedirectToAction(nameof(Index));
                }

                //Se asegura que exista la carpeta en donde almacenara los resultados de las busquedas
                if (!Directory.Exists(@"outputs"))
                {
                    System.IO.Directory.CreateDirectory("outputs");
                }

                string ruta = @"outputs/" + Singleton.Instance.RegistrosEncontrados[0].Nombre + ".csv";//Nombre del archivo

                for (int i = 0; i < Singleton.Instance.RegistrosEncontrados.Count; i++)
                {
                    string json = JsonConvert.SerializeObject(Singleton.Instance.RegistrosEncontrados[i]) + "\n";
                    System.IO.File.AppendAllText(ruta, json);
                }
                TempData["Mensaje"] = "La persona fue encontrada con exito";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["Mensaje"] = "Coloque Datos Para Buscar";
                return RedirectToAction(nameof(Index));
            }
        }

        public void ObtenerTabla(string mensaje, List<string> MensajeCodificado)
        {
            Singleton.Instance.ListaHuffman.Clear();
            string[] tabla = new string[mensaje.Length];

            for (int i = 0; i < mensaje.Length; i++)
            {
                string aux = mensaje.Substring(i, 1);
                tabla[i] = aux;
                MensajeCodificado.Add(aux);
            }

            for (int i = 0; i < tabla.Length; i++)
            {
                Huffman<Registros> auxHuffman = new Huffman<Registros>();

                if (Singleton.Instance.ListaHuffman.Exists(x => x.Caracter == tabla[i]))
                {
                    Singleton.Instance.ListaHuffman.Find(x => x.Caracter == tabla[i]).IncrementarFrecuencia();
                }
                else
                {
                    auxHuffman.Agregar(tabla[i]);
                    Singleton.Instance.ListaHuffman.Add(auxHuffman);
                }
            }
        }

        public void CrearArbol(List<Huffman<Registros>> huffmen)
        {
            while (huffmen.Count > 1)
            {
                Huffman<Registros> auxHuffman = new Huffman<Registros>();
                Huffman<Registros> nodo1 = huffmen[0];
                huffmen.RemoveAt(0);
                Huffman<Registros> nodo2 = huffmen[0];
                huffmen.RemoveAt(0);

                auxHuffman.UnirNodos(nodo1, nodo2);

                huffmen.Add(auxHuffman);
                huffmen = huffmen.OrderBy(x => x.Frecuencia).ToList();
                Singleton.Instance.ListaHuffman = huffmen;
            }
        }

        public void CrearCodigo(string codigo, Huffman<Registros> nodo, List<string> MensajeCodificado)
        {
            if (nodo == null)
            {
                return;
            }
            if (nodo.Izquierda == null && nodo.Derecha == null)
            {
                nodo.Codigo = codigo;

                for (int i = 0; i < MensajeCodificado.Count; i++)
                {
                    if (MensajeCodificado[i] == nodo.Caracter)
                    {
                        MensajeCodificado[i] = nodo.Codigo;
                    }
                }
            }
            CrearCodigo(codigo + "0", nodo.Izquierda, MensajeCodificado);
            CrearCodigo(codigo + "1", nodo.Derecha, MensajeCodificado);
        }

        public ActionResult BuscarDPI(string id)
        {
            try
            {
                Registros NuevoRegistro = new Registros
                {
                    Dpi = id
                };

                Singleton.Instance.RegistrosEncontrados.Add(Singleton.Instance.AVLDPI.Busqueda(NuevoRegistro, NuevoRegistro.InsertarPorDPI));

                if (Singleton.Instance.RegistrosEncontrados == null)
                {
                    TempData["Mensaje"] = "La Persona No Existe o Coloco Mal Los Datos";
                    return RedirectToAction(nameof(Index));
                }

                Decodificar(Singleton.Instance.RegistrosEncontrados[0]);


                //Se asegura que exista la carpeta en donde almacenara los resultados de las busquedas
                if (!Directory.Exists(@"outputs"))
                {
                    System.IO.Directory.CreateDirectory("outputs");
                }

                string ruta = @"outputs/" + Singleton.Instance.RegistrosEncontrados[0].Nombre + ".csv";//Nombre del archivo

                for (int i = 0; i < Singleton.Instance.RegistrosEncontrados.Count; i++)
                {
                    string json = JsonConvert.SerializeObject(Singleton.Instance.RegistrosEncontrados[i]) + "\n";
                    System.IO.File.AppendAllText(ruta, json);
                }
                TempData["Mensaje"] = "La persona fue encontrada con exito";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["Mensaje"] = "Coloque Datos Para Buscar";
                return RedirectToAction(nameof(Index));
            }   
        }

        public void Decodificar(Registros encontrado)
        {
            for (int k = 0; k < encontrado.Empresas.Length; k++)
            {
                string compania = encontrado.Empresas[k].Split(":")[0];
                string ID = encontrado.Empresas[k].Split(":")[1];

                List<string> MensajeCodificado = new List<string>();

                ObtenerTabla(compania, MensajeCodificado);

                Singleton.Instance.ListaHuffman = Singleton.Instance.ListaHuffman.OrderBy(x => x.Frecuencia).ToList();

                CrearArbol(Singleton.Instance.ListaHuffman);
                CrearCodigo("", Singleton.Instance.ListaHuffman[0], MensajeCodificado);

                List<string> decodificador = new List<string>();
                int aux = 0;
                var d = ID.Length;
                if (MensajeCodificado.Count < 13)
                {
                    for (int i = 0; i < MensajeCodificado.Count; i++)
                    {
                        decodificador.Add(ID.Substring(aux, MensajeCodificado[i].Length));
                        aux += MensajeCodificado[i].Length;
                    }
                    for (int i = 0; i < (13 - MensajeCodificado.Count); i++)
                    {
                        decodificador.Add(ID.Substring(aux, 1));
                        aux += 1;
                    }
                }
                else
                {
                    for (int i = 0; i < 13; i++)
                    {
                        decodificador.Add(ID.Substring(aux, MensajeCodificado[i].Length));
                        aux += MensajeCodificado[i].Length;
                    }
                }

                List<string> DPIDescodificado = new List<string>();
                if (MensajeCodificado.Count < 13)
                {
                    for (int i = 0; i < MensajeCodificado.Count; i++)
                    {
                        int aux1 = Convert.ToInt32(decodificador[i]);
                        int aux2 = Convert.ToInt32(MensajeCodificado[i]);
                        string auxNum = "";

                        if (aux2 == 0 || MensajeCodificado[i].Length != Convert.ToString(aux2).Length)
                        {
                            for (int z = 0; z < MensajeCodificado[i].Length; z++)
                            {
                                auxNum += "1";
                                aux2 = Convert.ToInt32(auxNum);
                            }
                        }
                        DPIDescodificado.Add(Convert.ToString(aux1 - aux2));
                    }
                    for (int i = 0; i < (13 - MensajeCodificado.Count); i++)
                    {
                        DPIDescodificado.Add(decodificador[MensajeCodificado.Count + i]);//fdddddddddddddddddddddddddddddddddddd
                    }
                }
                else
                {
                    for (int i = 0; i < decodificador.Count; i++)
                    {
                        int aux1 = Convert.ToInt32(decodificador[i]);
                        int aux2 = Convert.ToInt32(MensajeCodificado[i]);

                        string auxNum = "";

                        if (aux2 == 0 || MensajeCodificado[i].Length != Convert.ToString(aux2).Length)
                        {
                            for (int z = 0; z < MensajeCodificado[i].Length; z++)
                            {
                                auxNum += "1";
                                aux2 = Convert.ToInt32(auxNum);
                            }
                        }
                        DPIDescodificado.Add(Convert.ToString(aux1 - aux2));
                    }
                }
                string DPICodificado = string.Join("", DPIDescodificado);

                encontrado.Empresas[k] += ":" + DPICodificado;

            }

        }

    }
}