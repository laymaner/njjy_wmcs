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
             "Entity.address":{
                 label: "address",
                 rules: [],
                    type: "input"
            },
             "Entity.contacts":{
                 label: "contacts",
                 rules: [],
                    type: "input"
            },
             "Entity.description":{
                 label: "description",
                 rules: [],
                    type: "input"
            },
             "Entity.fax":{
                 label: "fax",
                 rules: [],
                    type: "input"
            },
             "Entity.mail":{
                 label: "mail",
                 rules: [],
                    type: "input"
            },
             "Entity.mobile":{
                 label: "mobile",
                 rules: [],
                    type: "input"
            },
             "Entity.phone":{
                 label: "phone",
                 rules: [],
                    type: "input"
            },
             "Entity.proprietorCode":{
                 label: "proprietorCode",
                 rules: [],
                    type: "input"
            },
             "Entity.suppilerFullname":{
                 label: "suppilerFullname",
                 rules: [],
                    type: "input"
            },
             "Entity.suppilerFullnameAlias":{
                 label: "suppilerFullnameAlias",
                 rules: [],
                    type: "input"
            },
             "Entity.suppilerFullnameEn":{
                 label: "suppilerFullnameEn",
                 rules: [],
                    type: "input"
            },
             "Entity.supplierName":{
                 label: "supplierName",
                 rules: [{ required: true, message: "supplierName"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.supplierNameAlias":{
                 label: "supplierNameAlias",
                 rules: [],
                    type: "input"
            },
             "Entity.supplierNameEn":{
                 label: "supplierNameEn",
                 rules: [],
                    type: "input"
            },
             "Entity.supplierCode":{
                 label: "supplierCode",
                 rules: [{ required: true, message: "supplierCode"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.usedFlag":{
                 label: "usedFlag",
                 rules: [{ required: true, message: "usedFlag"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.whouseNo":{
                 label: "whouseNo",
                 rules: [],
                    type: "input"
            },
             "Entity.zip":{
                 label: "zip",
                 rules: [],
                    type: "input"
            },
             "Entity.companyCode":{
                 label: "companyCode",
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
