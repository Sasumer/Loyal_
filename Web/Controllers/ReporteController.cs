using ApplicationCore.Services;
using Infraestructure.Models;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Security;
using Web.Util;

namespace Web.Controllers
{
    public class ReporteController : Controller
    {
        // GET: Reporte
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult EncFacturaCatalogo()
        {
            IEnumerable<ENC_FACTURA> lista = null;
            try
            {

                IServiceEncFactura _ServiceEncFactura = new ServiceEncFactura();
                lista = _ServiceEncFactura.GetEncFactura();
                return View(lista);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        /// <summary>
        /// https://riptutorial.com/itext
        /// Nugget iText7
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Reportes)]
        public ActionResult CreatePdEncFacturaCatalogo()
        {
            //Ejemplos IText7 https://kb.itextpdf.com/home/it7kb/examples
            IEnumerable<ENC_FACTURA> lista = null;
            try
            {
                // Extraer informacion
                IServiceEncFactura _ServiceEncFactura = new ServiceEncFactura();
                lista = _ServiceEncFactura.GetEncFactura();

                // Crear stream para almacenar en memoria el reporte 
                MemoryStream ms = new MemoryStream();
                //Initialize writer
                PdfWriter writer = new PdfWriter(ms);

                //Initialize document
                PdfDocument pdfDoc = new PdfDocument(writer);
                Document doc = new Document(pdfDoc);

                Paragraph header = new Paragraph("Catálogo de Facturas")
                                   .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
                                   .SetFontSize(14)
                                   .SetFontColor(ColorConstants.BLUE);
                doc.Add(header);


                // Crear tabla con 5 columnas 
                Table table = new Table(5, true);


                // los Encabezados
                table.AddHeaderCell("ID");
                table.AddHeaderCell("Fecha");
                table.AddHeaderCell("Usuario");
                table.AddHeaderCell("Tipo de Factura");
                table.AddHeaderCell("Comentario");
                //table.AddHeaderCell("Imagen");


                foreach (var item in lista)
                {

                    // Agregar datos a las celdas
                    table.AddCell(new Paragraph(item.ID.ToString()));
                    table.AddCell(new Paragraph(item.FECHA));
                    table.AddCell(new Paragraph(item.USUARIO.Nombre));
                    table.AddCell(new Paragraph(item.TIPO_FACTURA.DESCRIPCION));
                    table.AddCell(new Paragraph(item.Comentario));
                    // Convierte la imagen que viene en Bytes en imagen para PDF
                    //Image image = new Image(ImageDataFactory.Create(item.PHOTO));

                    // Tamaño de la imagen
                    //image = image.SetHeight(75).SetWidth(75);
                    //table.AddCell(image);
                }
                doc.Add(table);



                // Colocar número de páginas
                int numberOfPages = pdfDoc.GetNumberOfPages();
                for (int i = 1; i <= numberOfPages; i++)
                {

                    // Write aligned text to the specified by parameters point
                    doc.ShowTextAligned(new Paragraph(String.Format("pag {0} of {1}", i, numberOfPages)),
                            559, 826, i, TextAlignment.RIGHT, VerticalAlignment.TOP, 0);
                }


                //Close document
                doc.Close();
                // Retorna un File
                return File(ms.ToArray(), "application/pdf", "reporte.pdf");

            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }

        }

    }
}