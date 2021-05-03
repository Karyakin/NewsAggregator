using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ParseInterface
{
   public interface IWebPageParser

    {
        Task<string> Parse(SyndicationItem syndicationItem);
    }
}
