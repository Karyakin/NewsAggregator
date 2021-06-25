using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.ServiseHelpers
{
   public interface ITexterra
    {
        public Task<IEnumerable<Root>> GetTexterra(string summary);
    }
}
