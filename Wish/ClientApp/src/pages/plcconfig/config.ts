import i18n from "@/lang";

export const ASSEMBLIES: Array<string> = [
  "add",
  "edit",
  "delete",
  "export",
  "imported",
  "plcopen",
  "plcclose"
];

export const TABLE_HEADER: Array<object> = [


    {
        key: "Plc_Code",
        label: i18n.t("plcconfig.Plc_Code")
    },
    {
        key: "Plc_Name",
        label: i18n.t("plcconfig.Plc_Name")
    },
    {
        key: "IP_Address",
        label: i18n.t("plcconfig.IP_Address")
    },
    {
        key: "IP_Port",
        label: i18n.t("plcconfig.IP_Port")
    },
    {
        key: "ConnType",
        label: i18n.t("plcconfig.ConnType")
    },
    // {
    //     key: "IsConnect",
    //     label: i18n.t("plcconfig.IsConnect"),
    //     isSlot: true 
    // },
    {
        key: "IsEnabled",
        label: i18n.t("plcconfig.IsEnabled"),
        isSlot: true 
    },
    {
        key: "Scan_Cycle",
        label: i18n.t("plcconfig.Scan_Cycle")
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
    // {
    //     key: "Heartbeat_DB",
    //     label: i18n.t("plcconfig.Heartbeat_DB")
    // },
    // {
    //     key: "Heartbeat_Address",
    //     label: i18n.t("plcconfig.Heartbeat_Address")
    // },
    // {
    //     key: "Heartbeat_Enabled",
    //     label: i18n.t("plcconfig.Heartbeat_Enabled"),
    //     isSlot: true 
    // },
    // {
    //     key: "Heartbeat_WriteInterval",
    //     label: i18n.t("plcconfig.Heartbeat_WriteInterval")
    // },
    // {
    //     key: "Describe",
    //     label: i18n.t("plcconfig.Describe")
    // },
  { isOperate: true, label: i18n.t(`table.actions`), actions: ["detail", "edit", "deleted"] } //操作列
];

export const ConnTypeTypes: Array<any> = [
  { Text: "HSL", Value: "HSL" },
  { Text: "Socket", Value: "Socket" }
];
export const UsedFlagTypes: Array<any> = [
    { Text: "有效", Value: "true" },
    { Text: "无效", Value: "false" }
  ];

