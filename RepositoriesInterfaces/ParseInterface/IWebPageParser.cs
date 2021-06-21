using Entities.Entity.NewsEnt;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;

namespace Contracts.ParseInterface
{
    public interface IWebPageParser
    {
        Task<NewStrings> Parse(SyndicationItem syndicationItem);
    }
}
