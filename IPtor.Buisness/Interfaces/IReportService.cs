using IPtor.Buisness.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPtor.Buisness.Interfaces
{
    public interface IReportService
    {
        // Сгенерировать Excel-файл акта приёмки в ремонт
        void GenerateTransferAct(TransferActData data, string templatePath, string outputPath);

        // Сгенерировать Excel-файл акта тех. состояния
        void GenerateTOAct(TOAktData data, string templatePath, string outputPath);
    }
}
