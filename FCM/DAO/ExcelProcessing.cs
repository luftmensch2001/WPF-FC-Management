using System;
using System.Collections.Generic;
using System.Text;
using FCM.DTO;
using System.Data;
using OfficeOpenXml;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Drawing;
using FCM.ViewModel;
using FCM.View;
using System.IO;
using OfficeOpenXml.Style;
using OfficeOpenXml.Drawing;
using System.Diagnostics;

namespace FCM.DAO
{
    class ExcelProcessing
    {
        private static ExcelProcessing instance;

        public static ExcelProcessing Instance
        {
            get { if (instance == null) instance = new ExcelProcessing(); return instance; }
            set => instance = value;
        }
        public bool ImportTeam(AddTeamWindow parameter)
        {
            MessageBox.Show("Import");
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            Team team;
            OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Excel files(*.xml;*.xlsx;*.xlsm)|*.xml;*.xlsx;*.xlsm", Multiselect = false };
            openFileDialog.ShowDialog();
            string path = openFileDialog.FileName;
            ExcelPackage package = null;
            if (path != "")
            {
                try
                {
                    try
                    {
                        package = new ExcelPackage(path);
                        ExcelWorksheet workSheet = package.Workbook.Worksheets[0];
                        bool isFull = true;
                        for (int i = 0; i < parameter.boards.Count; i++)
                            if (workSheet.Cells[3, 3].Value.ToString() == parameter.boards[i].nameBoard)
                            {
                                isFull = false;
                                break;
                            }
                        if (isFull)
                        {
                            MessageBox.Show("Bảng này đã đủ số lượng đội bóng");
                            return false;
                        }


                        string namePicture = workSheet.Cells[7, 3].Value.ToString();
                        var pic = workSheet.Drawings[namePicture] as ExcelPicture;
                        int countPlayer = Int32.Parse(workSheet.Cells[4, 3].Value.ToString());
                        team = new Team(parameter.idTournament, workSheet.Cells[3, 3].Value.ToString(),
                                                                workSheet.Cells[2, 3].Value.ToString(),
                                                                workSheet.Cells[5, 3].Value.ToString(),
                                                                workSheet.Cells[6, 3].Value.ToString(),
                                                                workSheet.Cells[7, 3].Value.ToString(),
                                                                ImageProcessing.Instance.convertImgToByte(pic.Image));
                        if (team.idTournamnt == -1 || team.nameTeam == "" || team.coach == "" || team.nation == "" || team.stadium == "")
                        {
                            MessageBox.Show("Thiếu thông tin đội bóng", "lỗi");
                            return false;
                        }
                        TeamDAO.Instance.CreateTeams(team);
                        int countOutNation = 0;
                        int countPlayerDone = 0;
                        try
                        {
                            for (int i = 15; i < 15 + countPlayer; i++)
                            {
                                if (countPlayerDone < parameter.setting.maxPlayerOfTeam)
                                    if (countOutNation < parameter.setting.maxForeignPlayers || workSheet.Cells[i, 6].Value.ToString() == team.nation)
                                    {
                                        string namePicturePlayer = workSheet.Cells[7, 3].Value.ToString();
                                        pic = workSheet.Drawings[namePicturePlayer] as ExcelPicture;
                                        Player player = new Player(TeamDAO.Instance.GetNewestTeamid(team.idTournamnt),
                                                                    workSheet.Cells[i, 2].Value.ToString(),
                                                                    Int32.Parse(workSheet.Cells[i, 3].Value.ToString()),
                                                                    DateTime.Parse(workSheet.Cells[i, 5].Value.ToString()),
                                                                    workSheet.Cells[i, 4].Value.ToString(),
                                                                    workSheet.Cells[i, 6].Value.ToString(),
                                                                    workSheet.Cells[i, 7].Value.ToString(),
                                                                    ImageProcessing.Instance.convertImgToByte(pic.Image));
                                        PlayerDAO.Instance.CreatePlayers(player);
                                        if (workSheet.Cells[i, 6].Value.ToString() != team.nation)
                                            countOutNation++;
                                        countPlayerDone++;
                                    }
                            }
                        }
                        catch
                        {
                            MessageBox.Show("Thông tin cầu thủ lỗi", "Lỗi");
                            return false;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Thông tin đội bóng lỗi", "Lỗi");
                        return false;
                    }
                }
 
                
                catch
                {
                    MessageBox.Show("File không hợp lệ, vui lòng chọn lại", "Lỗi");
                    return false;
                }
            }
            if (package == null)
                return false;
            return true;
        }
        public void ExportFile(Team team)
        {
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            string filePath = "";
            // tạo SaveFileDialog để lưu file excel
            SaveFileDialog dialog = new SaveFileDialog();

            // chỉ lọc ra các file có định dạng Excel
            dialog.Filter = "Excel | *.xlsx | Excel 2003 | *.xls";

            // Nếu mở file và chọn nơi lưu file thành công sẽ lưu đường dẫn lại dùng
            if (dialog.ShowDialog() == true)
            {
                filePath = dialog.FileName;
            }
            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Đường dẫn báo cáo không hợp lệ");
                return;
            }

            using (ExcelPackage p = new ExcelPackage())
            {
                // đặt tên người tạo file
                p.Workbook.Properties.Author = "Group6";

                // đặt tiêu đề cho file
                p.Workbook.Properties.Title = "Đội bóng";

                //Tạo một sheet để làm việc trên đó
                p.Workbook.Worksheets.Add(team.nameTeam);

                // lấy sheet vừa add ra để thao tác
                ExcelWorksheet ws = p.Workbook.Worksheets[0];

                // đặt tên cho sheet
                ws.Name = team.nameTeam;
                // fontsize mặc định cho cả sheet
                ws.Cells.Style.Font.Size = 11;
                // font family mặc định cho cả sheet
                ws.Cells.Style.Font.Name = "Calibri";

                /////////////////////////////////////////Bảng đội bóng /////////////////////////////////////////////////////////


                ///Tiêu đề đội bóng
                ///
                List<Player> players = PlayerDAO.Instance.GetListPlayer(team.id);
                ws.Cells[1, 1, 1, 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[1, 1, 1, 3].Merge = true;
                ws.Cells[1, 1, 1, 3].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                ws.Cells[1, 1, 1, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[1, 1, 1, 3].Value = "Thông tin đội bóng";
                ws.Cells[1, 1, 1, 3].Style.Border.BorderAround(ExcelBorderStyle.Thin);


                for (int i = 2; i <= 7; i++)
                {
                    ws.Cells[i, 1, i, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    ws.Cells[i, 1, i, 2].Merge = true;
                    ws.Cells[i, 1, i, 2].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                    ws.Cells[i, 1, i, 2].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    ws.Cells[i, 1, i, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }
                ///Tên đội
                ws.Cells[2, 1, 2, 2].Value = "Tên đội bóng";
                ws.Cells[2, 3].Value = team.nameTeam;
                ws.Cells[2, 3].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                ///Tên bảng
                ws.Cells[3, 1, 3, 2].Value = "Tên bảng đấu";
                ws.Cells[3, 3].Value = team.nameBoard;
                ws.Cells[3, 3].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                ///
                ws.Cells[4, 1, 4, 2].Value = "Số cầu thủ";
                ws.Cells[4, 3].Value = players.Count;
                ws.Cells[4, 3].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                ///Huấn luyện viên
                ws.Cells[5, 1, 5, 2].Value = "Huấn luyện viên";
                ws.Cells[5, 3].Value = team.coach;
                ws.Cells[5, 3].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                ///Sân đấu
                ws.Cells[6, 1, 6, 2].Value = "Sân nhà";
                ws.Cells[6, 3].Value = team.stadium;
                ws.Cells[6, 3].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                ///Quốc gia
                ws.Cells[7, 1, 7, 2].Value = "Quốc gia";
                ws.Cells[7, 3].Value = team.nation;
                ws.Cells[7, 3].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                ///Hình ảnh đội
                ws.Cells[8, 1, 9, 1].Value = "Logo đội";
                ws.Cells[8, 1, 9, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[8, 1, 9, 1].Merge = true;
                ws.Cells[8, 1, 9, 1].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                ws.Cells[8, 1, 9, 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                ws.Cells[8, 1, 9, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                //Tên hình ảnh
                ws.Cells[8, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[8, 2].Merge = true;
                ws.Cells[8, 2].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                ws.Cells[8, 2].Value = "Tên hình ảnh";
                ws.Cells[8, 2].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                ws.Cells[8, 3].Value = team.nameTeam;
                ws.Cells[8, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                //Hình ảnh
                ws.Cells[9, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[9, 2].Merge = true;
                ws.Cells[9, 2].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                ws.Cells[9, 2].Value = "Hình ảnh";
                ws.Cells[9, 2].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                var pic = ws.Drawings.AddPicture(team.nameTeam, ImageProcessing.Instance.ByteToImg(team.logo));
                ws.Rows[9].Height = 60;
                ws.Columns.Width = 60;
                pic.SetPosition(8, 10, 2, 80);
                pic.SetSize((int)ws.Columns[3].Width, (int)ws.Rows[9].Height);
                ws.Cells[9, 3].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                // Auto fill
                ws.Columns[1, 10].AutoFit(30);

                // Border Table
                ws.Cells[1, 1, 9, 3].Style.Border.BorderAround(ExcelBorderStyle.Thick);
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                /////////////////////////////////////////Danh sách cầu thủ /////////////////////////////////////////////////////////

                ///Tiêu đề cầu thủ
                ws.Cells[12, 1, 12, 9].Value = "Danh sách cầu thủ";
                ws.Cells[12, 1, 12, 9].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[12, 1, 12, 9].Merge = true;
                ws.Cells[12, 1, 12, 9].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                ws.Cells[12, 1, 12, 9].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                ws.Cells[13, 1, 14, 9].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[13, 1, 14, 9].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                for (int i = 1; i <= 7; i++)
                {
                    ws.Cells[13, i, 14, i].Merge = true;
                    ws.Cells[13, i, 14, i].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                }

                ///STT
                ws.Cells[13, 1, 13, 1].Value = "STT";

                ///Tên cầu thủ
                ws.Cells[13, 2, 13, 2].Value = "Tên cầu thủ";

                ///Số áo
                ws.Cells[13, 3, 13, 3].Value = "Số áo thi đấu";

                ///Vị trí
                ws.Cells[13, 4, 13, 4].Value = "Vị trí";

                ///Ngày sinh
                ws.Cells[13, 5, 13, 5].Value = "Ngày sinh";

                ///Quốc gia
                ws.Cells[13, 6, 13, 6].Value = "Quốc gia";

                ///Chú ý
                ws.Cells[13, 7, 13, 7].Value = "Ghi chú";

                ///Hình ảnh
                ws.Cells[13, 8, 13, 9].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[13, 8, 13, 9].Merge = true;
                ws.Cells[13, 8, 13, 9].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                ws.Cells[13, 8, 13, 8].Value = "Hình ảnh cầu thủ cầu thủ";
                ws.Cells[13, 8, 13, 9].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                //Tên hình ảnh
                ws.Cells[14, 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[14, 8].Merge = true;
                ws.Cells[14, 8].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                ws.Cells[14, 8].Value = "Tên hình ảnh";
                ws.Cells[14, 8].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                //Hình ảnh
                ws.Cells[14, 9].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[14, 9].Merge = true;
                ws.Cells[14, 9].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                ws.Cells[14, 9].Value = "Hình ảnh";
                ws.Cells[14, 9].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                /////////////////////////////////////////////
                /////////////////List Player/////////////////
                /////////////////////////////////////////////

                for (int i = 0; i < players.Count; i++)
                {
                    ws.Cells[14 + i + 1, 1].Value = i + 1;
                    ws.Cells[14 + i + 1, 2].Value = players[i].namePlayer;
                    ws.Cells[14 + i + 1, 3].Value = players[i].uniformNumber;
                    ws.Cells[14 + i + 1, 4].Value = players[i].position;
                    ws.Cells[14 + i + 1, 5].Value = players[i].birthDay.ToString("M/d/yyyy");
                    ws.Cells[14 + i + 1, 6].Value = players[i].nationality;
                    ws.Cells[14 + i + 1, 7].Value = players[i].note;
                    ws.Cells[14 + i + 1, 8].Value = players[i].namePlayer;
                    pic = ws.Drawings.AddPicture(players[i].namePlayer, ImageProcessing.Instance.ByteToImg(team.logo));
                    ws.Rows[14 + i + 1].Height = 60;
                    ws.Columns[9].Width = 60;
                    pic.SetPosition(14 + i, 10, 8, 80);
                    pic.SetSize((int)ws.Columns[9].Width, (int)ws.Rows[14 + i + 1].Height);
                    for (int j = 1; j <= 9; j++)
                    {
                        ws.Cells[14 + i + 1, j].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    }
                }



                /////////////////////////////////////////////
                /////////////////////////////////////////////
                /////////////////////////////////////////////

                // Border Table
                ws.Columns[1, 10].AutoFit(30);
                int count = players.Count;
                ws.Cells[12, 1, 14 + count, 9].Style.Border.BorderAround(ExcelBorderStyle.Thick);
                ws.Cells[1, 1, 14 + count, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[1, 1, 14 + count, 9].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                Byte[] bin = p.GetAsByteArray();
                File.WriteAllBytes(filePath, bin);

                var proc = new Process();
                proc.StartInfo = new ProcessStartInfo(filePath)
                {
                    UseShellExecute = true
                };
                proc.Start();
            }
        }
    }
}
