using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using ProjectSims.Domain.Model;

namespace ProjectSims.Service
{
    public static class PDFService
    {
        private static string OpenFilePicker()
        {
            SaveFileDialog saveFileDialog = new();
            saveFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
            saveFileDialog.DefaultExt = "pdf";
            if (saveFileDialog.ShowDialog() == true)
                return saveFileDialog.FileName;
            throw new Exception("Save file dialog returned error!");
        }

        public static void GenerateValidVouchersPDF(List<Voucher> vouchers)
        {
            try
            {
                string filePath = OpenFilePicker();

                Document document = new();
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
                writer.SetPdfVersion(PdfWriter.PDF_VERSION_1_7);
                writer.SetFullCompression();

                document.Open();              

                // Dodavanje slike
                string imagePath = "Resources/Images/Guest2/whitelogo.png";
                string absolutePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, imagePath);
                Image image = Image.GetInstance(absolutePath);
                image.Alignment = Element.ALIGN_CENTER;
                image.ScaleToFit(150f, 150f); // Prilagodi veličinu slike
                document.Add(image);

                // Dodavanje praznog retka za razmak
                document.Add(new Paragraph(" "));

                // Dodavanje naslova iznad tablice
                Paragraph title = new Paragraph("Lista svih trenutno vazecih vaucera:", new Font(Font.FontFamily.HELVETICA, 18, Font.BOLD));
                title.Alignment = Element.ALIGN_CENTER;
                document.Add(title);

                // Dodavanje praznog retka za razmak
                document.Add(new Paragraph(" "));

                // Dodavanje tablice
                PdfPTable table = new PdfPTable(3); // 3 stupca
                table.WidthPercentage = 90; // Postavljanje širine tablice na 100% širine stranice

                // Dodavanje zaglavlja tablice
                table.AddCell("Broj Vaucera");
                table.AddCell("Datum osvajanja vaucera");
                table.AddCell("Datum do kada vazi");

                // Dodavanje redova iz liste vaučera
                foreach (Voucher voucher in vouchers)
                {
                    table.AddCell(voucher.Id.ToString());
                    table.AddCell(voucher.CreationDate.ToString(("dd.MM.yyyy")));
                    table.AddCell(voucher.ExpirationDate.ToString(("dd.MM.yyyy")));
                }

                document.Add(table);

                document.Add(new Paragraph(" "));

                // Dodavanje paragrafa ispod tablice
                string paragraphText = "U tabeli se nalazi lista svih vazecih vaucera." +
                    " Odgovarajuci vaucer mozete upotrijebiti samo jednom prilikom rezervacije ture." +
                    " Kada se tura rezervise upotrebom vaucera, automatski taj vaucer postaje nevazeci." +
                    " Takodje, vaucer postaje nevazeci ukoliko ostane neupotrebljen do datuma isteka vaucera.";
                Paragraph paragraph = new Paragraph(paragraphText);
                document.Add(paragraph);

                // Dodavanje trenutnog datuma i vremena
                DateTime currentTime = DateTime.Now;
                string currentDateTime = currentTime.ToString("dd.MM.yyyy HH:mm:ss");
                Paragraph dateTimeParagraph = new Paragraph("Trenutni datum i vrijeme: " + currentDateTime);
                dateTimeParagraph.Alignment = Element.ALIGN_RIGHT;
                document.Add(dateTimeParagraph);

                document.Close();

                MessageBox.Show("PDF fajl je uspjesno izgenerisan.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greska prilikom generisanaja PDF fajla: " + ex.Message);
            }
        }
        
    }
}
