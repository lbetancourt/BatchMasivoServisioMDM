using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using BatchMasivoServisioMDM.Models;
using BatchMasivoServisioMDM.Servicio;
using Microsoft.Extensions.Configuration;

namespace BatchMasivoServisioMDM
{
    public class ProcesarError
    {
        public readonly IntegracionsContext _context;
        public IConfiguration _configuration;
        public ProcesarError(IntegracionsContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public void IniciarProceso(DateTime dInicio, DateTime dFin)
        {
            List<LogViewModel> lErrores = ConsultarErrores(dInicio, dFin);
            List<String> lIDMensaje = ConsultaIDMensaje(lErrores);
            List<LogViewModel> lSolicitudes = ConsultarSolicitudes(lIDMensaje);
            string sendpoint = _configuration["Endpoint:UrlServicio"];

            foreach (LogViewModel lvmSolicitud in lSolicitudes)
            {
                int ioperacion = EvaluaOperacion(lvmSolicitud.XmlRequest);

                switch (ioperacion)
                {
                    case 1://insert
                        insert_Input insert = Extension.Deserialize<insert_Input>(lvmSolicitud.XmlRequest.Substring(1));
                        new WFCServicioMDM().Insertar(sendpoint, insert);
                        break;
                    case 2://update
                        update_Input update = Extension.Deserialize<update_Input>(lvmSolicitud.XmlRequest.Substring(1));
                        new WFCServicioMDM().Update(sendpoint, update);
                        break;
                    case 3://delete
                        delete_Input delete = Extension.Deserialize<delete_Input>(lvmSolicitud.XmlRequest.Substring(1));
                        new WFCServicioMDM().Delete(sendpoint, delete);
                        break;
                }
            }
        }
        public List<LogViewModel> ConsultarErrores(DateTime dInicio, DateTime dFin)
        {
            string cadenaError = "<retornoError";
            string cadenaEmpiezaCon = "Servicio: AltaBajaModificacionArticulos-RetornoIBM Entidad:";
            return _context.LogIntegraciones.Where(log => log.FechaInicio >= dInicio && log.FechaInicio <= dFin && log.XmlRequest.Contains(cadenaError) && log.Operacion.StartsWith(cadenaEmpiezaCon)).Select(log => new LogViewModel()
            {
                FechaInicio = log.FechaInicio,
                FechaFin = log.FechaFin,
                XmlRequest = log.XmlRequest,
                Usuario = log.Usuario,
                Codigo = log.Codigo
            }).ToList();
        }

        public List<String> ConsultaIDMensaje(List<LogViewModel> lErrores)
        {
            List<String> lIDMensaje = new List<string>();
            XmlDocument xDocumento;

            foreach (LogViewModel logviewmodel in lErrores)
            {
                xDocumento = new XmlDocument();
                xDocumento.LoadXml(logviewmodel.XmlRequest.Substring(1));
                lIDMensaje.Add(xDocumento.GetElementsByTagName("id_mensaje").Item(0).InnerText);
            }

            return lIDMensaje;
        }

        public List<LogViewModel> ConsultarSolicitudes(List<String> lIDMensaje)
        {
            List<LogViewModel> lSolicitudes = new List<LogViewModel>();
            string cadenaError = "<retorno";
            foreach (String sIDMensaje in lIDMensaje)
            {
                var resultQuery = _context.LogIntegraciones.Where(log => log.XmlRequest.Contains(sIDMensaje) && !(log.XmlRequest.Contains(cadenaError))).Select(log => new LogViewModel()
                {
                    FechaInicio = log.FechaInicio,
                    FechaFin = log.FechaFin,
                    XmlRequest = log.XmlRequest,
                    Usuario = log.Usuario,
                    Codigo = log.Codigo
                }).FirstOrDefault();
                lSolicitudes.Add(resultQuery);
            }
            return lSolicitudes;
        }

        public int EvaluaOperacion(string sXmlRequest)
        {
            int iretorno = 0;
            XmlDocument xDocumento = new XmlDocument();
            xDocumento.LoadXml(sXmlRequest.Substring(1));
            XmlNode xnodo = xDocumento.FirstChild;
            string soperacion = xnodo.NextSibling.Name;
            switch (soperacion)
            {
                case "insert_Input":
                    iretorno = 1;
                    break;
                case "update_Input":
                    iretorno = 2;
                    break;
                case "delete_Input":
                    iretorno = 3;
                    break;
                default:
                    iretorno = 1;
                    break;
            }

            return iretorno;
        }
    }
}