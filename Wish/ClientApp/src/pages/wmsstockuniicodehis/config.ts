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
        key: "uniicode",
        label: i18n.t("wmsstockuniicodehis.uniicode"),
        width: 180
    },
    
    {
        key: "supplierCode",
        label: i18n.t("wmsstockuniicodehis.supplierCode"),
        width: 150
    }, 
    {
        key: "palletBarcode",
        label: i18n.t("wmsstockuniicodehis.palletBarcode"),
        width: 150
    },  
    {
        key: "projectNo",
        label: i18n.t("wmsstockuniicodehis.projectNo"),
        width: 150
    },
    {
        key: "stockCode",
        label: i18n.t("wmsstockuniicodehis.stockCode"),
        width: 150
    },
    {
        key: "stockStatusDesc",
        label: i18n.t("wmsstockuniicodehis.stockStatus"),
        width: 100
    },
    {
        key: "binNo",
        label: i18n.t("wmsstockuniicodehis.binNo"),
        width: 150
    },
    {
        key: "stockDtlId",
        label: i18n.t("wmsstockuniicodehis.stockDtlId"),
        width: 120
    },
    {
        key: "qty",
        label: i18n.t("wmsstockuniicodehis.qty"),
        width: 180
    },
    {
        key: "occupyQty",
        label: i18n.t("wmsstockuniicodehis.occupyQty"),
        width: 180
    },
    {
        key: "areaNo",
        label: i18n.t("wmsstockuniicodehis.areaNo"),
        width: 80
    },
    {
        key: "batchNo",
        label: i18n.t("wmsstockuniicodehis.batchNo"),
        width: 150
    },
    {
        key: "unpackStatusDesc",
        label: i18n.t("wmsstockuniicode.unpackStatus")
    },
    // {
    //     key: "dataCode",
    //     label: i18n.t("wmsstockuniicodehis.dataCode")
    // },
    // {
    //     key: "delayFrozenFlag",
    //     label: i18n.t("wmsstockuniicodehis.delayFrozenFlag")
    // },
    // {
    //     key: "delayFrozenReason",
    //     label: i18n.t("wmsstockuniicodehis.delayFrozenReason")
    // },
    // {
    //     key: "delayReason",
    //     label: i18n.t("wmsstockuniicodehis.delayReason")
    // },
    // {
    //     key: "delayTimes",
    //     label: i18n.t("wmsstockuniicodehis.delayTimes")
    // },
    {
        key: "delayToEndDate",
        label: i18n.t("wmsstockuniicodehis.delayToEndDate"),
        width: 150
    },
    // {
    //     key: "driedScrapFlag",
    //     label: i18n.t("wmsstockuniicodehis.driedScrapFlag")
    // },
    // {
    //     key: "driedTimes",
    //     label: i18n.t("wmsstockuniicodehis.driedTimes")
    // },
    // {
    //     key: "erpWhouseNo",
    //     label: i18n.t("wmsstockuniicodehis.erpWhouseNo")
    // },
    {
        key: "expDate",
        label: i18n.t("wmsstockuniicodehis.expDate"),
        width: 150
    },
    // {
    //     key: "exposeFrozenFlag",
    //     label: i18n.t("wmsstockuniicodehis.exposeFrozenFlag")
    // },
    // {
    //     key: "exposeFrozenReason",
    //     label: i18n.t("wmsstockuniicodehis.exposeFrozenReason")
    // },
    {
        key: "chipSize",
        label: i18n.t("wmsstockuniicodehis.chipSize")
    },
    {
        key: "chipThickness",
        label: i18n.t("wmsstockuniicodehis.chipThickness")
    },
    {
        key: "chipModel",
        label: i18n.t("wmsstockuniicodehis.chipModel"),
        width: 300
    },
    {
        key: "dafType",
        label: i18n.t("wmsstockuniicodehis.dafType"),
        width: 300
    },
    // {
    //     key: "inspectionResult",
    //     label: i18n.t("wmsstockuniicodehis.inspectionResult")
    // },
    {
        key: "inwhTime",
        label: i18n.t("wmsstockuniicodehis.inwhTime"),
        width: 150
    },
    // {
    //     key: "leftMslTimes",
    //     label: i18n.t("wmsstockuniicodehis.leftMslTimes")
    // },
    {
        key: "materialName",
        label: i18n.t("wmsstockuniicodehis.materialName"),
        width: 100
    },
    {
        key: "materialCode",
        label: i18n.t("wmsstockuniicodehis.materialCode"),
        width: 120
    },
    // {
    //     key: "materialSpec",
    //     label: i18n.t("wmsstockuniicodehis.materialSpec")
    // },
    // {
    //     key: "mslGradeCode",
    //     label: i18n.t("wmsstockuniicodehis.mslGradeCode")
    // },
    
    // {
    //     key: "packageTime",
    //     label: i18n.t("wmsstockuniicodehis.packageTime")
    // },
    // {
    //     key: "palletBarcode",
    //     label: i18n.t("wmsstockuniicodehis.palletBarcode")
    // },
    // {
    //     key: "positionNo",
    //     label: i18n.t("wmsstockuniicodehis.positionNo")
    // },
    // {
    //     key: "productDate",
    //     label: i18n.t("wmsstockuniicodehis.productDate")
    // },
    
    // {
    //     key: "proprietorCode",
    //     label: i18n.t("wmsstockuniicodehis.proprietorCode")
    // },
    // {
    //     key: "realExposeTimes",
    //     label: i18n.t("wmsstockuniicodehis.realExposeTimes")
    // },
    // {
    //     key: "skuCode",
    //     label: i18n.t("wmsstockuniicodehis.skuCode")
    // },
   
    // {
    //     key: "supplierExposeTimes",
    //     label: i18n.t("wmsstockuniicodehis.supplierExposeTimes")
    // },
    // {
    //     key: "supplierName",
    //     label: i18n.t("wmsstockuniicodehis.supplierName")
    // },
    // {
    //     key: "supplierNameAlias",
    //     label: i18n.t("wmsstockuniicodehis.supplierNameAlias")
    // },
    // {
    //     key: "supplierNameEn",
    //     label: i18n.t("wmsstockuniicodehis.supplierNameEn")
    // },
    
    // {
    //     key: "unpackStatus",
    //     label: i18n.t("wmsstockuniicodehis.unpackStatus")
    // },
    // {
    //     key: "unpackTime",
    //     label: i18n.t("wmsstockuniicodehis.unpackTime")
    // },
    {
        key: "unitCode",
        label: i18n.t("wmsstockuniicodehis.unitCode")
    },
    // {
    //     key: "whouseNo",
    //     label: i18n.t("wmsstockuniicodehis.whouseNo")
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


