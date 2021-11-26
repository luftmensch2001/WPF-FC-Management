using FCM.DTO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FCM.DAO
{
    class PDFProcessing
    {
        void ExportToPdf(System.Windows.Controls.DataGrid grid, List<TeamScoreDetails> rank, string nameBoard)
        {
            //Add Header
            BaseFont bff = BaseFont.CreateFont(Environment.GetEnvironmentVariable("windir") + @"\\fonts\times.ttf", BaseFont.IDENTITY_H, true);
            iTextSharp.text.Font NormalFont = new iTextSharp.text.Font(bff, 20, iTextSharp.text.Font.NORMAL);
            Paragraph prgHeading = new Paragraph();
            prgHeading.Alignment = Element.ALIGN_CENTER;
            string strHeader = "Bảng xếp hạng";
            prgHeading.Add(new Chunk(strHeader.ToUpper(), NormalFont));


            // Add Table
            PdfPTable pdfPTable = new PdfPTable(grid.Columns.Count);
            pdfPTable.SpacingBefore = 10f;
            pdfPTable.DefaultCell.Padding = 3;
            pdfPTable.WidthPercentage = 100;
            pdfPTable.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfPTable.DefaultCell.BorderWidth = 1;
            pdfPTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfPTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfPTable.SetWidths(new float[] { 70, 40, 140, 70, 70, 70, 70, 70, 70, 140 });



            iTextSharp.text.Font text = new iTextSharp.text.Font(bff, 10, iTextSharp.text.Font.NORMAL);
            PdfPTable table = new PdfPTable(grid.Columns.Count);
            //Add header
            foreach (System.Windows.Controls.DataGridColumn column in grid.Columns)
            {
                //MessageBox.Show(column.Header.ToString());
                PdfPCell cell = new PdfPCell(new Phrase(column.Header.ToString(), text));
                cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfPTable.AddCell(cell);
            }
            //Add datarow
            int rCnt = 0;
            foreach (TeamScoreDetails team in rank)
            {
                //Rank
                pdfPTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                pdfPTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfPTable.AddCell(new Phrase(rank[rCnt].rankTeam.ToString(), text));

                //Logo
                pdfPTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                pdfPTable.DefaultCell.VerticalAlignment = Element.ALIGN_BOTTOM;
                byte[] imageByte = ImageProcessing.Instance.convertBitmapImageToByte(rank[rCnt].logo);
                iTextSharp.text.Image myImage = iTextSharp.text.Image.GetInstance(imageByte);
                myImage.ScaleAbsolute(20, 20);
                pdfPTable.AddCell(new Phrase(new Chunk(myImage, 0f, 0f, false)));

                //NameTeam
                pdfPTable.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfPTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfPTable.AddCell(new Phrase(rank[rCnt].nameTeam.ToString(), text));

                //Match
                pdfPTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                pdfPTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfPTable.AddCell(new Phrase(rank[rCnt].m.ToString(), text));

                //Win
                pdfPTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                pdfPTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfPTable.AddCell(new Phrase(rank[rCnt].w.ToString(), text));

                //Draw
                pdfPTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                pdfPTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfPTable.AddCell(new Phrase(rank[rCnt].d.ToString(), text));

                //Lose
                pdfPTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                pdfPTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfPTable.AddCell(new Phrase(rank[rCnt].l.ToString(), text));

                //Point
                pdfPTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                pdfPTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfPTable.AddCell(new Phrase(rank[rCnt].pts.ToString(), text));

                //GoalDraw
                pdfPTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                pdfPTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfPTable.AddCell(new Phrase(rank[rCnt].gD.ToString(), text));

                //ImageFLM
                imageByte = ImageProcessing.Instance.convertBitmapImageToByte(rank[rCnt].imageFLM);
                myImage = iTextSharp.text.Image.GetInstance(imageByte);
                pdfPTable.AddCell(myImage);

                rCnt++;
            }

            //save file;
            var savefiledialoge = new SaveFileDialog();
            savefiledialoge.FileName = "BXH" + nameBoard;
            savefiledialoge.DefaultExt = ".pdf";

            if (savefiledialoge.ShowDialog() == true)
            {
                using (FileStream stream = new FileStream(savefiledialoge.FileName, FileMode.Create))
                {
                    Document pdfdoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    PdfWriter.GetInstance(pdfdoc, stream);
                    pdfdoc.Open();
                    pdfdoc.Add(prgHeading);
                    if (nameBoard != " ")
                    {
                        Paragraph p = new Paragraph(nameBoard, text);
                        p.Alignment = Element.ALIGN_CENTER;
                        pdfdoc.Add(p);
                    }
                    pdfdoc.Add(pdfPTable);
                    pdfdoc.Close();
                    stream.Close();
                }
            }
        }
    }
}
