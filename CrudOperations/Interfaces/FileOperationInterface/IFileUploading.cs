using CrudOperations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudOperations.Interfaces
{
    public interface IFileUploading
    {
        string FileUpload(Product pro);

        bool FileDelete(string Image);

   
    }
}
