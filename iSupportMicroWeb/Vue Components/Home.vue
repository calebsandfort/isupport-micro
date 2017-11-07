<template>
    <div id="home">
        <b-button variant="primary" @click="handler">Handler</b-button>
    </div>
</template>

<script>
    import axios from 'axios';

    export default {
        name: 'home',
        methods: {
            handler: function (event) {
                axios.post('/api/customer/add', {
                    FirstName: 'Fred',
                    LastName: 'Flintstone',
                    Email: 'Fred.Flintstone@gmail.com'
                })
                    .then(function (response) {
                        console.log(response);
                    })
                    .catch(function (error) {
                        console.log(error);
                    });
            }
        }
    }
</script>

<style lang="scss">

</style>