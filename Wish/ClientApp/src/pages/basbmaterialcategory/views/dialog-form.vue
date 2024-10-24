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
             "Entity.delayDays":{
                 label: "delayDays",
                 rules: [{ required: true, message: "delayDays"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.materialFlag":{
                 label: "materialFlag",
                 rules: [{ required: true, message: "materialFlag"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.isAutoDelay":{
                 label: "isAutoDelay",
                 rules: [{ required: true, message: "isAutoDelay"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.materialCategoryDesc":{
                 label: "materialCategoryDesc",
                 rules: [],
                    type: "input"
            },
             "Entity.materialCategoryName":{
                 label: "materialCategoryName",
                 rules: [{ required: true, message: "materialCategoryName"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.materialCategoryNameAlias":{
                 label: "materialCategoryNameAlias",
                 rules: [],
                    type: "input"
            },
             "Entity.materialCategoryNameEn":{
                 label: "materialCategoryNameEn",
                 rules: [],
                    type: "input"
            },
             "Entity.materialCategoryCode":{
                 label: "materialCategoryCode",
                 rules: [{ required: true, message: "materialCategoryCode"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.proprietorCode":{
                 label: "proprietorCode",
                 rules: [],
                    type: "input"
            },
             "Entity.usedFlag":{
                 label: "usedFlag",
                 rules: [{ required: true, message: "usedFlag"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.virtualFlag":{
                 label: "virtualFlag",
                 rules: [{ required: true, message: "virtualFlag"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.whouseNo":{
                 label: "whouseNo",
                 rules: [],
                    type: "input"
            }
                
            }
        };
    }
    beforeOpen() {

    }
}
</script>
