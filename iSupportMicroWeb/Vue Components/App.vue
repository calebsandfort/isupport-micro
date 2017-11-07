<template>
    <div id="app">
        <b-navbar toggleable="md" type="dark" variant="dark">
            <div class="container">
                <b-nav-toggle target="nav_collapse"></b-nav-toggle>

                <b-navbar-brand href="#">iSupport Micro</b-navbar-brand>

                <b-collapse is-nav id="nav_collapse">

                    <b-nav is-nav-bar>
                        <b-nav-item to="/home">Home</b-nav-item>
                        <b-nav-item to="/customers">Customers</b-nav-item>
                        <b-nav-item to="/incidents">Incidents</b-nav-item>
                    </b-nav>
                </b-collapse>
            </div>
        </b-navbar>
        <div  class="container pt-3">
            <router-view></router-view>
        </div>
    </div>
</template>

<script>
    export default {
        name: 'app'
    }
</script>

<style lang="scss">

</style>