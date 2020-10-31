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
        <v-subheader v-if="items.length > 0" style="margin-top: -12px"
          >COMERCIO ADHERIDO: UserStory Food</v-subheader
        >
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
        <v-list-item v-if="items.length > 0" class="mt-3">
          <v-list-item-content>
            <v-list-item-title class="title"
              >Total: ${{ getTotal }}</v-list-item-title
            >
          </v-list-item-content>
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
import CocaCola from "../assets/cocacola.jpeg";
import Hamburguesa from "../assets/hamburguesa.jpg";
import Pizza from "../assets/pizza.jpeg";
export default {
  data() {
    return {
      items: [
        {
          id: 1,
          name: "Hamburguesa",
          subtitle: "Cantidad: 2 - $600",
          price: 600,
          avatar: Hamburguesa,
        },
        {
          id: 2,
          name: "Pizza",
          subtitle: "Cantidad: 1 - $325",
          price: 325,
          avatar: Pizza,
        },
        {
          id: 3,
          name: "Coca-Cola",
          subtitle: "Cantidad: 5 - $500",
          price: 500,
          avatar: CocaCola,
        },
      ],
    };
  },

  methods: {
    removeElement: function(index) {
      this.items.splice(index, 1);
    },
    pay() {
      if (this.items.length === 0) {
        swal("Debe ingresar al menos un item al carrito", "", "warning");
      } else {
        this.$router.push({
          name: "Checkout",
          params: {
            total: this.getTotal,
          },
        });
      }
    },
  },

  computed: {
    getTotal() {
      let price = 0;
      this.items.forEach((i) => {
        price += i.price;
      });
      return price;
    },
  },
};
</script>
