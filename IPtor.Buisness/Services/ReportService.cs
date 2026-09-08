using ClosedXML.Excel;
using IPtor.Buisness.Interfaces;
using IPtor.Buisness.Models;

namespace IPtor.Buisness.Services
{
    public class ReportService : IReportService
    {
        public void GenerateTransferAct(TransferActData data, string templatePath, string outputPath)
        {
            using (var workbook = new XLWorkbook(templatePath))
            {
                var worksheet = workbook.Worksheet(1);
                // 1. ИНФОРМАЦИЯ ОБ АВТОМОБИЛЕ
                string fullName = $"{data.Car.Name} {data.Car.Model}";
                worksheet.Cell("A13").Value = fullName;
                worksheet.Cell("H13").Value = data.Car.Year > 0 ? data.Car.Year.ToString() : "";
                worksheet.Cell("E14").Value = data.Car.Number ?? "";
                worksheet.Cell("D15").Value = data.Car.Vin ?? "";
                worksheet.Cell("I16").Value = data.Car.Certificate ?? "";
                worksheet.Cell("D17").Value = data.Car.Formular > 0 ? data.Car.Formular.ToString() : "";
                worksheet.Cell("C16").Value = data.Car.EngineNumber ?? "-";
                worksheet.Cell("F32").Value = data.Car.Certificate ?? "";
                worksheet.Cell("C33").Value = data.Car.Formular > 0 ? data.Car.Formular.ToString() : "";

                string dateformular = data.Car.FormularDate.ToString("dd.MM.yyyy");
                worksheet.Cell("F33").Value = $"от {dateformular}";

                // 2. ДЕФЕКТЫ
                worksheet.Cell("A29").Value = data.Defects.Staff ?? "";
                worksheet.Cell("C35").Value = data.Defects.EngineDefect ?? "";
                worksheet.Cell("E36").Value = data.Defects.Electro ?? "";
                worksheet.Cell("C37").Value = data.Defects.Clutch ?? "";
                worksheet.Cell("G38").Value = data.Defects.Kpp ?? "";
                worksheet.Cell("E39").Value = data.Defects.Steering ?? "";
                worksheet.Cell("G40").Value = data.Defects.FrontGear ?? "";
                worksheet.Cell("G41").Value = data.Defects.BackGear ?? "";
                worksheet.Cell("C42").Value = data.Defects.Frame ?? "";
                worksheet.Cell("F43").Value = data.Defects.Body ?? "";
                worksheet.Cell("F44").Value = data.Defects.SpecEq ?? "";
                worksheet.Cell("F45").Value = data.Defects.ExternalCond ?? "";
                worksheet.Cell("G46").Value = data.Defects.NumberWheel ?? "";

                if (data.Defects.FuelAvail > 0)
                {
                    worksheet.Cell("E47").Value = $"{data.Defects.FuelAvail}";
                }

                worksheet.Cell("D48").Value = data.Defects.Conclusion ?? "";
                worksheet.Cell("A58").Value = DateTime.Now.ToString("dd.MM.yyyy");

                // 3. ОТВЕТСТВЕННЫЕ ЛИЦА
                string predUTO = $"{data.AcceptUtoPerson?.Position ?? ""} {data.AcceptUtoPerson?.UndPosition}";
                worksheet.Cell("A9").Value = predUTO;
                worksheet.Cell("G9").Value = data.AcceptUtoPerson?.Name ?? "";
                worksheet.Cell("G10").Value = data.ResponcePerson?.Service.ToString() ?? "";
                worksheet.Cell("A11").Value = $"{data.ResponcePerson?.Position} {data.ResponcePerson?.UndPosition}" ?? "";
                worksheet.Cell("G11").Value = data.ResponcePerson?.Name ?? "";

                // Нижние фамилии
                worksheet.Cell("H51").Value = data.SheffPassService?.Name ?? "";
                worksheet.Cell("H54").Value = data.ResponcePerson?.Name ?? "";
                worksheet.Cell("H55").Value = data.Driver?.Name ?? "";
                worksheet.Cell("H57").Value = data.AdditionalPerson?.Name ?? "";

                // 4. НАРАБОТКА АВТОМОБИЛЯ
                worksheet.Cell("A19").Value = data.KmSpeedStart.ToString();
                worksheet.Cell("G19").Value = data.MhSpeedStart.ToString("F1");
                worksheet.Cell("A20").Value = data.KmGeneral.ToString();
                worksheet.Cell("G21").Value = data.AvarageRepair.ToString() ?? "0";
                worksheet.Cell("E22").Value = data.KapRepair.ToString() ?? "0";
                worksheet.Cell("I24").Value = data.KmSpeedKarRepair.ToString();
                worksheet.Cell("C25").Value = data.MhSpeedKapRepair.ToString("F1");
                worksheet.Cell("H25").Value = data.KmGeneralKapRepair.ToString();
                worksheet.Cell("G26").Value = data.KmSpeedTO.ToString();
                worksheet.Cell("C27").Value = data.MhSpeedTO.ToString("F1");
                worksheet.Cell("H27").Value = data.KmGeneralTO.ToString();

                workbook.SaveAs(outputPath);
            }
        }
        public void GenerateTOAct(TOAktData data, string templatePath, string outputPath)
        {
            using (var workbook = new XLWorkbook(templatePath))
            {
                var worksheet = workbook.Worksheet(1);

                // Вспомогательный метод на случай оставления полей пустыми
                string GetValueOrDefault(string value, string defaultValue = "Удовлетворительно")
                {
                    return string.IsNullOrWhiteSpace(value) ? defaultValue : value;
                }

                string GetValueOrDefault2(string value, string defaultValue = "нет")
                {
                    return string.IsNullOrWhiteSpace(value) ? defaultValue : value;
                }

                // Где эксплуатируется
                worksheet.Cell("F10").Value = data.Car.Service.ToString();

                // Полное наименование
                string fullName = $"{data.Car.Name} {data.Car.Model}";
                worksheet.Cell("D11").Value = fullName;

                // Инфа об автомобиле
                worksheet.Cell("F12").Value = data.Car.Vin ?? "нет";
                worksheet.Cell("C13").Value = data.Car.Year > 0 ? data.Car.Year.ToString() : "нет";
                worksheet.Cell("G13").Value = data.Car.Certificate ?? "нет";
                worksheet.Cell("D14").Value = data.KmSpeedometrStart.ToString();
                worksheet.Cell("H14").Value = data.MhSpeedStart.ToString();

                // Тех состояние
                worksheet.Cell("F17").Value = GetValueOrDefault(data.DefectsTO.Body);
                worksheet.Cell("F18").Value = GetValueOrDefault(data.DefectsTO.HeadBody);
                worksheet.Cell("F19").Value = GetValueOrDefault(data.DefectsTO.Engine);
                worksheet.Cell("F20").Value = GetValueOrDefault(data.DefectsTO.KPP);
                worksheet.Cell("F21").Value = GetValueOrDefault(data.DefectsTO.Rul);
                worksheet.Cell("F22").Value = GetValueOrDefault(data.DefectsTO.FrontGear);
                worksheet.Cell("F23").Value = GetValueOrDefault(data.DefectsTO.BackGear);
                worksheet.Cell("F24").Value = GetValueOrDefault(data.DefectsTO.Electro);
                worksheet.Cell("F25").Value = GetValueOrDefault(data.DefectsTO.PTV, "Отсутствуют");
                worksheet.Cell("F26").Value = GetValueOrDefault(data.DefectsTO.Water, "Отсутствуют");
                worksheet.Cell("F27").Value = GetValueOrDefault(data.DefectsTO.GeneralView);
                worksheet.Cell("F28").Value = GetValueOrDefault(data.DefectsTO.Spec);
                worksheet.Cell("F29").Value = GetValueOrDefault(data.DefectsTO.Break);
                worksheet.Cell("F30").Value = GetValueOrDefault(data.DefectsTO.Other);

                // Укомплектованность
                worksheet.Cell("H37").Value = GetValueOrDefault2(data.DefectsTO.PuskRuk);
                worksheet.Cell("H38").Value = GetValueOrDefault2(data.DefectsTO.Domkrat);
                worksheet.Cell("H39").Value = GetValueOrDefault2(data.DefectsTO.Nasos);
                worksheet.Cell("H40").Value = GetValueOrDefault2(data.DefectsTO.SHpric);
                worksheet.Cell("H41").Value = GetValueOrDefault2(data.DefectsTO.Molotok);
                worksheet.Cell("H42").Value = GetValueOrDefault2(data.DefectsTO.Ploskogubci);
                worksheet.Cell("H43").Value = GetValueOrDefault2(data.DefectsTO.Kluchi);
                worksheet.Cell("H44").Value = GetValueOrDefault2(data.DefectsTO.Cluchi2);
                worksheet.Cell("H45").Value = GetValueOrDefault2(data.DefectsTO.Cluchi3);
                worksheet.Cell("H46").Value = GetValueOrDefault2(data.DefectsTO.Cluchi4);
                worksheet.Cell("H47").Value = GetValueOrDefault2(data.DefectsTO.Chehli);
                worksheet.Cell("H48").Value = GetValueOrDefault2(data.DefectsTO.Aptechka);
                worksheet.Cell("H49").Value = GetValueOrDefault2(data.DefectsTO.Znak);
                worksheet.Cell("H50").Value = GetValueOrDefault2(data.DefectsTO.Disk);
                worksheet.Cell("H51").Value = GetValueOrDefault2(data.DefectsTO.Ognetush);
                worksheet.Cell("H52").Value = GetValueOrDefault2(data.DefectsTO.Radio);
                worksheet.Cell("H53").Value = GetValueOrDefault2(data.DefectsTO.Protivougon);
                worksheet.Cell("H54").Value = GetValueOrDefault2(data.DefectsTO.Gromkogov);
                worksheet.Cell("H55").Value = GetValueOrDefault2(data.DefectsTO.Protivotuman);
                worksheet.Cell("H56").Value = GetValueOrDefault2(data.DefectsTO.Mayak);
                worksheet.Cell("H57").Value = GetValueOrDefault2(data.DefectsTO.Fara);
                worksheet.Cell("H58").Value = GetValueOrDefault2(data.DefectsTO.Prochee, "-");
                worksheet.Cell("F60").Value = data.DefectsTO.Fuel > 0 ? data.DefectsTO.Fuel.ToString() : "";
                worksheet.Cell("E62").Value = GetValueOrDefault2(data.DefectsTO.Zakl, "Исправно");
                worksheet.Cell("C64").Value = GetValueOrDefault2(data.DefectsTO.Pred, "Отсутствуют");

                // Ответственные лица
                worksheet.Cell("A72").Value = data.SheffPassService?.Service.ToString() ?? "";
                worksheet.Cell("H72").Value = data.SheffPassService?.Name ?? "";
                worksheet.Cell("H76").Value = data.ResponcePerson?.Name ?? "";
                worksheet.Cell("H78").Value = data.Driver?.Name ?? "";

                workbook.SaveAs(outputPath);
            }
        }
    }
}
