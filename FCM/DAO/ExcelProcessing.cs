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
using System.Globalization;

namespace FCM.DAO
{
    class ExcelProcessing
    {
        private static ExcelProcessing instance;
        private string formatInfor;

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
                //try
                //{
                //    try
                //    {
                package = new ExcelPackage(path);
                ExcelWorksheet workSheet = package.Workbook.Worksheets[0];
                bool isFull = true;
                for (int i = 0; i < parameter.boards.Count; i++)
                    if (workSheet.Cells[3, 2].Value.ToString() == parameter.boards[i].nameBoard)
                    {
                        isFull = false;
                        break;
                    }
                if (isFull)
                {
                    MessageBox.Show("Bảng này đã đủ số lượng đội bóng");
                    return false;
                }


                string namePicture = workSheet.Cells[2, 2].Value.ToString();
                var pic = workSheet.Drawings[namePicture] as ExcelPicture;
                int countPlayer = Int32.Parse(workSheet.Cells[4, 2].Value.ToString());
                team = new Team(parameter.idTournament, workSheet.Cells[3, 2].Value.ToString(),
                                                        workSheet.Cells[2, 2].Value.ToString(),
                                                        workSheet.Cells[5, 2].Value.ToString(),
                                                        workSheet.Cells[6, 2].Value.ToString(),
                                                        workSheet.Cells[7, 2].Value.ToString(),
                                                        ImageProcessing.Instance.convertImgToByte(pic.Image));
                if (team.idTournamnt == -1 || team.nameTeam == "" || team.coach == "" || team.nation == "" || team.stadium == "")
                {
                    MessageBox.Show("Thiếu thông tin đội bóng", "lỗi");
                    return false;
                }
                TeamDAO.Instance.CreateTeams(team);
                int countOutNation = 0;
                int countPlayerDone = 0;
                //try
                //{
                for (int i = 13; i < 13 + countPlayer; i++)
                {
                    if (countPlayerDone < parameter.setting.maxPlayerOfTeam)
                        if (countOutNation < parameter.setting.maxForeignPlayers || workSheet.Cells[i, 6].Value.ToString() == team.nation)
                        {
                            string namePicturePlayer = workSheet.Cells[i, 3].Value.ToString();
                            pic = workSheet.Drawings[namePicturePlayer] as ExcelPicture;

                            if (workSheet.Cells[i, 3].Value != null &&
                                workSheet.Cells[i, 4].Value != null &&
                                workSheet.Cells[i, 6].Value != null &&
                                workSheet.Cells[i, 5].Value != null &&
                                workSheet.Cells[i, 7].Value != null &&
                                workSheet.Cells[i, 6].Value != null &&
                                pic.Image != null)
                            {
                                //MessageBox.Show(pic.Name);
                                //MessageBox.Show(workSheet.Cells[i, 6].Value.ToString());
                                string date = workSheet.Cells[i, 6].Value.ToString();
                                InputFormat.Instance.FomartSpace(date);
                                DateTime result;

                                if (date[1] == '/')
                                    date = "0" + date;
                                if (date[4] == '/')
                                    date = date.Insert(3,"0");
                                if (date.Length > 10)
                                    date = date.Remove(10);
                               // MessageBox.Show(date);
                                DateTime.TryParseExact(date, "dd/MM/yyyy", null,DateTimeStyles.None,out result);

                                result = DateTime.ParseExact(result.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null, DateTimeStyles.None);
                                //MessageBox.Show(result.ToString("dd/MM/yyyy"));
                                Player player = new Player(TeamDAO.Instance.GetNewestTeamid(team.idTournamnt),
                                                            workSheet.Cells[i, 3].Value.ToString(),
                                                            Int32.Parse(workSheet.Cells[i, 4].Value.ToString()),
                                                            result,
                                                            //DateTime.Parse(workSheet.Cells[i, 6].Value.ToString()),
                                                            workSheet.Cells[i, 5].Value.ToString(),
                                                            workSheet.Cells[i, 7].Value.ToString(),
                                                            workSheet.Cells[i, 8].Value == null ? "" : workSheet.Cells[i, 8].Value.ToString(),
                                                            ImageProcessing.Instance.convertImgToByte(pic.Image));
                                PlayerDAO.Instance.CreatePlayers(player);
                                if (workSheet.Cells[i, 7].Value.ToString() != team.nation)
                                    countOutNation++;
                                countPlayerDone++;
                            }
                        }
                }
                //}
                //catch
                //{
                //    MessageBox.Show("Thông tin cầu thủ lỗi", "Lỗi");
                //    return false;
                //}
                //    }
                //    catch
                //    {
                //        MessageBox.Show("Thông tin đội bóng lỗi", "Lỗi");
                //        return false;
                //    }
                //}


                //catch
                //{
                //    MessageBox.Show("File không hợp lệ, vui lòng chọn lại", "Lỗi");
                //    return false;
                //}
            }
            if (package == null)
                return false;
            return true;
        }
        public void ExportFile(Team team)
        {
            try
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
                    ws.Cells[1, 1, 1, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    ws.Cells[1, 1, 1, 2].Merge = true;
                    ws.Cells[1, 1, 1, 2].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                    ws.Cells[1, 1, 1, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[1, 1, 1, 2].Value = "Thông tin đội bóng";
                    ws.Cells[1, 1, 1, 2].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                    ///Tên đội
                    ws.Cells[2, 1].Value = "Tên đội bóng";
                    ws.Cells[2, 2].Value = team.nameTeam;
                    ws.Cells[2, 2].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                    ///Tên bảng
                    ws.Cells[3, 1].Value = "Tên bảng đấu";
                    ws.Cells[3, 2].Value = team.nameBoard;
                    ///
                    ws.Cells[4, 1].Value = "Số cầu thủ";
                    ws.Cells[4, 2].Value = players.Count;
                    ///Huấn luyện viên
                    ws.Cells[5, 1].Value = "Huấn luyện viên";
                    ws.Cells[5, 2].Value = team.coach;
                    ///Sân đấu
                    ws.Cells[6, 1].Value = "Sân nhà";
                    ws.Cells[6, 2].Value = team.stadium;
                    ///Quốc gia
                    ws.Cells[7, 1].Value = "Quốc gia";
                    ws.Cells[7, 2].Value = team.nation;
                    ///Hình ảnh đội
                    ws.Cells[8, 1].Value = "Quốc gia";
                    ws.Cells[8, 2].Value = team.logo;
                    var pic = ws.Drawings.AddPicture(team.nameTeam, ImageProcessing.Instance.ByteToImg(team.logo));
                    ws.Rows[8].Height = 60;
                    ws.Columns.Width = 60;
                    pic.SetPosition(7, 10, 1, 80);
                    pic.SetSize((int)ws.Columns[2].Width, (int)ws.Rows[8].Height);

                    for (int i = 0; i < 8; i++)
                    {
                        ws.Cells[i + 1, 2].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        ws.Cells[i + 1, 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    }
                    ws.Cells[2, 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    ws.Cells[2, 8].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                    // Auto fill
                    ws.Columns[1, 10].AutoFit(30);

                    // Border Table
                    ws.Cells[1, 1, 8, 2].Style.Border.BorderAround(ExcelBorderStyle.Thick);
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    /////////////////////////////////////////Danh sách cầu thủ /////////////////////////////////////////////////////////

                    ///Tiêu đề cầu thủ
                    ws.Cells[11, 1, 11, 8].Value = "Danh sách cầu thủ";
                    ws.Cells[11, 1, 11, 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    ws.Cells[11, 1, 11, 8].Merge = true;
                    ws.Cells[11, 1, 11, 8].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                    ws.Cells[11, 1, 11, 8].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                    ws.Cells[12, 1, 12, 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    ws.Cells[12, 1, 12, 8].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);

                    ///STT
                    ws.Cells[12, 1].Value = "STT";
                    ///Tên cầu thủ
                    ws.Cells[12, 2].Value = "Hình ảnh";
                    ///Tên cầu thủ
                    ws.Cells[12, 3].Value = "Tên";
                    ///Số áo
                    ws.Cells[12, 4].Value = "Số áo";
                    ///Vị trí
                    ws.Cells[12, 5].Value = "Vị trí";
                    ///Ngày sinh
                    ws.Cells[12, 6].Value = "Ngày sinh";
                    ///Quốc gia
                    ws.Cells[12, 7, 12, 7].Value = "Quốc tịch";
                    ///Chú ý
                    ws.Cells[12, 8, 12, 8].Value = "Ghi chú";

                    /////////////////////////////////////////////
                    /////////////////List Player/////////////////
                    /////////////////////////////////////////////

                    for (int i = 0; i < players.Count; i++)
                    {
                        ws.Cells[12 + i + 1, 1].Value = i + 1;
                        ws.Cells[12 + i + 1, 3].Value = players[i].namePlayer;
                        ws.Cells[12 + i + 1, 4].Value = players[i].uniformNumber;
                        ws.Cells[12 + i + 1, 5].Value = players[i].position;
                        ws.Cells[12 + i + 1, 6].Value = players[i].birthDay.ToString("M/d/yyyy");
                        ws.Cells[12 + i + 1, 7].Value = players[i].nationality;
                        ws.Cells[12 + i + 1, 8].Value = players[i].note;
                        pic = ws.Drawings.AddPicture(players[i].namePlayer + "1", ImageProcessing.Instance.ByteToImg(players[i].image));
                        ws.Rows[12 + i + 1].Height = 60;
                        ws.Columns[2].Width = 60;
                        pic.SetPosition(12 + i, 10, 1, 80);
                        pic.SetSize((int)ws.Columns[2].Width, (int)ws.Rows[12 + i + 1].Height);
                        for (int j = 1; j <= 8; j++)
                        {
                            ws.Cells[12 + i + 1, j].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        }
                    }



                    /////////////////////////////////////////////
                    /////////////////////////////////////////////
                    /////////////////////////////////////////////

                    // Border Table
                    ws.Columns[1, 10].AutoFit(30);
                    int count = players.Count;
                    ws.Cells[11, 1, 12 + count, 8].Style.Border.BorderAround(ExcelBorderStyle.Thick);
                    ws.Cells[1, 1, 12 + count, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[1, 1, 12 + count, 8].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


                    Byte[] bin = p.GetAsByteArray();
                    File.WriteAllBytes(filePath, bin);

                    var proc = new Process();
                    proc.StartInfo = new ProcessStartInfo(filePath)
                    {
                        UseShellExecute = true
                    };
                    MessageBox.Show("Xuất tệp tin thành công");
                    proc.Start();
                }
            }
            catch
            {
                MessageBox.Show("Xuất tệp tin thất bại");
            }
        }
    }
}
