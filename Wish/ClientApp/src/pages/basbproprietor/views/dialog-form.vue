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
                 rules: [{ required: true, message: "proprietorCode"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.proprietorFullname":{
                 label: "proprietorFullname",
                 rules: [],
                    type: "input"
            },
             "Entity.proprietorFullnameAlias":{
                 label: "proprietorFullnameAlias",
                 rules: [],
                    type: "input"
            },
             "Entity.proprietorFullnameEn":{
                 label: "proprietorFullnameEn",
                 rules: [],
                    type: "input"
            },
             "Entity.proprietorName":{
                 label: "proprietorName",
                 rules: [{ required: true, message: "proprietorName"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.proprietorNameAlias":{
                 label: "proprietorNameAlias",
                 rules: [],
                    type: "input"
            },
             "Entity.proprietorNameEn":{
                 label: "proprietorNameEn",
                 rules: [],
                    type: "input"
            },
             "Entity.usedFlag":{
                 label: "usedFlag",
                 rules: [{ required: true, message: "usedFlag"+this.$t("form.notnull"),trigger: "blur" }],
                    type: "input"
            },
             "Entity.zip":{
                 label: "zip",
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
