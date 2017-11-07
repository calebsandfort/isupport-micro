<template>
    <div id="customers">
        <vuetable ref="vuetable"
                  api-url="/api/customer"
                  :fields="['firstName', 'lastName', 'email']"></vuetable>
        <!--<table-component :data="fetchData" table-class="table table-striped table-bordered table-hover" thead-class="thead-light" tbody-class="">
            <table-column show="FirstName" label="First"></table-column>
            <table-column show="LastName" label="Last"></table-column>
            <table-column show="Email" label="Email"></table-column>
        </table-component>-->

        <!--<kendo-datasource ref="customerDatasource"
                          :transport-read-url="'/api/customer'"
                          :server-sorting="true">
        </kendo-datasource>

        <kendo-grid :height="550"
                    :data-source-ref="'customerDatasource'">
            <kendo-grid-column field="FirstName" title="First"></kendo-grid-column>
            <kendo-grid-column field="LastName" title="Last"></kendo-grid-column>
            <kendo-grid-column field="Email" title="Email"></kendo-grid-column>
        </kendo-grid>-->
    </div>
</template>

<script>
    import Vuetable from 'vuetable-2'

    export default {
        name: 'customers',
        components: {
            Vuetable
        },
        methods: {
            //async fetchData(gridOptions) {
            //    const response = await axios.get('/api/customer', gridOptions);

            //    // An object that has a `data` and an optional `pagination` property
            //    return { data: response };
            //}
        }
    }
</script>

<style lang="scss">

</style>