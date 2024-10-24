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
             "Entity.contacts":{
                 label: this.$t("baswwhouse.contacts"),
                 rules: [],
                    type: "input"
            },
             "Entity.maxTaskQty":{
                 label: this.$t("baswwhouse.maxTaskQty"),
                 rules: [],
                    type: "input"
            },
             "Entity.telephone":{
                 label: this.$t("baswwhouse.telephone"),
                 rules: [],
                    type: "input"
            },
             "Entity.usedFlag":{
                 label: this.$t("baswwhouse.usedFlag"),
                 rules: [{ required: true, message: this.$t("baswwhouse.usedFlag")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.whouseAddress":{
                 label:this.$t("baswwhouse.whouseAddress"),
                 rules: [],
                    type: "input"
            },
             "Entity.whouseName":{
                 label: this.$t("baswwhouse.whouseName"),
                 rules: [{ required: true, message: this.$t("baswwhouse.whouseName")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
            //  "Entity.whouseNameAlias":{
            //      label: this.$t("baswwhouse.whouseNameAlias"),
            //      rules: [],
            //         type: "input"
            // },
            //  "Entity.whouseNameEn":{
            //      label: this.$t("baswwhouse.whouseNameEn"),
            //      rules: [],
            //         type: "input"
            // },
             "Entity.whouseNo":{
                 label: this.$t("baswwhouse.whouseNo"),
                 rules: [{ required: true, message: this.$t("baswwhouse.whouseNo")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.whouseType":{
                 label: this.$t("baswwhouse.whouseType"),
                 rules: [{ required: true, message: this.$t("baswwhouse.whouseType")+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            }
                
            }
        };
    }
    beforeOpen() {

    }
}
</script>
