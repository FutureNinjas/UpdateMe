using System.Web;
using UpdateMe.Data.Models;

namespace UpdateMe.Services.Contracts
{
    public interface IReader
    {
        Course ReadFile(HttpPostedFileBase file);
    }
}
