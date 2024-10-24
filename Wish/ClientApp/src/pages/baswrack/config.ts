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
        label: i18n.t("baswrack.areaNo")
    },
    {
        key: "isInEnable",
        label: i18n.t("baswrack.isInEnable")
    },
    {
        key: "isInEnableDesc",
        label: i18n.t("baswrack.isInEnable")
    },
    {
        key: "isOutEnable",
        label: i18n.t("baswrack.isOutEnable")
    },
    {
        key: "isOutEnableDesc",
        label: i18n.t("baswrack.isOutEnable")
    },
    {
        key: "rackIdx",
        label: i18n.t("baswrack.rackIdx")
    },
    {
        key: "rackName",
        label: i18n.t("baswrack.rackName"),
        width: 150
    },
    // {
    //     key: "rackNameAlias",
    //     label: i18n.t("baswrack.rackNameAlias")
    // },
    // {
    //     key: "rackNameEn",
    //     label: i18n.t("baswrack.rackNameEn")
    // },
    {
        key: "rackNo",
        label: i18n.t("baswrack.rackNo")
    },
    {
        key: "regionNo",
        label: i18n.t("baswrack.regionNo")
    },
    {
        key: "roadwayNo",
        label: i18n.t("baswrack.roadwayNo")
    },
    {
        key: "usedFlag",
        label: i18n.t("baswrack.usedFlag")
    },
    {
        key: "usedFlagDesc",
        label: i18n.t("baswrack.usedFlag")
    },
    {
        key: "virtualFlag",
        label: i18n.t("baswrack.virtualFlag")
    },
    {
        key: "virtualFlagDesc",
        label: i18n.t("baswrack.virtualFlag")
    },
    {
        key: "whouseNo",
        label: i18n.t("baswrack.whouseNo")
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


