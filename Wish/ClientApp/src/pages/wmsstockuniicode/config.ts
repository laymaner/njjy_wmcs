import i18n from "@/lang";

export const ASSEMBLIES: Array<string> = [
  "add",
  "edit",
  "delete",
  "export",
  "imported",
  "manualout"
];

export const TABLE_HEADER: Array<object> = [


    {
        key: "uniicode",
        label: i18n.t("wmsstockuniicode.uniicode"),
        width: 180
    },
    {
        key: "supplierCode",
        label: i18n.t("wmsstockuniicode.supplierCode"),
        width: 150
    },
    {
        key: "palletBarcode",
        label: i18n.t("wmsstockuniicode.palletBarcode"),
        width: 150
    },
    {
        key: "projectNo",
        label: i18n.t("wmsstockuniicode.projectNo"),
        width: 150
    },
    {
        key: "stockCode",
        label: i18n.t("wmsstockuniicode.stockCode"),
        width: 150
    },
    // {
    //     key: "stockStatus",
    //     label: i18n.t("wmsstockuniicode.stockStatus"),
    //     width: 150
    // },
    {
        key: "stockStatusDesc",
        label: i18n.t("wmsstockuniicode.stockStatus"),
        width: 100
    },
    {
        key: "binNo",
        label: i18n.t("wmsstockuniicode.binNo"),
        width: 150
    },
    {
        key: "stockDtlId",
        label: i18n.t("wmsstockuniicode.stockDtlId"),
        width: 120
    },
    {
        key: "qty",
        label: i18n.t("wmsstockuniicode.qty"),
        width: 180
    },
    {
        key: "occupyQty",
        label: i18n.t("wmsstockuniicode.occupyQty"),
        width: 180
    },
    {
        key: "areaNo",
        label: i18n.t("wmsstockuniicode.areaNo"),
        width: 80
    },
    {
        key: "batchNo",
        label: i18n.t("wmsstockuniicode.batchNo"),
        width: 150
    },
    {
        key: "unpackStatusDesc",
        label: i18n.t("wmsstockuniicode.unpackStatus")
    },
    // {
    //     key: "dataCode",
    //     label: i18n.t("wmsstockuniicode.dataCode")
    // },
    // {
    //     key: "delayFrozenFlag",
    //     label: i18n.t("wmsstockuniicode.delayFrozenFlag")
    // },
    // {
    //     key: "delayFrozenReason",
    //     label: i18n.t("wmsstockuniicode.delayFrozenReason")
    // },
    // {
    //     key: "delayReason",
    //     label: i18n.t("wmsstockuniicode.delayReason")
    // },
    // {
    //     key: "delayTimes",
    //     label: i18n.t("wmsstockuniicode.delayTimes")
    // },
    {
        key: "delayToEndDate",
        label: i18n.t("wmsstockuniicode.delayToEndDate"),
        width: 150
    },
    // {
    //     key: "driedScrapFlag",
    //     label: i18n.t("wmsstockuniicode.driedScrapFlag")
    // },
    // {
    //     key: "driedTimes",
    //     label: i18n.t("wmsstockuniicode.driedTimes")
    // },
    // {
    //     key: "erpWhouseNo",
    //     label: i18n.t("wmsstockuniicode.erpWhouseNo")
    // },
    {
        key: "expDate",
        label: i18n.t("wmsstockuniicode.expDate"),
        width: 150
    },
    // {
    //     key: "exposeFrozenFlag",
    //     label: i18n.t("wmsstockuniicode.exposeFrozenFlag")
    // },
    // {
    //     key: "exposeFrozenReason",
    //     label: i18n.t("wmsstockuniicode.exposeFrozenReason")
    // },
    {
        key: "chipSize",
        label: i18n.t("wmsstockuniicode.chipSize")
    },
    {
        key: "chipThickness",
        label: i18n.t("wmsstockuniicode.chipThickness")
    },
    {
        key: "chipModel",
        label: i18n.t("wmsstockuniicode.chipModel"),
        width: 300
    },
    {
        key: "dafType",
        label: i18n.t("wmsstockuniicode.dafType"),
        width: 300
    },
    // {
    //     key: "inspectionResult",
    //     label: i18n.t("wmsstockuniicode.inspectionResult")
    // },
    {
        key: "inwhTime",
        label: i18n.t("wmsstockuniicode.inwhTime"),
        width: 150
    },
    // {
    //     key: "leftMslTimes",
    //     label: i18n.t("wmsstockuniicode.leftMslTimes")
    // },
    {
        key: "materialName",
        label: i18n.t("wmsstockuniicode.materialName"),
        width: 100
    },
    {
        key: "materialCode",
        label: i18n.t("wmsstockuniicode.materialCode"),
        width: 120
    },
    // {
    //     key: "materialSpec",
    //     label: i18n.t("wmsstockuniicode.materialSpec")
    // },
    // {
    //     key: "mslGradeCode",
    //     label: i18n.t("wmsstockuniicode.mslGradeCode")
    // },
    
    // {
    //     key: "packageTime",
    //     label: i18n.t("wmsstockuniicode.packageTime")
    // },
    
    // {
    //     key: "positionNo",
    //     label: i18n.t("wmsstockuniicode.positionNo")
    // },
    // {
    //     key: "productDate",
    //     label: i18n.t("wmsstockuniicode.productDate"),
    //     width: 150
    // },
    
    // {
    //     key: "proprietorCode",
    //     label: i18n.t("wmsstockuniicode.proprietorCode")
    // },
    // {
    //     key: "realExposeTimes",
    //     label: i18n.t("wmsstockuniicode.realExposeTimes")
    // },
    // {
    //     key: "skuCode",
    //     label: i18n.t("wmsstockuniicode.skuCode")
    // },
    
    // {
    //     key: "supplierCode",
    //     label: i18n.t("wmsstockuniicode.supplierCode")
    // },
    // {
    //     key: "supplierExposeTimes",
    //     label: i18n.t("wmsstockuniicode.supplierExposeTimes")
    // },
    // {
    //     key: "supplierName",
    //     label: i18n.t("wmsstockuniicode.supplierName")
    // },
    // {
    //     key: "supplierNameAlias",
    //     label: i18n.t("wmsstockuniicode.supplierNameAlias")
    // },
    // {
    //     key: "supplierNameEn",
    //     label: i18n.t("wmsstockuniicode.supplierNameEn")
    // },
    
    // {
    //     key: "unpackStatus",
    //     label: i18n.t("wmsstockuniicode.unpackStatus")
    // },
    // {
    //     key: "unpackTime",
    //     label: i18n.t("wmsstockuniicode.unpackTime")
    // },
    {
        key: "unitCode",
        label: i18n.t("wmsstockuniicode.unitCode")
    },
    // {
    //     key: "whouseNo",
    //     label: i18n.t("wmsstockuniicode.whouseNo")
    // },
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
export const IsOrderTypes: Array<any> = [
    { Text: "是", Value: 1 },
    { Text: "否", Value: 0 }
  ];

