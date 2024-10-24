import i18n from "@/lang";

export const ASSEMBLIES: Array<string> = [
  "add",
  "edit",
  "delete",
  "export",
  "imported"
];

export const TABLE_HEADER: Array<object> = [


    {
        key: "areaNo",
        label: i18n.t("wmsstockdtl.areaNo")
    },
    {
        key: "erpWhouseNo",
        label: i18n.t("wmsstockdtl.erpWhouseNo")
    },
    {
        key: "inspectionResult",
        label: i18n.t("wmsstockdtl.inspectionResult")
    },
    {
        key: "lockFlag",
        label: i18n.t("wmsstockdtl.lockFlag")
    },
    {
        key: "lockReason",
        label: i18n.t("wmsstockdtl.lockReason")
    },
    {
        key: "materialName",
        label: i18n.t("wmsstockdtl.materialName")
    },
    {
        key: "materialCode",
        label: i18n.t("wmsstockdtl.materialCode")
    },
    {
        key: "materialSpec",
        label: i18n.t("wmsstockdtl.materialSpec")
    },
    {
        key: "occupyQty",
        label: i18n.t("wmsstockdtl.occupyQty")
    },
    {
        key: "palletBarcode",
        label: i18n.t("wmsstockdtl.palletBarcode")
    },
    {
        key: "projectNo",
        label: i18n.t("wmsstockdtl.projectNo")
    },
    {
        key: "proprietorCode",
        label: i18n.t("wmsstockdtl.proprietorCode")
    },
    {
        key: "qty",
        label: i18n.t("wmsstockdtl.qty")
    },
    {
        key: "skuCode",
        label: i18n.t("wmsstockdtl.skuCode")
    },
    {
        key: "stockCode",
        label: i18n.t("wmsstockdtl.stockCode")
    },
    {
        key: "stockDtlStatus",
        label: i18n.t("wmsstockdtl.stockDtlStatus")
    },
    {
        key: "supplierCode",
        label: i18n.t("wmsstockdtl.supplierCode")
    },
    {
        key: "supplierName",
        label: i18n.t("wmsstockdtl.supplierName")
    },
    // {
    //     key: "supplierNameAlias",
    //     label: i18n.t("wmsstockdtl.supplierNameAlias")
    // },
    // {
    //     key: "supplierNameEn",
    //     label: i18n.t("wmsstockdtl.supplierNameEn")
    // },
    {
        key: "whouseNo",
        label: i18n.t("wmsstockdtl.whouseNo")
    },
    {
        key: "unitCode",
        label: i18n.t("wmsstockdtl.unitCode")
    },{
        key: "CreateBy",
        label: i18n.t("CommonString.CreateBy"),
        width: 100
    },
    {
        key: "CreateTime",
        label: i18n.t("CommonString.CreateTime"),
        width: 150
    },{
        key: "UpdateBy",
        label: i18n.t("CommonString.UpdateBy"),
        width: 100
    },
    {
        key: "UpdateTime",
        label: i18n.t("CommonString.UpdateTime"),
        width: 150
    },
   
  { isOperate: true, label: i18n.t(`table.actions`), actions: ["detail", "edit", "deleted"] } //操作列
];


