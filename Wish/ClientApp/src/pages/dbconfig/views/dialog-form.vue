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
    @Action
    getPlcConfig;
    @State
    getPlcConfigData;

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
             "Entity.Block_Code":{
                 label: this.$t("dbconfig.Block_Code"),
                 rules: [{ required: true, message: this.$t("dbconfig.Block_Code")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.Block_Name":{
                 label: this.$t("dbconfig.Block_Name"),
                 rules: [{ required: true, message: this.$t("dbconfig.Block_Name")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.Block_Offset":{
                 label: this.$t("dbconfig.Block_Offset"),
                 rules: [{ required: true, message: this.$t("dbconfig.Block_Offset")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.Block_Length":{
                 label: this.$t("dbconfig.Block_Length"),
                 rules: [{ required: true, message: this.$t("dbconfig.Block_Length")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.PlcConfigId":{
                 label: this.$t("dbconfig.PlcConfigId"),
                 rules: [{ required: true, message: this.$t("dbconfig.PlcConfigId")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "select",
                    children: this.getPlcConfigData,
                    props: {
                        clearable: true
                    }
            },
             "Entity.Describe":{
                 label: this.$t("dbconfig.Describe"),
                 rules: [],
                    type: "input"
            }
                
            }
        };
    }
    beforeOpen() {
        this.getPlcConfig();

    }
}
</script>
