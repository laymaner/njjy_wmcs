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
        label: i18n.t("wmsitninventoryrecorddifhis.batchNo")
    },
    {
        key: "dataCode",
        label: i18n.t("wmsitninventoryrecorddifhis.dataCode")
    },
    {
        key: "delayFrozenFlag",
        label: i18n.t("wmsitninventoryrecorddifhis.delayFrozenFlag")
    },
    {
        key: "delayFrozenReason",
        label: i18n.t("wmsitninventoryrecorddifhis.delayFrozenReason")
    },
    {
        key: "delayReason",
        label: i18n.t("wmsitninventoryrecorddifhis.delayReason")
    },
    {
        key: "delayTimes",
        label: i18n.t("wmsitninventoryrecorddifhis.delayTimes")
    },
    {
        key: "delayToEndDate",
        label: i18n.t("wmsitninventoryrecorddifhis.delayToEndDate")
    },
    {
        key: "difQty",
        label: i18n.t("wmsitninventoryrecorddifhis.difQty")
    },
    {
        key: "driedScrapFlag",
        label: i18n.t("wmsitninventoryrecorddifhis.driedScrapFlag")
    },
    {
        key: "driedTimes",
        label: i18n.t("wmsitninventoryrecorddifhis.driedTimes")
    },
    {
        key: "expDate",
        label: i18n.t("wmsitninventoryrecorddifhis.expDate")
    },
    {
        key: "exposeFrozenFlag",
        label: i18n.t("wmsitninventoryrecorddifhis.exposeFrozenFlag")
    },
    {
        key: "exposeFrozenReason",
        label: i18n.t("wmsitninventoryrecorddifhis.exposeFrozenReason")
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
        label: i18n.t("wmsitninventoryrecorddifhis.inspectionResult")
    },
    {
        key: "inventoryDtlId",
        label: i18n.t("wmsitninventoryrecorddifhis.inventoryDtlId")
    },
    {
        key: "inventoryNo",
        label: i18n.t("wmsitninventoryrecorddifhis.inventoryNo")
    },
    {
        key: "inventoryQty",
        label: i18n.t("wmsitninventoryrecorddifhis.inventoryQty")
    },
    {
        key: "inventoryRecordId",
        label: i18n.t("wmsitninventoryrecorddifhis.inventoryRecordId")
    },
    {
        key: "leftMslTimes",
        label: i18n.t("wmsitninventoryrecorddifhis.leftMslTimes")
    },
    {
        key: "materialName",
        label: i18n.t("wmsitninventoryrecorddifhis.materialName")
    },
    {
        key: "materialCode",
        label: i18n.t("wmsitninventoryrecorddifhis.materialCode")
    },
    {
        key: "materialSpec",
        label: i18n.t("wmsitninventoryrecorddifhis.materialSpec")
    },
    {
        key: "mslGradeCode",
        label: i18n.t("wmsitninventoryrecorddifhis.mslGradeCode")
    },
    {
        key: "packageTime",
        label: i18n.t("wmsitninventoryrecorddifhis.packageTime")
    },
    {
        key: "palletBarcode",
        label: i18n.t("wmsitninventoryrecorddifhis.palletBarcode")
    },
    {
        key: "positionNo",
        label: i18n.t("wmsitninventoryrecorddifhis.positionNo")
    },
    {
        key: "productDate",
        label: i18n.t("wmsitninventoryrecorddifhis.productDate")
    },
    {
        key: "projectNo",
        label: i18n.t("wmsitninventoryrecorddifhis.projectNo")
    },
    {
        key: "proprietorCode",
        label: i18n.t("wmsitninventoryrecorddifhis.proprietorCode")
    },
    {
        key: "realExposeTimes",
        label: i18n.t("wmsitninventoryrecorddifhis.realExposeTimes")
    },
    {
        key: "skuCode",
        label: i18n.t("wmsitninventoryrecorddifhis.skuCode")
    },
    {
        key: "stockCode",
        label: i18n.t("wmsitninventoryrecorddifhis.stockCode")
    },
    {
        key: "stockDtlId",
        label: i18n.t("wmsitninventoryrecorddifhis.stockDtlId")
    },
    {
        key: "supplierCode",
        label: i18n.t("wmsitninventoryrecorddifhis.supplierCode")
    },
    {
        key: "supplierExposeTimes",
        label: i18n.t("wmsitninventoryrecorddifhis.supplierExposeTimes")
    },
    {
        key: "supplierName",
        label: i18n.t("wmsitninventoryrecorddifhis.supplierName")
    },
    {
        key: "supplierNameAlias",
        label: i18n.t("wmsitninventoryrecorddifhis.supplierNameAlias")
    },
    {
        key: "supplierNameEn",
        label: i18n.t("wmsitninventoryrecorddifhis.supplierNameEn")
    },
    {
        key: "uniicode",
        label: i18n.t("wmsitninventoryrecorddifhis.uniicode")
    },
    {
        key: "unpackTime",
        label: i18n.t("wmsitninventoryrecorddifhis.unpackTime")
    },
    {
        key: "whouseNo",
        label: i18n.t("wmsitninventoryrecorddifhis.whouseNo")
    },
    {
        key: "areaNo",
        label: i18n.t("wmsitninventoryrecorddifhis.areaNo")
    },
    {
        key: "delFlag",
        label: i18n.t("wmsitninventoryrecorddifhis.delFlag")
    },
    {
        key: "erpWhouseNo",
        label: i18n.t("wmsitninventoryrecorddifhis.erpWhouseNo")
    },
    {
        key: "inwhTime",
        label: i18n.t("wmsitninventoryrecorddifhis.inwhTime")
    },
    {
        key: "occupyQty",
        label: i18n.t("wmsitninventoryrecorddifhis.occupyQty")
    },
    {
        key: "qty",
        label: i18n.t("wmsitninventoryrecorddifhis.qty")
    },
    {
        key: "unitCode",
        label: i18n.t("wmsitninventoryrecorddifhis.unitCode")
    },
    {
        key: "fileedId",
        label: i18n.t("wmsitninventoryrecorddifhis.fileedId")
    },
    {
        key: "fileedName",
        label: i18n.t("wmsitninventoryrecorddifhis.fileedName")
    },
    {
        key: "oldStockDtlId",
        label: i18n.t("wmsitninventoryrecorddifhis.oldStockDtlId")
    },
    {
        key: "projectNoBak",
        label: i18n.t("wmsitninventoryrecorddifhis.projectNoBak")
    },
    {
        key: "unpackStatus",
        label: i18n.t("wmsitninventoryrecorddifhis.unpackStatus")
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


