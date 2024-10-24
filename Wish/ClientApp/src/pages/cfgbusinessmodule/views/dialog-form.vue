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
             "Entity.businessModuleDesc":{
                 label: "businessModuleDesc",
                 rules: [],
                    type: "input"
            },
             "Entity.businessModuleName":{
                 label: "businessModuleName",
                 rules: [{ required: true, message: "businessModuleName"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.businessModuleNameAlias":{
                 label: "businessModuleNameAlias",
                 rules: [],
                    type: "input"
            },
             "Entity.businessModuleNameEn":{
                 label: "businessModuleNameEn",
                 rules: [],
                    type: "input"
            },
             "Entity.businessModuleSort":{
                 label: "businessModuleSort",
                 rules: [{ required: true, message: "businessModuleSort"+this.$t("form.notnull"),trigger: "blur" }],
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
