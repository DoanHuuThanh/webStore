using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doanhuuthanh_web.Application.common
{
    public interface IStorageService
    {
        string GetFilterFile(string filename);
        Task SaveFileAsync(Stream mediaBinaryStream, string filename);
        Task DeleteFileAsync(string filename);
    }
}
