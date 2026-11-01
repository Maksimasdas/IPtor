using IPtor.Domain.Enums;
using System.Collections.Generic;
using System.Linq;

namespace IPtor.Helpers
{
    public static class EnumHelper
    {
        /// <summary>
        /// Список служб для фильтра: "Все службы" + все службы (без None).
        /// </summary>
        public static List<string> GetServiceTypesWithAll()
        {
            var list = new List<string> { "Все службы" };
            list.AddRange(GetServiceTypes());
            return list;
        }

        /// <summary>
        /// Список служб без "Все службы" и без None (для ComboBox в формах).
        /// </summary>
        public static List<string> GetServiceTypes()
        {
            return System.Enum.GetValues(typeof(ServiceType))
                .Cast<ServiceType>()
                .Where(s => s != ServiceType.None)  // скрываем техническое значение
                .Select(s => s.ToString())
                .ToList();
        }
    }
}