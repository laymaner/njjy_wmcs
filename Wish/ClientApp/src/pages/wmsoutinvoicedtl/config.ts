import i18n from "@/lang";

export const ASSEMBLIES: Array<string> = [
  "add",
  "edit",
  "delete",
  "export",
  "imported",
  "createinvoice"
];

export const TABLE_HEADER: Array<object> = [


    {
        key: "allocatResult",
        label: i18n.t("wmsoutinvoicedtl.allocatResult")
    },
    {
        key: "allotQty",
        label: i18n.t("wmsoutinvoicedtl.allotQty")
    },
    {
        key: "areaNo",
        label: i18n.t("wmsoutinvoicedtl.areaNo")
    },
    {
        key: "assemblyIdx",
        label: i18n.t("wmsoutinvoicedtl.assemblyIdx")
    },
    {
        key: "batchNo",
        label: i18n.t("wmsoutinvoicedtl.batchNo")
    },
    {
        key: "belongDepartment",
        label: i18n.t("wmsoutinvoicedtl.belongDepartment")
    },
    {
        key: "completeQty",
        label: i18n.t("wmsoutinvoicedtl.completeQty")
    },
    {
        key: "erpUndeliverQty",
        label: i18n.t("wmsoutinvoicedtl.erpUndeliverQty")
    },
    {
        key: "erpWhouseNo",
        label: i18n.t("wmsoutinvoicedtl.erpWhouseNo")
    },
    // {
    //     key: "extend1",
    //     label: "extend1"
    // },
    // {
    //     key: "extend10",
    //     label: "extend10"
    // },
    // {
    //     key: "extend11",
    //     label: "extend11"
    // },
    // {
    //     key: "extend12",
    //     label: "extend12"
    // },
    // {
    //     key: "extend13",
    //     label: "extend13"
    // },
    // {
    //     key: "extend14",
    //     label: "extend14"
    // },
    // {
    //     key: "extend15",
    //     label: "extend15"
    // },
    // {
    //     key: "extend2",
    //     label: "extend2"
    // },
    // {
    //     key: "extend3",
    //     label: "extend3"
    // },
    // {
    //     key: "extend4",
    //     label: "extend4"
    // },
    // {
    //     key: "extend5",
    //     label: "extend5"
    // },
    // {
    //     key: "extend6",
    //     label: "extend6"
    // },
    // {
    //     key: "extend7",
    //     label: "extend7"
    // },
    // {
    //     key: "extend8",
    //     label: "extend8"
    // },
    // {
    //     key: "extend9",
    //     label: "extend9"
    // },
    {
        key: "externalOutDtlId",
        label: i18n.t("wmsoutinvoicedtl.externalOutDtlId")
    },
    {
        key: "externalOutNo",
        label: i18n.t("wmsoutinvoicedtl.externalOutNo")
    },
    {
        key: "invoiceDtlStatus",
        label: i18n.t("wmsoutinvoicedtl.invoiceDtlStatus")
    },
    {
        key: "invoiceNo",
        label: i18n.t("wmsoutinvoicedtl.invoiceNo")
    },
    {
        key: "materialName",
        label: i18n.t("wmsoutinvoicedtl.materialName")
    },
    {
        key: "materialCode",
        label: i18n.t("wmsoutinvoicedtl.materialCode")
    },
    {
        key: "materialSpec",
        label: i18n.t("wmsoutinvoicedtl.materialSpec")
    },
    {
        key: "orderDtlId",
        label: i18n.t("wmsoutinvoicedtl.orderDtlId")
    },
    {
        key: "orderNo",
        label: i18n.t("wmsoutinvoicedtl.orderNo")
    },
    {
        key: "productLocation",
        label: i18n.t("wmsoutinvoicedtl.productLocation")
    },
    {
        key: "productDeptCode",
        label: i18n.t("wmsoutinvoicedtl.productDeptCode")
    },
    {
        key: "productDeptName",
        label: i18n.t("wmsoutinvoicedtl.productDeptName")
    },
    {
        key: "projectNo",
        label: i18n.t("wmsoutinvoicedtl.projectNo")
    },
    {
        key: "proprietorCode",
        label: i18n.t("wmsoutinvoicedtl.proprietorCode")
    },
    {
        key: "invoiceQty",
        label: i18n.t("wmsoutinvoicedtl.invoiceQty")
    },
    {
        key: "putdownQty",
        label: i18n.t("wmsoutinvoicedtl.putdownQty")
    },
    {
        key: "productSn",
        label: i18n.t("wmsoutinvoicedtl.productSn")
    },
    {
        key: "originalSn",
        label: i18n.t("wmsoutinvoicedtl.originalSn")
    },
    {
        key: "supplyType",
        label: i18n.t("wmsoutinvoicedtl.supplyType")
    },
    {
        key: "ticketPlanBeginTime",
        label: i18n.t("wmsoutinvoicedtl.ticketPlanBeginTime")
    },
    {
        key: "waveNo",
        label: i18n.t("wmsoutinvoicedtl.waveNo")
    },
    {
        key: "whouseNo",
        label: i18n.t("wmsoutinvoicedtl.whouseNo")
    },
    {
        key: "companyCode",
        label: i18n.t("wmsoutinvoicedtl.companyCode")
    },
    // {
    //     key: "intfId",
    //     label: "intfId"
    // },
    // {
    //     key: "intfBatchId",
    //     label: "intfBatchId"
    // },
    {
        key: "supplierCode",
        label: i18n.t("wmsoutinvoicedtl.supplierCode")
    },
    {
        key: "supplierName",
        label: i18n.t("wmsoutinvoicedtl.supplierName")
    },
    // {
    //     key: "supplierNameEn",
    //     label: "supplierNameEn"
    // },
    // {
    //     key: "supplierNameAlias",
    //     label: "supplierNameAlias"
    // },
    {
        key: "ticketNo",
        label: i18n.t("wmsoutinvoicedtl.ticketNo")
    },
    {
        key: "unitCode",
        label: i18n.t("wmsoutinvoicedtl.unitCode")
    },
  { isOperate: true, label: i18n.t(`table.actions`), actions: ["detail", "edit", "deleted"] } //操作列
];


