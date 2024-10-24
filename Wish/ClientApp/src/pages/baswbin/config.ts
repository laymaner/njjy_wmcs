import { style } from "@/config";
import i18n from "@/lang";

export const ASSEMBLIES: Array<string> = [
  "add",
  "edit",
  "delete",
  "export",
  "imported",
  "openbin",
  "closebin"
];

export const TABLE_HEADER: Array<object> = [


    {
        key: "areaNo",
        label: i18n.t("baswbin.areaNo"),
    },
    {
        key: "binNo",
        label: i18n.t("baswbin.binNo"),
        width: 120
    },
    {
        key: "binName",
        label: i18n.t("baswbin.binName"),
        width: 180
    },
    // {
    //     key: "bearWeight",
    //     label: i18n.t("baswbin.bearWeight")
    // },
    {
        key: "binRow",
        label: i18n.t("baswbin.binRow")
    },
    {
        key: "binCol",
        label: i18n.t("baswbin.binCol")
    },
    {
        key: "binLayer",
        label: i18n.t("baswbin.binLayer")
    },
    // {
    //     key: "binErrFlag",
    //     label: i18n.t("baswbin.binErrFlag")
    // },
    {
        key: "binErrFlagDesc",
        label: i18n.t("baswbin.binErrFlag")
    },
    {
        key: "binErrMsg",
        label: i18n.t("baswbin.binErrMsg")
    },
    {
        key: "rackNo",
        label: i18n.t("baswbin.rackNo")
    },
    {
        key: "regionNo",
        label: i18n.t("baswbin.regionNo")
    },
    {
        key: "roadwayNo",
        label: i18n.t("baswbin.roadwayNo")
    },
    // {
    //     key: "usedFlag",
    //     label: i18n.t("baswbin.usedFlag")
    // },
    {
        key: "usedFlagDesc",
        label: i18n.t("baswbin.usedFlag")
    },
    // {
    //     key: "binGroupIdx",
    //     label: i18n.t("baswbin.binGroupIdx")
    // },
    {
        key: "binGroupNo",
        label: i18n.t("baswbin.binGroupNo")
    },
    {
        key: "binHeight",
        label: i18n.t("baswbin.binHeight")
    },
    {
        key: "binLength",
        label: i18n.t("baswbin.binLength")
    },
    
    // {
    //     key: "binNameAlias",
    //     label: i18n.t("baswbin.binNameAlias")
    // },
    // {
    //     key: "binNameEn",
    //     label: i18n.t("baswbin.binNameEn")
    // },
    
    {
        key: "binPriority",
        label: i18n.t("baswbin.binPriority")
    },
    {
        key: "binType",
        label: i18n.t("baswbin.binType")
    },
    {
        key: "binWidth",
        label: i18n.t("baswbin.binWidth")
    },
    {
        key: "capacitySize",
        label: i18n.t("baswbin.capacitySize")
    },
    {
        key: "extensionGroupNo",
        label: i18n.t("baswbin.extensionGroupNo")
    },
    {
        key: "extensionIdx",
        label: i18n.t("baswbin.extensionIdx")
    },
    // {
    //     key: "fireFlag",
    //     label: i18n.t("baswbin.fireFlag")
    // },
    // {
    //     key: "isInEnable",
    //     label: i18n.t("baswbin.isInEnable")
    // },
    {
        key: "isInEnableDesc",
        label: i18n.t("baswbin.isInEnable")
    },
    // {
    //     key: "isOutEnable",
    //     label: i18n.t("baswbin.isOutEnable")
    // },
    {
        key: "isOutEnableDesc",
        label: i18n.t("baswbin.isOutEnable")
    },
    {
        key: "isValidityPeriod",
        label: i18n.t("baswbin.isValidityPeriod")
    },
    {
        key: "palletDirect",
        label: i18n.t("baswbin.palletDirect")
    },
    
    // {
    //     key: "virtualFlag",
    //     label: i18n.t("baswbin.virtualFlag")
    // },
    {
        key: "virtualFlagDesc",
        label: i18n.t("baswbin.virtualFlag")
    },
    {
        key: "whouseNo",
        label: i18n.t("baswbin.whouseNo")
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


