using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.CurrencyExchange
{
    /// <summary>
    /// Класс взят с сайта нацбанка. Свойства не трогал. Закомитанный код - альтернативная запись.
    /// </summary>
    public class Rate
    {
        [Key]
        public int Cur_ID { get; set; }
        public DateTime Date { get; set; }
        public string Cur_Abbreviation { get; set; }
        public int Cur_Scale { get; set; }
        public string Cur_Name { get; set; }
        public decimal? Cur_OfficialRate { get; set; }// == public Nullable<decimal> Cur_OfficialRate { get; set; }
    }
}
