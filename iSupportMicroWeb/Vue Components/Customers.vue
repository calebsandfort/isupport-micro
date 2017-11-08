<template>
    <div id="customers">
        <vuetable ref="vuetable"
                  api-url="/api/customer"
                  :fields="fields"></vuetable>
    </div>
</template>

<script>
    import Vuetable from 'vuetable-2'

    export default {
        name: 'customers',
        components: {
            Vuetable
        },
        data: function () {
            return {
                fields: [
                    {
                        name: "firstName",
                        title: "First",
                        sortField: "FirstName"
                    },
                    {
                        name: "lastName",
                        title: "Last",
                        sortField: "LastName"
                    },
                    {
                        name: "email",
                        sortField: "Email"
                    }
                ]
            };
        }
    }
</script>

<style lang="scss">

</style>