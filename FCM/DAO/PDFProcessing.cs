﻿using FCM.DTO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace FCM.DAO
{
    class PDFProcessing
    {
        private static PDFProcessing instance;
        public static PDFProcessing Instance
        {
            get { if (instance == null) instance = new PDFProcessing(); return instance; }
            set => instance = value;
        }
        public void ExportRankingToPdf(System.Windows.Controls.DataGrid grid, List<TeamScoreDetails> rank, string nameBoard)
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
                MessageBox.Show("Xuất file thành công");
            }
        }

        public void ExportTeamStatistic(System.Windows.Controls.DataGrid grid, List<TeamStatistic> list)
        {
            //Add Header
            BaseFont bff = BaseFont.CreateFont(Environment.GetEnvironmentVariable("windir") + @"\\fonts\times.ttf", BaseFont.IDENTITY_H, true);
            iTextSharp.text.Font NormalFont = new iTextSharp.text.Font(bff, 20, iTextSharp.text.Font.NORMAL);
            Paragraph prgHeading = new Paragraph();
            prgHeading.Alignment = Element.ALIGN_CENTER;
            string strHeader = "Thống kê đội bóng";
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
            pdfPTable.SetWidths(new float[] { 70, 40, 140, 70, 70, 70, 70, 70, 70 });

            iTextSharp.text.Font text = new iTextSharp.text.Font(bff, 10, iTextSharp.text.Font.NORMAL);
            PdfPTable table = new PdfPTable(grid.Columns.Count);
            //Add header
            foreach (System.Windows.Controls.DataGridColumn column in grid.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column.Header.ToString(), text));
                cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfPTable.AddCell(cell);
            }

            //Add datarow
            int rCnt = 0;
            foreach (TeamStatistic team in list)
            {
                //Index
                pdfPTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                pdfPTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfPTable.AddCell(new Phrase(list[rCnt].index.ToString(), text));

                //Logo
                pdfPTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                pdfPTable.DefaultCell.VerticalAlignment = Element.ALIGN_BOTTOM;
                byte[] imageByte = ImageProcessing.Instance.convertBitmapImageToByte(list[rCnt].logo);
                iTextSharp.text.Image myImage = iTextSharp.text.Image.GetInstance(imageByte);
                myImage.ScaleAbsolute(20, 20);
                pdfPTable.AddCell(new Phrase(new Chunk(myImage, 0f, 0f, false)));

                //NameTeam
                pdfPTable.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfPTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfPTable.AddCell(new Phrase(list[rCnt].nameTeam.ToString(), text));

                //Match
                pdfPTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                pdfPTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfPTable.AddCell(new Phrase(list[rCnt].m.ToString(), text));

                //GF
                pdfPTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                pdfPTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfPTable.AddCell(new Phrase(list[rCnt].gf.ToString(), text));

                //GA
                pdfPTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                pdfPTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfPTable.AddCell(new Phrase(list[rCnt].ga.ToString(), text));

                //YC
                pdfPTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                pdfPTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfPTable.AddCell(new Phrase(list[rCnt].yc.ToString(), text));

                //RC
                pdfPTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                pdfPTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfPTable.AddCell(new Phrase(list[rCnt].rc.ToString(), text));

                //SumC
                pdfPTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                pdfPTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfPTable.AddCell(new Phrase(list[rCnt].sumc.ToString(), text));

                rCnt++;
            }

            //save file;
            var savefiledialoge = new SaveFileDialog();
            savefiledialoge.FileName = "Thống kê đội bóng" ;
            savefiledialoge.DefaultExt = ".pdf";

            if (savefiledialoge.ShowDialog() == true)
            {
                using (FileStream stream = new FileStream(savefiledialoge.FileName, FileMode.Create))
                {
                    Document pdfdoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    PdfWriter.GetInstance(pdfdoc, stream);
                    pdfdoc.Open();
                    pdfdoc.Add(prgHeading);
                    pdfdoc.Add(pdfPTable);
                    pdfdoc.Close();
                    stream.Close();
                    MessageBox.Show("Xuất file thành công");
                }
            }
        }
        public void ExportPlayerStatistic(System.Windows.Controls.DataGrid grid, List<PlayerStatistic> list)
        {
            //Add Header
            BaseFont bff = BaseFont.CreateFont(Environment.GetEnvironmentVariable("windir") + @"\\fonts\times.ttf", BaseFont.IDENTITY_H, true);
            iTextSharp.text.Font NormalFont = new iTextSharp.text.Font(bff, 20, iTextSharp.text.Font.NORMAL);
            Paragraph prgHeading = new Paragraph();
            prgHeading.Alignment = Element.ALIGN_CENTER;
            string strHeader = "Thống kê cầu thủ";
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
            pdfPTable.SetWidths(new float[] { 60, 160, 60, 40, 140, 70, 70, 70, 70 });

            iTextSharp.text.Font text = new iTextSharp.text.Font(bff, 10, iTextSharp.text.Font.NORMAL);
            PdfPTable table = new PdfPTable(grid.Columns.Count);
            //Add header
            foreach (System.Windows.Controls.DataGridColumn column in grid.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column.Header.ToString(), text));
                cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfPTable.AddCell(cell);
            }

            //Add datarow
            int rCnt = 0;
            foreach (PlayerStatistic team in list)
            {
                //Index
                pdfPTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                pdfPTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfPTable.AddCell(new Phrase(list[rCnt].index.ToString(), text));

                //NamePlayer
                pdfPTable.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfPTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfPTable.AddCell(new Phrase(list[rCnt].namePlayer.ToString(), text));

                //Number
                pdfPTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                pdfPTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfPTable.AddCell(new Phrase(list[rCnt].number.ToString(), text));

                //Logo
                pdfPTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                pdfPTable.DefaultCell.VerticalAlignment = Element.ALIGN_BOTTOM;
                byte[] imageByte = ImageProcessing.Instance.convertBitmapImageToByte(list[rCnt].logo);
                iTextSharp.text.Image myImage = iTextSharp.text.Image.GetInstance(imageByte);
                myImage.ScaleAbsolute(20, 20);
                pdfPTable.AddCell(new Phrase(new Chunk(myImage, 0f, 0f, false)));

                //NameTeam
                pdfPTable.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfPTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfPTable.AddCell(new Phrase(list[rCnt].nameTeam.ToString(), text));

                //Goal
                pdfPTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                pdfPTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfPTable.AddCell(new Phrase(list[rCnt].goal.ToString(), text));

                //Assist
                pdfPTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                pdfPTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfPTable.AddCell(new Phrase(list[rCnt].assist.ToString(), text));

                //YC
                pdfPTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                pdfPTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfPTable.AddCell(new Phrase(list[rCnt].yc.ToString(), text));

                //RC
                pdfPTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                pdfPTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfPTable.AddCell(new Phrase(list[rCnt].rc.ToString(), text));

                rCnt++;
            }

            //save file;
            var savefiledialoge = new SaveFileDialog();
            savefiledialoge.FileName = "Thống kê cầu thủ";
            savefiledialoge.DefaultExt = ".pdf";

            if (savefiledialoge.ShowDialog() == true)
            {
                using (FileStream stream = new FileStream(savefiledialoge.FileName, FileMode.Create))
                {
                    Document pdfdoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    PdfWriter.GetInstance(pdfdoc, stream);
                    pdfdoc.Open();
                    pdfdoc.Add(prgHeading);
                    pdfdoc.Add(pdfPTable);
                    pdfdoc.Close();
                    stream.Close();
                    MessageBox.Show("Xuất file thành công");
                }
            }
        }
        public void ExportCardStatistic(System.Windows.Controls.DataGrid grid, List<CardStatistic> list)
        {
            //Add Header
            BaseFont bff = BaseFont.CreateFont(Environment.GetEnvironmentVariable("windir") + @"\\fonts\times.ttf", BaseFont.IDENTITY_H, true);
            iTextSharp.text.Font NormalFont = new iTextSharp.text.Font(bff, 20, iTextSharp.text.Font.NORMAL);
            Paragraph prgHeading = new Paragraph();
            prgHeading.Alignment = Element.ALIGN_CENTER;
            string strHeader = "Thống kê thẻ phạt";
            prgHeading.Add(new Chunk(strHeader.ToUpper(), NormalFont));


            // Add Table
            PdfPTable pdfPTable = new PdfPTable(grid.Columns.Count);
            pdfPTable.SpacingBefore = 10f;
            pdfPTable.DefaultCell.Padding = 3;
            pdfPTable.WidthPercentage = 60;
            pdfPTable.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfPTable.DefaultCell.BorderWidth = 1;
            pdfPTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfPTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfPTable.SetWidths(new float[] { 70, 70, 70, 70 });

            iTextSharp.text.Font text = new iTextSharp.text.Font(bff, 10, iTextSharp.text.Font.NORMAL);
            PdfPTable table = new PdfPTable(grid.Columns.Count);
            //Add header
            foreach (System.Windows.Controls.DataGridColumn column in grid.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column.Header.ToString(), text));
                cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfPTable.AddCell(cell);
            }

            //Add datarow
            int rCnt = 0;
            foreach (CardStatistic team in list)
            {
                //Round
                pdfPTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                pdfPTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfPTable.AddCell(new Phrase(list[rCnt].round.ToString(), text));

                //YC
                pdfPTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                pdfPTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfPTable.AddCell(new Phrase(list[rCnt].yc.ToString(), text));

                //RC
                pdfPTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                pdfPTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfPTable.AddCell(new Phrase(list[rCnt].rc.ToString(), text));

                //SumC
                pdfPTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                pdfPTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfPTable.AddCell(new Phrase(list[rCnt].sumc.ToString(), text));

                rCnt++;
            }

            //save file;
            var savefiledialoge = new SaveFileDialog();
            savefiledialoge.FileName = "Thống kê thẻ phạt";
            savefiledialoge.DefaultExt = ".pdf";

            if (savefiledialoge.ShowDialog() == true)
            {
                using (FileStream stream = new FileStream(savefiledialoge.FileName, FileMode.Create))
                {
                    Document pdfdoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    PdfWriter.GetInstance(pdfdoc, stream);
                    pdfdoc.Open();
                    pdfdoc.Add(prgHeading);
                    pdfdoc.Add(pdfPTable);
                    pdfdoc.Close();
                    stream.Close();
                }
                MessageBox.Show("Xuất file thành công");
            }
        }
    }
}
