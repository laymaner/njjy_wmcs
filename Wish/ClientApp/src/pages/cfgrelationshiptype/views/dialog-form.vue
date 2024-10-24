<template>
    <wtm-dialog-box :is-show.sync="isShow" :status="status" :events="formEvent">
        <wtm-create-form :ref="refName" :status="status" :options="formOptions" ></wtm-create-form>
    </wtm-dialog-box>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { Action, State } from "vuex-class";
import formMixin from "@/vue-custom/mixin/form-mixin";
import UploadImg from "@/components/page/UploadImg.vue";


@Component({
    mixins: [formMixin()]
})
export default class Index extends Vue {

    // 表单结构
    get formOptions() {
        const filterMethod = (query, item) => {
            return item.label.indexOf(query) > -1;
        };
        return {
            formProps: {
                "label-width": "100px"
            },
            formItem: {
                "Entity.ID": {
                    isHidden: true
                },
             "Entity.developFlag":{
                 label: "developFlag",
                 rules: [{ required: true, message: "developFlag"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.leftAttributes":{
                 label: "leftAttributes",
                 rules: [],
                    type: "input"
            },
             "Entity.leftTable":{
                 label: "leftTable",
                 rules: [],
                    type: "input"
            },
             "Entity.leftTableCode":{
                 label: "leftTableCode",
                 rules: [],
                    type: "input"
            },
             "Entity.leftTableCodeLabel":{
                 label: "leftTableCodeLabel",
                 rules: [],
                    type: "input"
            },
             "Entity.leftTableCodeLabelAlias":{
                 label: "leftTableCodeLabelAlias",
                 rules: [],
                    type: "input"
            },
             "Entity.leftTableCodeLabelEn":{
                 label: "leftTableCodeLabelEn",
                 rules: [],
                    type: "input"
            },
             "Entity.leftTableName":{
                 label: "leftTableName",
                 rules: [],
                    type: "input"
            },
             "Entity.leftTableNameAlias":{
                 label: "leftTableNameAlias",
                 rules: [],
                    type: "input"
            },
             "Entity.leftTableNameEn":{
                 label: "leftTableNameEn",
                 rules: [],
                    type: "input"
            },
             "Entity.leftTableNameLabel":{
                 label: "leftTableNameLabel",
                 rules: [],
                    type: "input"
            },
             "Entity.leftTableNameLabelAlias":{
                 label: "leftTableNameLabelAlias",
                 rules: [],
                    type: "input"
            },
             "Entity.leftTableNameLabelEn":{
                 label: "leftTableNameLabelEn",
                 rules: [],
                    type: "input"
            },
             "Entity.leftWhouse":{
                 label: "leftWhouse",
                 rules: [],
                    type: "input"
            },
             "Entity.relationshipTypeCode":{
                 label: "relationshipTypeCode",
                 rules: [{ required: true, message: "relationshipTypeCode"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.relationshipTypeDesc":{
                 label: "relationshipTypeDesc",
                 rules: [],
                    type: "input"
            },
             "Entity.relationshipTypeName":{
                 label: "relationshipTypeName",
                 rules: [{ required: true, message: "relationshipTypeName"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.relationshipTypeNameAlias":{
                 label: "relationshipTypeNameAlias",
                 rules: [],
                    type: "input"
            },
             "Entity.relationshipTypeNameEn":{
                 label: "relationshipTypeNameEn",
                 rules: [],
                    type: "input"
            },
             "Entity.rightAttributes":{
                 label: "rightAttributes",
                 rules: [],
                    type: "input"
            },
             "Entity.rightTable":{
                 label: "rightTable",
                 rules: [],
                    type: "input"
            },
             "Entity.rightTableCode":{
                 label: "rightTableCode",
                 rules: [],
                    type: "input"
            },
             "Entity.rightTableCodeLabel":{
                 label: "rightTableCodeLabel",
                 rules: [],
                    type: "input"
            },
             "Entity.rightTableCodeLabelAlias":{
                 label: "rightTableCodeLabelAlias",
                 rules: [],
                    type: "input"
            },
             "Entity.rightTableCodeLabelEn":{
                 label: "rightTableCodeLabelEn",
                 rules: [],
                    type: "input"
            },
             "Entity.rightTableName":{
                 label: "rightTableName",
                 rules: [],
                    type: "input"
            },
             "Entity.rightTableNameAlias":{
                 label: "rightTableNameAlias",
                 rules: [],
                    type: "input"
            },
             "Entity.rightTableNameEn":{
                 label: "rightTableNameEn",
                 rules: [],
                    type: "input"
            },
             "Entity.rightTableNameLabel":{
                 label: "rightTableNameLabel",
                 rules: [],
                    type: "input"
            },
             "Entity.rightTableNameLabelAlias":{
                 label: "rightTableNameLabelAlias",
                 rules: [],
                    type: "input"
            },
             "Entity.rightTableNameLabelEn":{
                 label: "rightTableNameLabelEn",
                 rules: [],
                    type: "input"
            },
             "Entity.rightTablePriority":{
                 label: "rightTablePriority",
                 rules: [{ required: true, message: "rightTablePriority"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.rightWhouse":{
                 label: "rightWhouse",
                 rules: [],
                    type: "input"
            },
             "Entity.usedFlag":{
                 label: "usedFlag",
                 rules: [{ required: true, message: "usedFlag"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            }
                
            }
        };
    }
    beforeOpen() {

    }
}
</script>
