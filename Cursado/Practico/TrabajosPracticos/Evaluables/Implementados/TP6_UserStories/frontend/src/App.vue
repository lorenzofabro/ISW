<template>
  <v-app style="font-family: ProximaNova !important; background-color: #EBEBEB">
    <v-app-bar app dark>
      <v-toolbar-title class="title"
        ><span class="yellow--text text--darken-2">DELIVERY</span
        ><span class="yellow--text text--lighten-2">EAT</span
        >🍔</v-toolbar-title
      >
      <v-spacer></v-spacer>
      <v-btn icon @click="$router.push({ name: 'Main' })">
        <v-icon>mdi-home</v-icon>
      </v-btn>
      <v-btn icon>
        <v-icon>mdi-magnify</v-icon>
      </v-btn>
      <v-btn icon>
        <v-icon>mdi-dots-vertical</v-icon>
      </v-btn>
    </v-app-bar>
    <v-main class="mt-5">
      <v-scroll-y-transition mode="out-in">
        <router-view></router-view>
      </v-scroll-y-transition>
    </v-main>
    <Footer />
  </v-app>
</template>

<style>
@font-face {
  font-family: "ProximaNova";
  src: url("~@/assets/ProximaNova-Regular.otf");
}
</style>

<script>
import Footer from "./views/Footer";
export default {
  components: { Footer },
  name: "App",
};
</script>
