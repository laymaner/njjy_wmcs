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
        key: "blindFlag",
        label: i18n.t("wmsitninventoryrecordhis.blindFlag")
    },
    {
        key: "confirmBy",
        label: i18n.t("wmsitninventoryrecordhis.confirmBy")
    },
    {
        key: "confirmQty",
        label: i18n.t("wmsitninventoryrecordhis.confirmQty")
    },
    {
        key: "confirmReason",
        label: i18n.t("wmsitninventoryrecordhis.confirmReason")
    },
    {
        key: "differenceFlag",
        label: i18n.t("wmsitninventoryrecordhis.differenceFlag")
    },
    {
        key: "docTypeCode",
        label: i18n.t("wmsitninventoryrecordhis.docTypeCode")
    },
    {
        key: "erpWhouseNo",
        label: i18n.t("wmsitninventoryrecordhis.erpWhouseNo")
    },
    {
        key: "inOutTypeNo",
        label: i18n.t("wmsitninventoryrecordhis.inOutTypeNo")
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
        key: "inspectionResult",
        label: i18n.t("wmsitninventoryrecordhis.inspectionResult")
    },
    {
        key: "inventoryBy",
        label: i18n.t("wmsitninventoryrecordhis.inventoryBy")
    },
    {
        key: "inventoryDtlId",
        label: i18n.t("wmsitninventoryrecordhis.inventoryDtlId")
    },
    {
        key: "inventoryNo",
        label: i18n.t("wmsitninventoryrecordhis.inventoryNo")
    },
    {
        key: "inventoryQty",
        label: i18n.t("wmsitninventoryrecordhis.inventoryQty")
    },
    {
        key: "inventoryReason",
        label: i18n.t("wmsitninventoryrecordhis.inventoryReason")
    },
    {
        key: "inventoryRecordStatus",
        label: i18n.t("wmsitninventoryrecordhis.inventoryRecordStatus")
    },
    {
        key: "materialName",
        label: i18n.t("wmsitninventoryrecordhis.materialName")
    },
    {
        key: "materialCode",
        label: i18n.t("wmsitninventoryrecordhis.materialCode")
    },
    {
        key: "materialSpec",
        label: i18n.t("wmsitninventoryrecordhis.materialSpec")
    },
    {
        key: "occupyQty",
        label: i18n.t("wmsitninventoryrecordhis.occupyQty")
    },
    {
        key: "palletBarcode",
        label: i18n.t("wmsitninventoryrecordhis.palletBarcode")
    },
    {
        key: "projectNo",
        label: i18n.t("wmsitninventoryrecordhis.projectNo")
    },
    {
        key: "proprietorCode",
        label: i18n.t("wmsitninventoryrecordhis.proprietorCode")
    },
    {
        key: "putdownLocNo",
        label: i18n.t("wmsitninventoryrecordhis.putdownLocNo")
    },
    {
        key: "skuCode",
        label: i18n.t("wmsitninventoryrecordhis.skuCode")
    },
    {
        key: "stockCode",
        label: i18n.t("wmsitninventoryrecordhis.stockCode")
    },
    {
        key: "stockDtlId",
        label: i18n.t("wmsitninventoryrecordhis.stockDtlId")
    },
    {
        key: "stockQty",
        label: i18n.t("wmsitninventoryrecordhis.stockQty")
    },
    {
        key: "supplierCode",
        label: i18n.t("wmsitninventoryrecordhis.supplierCode")
    },
    {
        key: "supplierName",
        label: i18n.t("wmsitninventoryrecordhis.supplierName")
    },
    {
        key: "supplierNameAlias",
        label: i18n.t("wmsitninventoryrecordhis.supplierNameAlias")
    },
    {
        key: "supplierNameEn",
        label: i18n.t("wmsitninventoryrecordhis.supplierNameEn")
    },
    {
        key: "whouseNo",
        label: i18n.t("wmsitninventoryrecordhis.whouseNo")
    },
    {
        key: "CreateBy",
        label: i18n.t("CommonString.CreateBy"),
        width: 100
    },
    {
        key: "CreateTime",
        label: i18n.t("CommonString.CreateTime"),
        width: 150
    },
    {
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


