<template>
    <div id="customers">
        <vuetable ref="vuetable"
                  api-url="/api/customer"
                  :fields="['firstName', 'lastName', 'email']"></vuetable>
    </div>
</template>

<script>
    import Vuetable from 'vuetable-2'

    export default {
        name: 'customers',
        components: {
            Vuetable
        }
    }
</script>

<style lang="scss">

</style>