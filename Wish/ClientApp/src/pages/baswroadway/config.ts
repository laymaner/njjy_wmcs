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
        label: i18n.t("baswroadway.areaNo")
    },
    // {
    //     key: "errFlag",
    //     label: i18n.t("baswroadway.errFlag")
    // },
    {
        key: "errFlagDesc",
        label: i18n.t("baswroadway.errFlag")
    },
    {
        key: "errMsg",
        label: i18n.t("baswroadway.errMsg"),
        width: 150
    },
    {
        key: "regionNo",
        label: i18n.t("baswroadway.regionNo")
    },
    {
        key: "reservedQty",
        label: i18n.t("baswroadway.reservedQty")
    },
    {
        key: "roadwayName",
        label: i18n.t("baswroadway.roadwayName")
    },
    // {
    //     key: "roadwayNameAlias",
    //     label: i18n.t("baswroadway.roadwayNameAlias")
    // },
    // {
    //     key: "roadwayNameEn",
    //     label: i18n.t("baswroadway.roadwayNameEn")
    // },
    {
        key: "roadwayNo",
        label: i18n.t("baswroadway.roadwayNo")
    },
    // {
    //     key: "usedFlag",
    //     label: i18n.t("baswroadway.usedFlag")
    // },
    {
        key: "usedFlagDesc",
        label: i18n.t("baswroadway.usedFlag")
    },
    // {
    //     key: "virtualFlag",
    //     label: i18n.t("baswroadway.virtualFlag")
    // },
    {
        key: "virtualFlagDesc",
        label: i18n.t("baswroadway.virtualFlag")
    },
    {
        key: "whouseNo",
        label: i18n.t("baswroadway.whouseNo")
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


