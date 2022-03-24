using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicierWebApiV1.Core.Shared
{
    public enum ResponseType
    {
        Successful = 200,
        NotFound = 404,
        ServerError = 500,

    }
}
