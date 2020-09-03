<template>
  <v-container>
    <v-card outlined>
      <v-container fluid class="ma-0 mb-5" style="background-color:#1F7087">
        <v-card-title
          class="title text-uppercase font-weight-bold white--text"
          style="font-family: ProximaNova !important; word-break: break-word;"
          >Carrito</v-card-title
        >
      </v-container>
      <v-list>
        <v-list-item v-for="(item, key) in items" :key="item.id">
          <v-list-item-avatar>
            <v-img :src="item.avatar"></v-img>
          </v-list-item-avatar>
          <v-list-item-content>
            <v-list-item-title v-text="item.name"></v-list-item-title>
            <v-list-item-subtitle v-text="item.subtitle"></v-list-item-subtitle>
          </v-list-item-content>
          <v-list-item-action>
            <v-btn icon>
              <v-icon color="grey lighten-1" @click="removeElement(key)"
                >mdi-delete</v-icon
              >
            </v-btn>
          </v-list-item-action>
        </v-list-item>
        <v-list-item v-if="items.length === 0">
          <v-list-item-content>
            <v-list-item-title class="text-center" style="margin-top: -12px"
              ><span style="font-size: 25px !important"
                >Tu carrito está vacío 😢</span
              ></v-list-item-title
            >
          </v-list-item-content>
        </v-list-item>
      </v-list>
    </v-card>
    <v-container class="mt-5 text-center">
      <v-btn color="#1F7087" class="mr-4 white--text" @click="pay()">
        Ir a pagar
      </v-btn>
    </v-container>
  </v-container>
</template>

<script>
import swal from "sweetalert";
export default {
  data() {
    return {
      items: [
        {
          id: 1,
          name: "Articulo 1",
          subtitle: "Cantidad: 2 - $125",
          avatar:
            "https://scontent.fcor3-1.fna.fbcdn.net/v/t1.0-9/10155657_10206531880394586_1684537497252052375_n.jpg?_nc_cat=107&_nc_sid=09cbfe&_nc_ohc=CH9EUke0uFwAX-QEXnT&_nc_oc=AQnygHhtP1kSIoHKHzNO14G6gQVhB6ixc-w-L5geNciSDUvdaXbesaTPzQ7rmtvy4qc&_nc_ht=scontent.fcor3-1.fna&oh=b90e66937a39e32df9552a705392bf8a&oe=5F74C966",
        },
        {
          id: 2,
          name: "Articulo 2",
          subtitle: "Cantidad: 2 - $125",
          avatar:
            "https://scontent.fcor3-1.fna.fbcdn.net/v/t1.0-9/10155657_10206531880394586_1684537497252052375_n.jpg?_nc_cat=107&_nc_sid=09cbfe&_nc_ohc=CH9EUke0uFwAX-QEXnT&_nc_oc=AQnygHhtP1kSIoHKHzNO14G6gQVhB6ixc-w-L5geNciSDUvdaXbesaTPzQ7rmtvy4qc&_nc_ht=scontent.fcor3-1.fna&oh=b90e66937a39e32df9552a705392bf8a&oe=5F74C966",
        },
      ],
    };
  },

  methods: {
    removeElement: function(index) {
      this.items.splice(index, 1);
    },
    pay() {
      console.log(this.items.length);
      if (this.items.length === 0) {
        swal("Debe ingresar al menos un item al carrito", "", "warning");
      } else {
        this.$router.push({ name: "Checkout" });
      }
    },
  },
};
</script>
