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
        key: "Device_Code",
        label: "DeviceInfo.DeviceNo"
    },
    {
        key: "Device_Name",
        label: "DeviceInfo.DeviceName"
    },
    {
        key: "Device_Class",
        label: "实现类名"
    },
    {
        key: "DeviceType",
        label: "VersionInfo.Type"
    },
    {
        key: "Company",
        label: "VersionInfo.Company"
    },
    {
        key: "Config",
        label: "DeviceInfo.Config"
    },
    {
        key: "Describe",
        label: "DeviceInfo.Remark"
    },
  { isOperate: true, label: i18n.t(`table.actions`), actions: ["detail", "edit", "deleted"] } //操作列
];

export const DeviceTypeTypes: Array<any> = [
  { Text: "单工位堆垛机", Value: "S1Srm" },
  { Text: "双工位堆垛机", Value: "S2Srm" },
  { Text: "输送机", Value: "Conveyor" },
  { Text: "有轨道小车", Value: "RGV" }
];

