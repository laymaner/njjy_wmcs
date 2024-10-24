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
             "Entity.defaultFlag":{
                 label: "defaultFlag",
                 rules: [{ required: true, message: "defaultFlag"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.paramCode":{
                 label: "paramCode",
                 rules: [{ required: true, message: "paramCode"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.paramValueCode":{
                 label: "paramValueCode",
                 rules: [{ required: true, message: "paramValueCode"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.paramValueDesc":{
                 label: "paramValueDesc",
                 rules: [],
                    type: "input"
            },
             "Entity.paramValueName":{
                 label: "paramValueName",
                 rules: [{ required: true, message: "paramValueName"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.paramValueNameAlias":{
                 label: "paramValueNameAlias",
                 rules: [],
                    type: "input"
            },
             "Entity.paramValueNameEn":{
                 label: "paramValueNameEn",
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
