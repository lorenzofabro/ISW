<template>
  <v-container>
    <Cart />
    <v-container class="mt-5 text-center">
      <v-btn
        color="#1F7087"
        class="mr-4 white--text"
        :to="{ name: 'Checkout' }"
      >
        Ir a pagar
      </v-btn>
    </v-container>
  </v-container>
</template>

<script>
import Cart from "./Cart";
export default {
  components: { Cart },
};
</script>
