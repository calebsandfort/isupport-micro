<template>
    <div id="app">
        <sui-menu :activeIndex="0" :fixed="true" :widths="3">
            <sui-container>
                <router-link to="/home" class="item" active-class="active">Home</router-link>
                <router-link to="/customers" class="item" active-class="active">Customers</router-link>
                <router-link to="/incidents" class="item" active-class="active">Incidents</router-link>
            </sui-container>
        </sui-menu>
        <sui-container class="main">
            <router-view></router-view>
        </sui-container>
            <!--<b-navbar toggleable="md" type="dark" variant="dark">
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

        </div>-->
    </div>
</template>

<script>
    export default {
        name: 'app',
        methods: {
            navigate: function (path) {
                alert(path);
            }
        }
    }
</script>

<style lang="scss">
    .main.container {
        margin-top: 7em;
    }
</style>