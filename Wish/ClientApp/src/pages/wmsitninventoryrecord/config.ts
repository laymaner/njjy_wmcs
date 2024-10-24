import i18n from "@/lang";

export const ASSEMBLIES: Array<string> = [
  "add",
  "edit",
  "delete",
  "export",
  "imported",
  "createinventory"
];

export const TABLE_HEADER: Array<object> = [


    {
        key: "blindFlag",
        label: i18n.t("wmsitninventoryrecord.blindFlag")
    },
    {
        key: "confirmBy",
        label: i18n.t("wmsitninventoryrecord.confirmBy")
    },
    {
        key: "confirmQty",
        label: i18n.t("wmsitninventoryrecord.confirmQty")
    },
    {
        key: "confirmReason",
        label: i18n.t("wmsitninventoryrecord.confirmReason")
    },
    {
        key: "differenceFlag",
        label: i18n.t("wmsitninventoryrecord.differenceFlag")
    },
    {
        key: "docTypeCode",
        label: i18n.t("wmsitninventoryrecord.docTypeCode")
    },
    {
        key: "erpWhouseNo",
        label: i18n.t("wmsitninventoryrecord.erpWhouseNo")
    },
    {
        key: "inOutTypeNo",
        label: i18n.t("wmsitninventoryrecord.inOutTypeNo")
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
        label: i18n.t("wmsitninventoryrecord.inspectionResult")
    },
    {
        key: "inventoryBy",
        label: i18n.t("wmsitninventoryrecord.inventoryBy")
    },
    {
        key: "inventoryDtlId",
        label: i18n.t("wmsitninventoryrecord.inventoryDtlId")
    },
    {
        key: "inventoryNo",
        label: i18n.t("wmsitninventoryrecord.inventoryNo")
    },
    {
        key: "inventoryQty",
        label: i18n.t("wmsitninventoryrecord.inventoryQty")
    },
    {
        key: "inventoryReason",
        label: i18n.t("wmsitninventoryrecord.inventoryReason")
    },
    {
        key: "inventoryRecordStatus",
        label: i18n.t("wmsitninventoryrecord.inventoryRecordStatus")
    },
    {
        key: "materialName",
        label: i18n.t("wmsitninventoryrecord.materialName")
    },
    {
        key: "materialCode",
        label: i18n.t("wmsitninventoryrecord.materialCode")
    },
    {
        key: "materialSpec",
        label: i18n.t("wmsitninventoryrecord.materialSpec")
    },
    {
        key: "occupyQty",
        label: i18n.t("wmsitninventoryrecord.occupyQty")
    },
    {
        key: "palletBarcode",
        label: i18n.t("wmsitninventoryrecord.palletBarcode")
    },
    {
        key: "projectNo",
        label: i18n.t("wmsitninventoryrecord.projectNo")
    },
    {
        key: "proprietorCode",
        label: i18n.t("wmsitninventoryrecord.proprietorCode")
    },
    {
        key: "putdownLocNo",
        label: i18n.t("wmsitninventoryrecord.putdownLocNo")
    },
    {
        key: "skuCode",
        label: i18n.t("wmsitninventoryrecord.skuCode")
    },
    {
        key: "stockCode",
        label: i18n.t("wmsitninventoryrecord.stockCode")
    },
    {
        key: "stockDtlId",
        label: i18n.t("wmsitninventoryrecord.stockDtlId")
    },
    {
        key: "stockQty",
        label: i18n.t("wmsitninventoryrecord.stockQty")
    },
    {
        key: "supplierCode",
        label: i18n.t("wmsitninventoryrecord.supplierCode")
    },
    {
        key: "supplierName",
        label: i18n.t("wmsitninventoryrecord.supplierName")
    },
    // {
    //     key: "supplierNameAlias",
    //     label: i18n.t("wmsitninventoryrecord.supplierNameAlias")
    // },
    // {
    //     key: "supplierNameEn",
    //     label: i18n.t("wmsitninventoryrecord.supplierNameEn")
    // },
    {
        key: "whouseNo",
        label: i18n.t("wmsitninventoryrecord.whouseNo")
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


