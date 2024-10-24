using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wish.Areas.Config.Model;

namespace Wish.ViewModel.Common.Dtos
{
    public class CfgDocTypeDto
    {
        public CfgDocType cfgDocType { get; set; }

        public List<CfgDocTypeDtlDto> cfgDocTypeDtls { get; set; }

        public string GetParamCodeValue(string paramCode)
        {
            string paramValue = "";

            foreach (var item in cfgDocTypeDtls)
            {
                if (item.cfgDocTypeDtl.paramCode == paramCode)
                {
                    paramValue = item.cfgDocTypeDtl.paramValueCode;
                    break;
                }
            }

            return paramValue;
        }


    }

    public class CfgDocTypeDtlDto
    {
        public CfgDocTypeDtl cfgDocTypeDtl { get; set; }
        public CfgBusiness cfgBusiness { get; set; }
        public CfgBusinessModule cfgBusinessModule { get; set; }
        public CfgBusinessParam cfgBusinessParam { get; set; }
        public CfgBusinessParamValue cfgBusinessParamValue { get; set; }

    }
}
