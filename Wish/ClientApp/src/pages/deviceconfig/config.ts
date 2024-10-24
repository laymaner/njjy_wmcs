import i18n from "@/lang";

export const ASSEMBLIES: Array<string> = [
  "add",
  "edit",
  "delete",
  "export",
  "imported",
  "deviceopen",
  "deviceclose",
  "devicestepinit",
  "devicewait"
];

export const TABLE_HEADER: Array<object> = [


    {
        key: "Device_Code",
        label: i18n.t("deviceconfig.Device_Code")
    },
    // {
    //     key: "Device_Name",
    //     label: i18n.t("deviceconfig.Device_Name"),
    // },
    // {
    //     key: "WarehouseId",
    //     label: i18n.t("deviceconfig.WarehouseId")
    // },
    {
        key: "Device_Name_view",
        label: i18n.t("deviceconfig.Device_Name_view"),
        width: 180
    },
    {
        key: "IsEnabled",
        label: i18n.t("deviceconfig.IsEnabled"),
        isSlot: true 
    },
    // {
    //     key: "Exec_Flag",
    //     label: i18n.t("deviceconfig.Exec_Flag"),
    //     isSlot: true 
    // },
    // {
    //     key: "Device_Group",
    //     label: i18n.t("deviceconfig.Device_Group")
    // },
    {
        key: "Plc_Name_view",
        label: i18n.t("deviceconfig.Plc_Name_view")
    },
    // {
    //     key: "Plc2WcsStep",
    //     label: i18n.t("deviceconfig.Plc2WcsStep")
    // },
    {
        key: "Plc2WcsStepDesc",
        label: i18n.t("deviceconfig.Plc2WcsStep"),
        width:150
    },
    // {
    //     key: "Wcs2PlcStep",
    //     label: i18n.t("deviceconfig.Wcs2PlcStep")
    // },
    {
        key: "Wcs2PlcStepDesc",
        label: i18n.t("deviceconfig.Wcs2PlcStep"),
        width:150
    },
    // {
    //     key: "Mode",
    //     label: i18n.t("deviceconfig.Mode")
    // },
    {
        key: "ModeDesc",
        label: i18n.t("deviceconfig.Mode")
    },
    {
        key: "IsOnline",
        label: i18n.t("deviceconfig.IsOnline"),
        isSlot: true 
    },
    // {
    //     key: "Config",
    //     label: i18n.t("deviceconfig.Config")
    // },
    // {
    //     key: "Describe",
    //     label: i18n.t("deviceconfig.Describe")
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
