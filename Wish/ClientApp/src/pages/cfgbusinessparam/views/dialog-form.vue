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
             "Entity.businessCode":{
                 label: "businessCode",
                 rules: [{ required: true, message: "businessCode"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.businessModuleCode":{
                 label: "businessModuleCode",
                 rules: [{ required: true, message: "businessModuleCode"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.checkFlag":{
                 label: "checkFlag",
                 rules: [{ required: true, message: "checkFlag"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.paramCode":{
                 label: "paramCode",
                 rules: [{ required: true, message: "paramCode"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.paramDesc":{
                 label: "paramDesc",
                 rules: [],
                    type: "input"
            },
             "Entity.paramName":{
                 label: "paramName",
                 rules: [{ required: true, message: "paramName"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.paramNameAlias":{
                 label: "paramNameAlias",
                 rules: [],
                    type: "input"
            },
             "Entity.paramNameEn":{
                 label: "paramNameEn",
                 rules: [],
                    type: "input"
            },
             "Entity.paramSort":{
                 label: "paramSort",
                 rules: [{ required: true, message: "paramSort"+this.$t("form.notnull"),trigger: "blur" }],
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
