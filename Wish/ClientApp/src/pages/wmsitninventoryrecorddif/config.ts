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
        key: "batchNo",
        label: i18n.t("wmsitninventoryrecorddif.batchNo")
    },
    {
        key: "dataCode",
        label: i18n.t("wmsitninventoryrecorddif.dataCode")
    },
    {
        key: "delayFrozenFlag",
        label: i18n.t("wmsitninventoryrecorddif.delayFrozenFlag")
    },
    {
        key: "delayFrozenReason",
        label: i18n.t("wmsitninventoryrecorddif.delayFrozenReason")
    },
    {
        key: "delayReason",
        label: i18n.t("wmsitninventoryrecorddif.delayReason")
    },
    {
        key: "delayTimes",
        label: i18n.t("wmsitninventoryrecorddif.delayTimes")
    },
    {
        key: "delayToEndDate",
        label: i18n.t("wmsitninventoryrecorddif.delayToEndDate")
    },
    {
        key: "difQty",
        label: i18n.t("wmsitninventoryrecorddif.difQty")
    },
    {
        key: "driedScrapFlag",
        label: i18n.t("wmsitninventoryrecorddif.driedScrapFlag")
    },
    {
        key: "driedTimes",
        label: i18n.t("wmsitninventoryrecorddif.driedTimes")
    },
    {
        key: "expDate",
        label: i18n.t("wmsitninventoryrecorddif.expDate")
    },
    {
        key: "exposeFrozenFlag",
        label: i18n.t("wmsitninventoryrecorddif.exposeFrozenFlag")
    },
    {
        key: "exposeFrozenReason",
        label: i18n.t("wmsitninventoryrecorddif.exposeFrozenReason")
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
        label: i18n.t("wmsitninventoryrecorddif.inspectionResult")
    },
    {
        key: "inventoryDtlId",
        label: i18n.t("wmsitninventoryrecorddif.inventoryDtlId")
    },
    {
        key: "inventoryNo",
        label: i18n.t("wmsitninventoryrecorddif.inventoryNo")
    },
    {
        key: "inventoryQty",
        label: i18n.t("wmsitninventoryrecorddif.inventoryQty")
    },
    {
        key: "inventoryRecordId",
        label: i18n.t("wmsitninventoryrecorddif.inventoryRecordId")
    },
    {
        key: "leftMslTimes",
        label: i18n.t("wmsitninventoryrecorddif.leftMslTimes")
    },
    {
        key: "materialName",
        label: i18n.t("wmsitninventoryrecorddif.materialName")
    },
    {
        key: "materialCode",
        label: i18n.t("wmsitninventoryrecorddif.materialCode")
    },
    {
        key: "materialSpec",
        label: i18n.t("wmsitninventoryrecorddif.materialSpec")
    },
    {
        key: "mslGradeCode",
        label: i18n.t("wmsitninventoryrecorddif.mslGradeCode")
    },
    {
        key: "packageTime",
        label: i18n.t("wmsitninventoryrecorddif.packageTime")
    },
    {
        key: "palletBarcode",
        label: i18n.t("wmsitninventoryrecorddif.palletBarcode")
    },
    {
        key: "positionNo",
        label: i18n.t("wmsitninventoryrecorddif.positionNo")
    },
    {
        key: "productDate",
        label: i18n.t("wmsitninventoryrecorddif.productDate")
    },
    {
        key: "projectNo",
        label: i18n.t("wmsitninventoryrecorddif.projectNo")
    },
    {
        key: "proprietorCode",
        label: i18n.t("wmsitninventoryrecorddif.proprietorCode")
    },
    {
        key: "realExposeTimes",
        label: i18n.t("wmsitninventoryrecorddif.realExposeTimes")
    },
    {
        key: "skuCode",
        label: i18n.t("wmsitninventoryrecorddif.skuCode")
    },
    {
        key: "stockCode",
        label: i18n.t("wmsitninventoryrecorddif.stockCode")
    },
    {
        key: "stockDtlId",
        label: i18n.t("wmsitninventoryrecorddif.stockDtlId")
    },
    {
        key: "supplierCode",
        label: i18n.t("wmsitninventoryrecorddif.supplierCode")
    },
    {
        key: "supplierExposeTimes",
        label: i18n.t("wmsitninventoryrecorddif.supplierExposeTimes")
    },
    {
        key: "supplierName",
        label: i18n.t("wmsitninventoryrecorddif.supplierName")
    },
    {
        key: "supplierNameAlias",
        label: i18n.t("wmsitninventoryrecorddif.supplierNameAlias")
    },
    {
        key: "supplierNameEn",
        label: i18n.t("wmsitninventoryrecorddif.supplierNameEn")
    },
    {
        key: "uniicode",
        label: i18n.t("wmsitninventoryrecorddif.uniicode")
    },
    {
        key: "unpackTime",
        label: i18n.t("wmsitninventoryrecorddif.unpackTime")
    },
    {
        key: "whouseNo",
        label: i18n.t("wmsitninventoryrecorddif.whouseNo")
    },
    {
        key: "areaNo",
        label: i18n.t("wmsitninventoryrecorddif.areaNo")
    },
    {
        key: "delFlag",
        label: i18n.t("wmsitninventoryrecorddif.delFlag")
    },
    {
        key: "erpWhouseNo",
        label: i18n.t("wmsitninventoryrecorddif.erpWhouseNo")
    },
    {
        key: "inwhTime",
        label: i18n.t("wmsitninventoryrecorddif.inwhTime")
    },
    {
        key: "occupyQty",
        label: i18n.t("wmsitninventoryrecorddif.occupyQty")
    },
    {
        key: "qty",
        label: i18n.t("wmsitninventoryrecorddif.qty")
    },
    {
        key: "unitCode",
        label: i18n.t("wmsitninventoryrecorddif.unitCode")
    },
    {
        key: "fileedId",
        label: i18n.t("wmsitninventoryrecorddif.fileedId")
    },
    {
        key: "fileedName",
        label: i18n.t("wmsitninventoryrecorddif.fileedName")
    },
    {
        key: "oldStockDtlId",
        label: i18n.t("wmsitninventoryrecorddif.oldStockDtlId")
    },
    {
        key: "projectNoBak",
        label: i18n.t("wmsitninventoryrecorddif.projectNoBak")
    },
    {
        key: "unpackStatus",
        label: i18n.t("wmsitninventoryrecorddif.unpackStatus")
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


