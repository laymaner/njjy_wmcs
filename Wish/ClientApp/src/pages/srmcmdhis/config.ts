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
        key: "SubTask_No",
        label: i18n.t("srmcmdhis.SubTask_No"),
        width:180
    },
    {
        key: "Task_No",
        label: i18n.t("srmcmdhis.Task_No"),
        width:180
    },
    {
        key: "Serial_No",
        label: i18n.t("srmcmdhis.Serial_No")
    },
    {
        key: "Device_No",
        label: i18n.t("srmcmdhis.Device_No")
    },
    // {
    //     key: "Fork_No",
    //     label: i18n.t("srmcmdhis.Fork_No")
    // },
    // {
    //     key: "Station_Type",
    //     label: i18n.t("srmcmdhis.Station_Type")
    // },
    {
        key: "Check_Point",
        label: i18n.t("srmcmdhis.Check_Point")
    },
    // {
    //     key: "Task_Cmd",
    //     label: i18n.t("srmcmdhis.Task_Cmd")
    // },
    {
        key: "Task_Cmd_Desc",
        label: i18n.t("srmcmdhis.Task_Cmd")
    },
    {
        key: "Task_Type",
        label: i18n.t("srmcmdhis.Task_Type")
    },
    {
        key: "Pallet_Barcode",
        label: i18n.t("srmcmdhis.Pallet_Barcode"),
        width:120
    },
    // {
    //     key: "Exec_Status",
    //     label: i18n.t("srmcmdhis.Exec_Status")
    // },
    {
        key: "WaferID",
        label: i18n.t("srmcmdhis.WaferID"),
        width: 200
    },
    {
        key: "Exec_Status_Desc",
        label: i18n.t("srmcmdhis.Exec_Status")
    },
    {
        key: "Remark_Desc",
        label: i18n.t("srmcmdhis.Remark_Desc"),
        width: 500
    },
    {
        key: "From_Station",
        label: i18n.t("srmcmdhis.From_Station")
    },
    {
        key: "From_ForkDirection",
        label: i18n.t("srmcmdhis.From_ForkDirection")
    },
    {
        key: "From_Column",
        label: i18n.t("srmcmdhis.From_Column")
    },
    {
        key: "From_Layer",
        label: i18n.t("srmcmdhis.From_Layer")
    },
    {
        key: "From_Deep",
        label: i18n.t("srmcmdhis.From_Deep")
    },
    {
        key: "To_Station",
        label: i18n.t("srmcmdhis.To_Station")
    },
    {
        key: "To_ForkDirection",
        label: i18n.t("srmcmdhis.To_ForkDirection")
    },
    {
        key: "To_Column",
        label: i18n.t("srmcmdhis.To_Column")
    },
    {
        key: "To_Layer",
        label: i18n.t("srmcmdhis.To_Layer")
    },
    {
        key: "To_Deep",
        label: i18n.t("srmcmdhis.To_Deep")
    },
    {
        key: "Recive_Date",
        label: i18n.t("srmcmdhis.Recive_Date"),
        width: 150
    },
    {
        key: "Begin_Date",
        label: i18n.t("srmcmdhis.Begin_Date"),
        width: 150
    },
    {
        key: "Pick_Date",
        label: i18n.t("srmcmdhis.Pick_Date"),
        width: 150
    },
    {
        key: "Put_Date",
        label: i18n.t("srmcmdhis.Put_Date"),
        width: 150
    },
    {
        key: "Finish_Date",
        label: i18n.t("srmcmdhis.Finish_Date"),
        width: 150
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


