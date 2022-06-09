using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using Syncfusion.Pdf.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using zdravstvena_ustanova.Model;

namespace zdravstvena_ustanova.View.Pages.SecretaryPages
{
    /// <summary>
    /// Interaction logic for StatisticsPage.xaml
    /// </summary>
    public partial class StatisticsPage : Page
    {
        private HomePagePatients _homePagePatients;
        public StatisticsPage(HomePagePatients hpp)
        {
            InitializeComponent();
            DataContext = this;
            _homePagePatients = hpp;
        }

        private void pdfBTN_Click(object sender, RoutedEventArgs e)
        {
            if (startDP.SelectedDate == null || endDP.SelectedDate == null)
                    return;
            //TODO validacija
            DateTime start = (DateTime)startDP.SelectedDate;
            DateTime end = (DateTime)endDP.SelectedDate;
            var app = Application.Current as App;
            var rooms = app.RoomController.GetAll();
            

            PdfDocument doc = new PdfDocument();
            PdfPage page = doc.Pages.Add();
            PdfGraphics graphics = page.Graphics;
            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 24);
            PdfFont maliFont = new PdfStandardFont(PdfFontFamily.Helvetica, 16);

            //PdfBitmap image = new PdfBitmap("SecretaryPages\acc-icon.png");
            Stream imageStream = File.OpenRead("hospital.png");

            PdfBitmap image = new PdfBitmap(imageStream);
            graphics.DrawImage(image, 0, 0);
            graphics.DrawString("Zdravo klinka", maliFont, PdfBrushes.Black, new PointF(400, 20));
            //PdfPen pen = page.;
            graphics.DrawLine(PdfPens.Black, new PointF(0, 50), new PointF(520, 50));
            //graphics.DrawString("Zauzetost prostorija u periodu od" + start.Day.ToString() + "." start.Month.ToString() + " do " + end.Date.ToString(), font, PdfBrushes.Black, new PointF(0, 70));
            graphics.DrawString("Zauzetost prostorija u izabranom periodu", font, PdfBrushes.Black, new PointF(0, 70));
            /*
            PdfGrid pdfGrid = new PdfGrid();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Prostorija");
            dataTable.Columns.Add("Broj pregleda u vremenskom intervalu");

            foreach(Room r in rooms)
            {
                List<ScheduledAppointment> sa = (List<ScheduledAppointment>) app.ScheduledAppointmentController.GetFromToDatesForRoom(start, end, r.Id);
                dataTable.Rows.Add(new object[] { r.Name, sa.Count });
                
            }    

            pdfGrid.DataSource = dataTable;
            PdfGridRowStyle pdfGridRowStyle = new PdfGridRowStyle();

            pdfGridRowStyle.BackgroundBrush = PdfBrushes.LightYellow;

            pdfGridRowStyle.Font = new PdfStandardFont(PdfFontFamily.Courier, 10);
            pdfGrid.Rows[0].Style = pdfGridRowStyle;

            pdfGrid.Draw(page, new PointF(10, 150));*/



            PdfLightTable pdfLightTable = new PdfLightTable(); 
            pdfLightTable.DataSourceType = PdfLightTableDataSourceType.TableDirect;
            pdfLightTable.Columns.Add(new PdfColumn(" Prostorija"));
            pdfLightTable.Columns.Add(new PdfColumn(" Broj pregleda u zadatom intervalu"));

            foreach (Room r in rooms)
            {
                List<ScheduledAppointment> sa = (List<ScheduledAppointment>)app.ScheduledAppointmentController.GetFromToDatesForRoom(start, end, r.Id);
                pdfLightTable.Rows.Add(new object[] {" " + r.Name, " " + sa.Count });

            }

            PdfFont font1 = new PdfStandardFont(PdfFontFamily.Helvetica, 14);
            PdfFont font2 = new PdfStandardFont(PdfFontFamily.Helvetica, 12);

            //Declare and define the alternate style.

            PdfCellStyle altStyle = new PdfCellStyle(font2, PdfBrushes.Black, PdfPens.Black);

            
            
            //altStyle.BackgroundBrush = PdfBrushes.DarkGray;

            //Declare and define the header style.

            PdfCellStyle headerStyle = new PdfCellStyle(font1, PdfBrushes.Black, PdfPens.Black);
            headerStyle.BackgroundBrush = PdfBrushes.LightGray;


            //pdfLightTable.Style.AlternateStyle = altStyle;

            pdfLightTable.Style.DefaultStyle = altStyle;

            pdfLightTable.Style.HeaderStyle = headerStyle;

            //Show header in the table

            pdfLightTable.Style.ShowHeader = true;

            //Draw the PdfLightTable.

            pdfLightTable.Draw(page, new PointF(10, 150));
            doc.Save("IzvestajSekretar.pdf");
            doc.Close(true);

            //DataTable invoiceDetails = GetProductDetailsAsDataTable
            /*PdfDocument doc = new PdfDocument();
            PdfPage page = doc.Pages.Add();
            PdfGraphics graphics = page.Graphics;
            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 16);
            graphics.DrawString("Raspored terapija na sedmicnom nivou (" + DateTime.Now.Day.ToString() + "." + DateTime.Now.Month.ToString() + " - " + DateTime.Now.AddDays(7).Day.ToString() + "." + DateTime.Now.AddDays(7).Month.ToString() + ")", font, PdfBrushes.Black, new PointF(0, 0));
            
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Prostorija");
            dataTable.Columns.Add("Broj pregleda u vremenskom intervalu");
            dataTable.Rows.Add(new object[] { "soba", "45" });
            dataTable.Rows.Add(new object[] { "soba2", "35" });
            //Creates a PDF grid
            PdfGrid grid = new PdfGrid();
            //Adds the data source
            // grid.DataSource = invoiceDetails;
            grid.DataSource = dataTable;
            //Creates the grid cell styles
            PdfGridCellStyle cellStyle = new PdfGridCellStyle();
            cellStyle.Borders.All = PdfPens.White;
            PdfGridRow header = grid.Headers[0];
            //Creates the header style
            PdfGridCellStyle headerStyle = new PdfGridCellStyle();
            headerStyle.Borders.All = new PdfPen(new PdfColor(126, 151, 173));
            headerStyle.BackgroundBrush = new PdfSolidBrush(new PdfColor(126, 151, 173));
            headerStyle.TextBrush = PdfBrushes.White;
            headerStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 14f, PdfFontStyle.Regular);

            //Adds cell customizations
            for (int i = 0; i < header.Cells.Count; i++)
            {
                if (i == 0 || i == 1)
                    header.Cells[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle);
                else
                    header.Cells[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Right, PdfVerticalAlignment.Middle);
            }

            //Applies the header style
            header.ApplyStyle(headerStyle);
            cellStyle.Borders.Bottom = new PdfPen(new PdfColor(217, 217, 217), 0.70f);
            cellStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 12f);
            cellStyle.TextBrush = new PdfSolidBrush(new PdfColor(131, 130, 136));
            //Creates the layout format for grid
            PdfGridLayoutFormat layoutFormat = new PdfGridLayoutFormat();
            // Creates layout format settings to allow the table pagination
            layoutFormat.Layout = PdfLayoutType.Paginate;
            //Draws the grid to the PDF page.
            //PdfGridLayoutResult gridResult = grid.Draw(page, new RectangleF(new PointF(0, result.Bounds.Bottom + 40), new SizeF(graphics.ClientSize.Width, graphics.ClientSize.Height - 100)), layoutFormat);
            doc.Save("IzvestajSekretar.pdf");
            doc.Close(true);*/
        }

    }
}
