using Com.Wish.Model.Base;
using Com.Wish.Model.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wish.ViewModel.Common.Dtos
{
    public class AddWmsOutInvoiceDtlDto : WmsOutInvoiceDtl
    {
    }

    public class AddWmsOutInvoiceDto : WmsOutInvoice
    {
        public List<AddWmsOutInvoiceDtlDto> dtlList { get; set; }
    }


    public class CreateWmsOutInvoiceDto : WmsOutInvoice
    {
        public Object stockDtl { get; set; }

    }

    public class EditWmsOutInvoiceDto : WmsOutInvoice
    {
        public Object stockDtl { get; set; }

    }
    public class AddStockDtlDto : WmsStockDtl
    {
        /// <summary>
        /// 单据数量
        /// </summary>
        public decimal invoiceQty;
    }

    public class AddMaterialDto : BasBMaterial
    {
        /// <summary>
        /// ERP仓库
        /// </summary>
        public string erpWhouseNo { get; set; }
        /// <summary>
        /// 供方类型
        /// </summary>
        public string supplierType { get; set; }

        /// <summary>
        /// 供方编码
        /// </summary>
        public string supplierCode { get; set; }

        /// <summary>
        /// 批次
        /// </summary>
        public string batchNo { get; set; }

        /// <summary>
        /// 质量标记
        /// </summary>
        public string inspectionResult { get; set; }

        /// <summary>
        /// 单据数量
        /// </summary>
        public decimal invoiceQty { get; set; }
    }

    public class UpdateInvioceNoDto
    {
        /// <summary>
        /// 外部出库单号
        /// </summary>
        public string externalOutNo { get; set; }

        /// <summary>
        /// 外部出库单行号
        /// </summary>
        public string externalOutDtlNo { get; set; }

        /// <summary>
        /// ERP未发数量
        /// </summary>
        public decimal erpUndeliverQty { get; set; }

        /// <summary>
        /// 单据数量
        /// </summary>
        public decimal invoiceQty { get; set; }

        /// <summary>
        /// 原序列号
        /// </summary>
        public string originalSn { get; set; }

        /// <summary>
        /// 厂内/厂外
        /// </summary>
        public string site { get; set; }

        /// <summary>
        /// EPR仓库号
        /// </summary>
        public string erpWhouseNo { get; set; }
    }

    public class ResultGetStockDtlByMaterialDto
    {
        public int total { get; set; }
        public List<GetStockDtlByMaterialsDto> items { get; set; } = new List<GetStockDtlByMaterialsDto>();
    }
    public class GetStockDtlByMaterialsDto
    {
        public string materialCode { get; set; }
        public string materialName { get; set; }
        public string materialSpec { get; set; }
        public string supplierCode { get; set; }
        public string supplierName { get; set; }
        public string unitName { get; set; }
        public int? inspectionResult { get; set; }
        public string inspectionResultDesc { get; set; }
        public decimal? qty { get; set; }
        public decimal? occupyQty { get; set; }
        public string batchNo { get; set; }
        public string erpWhouseNo { get; set; }
        public string uniiCode { get; set; }
        public string palletBarcode { get; set; }
        public string projectNo { get; set; }
        public int? stockDtlStatus { get; set; }
    }

}
