<template>
    <div class="dashboard-container">
        <el-row :gutter="12">
            <el-col :span="8" :md="8" :xs="24" :sm="24">
                <el-row :gutter="10">
                    <el-col :span="12" :sm="24">
                    <e-charts-module />
                    </el-col>
                    <!-- <el-col :span="12" :sm="24">
                    
                    </el-col> -->
                </el-row>
            </el-col>
            <el-col :span="8" :xs="24">
                <e-chartsroadway-module />
            </el-col>
            <el-col :span="8" :xs="24">
                <e-chartsalarm-module />
            </el-col>
        </el-row>
        <el-row :gutter="12">
            <el-col :span="8" :xs="24">
                <e-chartss-module />
            </el-col>
            <el-col :span="8" :xs="24">
                <e-chartstaskhis-module />
            </el-col>
            <el-col :span="8" :xs="24">
                <e-chartsout-module />
            </el-col>
        </el-row>
        <el-row :gutter="10">
            <el-col :span="16" :md="16" :xs="24" :sm="24">
                <el-row :gutter="10">
                    <el-col :span="12" :xs="24">
                        <!-- <el-card shadow="hover">
                            <div slot="header">shortcut</div>
                            <el-row class="lump-wrap" :gutter="10">
                                <el-col :span="6" v-for="item of shortcutList" :key="item.Value">
                                    <el-link @click="onPush(item.Url)" :underline="false"><i :class="[item.Icon ? item.Icon : 'el-icon-edit']"></i>
                                        <div class="link-ctx">{{ item.Text }}</div>
                                    </el-link>
                                </el-col>
                                <el-col :span="6">
                                    <el-link target="_blank" href="https://wtmdoc.walkingtec.cn/" :underline="false"><i class="el-icon-document"></i>
                                        <div class="link-ctx">Doc</div>
                                    </el-link>
                                </el-col>
                                <el-col :span="6">
                                    <el-link target="_blank" href="http://localhost:6338/_codegen?ui=vue" :underline="false"><i class="el-icon-s-platform"></i>
                                        <div class="link-ctx">CodeGen</div>
                                    </el-link>
                                </el-col>
                            </el-row>
                        </el-card> -->
                    </el-col>
                    <el-col :span="12" :xs="24">
                        <!-- <el-card shadow="hover">
                            <div slot="header"><span>WTM open source framework</span></div>
                            <el-row class="lump-wrap cxt-left" :gutter="10">
                                <el-col :span="12">
                                    <div>
                                        <h3>Star</h3>
                                        <p>{{ getGithubInfoData.stargazers_count || "" }}</p>
                                    </div>
                                </el-col>
                                <el-col :span="12">
                                    <div>
                                        <h3>Fork</h3>
                                        <p>{{ getGithubInfoData.forks_count || "" }}</p>
                                    </div>
                                </el-col>
                                <el-col :span="12">
                                    <div>
                                        <h3>Watch</h3>
                                        <p>{{ getGithubInfoData.subscribers_count || "" }}</p>
                                    </div>
                                </el-col>
                                <el-col :span="12">
                                    <div>
                                        <h3>Issue</h3>
                                        <p>{{ getGithubInfoData.open_issues_count || "" }}</p>
                                    </div>
                                </el-col>
                            </el-row>
                        </el-card> -->
                    </el-col>
                    <!-- <el-col :span="24" :sm="24">
                        <e-charts-module />
                    </el-col> -->
                </el-row>
            </el-col>
            <el-col :span="8" :md="8" :xs="24" :sm="24">
                <el-col :span="24">
                    <!-- <el-card shadow="hover">
                        <div slot="header">Framework features</div>
                        <i class="el-icon-check"></i>One click to generate WTM project<br />
                        <i class="el-icon-check"></i>One click to generate add, delete, modify and query<br />
                        <i class="el-icon-check"></i>Support identification and code generation of one-to-many and many-to-many association models<br />
                        <i class="el-icon-check"></i>support front-back end separation mode (React+AntD,Vue+Element) and non-separation mode(LayUI)<br />
                        <i class="el-icon-check"></i>Support 3 databases: sqlserver, MySQL and PgSQL<br />
                        <i class="el-icon-check"></i>Encapsulates most of the controls of layUI, antd and element<br />
                        <i class="el-icon-check"></i>A lot of base classes are provided ,encapsulate most common background operations<br />
                        <i class="el-icon-check"></i>Provide common modules such as users, roles, user groups, menus, logs, etc<br />
                        <i class="el-icon-check"></i>Support the development and configuration of data authority<br />
                        <i class="el-icon-check"></i>Supportread/ write splitting ;Support database sub Library
                    </el-card> -->
                </el-col>
                <el-col :span="24">
                    <!-- <el-card shadow="hover">
                        <div slot="header">Author's words</div>
                        <p>
                            WTM framework, full name walkingtec MVVM. Walkingtec is my own company. WTM is a rapid development framework.How fast ? At least at present, in the open source project of DotNetcore, I haven't seen another faster one. Our goal of WTM is to speed up the development and reduce cost to the greatest extent.
                        </p>
                        <p>
                            WTM has been loved by more and more developers since its open source. WTM will surely repay your love with a more mature and stable version.Specially thanks to（layui.com Improve yourself, benefit others, we am not alone!
                        </p>
                        <p>
                            —— Open source address of framework：<el-link href="https://github.com/dotnetcore/WTM" target="_blank" type="primary">https://github.com/dotnetcore/WTM</el-link>
                        </p>
                        <p>
                            —— Framework online document:<el-link href="https://wtmdoc.walkingtec.cn" target="_blank" type="primary">https://wtmdoc.walkingtec.cn</el-link>
                        </p>
                        <p>
                            —— Frame QQ communication：694148336
                        </p>
                    </el-card> -->
                </el-col>
            </el-col>
        </el-row>
        <el-row :gutter="20">
            <el-col :span="24">
                <!-- <el-calendar v-model="value" /> -->
            </el-col>
        </el-row>
    </div>
</template>

<script lang="ts">
import indexMixin from "./index-mixin";
export default indexMixin;
</script>
<style lang="less">
.lump-wrap {
    .el-link.el-link--default {
        width: 100%;
        .el-link--inner {
            width: 100%;
        }
    }
}
</style>
<style lang="less" rel="stylesheet/less" scoped>
@import "~@/assets/css/mixin.less";
.dashboard-container {
    padding: 10px;
    p {
        text-indent: 2em;
    }
    .title {
        margin-bottom: 5px;
        font-weight: 300;
        font-size: 16px;
    }
    .category {
        text-indent: 0;
        color: #999999;
        .text-success {
            color: red;
        }
    }
    .stats {
        color: #999999;
    }

    .el-row {
        margin-bottom: 20px;
    }
    .el-card__header {
        color: #333;
        font-size: 14px;
    }
    .el-card {
        font-size: 14px;
        color: #666;
    }
    .lump-wrap {
        margin-bottom: 0;
        min-height: 160px;
        text-align: center;
        &.cxt-left {
            .el-col {
                .flexalign(flex-start);
                box-sizing: border-box;
                padding-bottom: 10px;
                div {
                    background-color: #f8f8f8;
                    width: 100%;
                    box-sizing: border-box;
                    padding: 0 10px;
                }
            }
        }
        .el-col {
            .center(column);
            i {
                width: 100%;
                height: 60px;
                line-height: 60px;
                text-align: center;
                border-radius: 2px;
                font-size: 30px;
                background-color: #f8f8f8;
            }
            h3 {
                padding: 5px 0;
                font-size: 12px;
            }
            p {
                font-style: normal;
                font-size: 30px;
                font-weight: 300;
                color: #009688;
                text-indent: 0;
            }
            .link-a {
                color: #666;
                font-size: 14px;
                text-align: center;
            }
        }
        .link-ctx {
            min-width: 80px;
        }
    }
}
</style>
