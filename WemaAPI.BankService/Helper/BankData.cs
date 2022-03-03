using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WemaAPI.BankService.Helper
{
    public class BankData
    {

        public class Rootobject
        {
            public Result[] result { get; set; }
            public object errorMessage { get; set; }
            public object errorMessages { get; set; }
            public bool hasError { get; set; }
            public DateTime timeGenerated { get; set; }
        }

        public class Result
        {
            public string bankName { get; set; }
            public string bankCode { get; set; }
        }

    }
}
