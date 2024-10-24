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
        key: "unitCode",
        label: "unitCode"
    },
    {
        key: "unitName",
        label: "unitName"
    },
    {
        key: "unitNameAlias",
        label: "unitNameAlias"
    },
    {
        key: "unitNameEn",
        label: "unitNameEn"
    },
    {
        key: "unitType",
        label: "unitType"
    },
    {
        key: "usedFlag",
        label: "usedFlag"
    },
    {
        key: "whouseNo",
        label: "whouseNo"
    },
  { isOperate: true, label: i18n.t(`table.actions`), actions: ["detail", "edit", "deleted"] } //操作列
];


