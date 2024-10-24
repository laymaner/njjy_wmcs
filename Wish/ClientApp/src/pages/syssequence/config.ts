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
        key: "SeqCode",
        label: "SeqCode"
    },
    {
        key: "SeqDesc",
        label: "SeqDesc"
    },
    {
        key: "SeqType",
        label: "SeqType"
    },
    {
        key: "NowSn",
        label: "NowSn"
    },
    {
        key: "MinSn",
        label: "MinSn"
    },
    {
        key: "MaxSn",
        label: "MaxSn"
    },
    {
        key: "SeqSnLen",
        label: "SeqSnLen"
    },
    {
        key: "SeqPrefix",
        label: "SeqPrefix"
    },
    {
        key: "SeqDate",
        label: "SeqDate"
    },
  { isOperate: true, label: i18n.t(`table.actions`), actions: ["detail", "edit", "deleted"] } //操作列
];


