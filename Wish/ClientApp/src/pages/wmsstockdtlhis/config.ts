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
        label: i18n.t("wmsstockdtlhis.areaNo")
    },
    {
        key: "erpWhouseNo",
        label: i18n.t("wmsstockdtlhis.erpWhouseNo")
    },
    {
        key: "inspectionResult",
        label: i18n.t("wmsstockdtlhis.inspectionResult")
    },
    {
        key: "lockFlag",
        label: i18n.t("wmsstockdtlhis.lockFlag")
    },
    {
        key: "lockReason",
        label: i18n.t("wmsstockdtlhis.lockReason")
    },
    {
        key: "materialName",
        label: i18n.t("wmsstockdtlhis.materialName")
    },
    {
        key: "materialCode",
        label: i18n.t("wmsstockdtlhis.materialCode")
    },
    {
        key: "materialSpec",
        label: i18n.t("wmsstockdtlhis.materialSpec")
    },
    {
        key: "occupyQty",
        label: i18n.t("wmsstockdtlhis.occupyQty")
    },
    {
        key: "palletBarcode",
        label: i18n.t("wmsstockdtlhis.palletBarcode")
    },
    {
        key: "projectNo",
        label: i18n.t("wmsstockdtlhis.projectNo")
    },
    {
        key: "proprietorCode",
        label: i18n.t("wmsstockdtlhis.proprietorCode")
    },
    {
        key: "qty",
        label: i18n.t("wmsstockdtlhis.qty")
    },
    {
        key: "skuCode",
        label: i18n.t("wmsstockdtlhis.skuCode")
    },
    {
        key: "stockCode",
        label: i18n.t("wmsstockdtlhis.stockCode")
    },
    {
        key: "stockDtlStatus",
        label: i18n.t("wmsstockdtlhis.stockDtlStatus")
    },
    {
        key: "supplierCode",
        label: i18n.t("wmsstockdtlhis.supplierCode")
    },
    {
        key: "supplierName",
        label: i18n.t("wmsstockdtlhis.supplierName")
    },
    // {
    //     key: "supplierNameAlias",
    //     label: i18n.t("wmsstockdtlhis.supplierNameAlias")
    // },
    // {
    //     key: "supplierNameEn",
    //     label: i18n.t("wmsstockdtlhis.supplierNameEn")
    // },
    {
        key: "whouseNo",
        label: i18n.t("wmsstockdtlhis.whouseNo")
    },
    {
        key: "unitCode",
        label: i18n.t("wmsstockdtlhis.unitCode")
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


