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
        key: "AreaNo",
        label: "AreaNo"
    },
    {
        key: "DevNo",
        label: "DevNo"
    },
    {
        key: "PartNo",
        label: "PartNo"
    },
    {
        key: "PartLocNo",
        label: "PartLocNo"
    },
    {
        key: "DevRunMode",
        label: "DevRunMode"
    },
    {
        key: "SrmRoadway",
        label: "SrmRoadway"
    },
    {
        key: "SrmForkType",
        label: "SrmForkType"
    },
    {
        key: "SrmExecStep",
        label: "SrmExecStep"
    },
    {
        key: "IsInSitu",
        label: "IsInSitu"
    },
    {
        key: "IsAlarming",
        label: "IsAlarming"
    },
    {
        key: "AlarmCode",
        label: "AlarmCode"
    },
    {
        key: "IsFree",
        label: "IsFree"
    },
    {
        key: "IsHasGoods",
        label: "IsHasGoods"
    },
    {
        key: "CmdNo",
        label: "CmdNo"
    },
    {
        key: "PalletNo",
        label: "PalletNo"
    },
    {
        key: "OldPalletNo",
        label: "OldPalletNo"
    },
    {
        key: "ReadPalletNo",
        label: "ReadPalletNo"
    },
    {
        key: "StationNo",
        label: "StationNo"
    },
    {
        key: "CurrentX",
        label: "CurrentX"
    },
    {
        key: "CurrentY",
        label: "CurrentY"
    },
    {
        key: "CurrentZ",
        label: "CurrentZ"
    },
  { isOperate: true, label: i18n.t(`table.actions`), actions: ["detail", "edit", "deleted"] } //操作列
];


