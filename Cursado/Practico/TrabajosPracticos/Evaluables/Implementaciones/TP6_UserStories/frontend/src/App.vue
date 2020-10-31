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
      <v-btn icon class="mx-2" @click="dialogInfo = true">
        <v-icon>mdi-information</v-icon>
      </v-btn>
      <v-dialog v-model="dialogInfo" max-width="1024px">
        <v-card color="#EBEBEB" outlined>
          <v-img :src="userStory" shrink></v-img>
        </v-card>
      </v-dialog>
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
import UserStory from "./assets/userstory.png";
export default {
  components: { Footer },
  name: "App",
  data() {
    return {
      userStory: UserStory,
      dialogInfo: false,
    };
  },
};
</script>
